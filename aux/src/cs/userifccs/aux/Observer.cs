namespace Userifccs.Aux {

using System;

/// <summary>Observer class.</summary>
public class Observer : System.IEquatable<Observer>, Userifccs.Aux.IObserver {
    /*/// <summary>Constructor default for Observer.</summary>
    public Observer() : this()
    {
    	Data = "";
    }*/
    /*
    //destructor
    /// <summary>Destructor for Observer.</summary>
    ~Observer()
    {
		Console.WriteLine("destructing {0}", GetType());
	}
	*/
	/// <value>Gets|Sets the observer's data.</value>
    public string Data { get; set;
    }

    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {	//if (null == obj || !(obj is Observer))
        if (null == obj || GetType() != obj.GetType())
            return false;
        Observer p = obj as Observer;	//Observer p = (Observer)obj;
        return (null == p) ? false : (Data == p.Data);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as Observer);
    }

	/// <summary>Compares Observer instance equality.</summary>
    /// <param name="other">An Observer</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(Observer other)
    {	//if (null == obj || !(obj is Observer))
        return null != other && Data == other.Data;
    }

	/// <summary>Operator for Observer instance equality.</summary>
    /// <param name="lhs">An Observer</param>
    /// <param name="rhs">An Observer</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(Observer lhs, Observer rhs)
    {
		if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
			return true;
		return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
	}

	/// <summary>Operator for Observer instance inequality.</summary>
    /// <param name="lhs">An Observer</param>
    /// <param name="rhs">An Observer</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(Observer lhs, Observer rhs)
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
        hashCode = prime * hashCode + ((null != Data) ? Data.GetHashCode() : 0);
        return hashCode;
    }

    /// <inheritdoc/>
    public virtual void Update(string newData)
    {
    	Data = newData;
    }
}

}
