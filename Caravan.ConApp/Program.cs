using Caravan.Logic;
using System.Text;
namespace Caravan.ConApp;

internal class Program
{
  static void Main(string[ ] args)
  {
    Console.OutputEncoding = Encoding.UTF8;
    Logic.Caravan saharaExpress = new("Sahara-Express");

    Camel hoecke = new("Höcke" , 14);
    hoecke.Load = 2;
    Horse weidel = new("Weidel" , 44);
    weidel.Load = 2;
    Camel aladin = new("Aladin" , 20);
    Camel budi = new("Budi" , 11);

    saharaExpress.AddPackAnimal(hoecke);
    saharaExpress.AddPackAnimal(weidel);
    saharaExpress.AddPackAnimal(aladin);
    saharaExpress.AddPackAnimal(budi);

    saharaExpress[ "Höcke" ]!.Load += 1;
    saharaExpress[ 0 ]!.Load += 1;
    saharaExpress.AddLoad(4);

    saharaExpress.PrintCaravan();
    Console.WriteLine();
    Console.WriteLine(saharaExpress);
    Console.ReadLine();
  }
}
