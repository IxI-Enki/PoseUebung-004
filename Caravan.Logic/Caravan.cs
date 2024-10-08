using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace Caravan.Logic;

public class Caravan
{
  private class Element
  {
    public Element(PackAnimal animal , Element next)
    {
      Animal = animal;
      Next = next;
    }
    public PackAnimal Animal { get; set; }
    public Element Next { get; set; }
  }

  public Caravan()
  {

  }

  /// <summary>
  /// Gibt die Anzahl der Tragtiere in der Karavane zurück
  /// </summary>
  public int Count
  {
    get
    {
      int count = 0;
      PackAnimal? run = _first;

      while (run != null)
      {
        run = run.NextAnimal;
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
      int count = 0;
      PackAnimal? run = _first;

      while (run != null)
      {
        count = count + run.Load;
        run = run.NextAnimal;
      }
      return count;
    }
  }

  /// <summary>
  /// Indexer, der ein Packtier nach Namen sucht und zurückgibt.
  /// Existiert das Packtier nicht, wird NULL zurückgegeben.
  /// </summary>
  /// <param name="name">Name des Packtiers</param>
  /// <returns>Packtier</returns>
  public PackAnimal this[ string name ] { get => GetAnimalByIndexerName(name); }
  private PackAnimal GetAnimalByIndexerName(string name)
  {
    PackAnimal? run = null;

    if (_first == null)
      throw new ArgumentNullException("Empty caravan");

    run = _first;
    while (run!.Name != name)
    {
      run = run.NextAnimal;
    }
    return run!;
  }

  /// <summary>
  /// Indexer, der ein Packtier entsprechend der Position in der Karawane sucht 
  /// und zurückgibt (0 --> Erstes Tier in der Karawane)
  /// Existiert die Position nicht, wird NULL zurückgegeben.
  /// </summary>
  /// <param name="index"></param>
  /// <returns></returns>
  public PackAnimal this[ int index ] { get => GetAnimalByIndex(index); }
  private PackAnimal GetAnimalByIndex(int index)
  {
    CheckIndexOutOfRange(index);

    int counter = 0;
    PackAnimal? run = _first;

    while (counter < index)
    {
      counter++;
      run = run!.NextAnimal;
    }
    return run!;
  }
  private void CheckIndexOutOfRange(int index)
  {
    if (index < 0 || index >= Count)
      throw new ArgumentOutOfRangeException(nameof(index));
  }

  /// <summary>
  /// Liefert die Reisegeschwindigkeit dieser Karawane, die
  /// vom langsamsten Tier bestimmt wird. Dabei wird die Ladung 
  /// der Tiere berücksichtigt
  /// </summary>
  public int Pace { get => FindSlowestAnimal().Pace < 0 ? 0 : FindSlowestAnimal().Pace; }
  private PackAnimal FindSlowestAnimal()
  {
    PackAnimal? run = _first;
    PackAnimal? slowestAnimal = run;

    while (run!.NextAnimal != null)
    {
      if (slowestAnimal!.Pace > run.Pace)
        slowestAnimal = run;

      run = run.NextAnimal;
    }
    return slowestAnimal!;
  }

  /// <summary>
  /// Fügt ein Tragtier in die Karawane ein.
  /// Dem Tragtier wird mitgeteilt, in welcher Karawane es sich nun befindet.
  /// </summary>
  /// <param name="p">einzufügendes Tragtier</param>
  public void AddPackAnimal(PackAnimal p)
  {
    if (p == null)
      return;
    if (IsAlreadyInCaravan(p))
      return;
    if (p.MyCaravan != null)
      RemovePackAnimal(p);

    p.MyCaravan = this;

    if (_first == null)
      _first = p;
    else
    {
      PackAnimal run = _first;
      while (run.NextAnimal != null)
      {
        run = run.NextAnimal;
      }
      run.NextAnimal = p;
    }
  }

  private bool IsAlreadyInCaravan(PackAnimal p)
  {
    bool isInCaravan = false;
    int count = 0;
    PackAnimal? run = _first;
    while (count < Count)
    {
      if (run == p)
        return true;
      count++;

      run = run!.NextAnimal;
    }

    return isInCaravan;
  }

  /// <summary>
  /// Nimmt das Tragtier p aus dieser Karawane heraus
  /// </summary>
  /// <param name="p">Tragtier, das die Karawane verläßt</param>
  public void RemovePackAnimal(PackAnimal p)
  {
    p.MyCaravan = null;

    if (_first == p)
    {
      _first = _first.NextAnimal;
    }
    p.NextAnimal = null;

    PackAnimal? run = _first;

    while (run != null)
    {
      if (run == p)
        run = run.NextAnimal;

      run = run?.NextAnimal;
    }
  }

  /// <summary>
  /// Entlädt alle Tragtiere dieser Karawane
  /// </summary>
  public void Unload()
  {
    PackAnimal? p = _first;

    while (p != null)
    {
      p.Load = 0;

      if (p.NextAnimal != null)
        p = p.NextAnimal;
      else
        return;
    }
    return;
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
      FindFastestAnimal().Load += 1;
      loadToDistribute--;
    }
  }

  private PackAnimal FindFastestAnimal()
  {
    PackAnimal? run = _first;
    PackAnimal? fastestAnimal = run;

    while (run!.NextAnimal != null)
    {
      if (fastestAnimal!.Pace < run.Pace)
        fastestAnimal = run;

      run = run.NextAnimal;
    }
    return fastestAnimal!;
  }

  #region FIELDS
  private PackAnimal? _first = null;
  private string _caravanName = string.Empty;
  #endregion

  public override string ToString()
  {
    StringBuilder sb = new();

    sb.Append("Caravane: ");
    sb.Append(_caravanName);
    sb.Append("MaxPace: " + Pace + " mp ");

    sb.Append("PackAnimal(s): ");
    PackAnimal? run = _first;
    while (run != null)
    {
      sb.Append(" " + run);
      run = run.NextAnimal;
    }

    return sb.ToString();
  }



}