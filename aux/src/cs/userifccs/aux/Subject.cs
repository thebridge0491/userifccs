namespace Userifccs.Aux {

using System;
using SysCollGen = System.Collections.Generic;

/// <summary>Subject class.</summary>
public class Subject : System.IEquatable<Subject>, ISubject {
    private string _data;
    protected SysCollGen.HashSet<Userifccs.Aux.IObserver> _observers =
    	new SysCollGen.HashSet<Userifccs.Aux.IObserver>();

    /// <summary>Constructor default for Subject.</summary>
    public Subject() { }
    /*
    //destructor
    /// <summary>Destructor for Subject.</summary>
    ~Subject()
    {
		Console.WriteLine("destructing {0}", GetType());
	}
	*/

    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {	//if (null == obj || !(obj is Subject))
        if (null == obj || GetType() != obj.GetType())
            return false;
        Subject p = obj as Subject;	//Subject p = (Subject)obj;
        return (null == p) ? false : (_observers == p._observers);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as Subject);
    }

	/// <summary>Compares Subject instance equality.</summary>
    /// <param name="other">A Subject</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(Subject other)
    {	//if (null == obj || !(obj is Subject))
        return null != other &&  _observers == other._observers;
    }

	/// <summary>Operator for Subject instance equality.</summary>
    /// <param name="lhs">A Subject</param>
    /// <param name="rhs">A Subject</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(Subject lhs, Subject rhs)
    {
		if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
			return true;
		return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
	}

	/// <summary>Operator for Subject instance inequality.</summary>
    /// <param name="lhs">A Subject</param>
    /// <param name="rhs">A Subject</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(Subject lhs, Subject rhs)
    {
		return !(lhs == rhs);
	}

    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Format("{0}", GetType());
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        //return this.ToString().GetHashCode();
        int prime = -65336, hashCode = 17;
        hashCode = prime * hashCode + ((null != _observers) ? _observers.GetHashCode() : 0);
        return hashCode;
    }

    /// <inheritdoc/>
    public virtual void AttachObserver(Userifccs.Aux.IObserver obs)
    {
    	_observers.Add(obs);
    }

    /// <inheritdoc/>
    public virtual void DetachObserver(Userifccs.Aux.IObserver obs)
    {
    	_observers.Remove(obs);
    }

    /// <inheritdoc/>
    public virtual void NotifyObservers(string newData)
    {
		foreach (Userifccs.Aux.IObserver obs in _observers)
			obs.Update((null != newData) ? newData : "");
    }

	/// <summary>Main entry point.</summary>
    /// <param name="args">An array</param>
    /// <returns>The exit code.</returns>
		public static int Main(string[] args)
    {
		var subj = new Subject();
		var obs = new Observer();

		subj.AttachObserver(obs);
		subj.NotifyObservers("To be set -- HELP.");
		Console.Write("obs.Data: {0}\n", obs.Data);
		return 0;
	}
}

}
