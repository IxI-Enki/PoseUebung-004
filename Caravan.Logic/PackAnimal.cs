namespace Caravan.Logic;

/// <summary>
/// Abstrakte Basisklasse, die generelle Eigenschaften und Methoden von 
/// Packtieren beschreibt.
/// </summary>
public abstract class PackAnimal
{
  #region CONSTRUCTORS
  /// <summary>
  /// Damit die Vorlage compilierbar bleibt
  /// </summary>
  public PackAnimal()
  {

  }

  /// <summary>
  /// Name des Tiers und Maximalgeschwindigkeit des Tiers
  /// </summary>
  /// <param name="name"></param>
  /// <param name="maxPace"></param>
  public PackAnimal(string name , int maxPace)
  {
    _name = name;
    _maxPace = maxPace;
  }
  #endregion

  #region PROPERTIES
  public string Name { get { return _name!; } }

  /// <summary>
  /// Maximale Geschwindigkeit des Tiers
  /// </summary>
  public int MaxPace { get { return _maxPace; } }

  /// <summary>
  /// Anzahl der Ballen, die das Tier trägt
  /// </summary>
  public int Load
  {
    get { return _load; }
    set { _load = value < 0 ? 0 : value; }
  }

  /// <summary>
  /// Geschwindigkeit des Tiers
  /// </summary>
  public abstract int Pace { get; }  //! logisch eigentlich ein Property

  /// <summary>
  /// Karawane, in der das Tier mitläuft. Kann einfach durch Zuweisung 
  /// gewechselt werden. Umkettung in Karawanen erfolgt automatisch
  /// </summary>
  public Caravan? MyCaravan
  {
    get { return _myCaravan; }
    set { _myCaravan = value; }
  }
  #endregion

  #region OVERRIDES
  public override string ToString()
  {
    return $"{Name} ({Load}/{Pace}/{MaxPace})";
  }
  #endregion

  #region FIELDS
  private string? _name;
  private int _maxPace;
  private int _load = 0;
  private Caravan? _myCaravan = null;
  #endregion
}