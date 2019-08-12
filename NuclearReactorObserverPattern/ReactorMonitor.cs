using System;
using System.Collections.Generic;

namespace NuclearReactorObserverPattern
{
  // Subject
  public class ReactorMonitor : IObservable<ReactorStatus>
  {
    private readonly List<IObserver<ReactorStatus>> _observers;

    public ReactorMonitor()
    {
      _observers = new List<IObserver<ReactorStatus>>();
    }

    private class Unsubscriber : IDisposable
    {
      private readonly List<IObserver<ReactorStatus>> _observers;
      private readonly IObserver<ReactorStatus> _observer;

      public Unsubscriber(List<IObserver<ReactorStatus>> observers, IObserver<ReactorStatus> observer)
      {
        _observers = observers;
        _observer = observer;
      }

      public void Dispose()
      {
        if (_observer != null)
        {
          _observers.Remove(_observer);
          Console.WriteLine("\nObserver {0} was successfully removed.", _observer);
        }
      }
    }

    public IDisposable Subscribe(IObserver<ReactorStatus> observer)
    {
      if (!_observers.Contains(observer))
      {
        _observers.Add(observer);
        Console.WriteLine("\nObserver {0} was successfully registered.", observer);
      }

      return new Unsubscriber(_observers, observer);
    }

    private void UpdateObserversHelper(double k, bool status)
    // Helper function that sends the new ReactorStatus to the observers
    {
      var reactorData = new ReactorStatus(k, status, DateTime.Now);

      foreach (var observer in _observers)
        observer.OnNext(reactorData);
    }

    private void UpdateControlRodsHelper()
    // Helper function that toggles the state of ControlRodsAreInserted using a random multiplier
    // If the control rods are inserted, K will slightly decrease over time
    // If the control rods are removed, K will slightly increase over time
    {
      var rnd = new Random();
      var controlRodsAreInserted = false;
      var k = 1d;

      while (!Console.KeyAvailable)
      {
        System.Threading.Thread.Sleep(1000);
        if (!controlRodsAreInserted)
        {
          if (k > 1.05d)
          {
            controlRodsAreInserted = true;
            UpdateObserversHelper(k, true);
          }

          k += rnd.NextDouble() * 0.01;
        }
        else
        {
          if (k <= 1.0d)
          {
            controlRodsAreInserted = false;
            UpdateObserversHelper(k, false);
          }

          k -= rnd.NextDouble() * 0.01;
        }
      }
    }

    public void GetFissionRate()
    // Runs UpdateControlRodsHelper() until the Space Bar key is hit
    // Calls OnCompleted for each observer, and clears them from the list
    {
      Console.WriteLine("\n### Press SPACE-BAR to disconnect. ###\nWaiting for Nuclear Fission data...");
      do
      {
        UpdateControlRodsHelper();
      } while (Console.ReadKey(true).Key != ConsoleKey.Spacebar);

      foreach (var observer in _observers.ToArray())
        observer?.OnCompleted();

      _observers.Clear();
    }
  }
}