namespace Caravan.Logic;

public sealed class Camel : PackAnimal
{
  /// <summary>
  /// Kamel mit Maximalgeschwindigkeit 20 erzeugen
  /// </summary>
  /// <param name="name"></param>
  /// <param name="maxPace"></param>
  public Camel(string name , int maxPace)
    : base(name , maxPace < 0 ? 0 : maxPace > 20 ? 20 : maxPace)
  { }

  /// <summary>
  /// Geschwindigkeit in Abhängigkeit der Ladung (Reduktion um 1 je Ballen)
  /// </summary>
  public override int Pace { get { return MaxPace - Load; } }
}
