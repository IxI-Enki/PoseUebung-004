using System.Text;

namespace Caravan.Logic;

/// <summary>
/// Abstrakte Basisklasse, die generelle Eigenschaften und Methoden von 
/// Packtieren beschreibt.
/// </summary>
public abstract class PackAnimal
{
  /// <summary>
  /// Damit die Vorlage compilierbar bleibt
  /// </summary>
  public PackAnimal() { }
  /// <summary>
  /// Name des Tiers und Maximalgeschwindigkeit des Tiers
  /// </summary>
  /// <param name="name"></param>
  /// <param name="mp"></param>
  public PackAnimal(string name , int mp)
  {
    Name = name;
    MaxPace = mp;
  }

  public string? Name { get; set; }

  /// <summary>
  /// Maximale Geschwindigkeit des Tiers
  /// </summary>
  public int MaxPace { get; set; }

  /// <summary>
  /// Anzahl der Ballen, die das Tier trägt
  /// </summary>
  public int Load
  {
    get => _load;
    set => _load = value;
  }

  /// <summary>
  /// Geschwindigkeit des Tiers
  /// </summary>
  public abstract int Pace { get; }  //! logisch eigentlich ein Property

  /// <summary>
  /// Karawane, in der das Tier mitläuft. Kann einfach durch Zuweisung 
  /// gewechselt werden. Umkettung in Karawanen erfolgt automatisch
  /// </summary>
  public Caravan? MyCaravan { get; set; }

  public PackAnimal? NextAnimal { get; set; } = null;

  public override string ToString()
  {
    StringBuilder sb = new();
    sb.Append(Name);
    sb.Append('(');
    sb.Append(Load);
    sb.Append('/');
    sb.Append(Pace);
    sb.Append('/');
    sb.Append(MaxPace);
    sb.Append(")");
    return sb.ToString();
  }


  #region FIELDS
  private int _load = 0;
  #endregion


}
