namespace Userifccs.Demo {

//using System;

/// <summary>User struct.</summary>
public struct User {
    private System.DateTime timeIn;
    
    /// <value>Gets|Sets the user's name.</value>
    public string Name { get; set;  
    }
    
    /// <value>Gets|Sets the user's number.</value>
    public int Num { get; set;
    }
    
    /// <value>Gets|Sets the user's time in.</value>
    public System.DateTime TimeIn {
        get { return timeIn; }
        set { timeIn = value; }
    }
    
    /// <inheritdoc/>
    public override string ToString()
    {
		return string.Format("User: [Name: {0}; Num: {1}; TimeIn: {2}]", 
			Name, Num, TimeIn);
    }
}

}
