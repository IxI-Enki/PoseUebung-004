
namespace Caravan.Logic;

public class Caravan
{
  public Caravan(string name = "") { _caravanName = name; }

  /// <summary>
  /// Gibt die Anzahl der Tragtiere in der Karavane zurück
  /// </summary>
  public int Count
  {
    get
    {
      int count = 0;

      Element? run = _first;
      while (run != null)
      {
        run = run.Next;
        count++;
      }
      return count;
    }
  }

  /// <summary>
  /// Anzahl der Ballen der gesamten Karawane
  /// </summary>
  public int Load
  {
    get
    {
      int loadCount = 0;

      Element? run = _first;
      while (run != null)
      {
        loadCount += run.Animal.Load;
        run = run.Next;
      }
      return loadCount;
    }
  }

  /// <summary>
  /// Indexer, der ein Packtier nach Namen sucht und zurückgibt.
  /// Existiert das Packtier nicht, wird NULL zurückgegeben.
  /// </summary>
  /// <param name="name">Name des Packtiers</param>
  /// <returns>Packtier</returns>
  public PackAnimal? this[ string name ]
  {
    get
    {
      Element? run = _first;
      while (run != null && run.Animal.Name != name)
      {
        run = run.Next;
      }
      return run!.Animal;
    }
  }

  /// <summary>
  /// Indexer, der ein Packtier entsprechend der Position in der Karawane sucht 
  /// und zurückgibt (0 --> Erstes Tier in der Karawane)
  /// Existiert die Position nicht, wird NULL zurückgegeben.
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public PackAnimal? this[ int index ]
  {
    get
    {
      int count = 0;
      Element? run = _first;

      while (run != null && count < index)
      {
        run = run.Next;
        count++;
      }
      return run!.Animal;
    }
  }

  /// <summary>
  /// Liefert die Reisegeschwindigkeit dieser Karawane, die
  /// vom langsamsten Tier bestimmt wird. Dabei wird die Ladung 
  /// der Tiere berücksichtigt
  /// </summary>
  public int Pace
  {
    get
    {
      return FindSlowestAnimal().Pace < 0 ? 0 : FindSlowestAnimal().Pace;
    }
  }
  private PackAnimal FindSlowestAnimal()
  {
    Element? run = _first;
    Element? slowest = run;

    while (run!.Next != null)
    {
      if (run.Animal.Pace < slowest!.Animal.Pace)
        slowest = run;

      run = run.Next;
    }
    return slowest!.Animal;
  }

  /// <summary>
  /// Fügt ein Tragtier in die Karawane ein.
  /// Dem Tragtier wird mitgeteilt, in welcher Karawane es sich nun befindet.
  /// </summary>
  /// <param name="packAnimal">einzufügendes Tragtier</param>
  public void AddPackAnimal(PackAnimal? packAnimal)
  {
    //    if (packAnimal == null) return;
    if (packAnimal!.MyCaravan != null)
      RemovePackAnimal(packAnimal);

    if (_first == null)
    {
      packAnimal.MyCaravan = this;
      _first = new Element(packAnimal! , _first);
    }

    if (IsNotInCaravan(packAnimal))
    {
      Element? run = _first;

      while (run.Next != null)
      {
        run = run.Next;
      }
      packAnimal.MyCaravan = this;
      run.Next = new Element(packAnimal! , null);
    }
  }
  private bool IsNotInCaravan(PackAnimal packAnimal)
  {
    bool isInCaravan = false;
    Element? run = _first;

    while (run != null)
    {
      if (run.Animal == packAnimal)
        return !true;
      run = run.Next;
    }
    return !isInCaravan;
  }

  /// <summary>
  /// Nimmt das Tragtier o aus dieser Karawane heraus
  /// </summary>
  /// <param name="packAnimal">Tragtier, das die Karawane verläßt</param>
  public void RemovePackAnimal(PackAnimal packAnimal)
  {
    //    if (packAnimal == null || this.Count == 0) return;

    packAnimal.MyCaravan = null;

    if (_first!.Animal == packAnimal)
    {
      _first = _first.Next;
    }
    else
    {
      Element? run = _first;

      while (run != null && run.Next != null)
      {
        if (run.Next.Animal == packAnimal)
          run.Next = run.Next.Next;

        run = run.Next;
      }
    }
  }

  /// <summary>
  /// Entlädt alle Tragtiere dieser Karawane
  /// </summary>
  public void Unload()
  {
    Element? run = _first;
    while (run != null)
    {
      run.Animal.Load = 0;
      run = run.Next;
    }
  }

  /// <summary>
  /// Verteilt zusätzliche Ballen Ladung so auf die Tragtiere 
  /// der Karawane, dass die Reisegeschwindigkeit möglichst hoch bleibt
  /// Tipp: Gib immer einen Ballen auf das belastbarste (schnellste) Tier bis alle Ballen vergeben sind
  /// </summary>
  /// <param name="load">Anzahl der Ballen Ladung</param>
  public void AddLoad(int load)
  {
    int loadToDistribute = load;

    while (loadToDistribute > 0)
    {
      FindFastestAnimal().Load++;
      loadToDistribute--;
    }
  }
  private PackAnimal FindFastestAnimal()
  {
    Element? run = _first;
    Element? fastest = run;

    while (run!.Next != null)
    {
      if (run.Animal.Pace > fastest!.Animal.Pace)
        fastest = run;

      run = run.Next;
    }
    return fastest!.Animal;
  }

  #region FIELDS
  private Element? _first = null;
  private string _caravanName;        // implement mit nameof() ? 
  #endregion

  #region EMBEDED CLASS ELEMENT
  private class Element
  {
    public Element(PackAnimal animal , Element? next = null)
    {
      Animal = animal;
      Next = next;
    }
    public PackAnimal Animal { get; set; }
    public Element? Next { get; set; }
    #endregion
  }
}