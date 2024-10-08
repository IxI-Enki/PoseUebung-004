namespace Caravan.Logic;

/// <summary>
/// Pferd mit Maximalgeschwindigkeit 70 erzeugen
/// </summary>
/// <param name="name"></param>
/// <param name="mp"></param>
public class Horse(string name , int mp) : PackAnimal(name , mp)
{
  public int Mp { get; set; } = SetValidPace_MinMax(mp);
  private static int SetValidPace_MinMax(int mp)
    => mp < 0 ? 0 : mp > 70 ? 70 : mp;

  /// <summary>
  /// Geschwindigkeit in Abhängigkeit der Ladung (Reduktion um 10 je Ballen)
  /// </summary>
  public override int Pace { get => Math.Max(Mp - (Load * 10) , 0); }

}