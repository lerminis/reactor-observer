using System;

namespace NuclearReactorObserverPattern
{
    // Observer
    public sealed class ReactorReporter : IObserver<ReactorStatus>
    {
        private readonly string _name;
        private IDisposable _unsubscriber;

        public ReactorReporter(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }

        public void Subscribe(IObservable<ReactorStatus> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public void OnCompleted()
        {
            Console.WriteLine("\nAdditional reactor data will not be transmitted from {0}.", _name);
        }

        public void OnError(Exception error)
        {
            // Do nothing.
        }

        public void OnNext(ReactorStatus value)
        {
            Console.WriteLine(
                "\nControl rod status change detected from '{0}' at {1:g}:\n\tThe nuclear fission chain reaction rate (K) is {2}.\n\t {3}",
                _name, value.Date.ToString("HH:mm:ss tt"), value.NeutronMultiplicationFactor,
                value.ControlRodsAreInserted
                    ? "K was nearing supercriticality. The control rods have been re-inserted. "
                    : "K was slightly subcritical. The control rods have been removed.");
        }
    }
}