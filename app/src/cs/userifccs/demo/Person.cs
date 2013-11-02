namespace Userifccs.Demo {

using System;

/// <summary>Person class.</summary>
public class Person : System.IEquatable<Person> {
	//private static readonly log4net.ILog log = 
	//	log4net.LogManager.GetLogger(
	//	System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	private static readonly log4net.ILog log = 
		log4net.LogManager.GetLogger("prac");
    
    /// <summary>Constructor default for Person.</summary>
    public Person() : this("NoName", 0)
    { }
    
    /// <summary>Constructor for Person.</summary>
    /// <param name="name">A string</param>
    /// <param name="age">An integer</param>
    public Person(string name, int age)
    {
        log.Debug(string.Format("{0}()", GetType()));
        Name = name;
        Age = age;
    }
    /*
    //destructor
    /// <summary>Destructor for Person.</summary>
    ~Person()
    {
		Console.WriteLine("destructing {0}", GetType());
	}
	*/
	/// <value>Gets|Sets the person's name.</value>
    public string Name { get; set;
    }
    
    /// <value>Gets|Sets the person's age.</value>
    public int Age { get; set;
    }
    
    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {	//if (null == obj || !(obj is Person))
        if (null == obj || GetType() != obj.GetType())
            return false;
        Person p = obj as Person;	//Person p = (Person)obj;
        return (null == p) ? false : (Name == p.Name) && (Age == p.Age);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as Person);
    }
    
	/// <summary>Compares Person instance equality.</summary>
    /// <param name="other">A Person</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(Person other)
    {	//if (null == obj || !(obj is Person))
        return null != other && Name == other.Name && Age == other.Age;
    }
    
	/// <summary>Operator for Person instance equality.</summary>
    /// <param name="lhs">A Person</param>
    /// <param name="rhs">A Person</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(Person lhs, Person rhs)
    {
		if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
			return true;
		return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
	}
    
	/// <summary>Operator for Person instance inequality.</summary>
    /// <param name="lhs">A Person</param>
    /// <param name="rhs">A Person</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(Person lhs, Person rhs)
    {
		return !(lhs == rhs);
	}
    
    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Format("{0}: [Name: {1}; Age: {2}]", GetType(),
			Name, Age);
    }
    
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        //return this.ToString().GetHashCode();
        int prime = -65336, hashCode = 17;
        hashCode = prime * hashCode + ((null != Name) ? Name.GetHashCode() : 0);
        hashCode = prime * hashCode + Age;
        return hashCode;
    }
}

}
