namespace NuclearReactorObserverPattern
{
  internal static class Program
  {
    public static void Main()
    {
      var subject = new ReactorMonitor();
      var observer = new ReactorReporter("Reactor Core System");
      var observer2 = new ReactorReporter("Test Display");
      var observer3 = new ReactorReporter("Plant Faculty");

      observer.Subscribe(subject);
      observer2.Subscribe(subject);
      observer2.Unsubscribe();
      observer3.Subscribe(subject);
      subject.GetFissionRate();
    }
  }
}