namespace Userifccs.Gtk {

using System;
using System.Reflection;
using GtkX = global::Gtk;

/// <summary>HelloController class.</summary>
public class HelloController : System.IEquatable<HelloController> {
    private HelloModel _model1;
    private HelloFormView _view1;

    /// <summary>Constructor default for HelloController.</summary>
    public HelloController() : this("greet.txt")
    { }

    /// <summary>Constructor for HelloController.</summary>
    /// <param name="greetPath">A string</param>
    /// <param name="assy">An assembly or null.</param>
    /// <param name="rsrcPath">A string</param>
    public HelloController(string greetPath, Assembly assy = null,
    		string rsrcPath = "resources")
    {
        _model1 = new HelloModel(greetPath, assy, rsrcPath);
        //_view1 = new HelloView("window1");
        _view1 = new HelloFormView(assy, rsrcPath);

        /*((GtkX.Window)_view1.widgets["window1"]).DeleteEvent +=
        	new GtkX.DeleteEventHandler(window1_destroy_cb);
        ((GtkX.Dialog)_view1.widgets["dialog1"]).DeleteEvent +=
        	new GtkX.DeleteEventHandler(dialog1_destroy_cb);
        ((GtkX.Button)_view1.widgets["button1"]).Clicked +=
        	new EventHandler(button1_clicked_cb);
        ((GtkX.Dialog)_view1.widgets["dialog1"]).Response +=
        	new GtkX.ResponseHandler(dialog1_response_cb);
        ((GtkX.Entry)_view1.widgets["entry1"]).Activated +=
        	new EventHandler(entry1_activate_cb);*/
        _view1.Autoconnect(this);

        _model1.AttachObserver(_view1);
        ((GtkX.Window)_view1.widgets["window1"]).Title = "Hello_gui";
        ((GtkX.Window)_view1.widgets["window1"]).SetDefaultSize(200, 160);
        ((GtkX.Dialog)_view1.widgets["dialog1"]).Title = "dialog1";
        ((GtkX.Dialog)_view1.widgets["dialog1"]).SetDefaultSize(160, 100);
        ((GtkX.Window)_view1.widgets["window1"]).ShowAll();
    }
    /*
    //destructor
    /// <summary>Destructor for HelloController.</summary>
    ~HelloController()
    {
		Console.WriteLine("destructing {0}", GetType());
	}
	*/
	/// <value>Gets the HelloController's model.</value>
    public HelloModel Model { get { return _model1; }
    }

	/// <value>Gets the HelloController's view.</value>
    public HelloFormView View { get { return _view1; }
    }

    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {	//if (null == obj || !(obj is HelloController))
        if (null == obj || GetType() != obj.GetType())
            return false;
        HelloController p = obj as HelloController;	//HelloController p = (HelloController)obj;
        return (null == p) ? false : (Model == p.Model) && (View == p.View);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as HelloController);
    }

	/// <summary>Compares HelloController instance equality.</summary>
    /// <param name="other">A HelloController</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(HelloController other)
    {	//if (null == obj || !(obj is HelloController))
        return null != other && Model == other.Model && View == other.View;
    }

	/// <summary>Operator for HelloController instance equality.</summary>
    /// <param name="lhs">A HelloController</param>
    /// <param name="rhs">A HelloController</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(HelloController lhs, HelloController rhs)
    {
		if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
			return true;
		return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
	}

	/// <summary>Operator for HelloController instance inequality.</summary>
    /// <param name="lhs">A HelloController</param>
    /// <param name="rhs">A HelloController</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(HelloController lhs, HelloController rhs)
    {
		return !(lhs == rhs);
	}
	/*
    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Format("{0}", GetType());
    }
	*/
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        //return this.ToString().GetHashCode();
        int prime = -65336, hashCode = 17;
        hashCode = prime * hashCode + ((null != Model) ? Model.GetHashCode() : 0);
        hashCode = prime * hashCode + ((null != View) ? View.GetHashCode() : 0);
        return hashCode;
    }

	void window1_destroy_cb(object o, EventArgs args)
	{
		GtkX.Application.Quit();
	}

	void dialog1_destroy_cb(object o, EventArgs args)
	{
		GtkX.Application.Quit();
	}

	void button1_clicked_cb(object o, EventArgs args)
	{
        ((GtkX.TextView)View.widgets["textview1"]).Show();
        ((GtkX.Dialog)View.widgets["dialog1"]).ShowAll();
		((GtkX.Entry)View.widgets["entry1"]).Text = "";
	}

	void dialog1_response_cb(object o, GtkX.ResponseArgs args)
	{
		((GtkX.Entry)View.widgets["entry1"]).Activate();
        ((GtkX.Dialog)View.widgets["dialog1"]).Hide();
	}

	void entry1_activate_cb(object o, EventArgs args)
	{
        ((GtkX.Dialog)View.widgets["dialog1"]).Hide();
		Model.NotifyObservers(((GtkX.Entry)View.widgets["entry1"]).Text);
	}

	static void onDeleteEvent(object o, GtkX.DeleteEventArgs args)
	{
		GtkX.Application.Quit();
	}

	static void onExitButtonEvent(object o, EventArgs args)
	{
		GtkX.Application.Quit();
	}
}

}
