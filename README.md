# PoseUebung-004 -- Caravan

<summary>
  Abstrakte Basisklasse, die generelle Eigenschaften und Methoden von Packtieren beschreibt.
</summary>

```c#
public abstract class PackAnimal
{
  public PackAnimal() {  }
  public PackAnimal(string name , int mp)
  {
    _name = name;
    _maxPace = mp;
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

  private string? _name;
  private int _maxPace;
  private int _load = 0;
  private Caravan? _myCaravan = null;
 }
```  
