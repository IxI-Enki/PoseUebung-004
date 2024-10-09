# PoseUebung-004 -- Caravan

> ![image](https://github.com/user-attachments/assets/7eabbdeb-fc96-4a14-bd6d-2798d3e8d142)


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

  public abstract int Pace { get; } 
  public string Name { get { return _name!; } }
  public int MaxPace { get { return _maxPace; } }
  public int Load
  {
    get { return _load; }
    set { _load = value < 0 ? 0 : value; }
  }
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
<details>  
   <summary> klick for a short Version: </summary>  

```c#
public abstract class PackAnimal(string name , int maxPace)
{
  public abstract int Pace { get; }
  public string Name { get; } = name;
  public int MaxPace { get ; } = maxPace; 
  public Caravan? MyCaravan { get ; set ; } = null;
  public int Load { get => _load; set => _load = value < 0 ? 0 : value; }
  public override string ToString() => $"{Name} ({Load}/{Pace}/{MaxPace})";
  
  private int _load = 0;
}
```
</details>  

---  

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
<details>  
   <summary> klick for a short Version: </summary>  

```c#
public sealed class Camel(string name , int maxPace) 
  : PackAnimal(name , maxPace < 0 ? 0 : maxPace > 20 ? 20 : maxPace)
  {
    public override int Pace => MaxPace - Load;
  }
```
</details>  

---  

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
<details>  
   <summary> klick for a short Version: </summary>  

```c#
public sealed class Horse(string name , int maxPace) 
  : PackAnimal(name , maxPace < 0 ? 0 : maxPace > 70 ? 70 : maxPace)
  {
    public override int Pace => MaxPace - (10 * Load);
  }
```
</details>  

---  

<summary>
  Karavanen Struktur (unfinished ;) )
</summary>

```c#
public class Caravan
{
  public Caravan(string caravanName = "") { _caravanName = caravanName; }

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

  public void AddPackAnimal(PackAnimal? packAnimal)
  {
    if (packAnimal.MyCaravan != null)
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
    Element? run = _first;

    while (run != null)
    {
      if (run.Animal == packAnimal)
        return false;
      run = run.Next;
    }
    return true;
  }

  public void RemovePackAnimal(PackAnimal packAnimal)
  {
    packAnimal!.MyCaravan = null;

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

    while (run!.Next != null)
    {
      if (run.Animal.Pace > fastest!.Animal.Pace)
        fastest = run;

      run = run.Next;
    }
    return fastest!.Animal;
  }

  private Element? _first = null;
  private string _caravanName;

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
<details>  
   <summary> klick for a short Version: </summary>  

```c#
private class Element(PackAnimal animal , Element? next = null)
{
  public PackAnimal Animal { get; set; } = animal;
  public Element? Next { get; set; } = next;
}
```
 
</details>  
