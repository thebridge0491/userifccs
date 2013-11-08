namespace Userifccs.Curses.Tests {

using System;
using System.Reflection;
using System.Linq;
using SysTextRegex = System.Text.RegularExpressions;
using NUnit.Framework;
using Threading = System.Threading;

using Wrappercs.Curses;
using Util = Introcs.Util.Library;
using UiCurses = Userifccs.Curses;

[TestFixture]
public class TcViewController : Base.ClsBase {
    private float epsilon = 0.001f; //1.20e-7f;
    private int delaymsecs = 2500;
	private UiCurses.HelloController uicontroller = null;

	public void RefreshUI(HelloController ctrlr, int delayMsecs)
	{
		if (ctrlr.step_virtualscr())
		    //CursesC.update_panels();
		    CursesC.doupdate();
		Threading.Thread.Sleep(delayMsecs);
	}

    [TestFixtureSetUp]
    public override void SetUpClass()
    {
        base.SetUpClass();
        string envRsrcPath = Environment.GetEnvironmentVariable("RSRC_PATH");
        string rsrcPath = null != envRsrcPath ? envRsrcPath : "resources";
        Assembly assy = Assembly.GetAssembly(typeof(UiCurses.HelloController));
		uicontroller = new UiCurses.HelloController("greet.txt", assy, rsrcPath);
        Console.Error.WriteLine("SetUpClass({0})", GetType());
    }

    [TestFixtureTearDown]
    public override void TearDownClass()
    {
        Console.Error.WriteLine("TearDownClass({0})", GetType());
        base.TearDownClass();
    }

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        Console.Error.WriteLine("SetUp");
    }

    [TearDown]
    public override void TearDown()
    {
        Console.Error.WriteLine("TearDown");
        base.TearDown();
    }

    public override void Dispose()
    {
        Console.Error.WriteLine("Derived Dispose({0})", GetType());
        base.Dispose();
    }

	[Test] [Category("Tag1")]
	public void KeyEnterCbTest()
	{
		CursesC.ungetch((Int32)HelloView.Keys.KEY_ENTER);
		/*
		CursesC.top_panel(uicontroller.View.panels["panel_input"]);
		//CursesC.mvwprintw(CursesC.panel_window(
		//    uicontroller.View.panels["panel_input"]),
        //    1, 1, "xxxxxxx%sxxxxxxx", " Push Enter key ");
        CursesC.mvwaddstr(CursesC.panel_window(
            uicontroller.View.panels["panel_input"]),
            1, 1, "xxxxxxx Push Enter key xxxxxxx");
        CursesC.wrefresh(CursesC.panel_window(
            uicontroller.View.panels["panel_input"]));
        Threading.Thread.Sleep(2000);
        */
        //RefreshUI(uicontroller, delaymsecs);
        Assert.True(1 != CursesC.panel_hidden(
            uicontroller.View.panels["panel_input"]),
			"input panel not visible on ENTER key");
        string input = "John Doe";
        //CursesC.mvwprintw(CursesC.panel_window(
        //    uicontroller.View.panels["panel_input"]),
        //    1, 1, "%s", input);
        CursesC.mvwaddstr(CursesC.panel_window(
            uicontroller.View.panels["panel_input"]), 1, 1, input);
        uicontroller.Model.NotifyObservers(input);
        //SysTextRegex.Regex re = new SysTextRegex.Regex(@"(?i)John Doe");
        SysTextRegex.Regex re = new SysTextRegex.Regex(input);
        
        /*foreach (var obs in uicontroller.Model._observers) {
			SysTextRegex.Match m = re.Match(
			    (string)((Userifccs.Aux.Observer)obs).Data);
			Assert.True(m.Success, "'%s' not in obs[n].Data(%s)", input,
			    (string)((Userifccs.Aux.Observer)obs).Data);
		}*/
        SysTextRegex.Match m = re.Match(uicontroller.View.Data);
		Assert.True(m.Success, "'%s' not in uicontroller.View.Data(%s)",
		    input, uicontroller.View.Data);
	}
    
	[Test] [Category("Tag1")]
	public void KeyRunCbTest()
	{
		CursesC.ungetch((Int32)HelloView.Keys.KEY_RUN);
		RefreshUI(uicontroller, delaymsecs);
		Assert.True(1 != CursesC.panel_hidden(
            uicontroller.View.panels["panel_output"]),
			"output panel not visible on RUN key");
	}
    
	[Test] [Category("Tag1")]
	public void KeyEscapeCbTest()
	{
		CursesC.ungetch((Int32)HelloView.Keys.KEY_ESC);
		Assert.True(!uicontroller.step_virtualscr(),
			"step_virtualscr not False on ESC key");
	}
    
	[Test] [Category("Tag1")]
	public void KeyUnmappedCbTest()
	{
		CursesC.ungetch((Int32)'Z');
		RefreshUI(uicontroller, delaymsecs);
		Assert.True(1 != CursesC.panel_hidden(
            uicontroller.View.panels["panel_output"]),
			"output panel not visible on unmapped key");
	}
}

}
