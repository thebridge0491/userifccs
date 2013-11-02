namespace Userifccs.Aux {

using System;

/// <summary>Subject interface.</summary>
public interface ISubject {
    /// <summary>Attach observer for subject.</summary>
    /// <param name="obs">An observer</param>
    void AttachObserver(Userifccs.Aux.IObserver obs);

    /// <summary>Detach observer for subject.</summary>
    /// <param name="obs">An observer</param>
    void DetachObserver(Userifccs.Aux.IObserver obs);

    /// <summary>Notify observers of subject.</summary>
    /// <param name="arg">An arg value</param>
    void NotifyObservers(string arg);
}

}
