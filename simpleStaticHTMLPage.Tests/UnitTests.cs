using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace simpleStaticHTMLPage.Tests
{
    [TestFixture()]
    public class UnitTests
    {
        IWebDriver _driver = null;
        IJavaScriptExecutor _javaScriptExecutor = null;
        private const string SIMPLE_STATIC_LOCAL_HTML_PAGE = "http://localhost:59714/index.html";

        private TimeSpan maximumTimeToWaitForUIToFulfill = TimeSpan.FromSeconds(10);

        [SetUp]
        public void Initialize()
        {
            _driver = new ChromeDriver();
            _javaScriptExecutor = (IJavaScriptExecutor)_driver;

            _driver.Navigate().GoToUrl(SIMPLE_STATIC_LOCAL_HTML_PAGE);
        }

        [Test()]
        public void T__Index_Page_Has_Correct_Title()
        {
            //this test examines what title the target page has
            var actualTitle = _driver.Title;
            var expectedTitle = "Selenium demo";

            Assert.AreEqual(expectedTitle, actualTitle, "The index page title is expected to be 'Selenium demo' (the <title> element should contain 'Selenium demo')");
        }

        [Test()]
        public void T__Instruction_Header_Exists()
        {
            //do we have a <h1>Instructions</h1> on index.html?
            var header = _driver.FindElement(By.XPath("html/body/div/div/main/div/h1"));
            var headerText = header.Text;

            var actualHeaderText = headerText;
            var expectedHeaderText = "Instructions";

            Assert.AreEqual(expectedHeaderText, actualHeaderText, "The header text is expected to be 'Instructions'");
        }

        [Test()]
        public void T__Only_Numbers_Are_Valid_Input()
        {
            //is it allowed to write other input than numbers in input?

            var input = _driver.FindElement(By.XPath("html/body/div/div/main/div/form/div/input[@id ='txtNumber']"));
            const string SOME_ARBITRARY_TEXT = "consolit";

            //type something in the text box
            input.Clear(); // clear input field
            input.SendKeys(SOME_ARBITRARY_TEXT);   // add some text to input field

            //as the current html/javascript implementation will set the html input field in an invalid state, if we have wrong input,
            //we can examine the state to see if our error prone text input is valid

            var isValidInput = (bool)_javaScriptExecutor.ExecuteScript(
                "let txtNumber = document.getElementById('txtNumber');" +
                "let isValidInput = txtNumber.validity.valid;" +
                "return isValidInput;");

            var actualValidInput = isValidInput;
            var expectedValidInput = false;

            Assert.AreEqual(expectedValidInput, actualValidInput, "The input text is not supposed to be valid");
        }

        [Test()]
        public void T__Cant_Click_On_Multiplicate_Button_With_Invalid_Input()
        {
            //are we allowed to perform calculation, i.e. hit the MULTIPLY-button, when we have wrong input? 
            bool isDisabled = false; //just a dummy value, not meaning anything special

            //preparations: add some invalid input to input field
            var input = _driver.FindElement(By.XPath("html/body/div/div/main/div/form/div/input[@id ='txtNumber']"));
            const string SOME_ARBITRARY_TEXT = "consolit";

            //type something in the text box
            input.Clear(); // clear input field
            input.SendKeys(SOME_ARBITRARY_TEXT);   // add some text to input field

            //is the button disabled (not clickable)?
            var btnMultiplicate = _driver.FindElement(By.Id("btnMultiplicate"));
            bool.TryParse(btnMultiplicate.GetAttribute("disabled"), out isDisabled);

            var actualDisabledState = isDisabled;
            var expectedDisabledState = true;

            Assert.AreEqual(expectedDisabledState, actualDisabledState, "The should be disable (not clickable)");
        }

        [Test()]
        public void T__The_Calculated_Result_Is_Correct()
        {
            //do we get a correct result when we try to calculate the multiplication?

            //preparations: add some valid input to input field
            var input = _driver.FindElement(By.XPath("html/body/div/div/main/div/form/div/input[@id ='txtNumber']"));
            const string THIRTYTWO = "32";

            //type "32" in the text box
            input.Clear(); // clear input field
            input.SendKeys(THIRTYTWO);   // add THIRTYTWO to input field
            
            //click on MULTIPLICATE-button
            var btnMultiplicate = _driver.FindElement(By.Id("btnMultiplicate"));
            btnMultiplicate.Click();

            //is the resulting dialog visible?
            var dialog = _driver.FindElement(By.XPath("/html/body/div/div/main/div/dialog[@class='mdl-dialog']"));
            var dialogIsVisible = dialog.Displayed && dialog.Enabled;
            
            Assert.AreEqual(expected: true, actual: dialogIsVisible, message: "The result dialog should be visible");

            //lets get the result
            var resultParagraph = _driver.FindElement(By.XPath("/html/body/div/div/main/div/dialog[@class='mdl-dialog']/div/div/p"));
            var entireResultText = resultParagraph.Text;

            //analyze text
            var regex = new Regex($@"The multiplication, {THIRTYTWO} · 5, resulted in (?<result>\d+)");
            var match = regex.Match(entireResultText);

            //does the result text look like it should?
            var patternIsRecognized = match.Success;
            Assert.AreEqual(expected: true, actual: patternIsRecognized, message: "The result text should follow a predefined pattern.");

            //do we have a correct result?
            var resultValue = int.Parse(match.Groups["result"].Value);
            Assert.AreEqual(expected: 5 * int.Parse(THIRTYTWO), actual: resultValue, message: "The result should be 5 · 30, i.e. 160.");

        }

        [Test()]
        public void T__Input_IsClearedAfter_We_Have_Agreed_On_The_Result()
        {
            //is the input field cleared when we close
            var waitForUI = 5 * 1000;

            //preparations: add some valid input to input field
            var input = _driver.FindElement(By.XPath("html/body/div/div/main/div/form/div/input[@id ='txtNumber']"));
            const string THIRTYTWO = "32";

            //type "32" in the text box
            input.Clear();
            input.SendKeys(THIRTYTWO);

            System.Threading.Thread.Sleep(waitForUI);

            //did we get anything in our input?
            var inputText = input.GetAttribute("value");
            Assert.AreEqual(expected: THIRTYTWO, actual: inputText, message: "The test should have typed 32 in input field");

            //click on MULTIPLICATE-button
            var btnMultiplicate = _driver.FindElement(By.Id("btnMultiplicate"));
            btnMultiplicate.Click();

            //is the resulting dialog visible?
            var dialog = _driver.FindElement(By.XPath("/html/body/div/div/main/div/dialog[@class='mdl-dialog']"));
            var dialogIsVisible = dialog.Displayed && dialog.Enabled;
            Assert.AreEqual(expected: true, actual: dialogIsVisible, message: "The result dialog should be visible");

            //close dialog 
            var buttonAgree = _driver.FindElement(By.XPath("/html/body/div/div/main/div/dialog[@class='mdl-dialog']/div/button[contains(., 'Agree')]"));
            buttonAgree.Click();

            System.Threading.Thread.Sleep(waitForUI);

            //did we have anything in our input now?
            inputText = input.GetAttribute("value");
            Assert.AreEqual(expected: String.Empty, actual: inputText, message: "The input field should be empty!");
        }


        [TearDown]
        public void AfterTest()
        {
            if (this._driver != null)
                _driver.Close();
            _driver = null;
        }
    }
}
