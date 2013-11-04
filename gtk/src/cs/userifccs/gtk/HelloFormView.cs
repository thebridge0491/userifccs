namespace Userifccs.Gtk {

using System;
using System.Reflection;
using IO = System.IO;
using SysCollGen = System.Collections.Generic;
using GtkX = global::Gtk;

/// <summary>HelloFormView class.</summary>
public class HelloFormView : GtkX.Builder, Userifccs.Aux.IObserver {
    //public SysCollGen.Dictionary<string, GLib.Object> widgets =
    //	new SysCollGen.Dictionary<string, GLib.Object>();
    public SysCollGen.Dictionary<string, GtkX.Widget> widgets =
    	new SysCollGen.Dictionary<string, GtkX.Widget>();

    /// <summary>Constructor default for HelloFormView.</summary>
    public HelloFormView() : this(null)
    { }

    /// <summary>Constructor for HelloFormView.</summary>
    /// <param name="assy">An assembly or null.</param>
    /// <param name="rsrcPath">A string</param>
    public HelloFormView(Assembly assy = null, string rsrcPath = "resources")
    {
        Data = "";
        string uiform = (2 == GtkX.Global.MajorVersion) ?
        	"helloForm-gtk2.glade" : "helloForm-gtk3.glade";
        try {
			//AddFromString((new IO.StreamReader(rsrcPath + "/gtk/" + uiform)).ReadToEnd());
			AddFromString(IO.File.ReadAllText(rsrcPath + "/gtk/" + uiform));
		} catch (Exception exc0) {
			Console.Error.WriteLine("(exc: {0}) Bad var rsrcPath: {1}\n",
				exc0, rsrcPath);
			try {
				AddFromString(GetFromResources("gtk." + uiform, assy));
			} catch (Exception exc1) {
				AddFromString(GetFromResources(uiform, assy));
				//throw;
				//Environment.Exit(1);
			}
		}
		string[] names = {"window1", "dialog1", "button1", "textview1",
			"entry1"};
		foreach (string name in names)
			widgets.Add(name, (GtkX.Widget)GetObject(name));
    }
    /*
    //destructor
    /// <summary>Destructor for HelloFormView.</summary>
    ~HelloFormView()
    {
		Console.WriteLine("destructing {0}", GetType());
	}
	*/
	/// <value>Gets|Sets the HelloFormView's data.</value>
    public string Data { get; set;
    }

    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {	//if (null == obj || !(obj is HelloFormView))
        if (null == obj || GetType() != obj.GetType())
            return false;
        HelloFormView p = obj as HelloFormView;	//HelloFormView p = (HelloFormView)obj;
        return (null == p) ? false : (Data == p.Data) && 
        	(widgets == p.widgets);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as HelloFormView);
    }

	/// <summary>Compares HelloFormView instance equality.</summary>
    /// <param name="other">An HelloFormView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(HelloFormView other)
    {	//if (null == obj || !(obj is HelloFormView))
        return null != other && Data == other.Data && widgets == other.widgets;
    }

	/// <summary>Operator for HelloFormView instance equality.</summary>
    /// <param name="lhs">A HelloFormView</param>
    /// <param name="rhs">A HelloFormView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(HelloFormView lhs, HelloFormView rhs)
    {
		if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
			return true;
		return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
	}

	/// <summary>Operator for HelloFormView instance inequality.</summary>
    /// <param name="lhs">A HelloFormView</param>
    /// <param name="rhs">A HelloFormView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(HelloFormView lhs, HelloFormView rhs)
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
    
    static string GetFromResources(string rsrcFileNm,
  			Assembly assy = null, string prefix = null)
  	{
	  	Assembly assembly = null != assy ? assy :
	  		Assembly.GetExecutingAssembly();
		string pathPfx = null != prefix ? prefix :
			assembly.GetName().Name + ".resources";
		using (var strm = assembly.GetManifestResourceStream(rsrcFileNm) ??
				assembly.GetManifestResourceStream(pathPfx + "." + rsrcFileNm))
		{
			using (var reader = new System.IO.StreamReader(strm))
			{
				return reader.ReadToEnd();
			}
		}
	}
}

}
