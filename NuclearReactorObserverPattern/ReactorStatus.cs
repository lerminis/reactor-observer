using System;

namespace NuclearReactorObserverPattern
{
  // Data that the subject sends to the observers
  public struct ReactorStatus
  {
    // K = Neutron Multiplication Rate, and will slightly increase over time when the control rods are not inserted
    // We will assuming this nuclear reactor should operate between K=1 and K=1.05
    public double NeutronMultiplicationFactor { get; }

    // If the control rods are inserted, K will slightly decrease over time
    public bool ControlRodsAreInserted { get; }

    public DateTime Date { get; }

    public ReactorStatus(double neutronMultiplicationFactor, bool controlRodsAreInserted, DateTime date)
    {
      NeutronMultiplicationFactor = neutronMultiplicationFactor;
      ControlRodsAreInserted = controlRodsAreInserted;
      Date = date;
    }

  }
}