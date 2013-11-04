namespace Userifccs.Gtk {

using System;
using IO = System.IO;
using SysCollGen = System.Collections.Generic;
using GtkX = global::Gtk;

/// <summary>HelloView class.</summary>
public class HelloView : GtkX.Window, Userifccs.Aux.IObserver {
    //public SysCollGen.Dictionary<string, GLib.Object> widgets =
    //	new SysCollGen.Dictionary<string, GLib.Object>();
    public SysCollGen.Dictionary<string, GtkX.Widget> widgets =
    	new SysCollGen.Dictionary<string, GtkX.Widget>();

    /// <summary>Constructor for HelloView.</summary>
    /// <param name="title">A string</param>
    public HelloView(string title = "") : base(GtkX.WindowType.Toplevel)
    {
        Title = title;
        Data = "";
        widgets.Add("frame1", new GtkX.Frame("frame1"));
        widgets.Add("vbox1", new GtkX.VBox());
        widgets.Add("label1", new GtkX.Label("label1"));
        widgets.Add("button1", new GtkX.Button("button1"));
        widgets.Add("textview1", new GtkX.TextView());
        widgets.Add("dialog1", new GtkX.Dialog("dialog1", this,
        	GtkX.DialogFlags.DestroyWithParent));
        widgets.Add("entry1", new GtkX.Entry());

        ((GtkX.VBox)widgets["vbox1"]).PackStart((GtkX.Label)widgets["label1"],
        	true, true, 0);
        ((GtkX.VBox)widgets["vbox1"]).PackStart((GtkX.Button)widgets["button1"],
        	true, true, 0);
        ((GtkX.VBox)widgets["vbox1"]).PackStart(
        	(GtkX.TextView)widgets["textview1"], true, true, 0);
        //((GtkX.Dialog)widgets["dialog1"]).VBox.PackStart(
        //	(GtkX.Entry)widgets["entry1"], true, true, 0);
        ((GtkX.Dialog)widgets["dialog1"]).ContentArea.PackStart(
        	(GtkX.Entry)widgets["entry1"], true, true, 0);

        ((GtkX.Frame)widgets["frame1"]).Add((GtkX.VBox)widgets["vbox1"]);
        Add((GtkX.Frame)widgets["frame1"]);
        widgets.Add("window1", this);
    }
    /*
    //destructor
    /// <summary>Destructor for HelloView.</summary>
    ~HelloView()
    {
		Console.WriteLine("destructing {0}", GetType());
	}
	*/
	/// <value>Gets|Sets the HelloView's data.</value>
    public string Data { get; set;
    }

    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {	//if (null == obj || !(obj is HelloView))
        if (null == obj || GetType() != obj.GetType())
            return false;
        HelloView p = obj as HelloView;	//HelloView p = (HelloView)obj;
        return (null == p) ? false : (Data == p.Data) && (widgets == p.widgets);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as HelloView);
    }

	/// <summary>Compares HelloView instance equality.</summary>
    /// <param name="other">An HelloView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(HelloView other)
    {	//if (null == obj || !(obj is HelloView))
        return null != other && Data == other.Data && widgets == other.widgets;
    }

	/// <summary>Operator for HelloView instance equality.</summary>
    /// <param name="lhs">A HelloView</param>
    /// <param name="rhs">A HelloView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(HelloView lhs, HelloView rhs)
    {
		if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
			return true;
		return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
	}

	/// <summary>Operator for HelloView instance inequality.</summary>
    /// <param name="lhs">A HelloView</param>
    /// <param name="rhs">A HelloView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(HelloView lhs, HelloView rhs)
    {
		return !(lhs == rhs);
	}

    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Format("{0}: [Data: {1}]", GetType(), Data);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        //return this.ToString().GetHashCode();
        int prime = -65336, hashCode = 17;
        hashCode = prime * hashCode + ((null != Data) ? 
        	Data.GetHashCode() : 0);
        hashCode = prime * hashCode + ((null != widgets) ? 
        	widgets.GetHashCode() : 0);
        return hashCode;
    }

    /// <inheritdoc/>
    public void Update(string newData)
    {
    	Data = newData;
    	((GtkX.TextView)widgets["textview1"]).Buffer.Text = Data;
    }
}

}
