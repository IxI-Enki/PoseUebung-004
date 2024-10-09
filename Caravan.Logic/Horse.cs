namespace Caravan.Logic;

public sealed class Horse : PackAnimal
{
  /// <summary>
  /// Pferd mit Maximalgeschwindigkeit 70 erzeugen
  /// </summary>
  /// <param name="name"></param>
  /// <param name="maxPace"></param>
  public Horse(string name , int maxPace)
    : base(name , maxPace < 0 ? 0 : maxPace > 70 ? 70 : maxPace)
  { }

  /// <summary>
  /// Geschwindigkeit in Abhängigkeit der Ladung (Reduktion um 10 je Ballen)
  /// </summary>
  public override int Pace { get { return MaxPace - (10 * Load); } }
}
