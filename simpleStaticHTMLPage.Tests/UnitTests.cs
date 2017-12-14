using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace simpleStaticHTMLPage.Tests
{
    [TestFixture()]
    public class UnitTests
    {
        IWebDriver _driver = null;
        private const string SIMPLE_STATIC_LOCAL_HTML_PAGE = "http://localhost:59714/index.html";

        [SetUp]
        public void Initialize()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(SIMPLE_STATIC_LOCAL_HTML_PAGE);
        }

        [Test()]
        public void T__Index_Page_Has_Correct_Title()
        {
            //this test examines what title the target page has
            var actualTitle = _driver.Title;
            var expectedTitle = "Selenium demo";

            Assert.AreEqual(expectedTitle, actualTitle);
        }

        [Test()]
        public void T__Instruction_Header_Exists()
        {
            //do we have a <h1>Instructions</h1> on index.html?
            var header = _driver.FindElement(By.XPath("html/body/div/div/main/div/h1"));
            var headerText = header.Text;

            var actualHeaderText = headerText;
            var expectedHeaderText = "Instructions";

            Assert.AreEqual(expectedHeaderText, actualHeaderText);
        }

        [Test()]
        public void T__Only_Numbers_Are_Valid_Input()
        {
            //is it allowed to write other input than numbers in input

            //hint: google search on "selenium C# sendKeys"

            //this one is tricky... :-)
        }

        [Test()]
        public void T__Cant_Click_On_Multiplicate_Button_With_Invalid_Input()
        {
            //are we allowed to calculate, i.e. hit the MULTIPLY-button, when we have wrong input? 


        }

        [Test()]
        public void T__The_Calculated_Result_Is_Correct()
        {
            //do we get a correct result when we try to calculate the multiplication?

            //this one is tricky... :-)

        }

        [Test()]
        public void T__Input_IsClearedAfter_We_Have_Agreed_On_The_Result()
        {
            //is the input field cleared when we close

            //this one is tricky... :-)

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
