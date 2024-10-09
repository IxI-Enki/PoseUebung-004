# PoseUebung-004 -- Caravan

<summary>
  Abstrakte Basisklasse, die generelle Eigenschaften und Methoden von Packtieren beschreibt.
</summary>

```c#
public abstract class PackAnimal
{
  public PackAnimal() {  }
  public PackAnimal(string name , int maxPace)
  {
    _name = name;
    _maxPace = maxPace;
  }

  public string Name { get { return _name!; } }
  public int MaxPace { get { return _maxPace; } }
  public int Load
  {
    get { return _load; }
    set { _load = value < 0 ? 0 : value; }
  }
  public abstract int Pace { get; } 
  public Caravan? MyCaravan
  {
    get { return _myCaravan; }
    set { _myCaravan = value; }
  }

  public override string ToString()
  {
    return $"{Name} ({Load}/{Pace}/{MaxPace})";
  }

  private string? _name;
  private int _maxPace;
  private int _load = 0;
  private Caravan? _myCaravan = null;
 }
```


<summary>
  Kamel mit Maximalgeschwindigkeit 20
</summary>
 
```c#
public sealed class Camel : PackAnimal
{
  public Camel(string name , int maxPace)
    : base(name , maxPace < 0 ? 0 : maxPace > 20 ? 20 : maxPace)
  { }

  public override int Pace { get { return MaxPace - Load; } }
}
```

<summary>
  Pferd mit Maximalgeschwindigkeit 70
</summary>

```c#
public sealed class Horse : PackAnimal
{
  public Horse(string name , int maxPace)
    : base(name , maxPace < 0 ? 0 : maxPace > 70 ? 70 : maxPace)
  { }

  public override int Pace { get { return MaxPace - (10 * Load); } }
}
```

---  

<summary>
  Karavanen Struktur (unfinished ;) )
</summary>

```c#
public class Caravan
{
  public Caravan() { }

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

  public int Pace
  {
    get
    {
      return FindSlowestAnimal().Pace;
    }
  }

  private PackAnimal FindSlowestAnimal()
  {
    Element? run = _first;
    Element? slowest = run;

    while (run != null && slowest != null)
    {
      if (run.Animal.Pace < slowest!.Animal.Pace)
        slowest = run;

      run = run.Next;
    }
    return slowest!.Animal;
  }

  public void AddPackAnimal(PackAnimal? packAnimal)
  {
    if (packAnimal == null)
      return;

    if (_first == null)
      _first = new Element(packAnimal! , _first);
    else
    {
      Element? run = _first;

      while (run.Next != null)
      {
        run = run.Next;
      }
      run.Next = new Element(packAnimal! , null);
    }
  }

  public void RemovePackAnimal(PackAnimal packAnimal)
  {
    if (packAnimal == null || this.Count == 0)
      return;

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

  public void Unload()
  {
    Element? run = _first;
    while (run != null)
    {
      run.Animal.Load = 0;
      run = run.Next;
    }
  }

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

    while (run != null && fastest != null)
    {
      if (run.Animal.Pace > fastest!.Animal.Pace)
        fastest = run;

      run = run.Next;
    }
    return fastest!.Animal;
  }

  private Element? _first = null;
  private string _caravanName;        // implement mit nameof() ? 

  private class Element
  {
    public Element(PackAnimal animal , Element? next = null)
    {
      Animal = animal;
      Next = next;
    }
    public PackAnimal Animal { get; set; }
    public Element? Next { get; set; }
  }
}
```
