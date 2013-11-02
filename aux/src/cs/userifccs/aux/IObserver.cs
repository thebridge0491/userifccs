namespace Userifccs.Aux {

using System;

/// <summary>Observer interface.</summary>
public interface IObserver {
    /// <summary>Update/notify observer.</summary>
    /// <param name="newData">A string</param>
    void Update(string newData);
}

}
