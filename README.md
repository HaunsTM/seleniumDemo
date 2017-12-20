<h1>Selenium demo</h1>
<p>This is a simple application showing different task for Selenium with NUnit.</p>

<p>The "main" application is a simple HTML5/CSS3/javascript and takes input from user. After input, it validates that user enters valid input. If valid, then a dialog is displayed with the result (input x 5).</p>

<table>
<tr><td><img src="https://i.imgur.com/7U9e26p.png"/></td>&nbsp;<td></td><td><img src="https://i.imgur.com/Ow3qsVs.png"/></td></tr>
</table>

<p>The class <code>UnitTests.cs</code> consists of the following:</p>
<pre>
[TestFixture()]
public class UnitTests
{
	[Test()]
	public void T__Index_Page_Has_Correct_Title()
	{
		//this test examines what title the target page has
	}
	[Test()]
	public void T__Instruction_Header_Exists()
	{
		//do we have a &lt;h1&gt;Instructions&lt;/h1&gt; on index.html?
	}
	[Test()]
	public void T__Only_Numbers_Are_Valid_Input()
	{
		//is it allowed to write other input than numbers in input?
	}
	[Test()]
	public void T__Cant_Click_On_Multiplicate_Button_With_Invalid_Input()
	{
		//are we allowed to perform calculation, i.e. hit the MULTIPLY-button, when we have wrong input? 
	}
	[Test()]
	public void T__The_Calculated_Result_Is_Correct()
	{
		//do we get a correct result when we try to calculate the multiplication?
	}
	[Test()]
	public void T__Input_IsClearedAfter_We_Have_Agreed_On_The_Result()
	{
		//is the input field cleared when we close
	}
}</pre>
