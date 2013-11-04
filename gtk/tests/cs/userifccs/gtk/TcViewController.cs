namespace Userifccs.Gtk.Tests {

using System;
using System.Reflection;
using System.Linq;
using NUnit.Framework;
using Threading = System.Threading;
using GtkX = global::Gtk;

using Util = Introcs.Util.Library;
using UiGtk = Userifccs.Gtk;

[TestFixture]
public class TcViewController : Base.ClsBase {
    private float epsilon = 0.001f; //1.20e-7f;
    private int delaymsecs = 2500;
	private UiGtk.HelloController uicontroller = null;

	public void RefreshUI(int delayMsecs)
	{
		while (GtkX.Application.EventsPending())
			//GtkX.Main.IterationDo(false); //GtkX.Main.Iteration();
			GtkX.Application.RunIteration(false);
		Threading.Thread.Sleep(delayMsecs);
	}

    [TestFixtureSetUp]
    public override void SetUpClass()
    {
        base.SetUpClass();
        GtkX.Application.Init();
        string envRsrcPath = Environment.GetEnvironmentVariable("RSRC_PATH");
        string rsrcPath = null != envRsrcPath ? envRsrcPath : "resources";
        Assembly assy = Assembly.GetAssembly(typeof(UiGtk.HelloController));
		uicontroller = new UiGtk.HelloController("greet.txt", assy, rsrcPath);
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
	public void Button1ClickedCbTest()
	{
		((GtkX.Button)uicontroller.View.widgets["button1"]).Click();
		RefreshUI(delaymsecs);
		var dialog1 = (GtkX.Dialog)uicontroller.View.widgets["dialog1"];
		var textview1 = (GtkX.TextView)uicontroller.View.widgets["textview1"];
		Assert.True(dialog1.Visible && textview1.Visible,
			"dialog1 and textview1 not visible");
		var entry1 = (GtkX.Entry)uicontroller.View.widgets["entry1"];
			StringAssert.AreEqualIgnoringCase(entry1.Text, "");
	}

	[Test] [Category("Tag1")]
	public void Dialog1ResponseCbTest()
	{
		var dialog1 = (GtkX.Dialog)uicontroller.View.widgets["dialog1"];
		dialog1.Show();

		dialog1.Respond(1);
		RefreshUI(delaymsecs);
		Assert.True(!dialog1.Visible, "dialog1 visible");
	}

	[Test] [Category("Tag1")]
	public void Entry1ActivateCbTest()
	{
		var dialog1 = (GtkX.Dialog)uicontroller.View.widgets["dialog1"];
		var entry1 = (GtkX.Entry)uicontroller.View.widgets["entry1"];
		dialog1.Show();
		entry1.Text = "John Doe";

		entry1.Activate();
		RefreshUI(delaymsecs);
		var textview1 = (GtkX.TextView)uicontroller.View.widgets["textview1"];
		var iterStart = textview1.Buffer.GetIterAtOffset(0);
		var iterEnd = textview1.Buffer.GetIterAtOffset(-1);
		/*foreach (var obs in uicontroller.Model._observers)
			Assert.AreEqual(textview1.Buffer.GetSlice(iterStart, iterEnd, 
				false), (string)((Userifccs.Aux.Observer)obs).Data, 
				"textview1.Buffer != obs[n].Data");*/
		Assert.AreEqual(textview1.Buffer.GetSlice(iterStart, iterEnd, false),
			uicontroller.View.Data, "textview1.Buffer != View.Data");
		Assert.True(!dialog1.Visible, "dialog1 visible");
	}
}

}
