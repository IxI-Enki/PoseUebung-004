using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Caravan.Logic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Caravan.Mono;

public class CaravanManager
{
  private List<Caravan> _caravans = [ ];
  private int _lastPosX = 0;
  public List<Caravan> AllCaravans { get => _caravans; set => _caravans = value; }

  internal void Draw()
  {
    if (AllCaravans != null)
      DrawCaravan(AllCaravans[ 0 ]);
  }

  private void DrawCaravan(Caravan caravan)
  {
    int counter = 0;
    Caravan.Element run = new(caravan[ 0 ]);
    while (run != null && counter < caravan.Count)
    {
      counter++;
      if (counter < caravan.Count)
        run.Next = new(caravan[ counter ]);
      DrawPackAnimal(run);
      run = run.Next;
      _lastPosX += 64;
    }
    _lastPosX = 0;
  }

  private void DrawPackAnimal(Caravan.Element run)
  {

    if (run != null)
    {
      PackAnimal animalToDraw = run.Animal;

      Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
      Globals.SpriteBatch.Draw(
        GetSprite(animalToDraw) ,
        new Rectangle(_lastPosX , 0 , 64 , 64) ,
        Color.White);
      Globals.SpriteBatch.End();
    }
  }

  private Texture2D GetSprite(PackAnimal animal)
  {
    if (animal is Camel)
      return Globals.camel;
    if (animal is Horse)
      return Globals.horse;
    else
      return null;
  }

  internal void Initialize()
  {
    Caravan saharaExpress = new("Sahara Express");
    AllCaravans.Add(saharaExpress);

    Camel hoecke = new("Höcke" , 12);
    Camel hoecke2 = new("Höcke2" , 12);
    Camel hoecke3 = new("Höcke2" , 12);
    Camel hoecke4 = new("Höcke2" , 12);
    Camel hoecke5 = new("Höcke2" , 12);
    Camel hoecke6 = new("Höcke2" , 12);
    Horse alice = new("Alice" , 22);
    hoecke.Load = 2;
    saharaExpress.AddPackAnimal(hoecke);
    saharaExpress.AddPackAnimal(hoecke2);
    saharaExpress.AddPackAnimal(hoecke3);
    saharaExpress.AddPackAnimal(hoecke4);
    saharaExpress.AddPackAnimal(hoecke5);
    saharaExpress.AddPackAnimal(hoecke6);
    saharaExpress.AddPackAnimal(alice);
  }
}

public class Caravan
{
  public Caravan(string name = "") { _caravanName = name; }

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
  public void RemovePackAnimal(PackAnimal packAnimal)
  {
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
  public override string ToString()
  {
    StringBuilder sb = new();

    sb.Append($"Name der Karavane: {_caravanName}" + "\n");
    sb.Append($"Ladung           : {Load} Ballen" + "\n");
    sb.Append($"Geschwindigkeit  : {Pace} km/h" + "\n");
    sb.Append($"Anzahl der Tiere : {Count} Tiere" + "\n");
    sb.Append("----------------------------------------\n");
    Element? run = _first;
    while (run != null)
    {
      sb.Append($"{run.Animal}" + "\n");
      run = run.Next;
    }
    return sb.ToString();
  }

  private Element? _first = null;
  private string _caravanName;

  public class Element(PackAnimal animal , Element? next = null)
  {
    public PackAnimal Animal { get; set; } = animal;
    public Element? Next { get; set; } = next;
  }
}

public abstract class PackAnimal(string name , int maxPace)
{
  public abstract int Pace { get; }
  public string Name { get; } = name;
  public int MaxPace { get; } = maxPace;
  public Caravan? MyCaravan { get; set; } = null;
  public int Load { get => _load; set => _load = value < 0 ? 0 : value; }
  public override string ToString() => $"{Name} ({Load}/{Pace}/{MaxPace})";

  private int _load = 0;
}
public sealed class Camel(string name , int maxPace)
  : PackAnimal(name , maxPace < 0 ? 0 : maxPace > 20 ? 20 : maxPace)
{
  public override int Pace => MaxPace - Load;
}
public sealed class Horse(string name , int maxPace)
  : PackAnimal(name , maxPace < 0 ? 0 : maxPace > 70 ? 70 : maxPace)
{
  public override int Pace => MaxPace - Load * 10;
}