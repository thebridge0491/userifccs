namespace Userifccs.Curses {

using System;
using System.Reflection;
using IO = System.IO;
using SysCollGen = System.Collections.Generic;

/// <summary>HelloModel class.</summary>
public class HelloModel : Userifccs.Aux.Subject {
    protected string _hellopfx = "";

    /// <summary>Constructor default for HelloModel.</summary>
    public HelloModel() : this("greet.txt")
    { }

    /// <summary>Constructor for HelloModel.</summary>
    /// <param name="greetPath">A string</param>
    /// <param name="assy">An assembly or null.</param>
    /// <param name="rsrcPath">A string</param>
    public HelloModel(string greetPath, Assembly assy = null,
    		string rsrcPath = "resources")
    {
        try {
			//_hellopfx = (new IO.StreamReader(rsrcPath + "/" + greetPath)).ReadToEnd().TrimEnd('\n', '\r');
			_hellopfx = IO.File.ReadAllText(rsrcPath + "/" + greetPath).TrimEnd('\n', '\r');
		} catch (Exception exc0) {
			Console.Error.WriteLine("(exc: {0}) Bad var rsrcPath: {1}\n",
				exc0, rsrcPath);
			try {
				_hellopfx = GetFromResources(greetPath, 
					assy).TrimEnd('\n', '\r');
			} catch (Exception exc1) {
				_hellopfx = "To be set -- HELP.";
				throw;
				//Environment.Exit(1);
			}
		}
    }
    /*
    //destructor
    /// <summary>Destructor for HelloModel.</summary>
    ~HelloModel()
    {
		Console.WriteLine("destructing {0}", GetType());
	}
	*/

    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {	//if (null == obj || !(obj is HelloModel))
        if (null == obj || GetType() != obj.GetType())
            return false;
        HelloModel p = obj as HelloModel;	//HelloModel p = (HelloModel)obj;
        return (null == p) ? false : (_observers == p._observers) && (_hellopfx == p._hellopfx);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as HelloModel);
    }

	/// <summary>Compares HelloModel instance equality.</summary>
    /// <param name="other">A HelloModel</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(HelloModel other)
    {	//if (null == obj || !(obj is HelloModel))
        return null != other && _observers == other._observers &&
        	_hellopfx == other._hellopfx;
    }

	/// <summary>Operator for HelloModel instance equality.</summary>
    /// <param name="lhs">A HelloModel</param>
    /// <param name="rhs">A HelloModel</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(HelloModel lhs, HelloModel rhs)
    {
		if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
			return true;
		return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
	}

	/// <summary>Operator for HelloModel instance inequality.</summary>
    /// <param name="lhs">A HelloModel</param>
    /// <param name="rhs">A HelloModel</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(HelloModel lhs, HelloModel rhs)
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
        hashCode = prime * hashCode + ((null != _observers) ? 
        	_observers.GetHashCode() : 0);
        hashCode = prime * hashCode + ((null != _hellopfx) ? 
        	_hellopfx.GetHashCode() : 0);
        return hashCode;
    }
    
    /// <inheritdoc/>
    public override void NotifyObservers(string newData)
    {
		foreach (Userifccs.Aux.IObserver obs in _observers)
			obs.Update(string.Format("{0}{1}!", _hellopfx,
			    (null != newData) ? newData : ""));
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

	/// <summary>Main entry point.</summary>
    /// <param name="args">An array</param>
    /// <returns>The exit code.</returns>
	public static int Main(string[] args)
    {
		var model1 = new HelloModel("greet.txt");
		var view1 = new HelloView();

		model1.AttachObserver(view1);
		model1.NotifyObservers("To be set -- HELP.");
		Console.Write("view1.Data: {0}\n", view1.Data);
		return 0;
	}
}

}
