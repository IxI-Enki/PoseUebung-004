namespace Caravan.Logic;

/// <summary>
/// Kamel mit Maximalgeschwindigkeit 20 erzeugen
/// </summary>
/// <param name="name"></param>
/// <param name="mp"></param>
public class Camel(string name , int mp) : PackAnimal(name , mp)
{
  public int Mp { get; set; } = SetValidPace_MinMax(mp);
  private static int SetValidPace_MinMax(int mp)
    => mp < 0 ? 0 : mp > 20 ? 20 : mp;

  /// <summary>
  /// Geschwindigkeit in Abhängigkeit der Ladung (Reduktion um 1 je Ballen)
  /// </summary>
  public override int Pace { get => Math.Max(Mp - Load , 0); }
}