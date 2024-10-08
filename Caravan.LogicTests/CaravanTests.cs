using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caravan.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Logic.Tests
{

  [TestClass]
  public class CaravanUnitTests
  {
    /// <summary>
    ///A test for Unload
    ///</summary>
    [TestMethod()]
    public void ItShouldUnloadAllAnimals_GivenCaravanWithAnimals()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      Logic.Camel willi = new Logic.Camel("Willi" , 15);
      Logic.Horse moritz = new Logic.Horse("Moritz" , 35);
      willi.Load = 10;
      moritz.Load = 3;

      // Assert before unloading
      Assert.AreEqual(10 , willi.Load , "Die Beladung mit 10 ist nicht korrekt");
      Assert.AreEqual(3 , moritz.Load , "Die Beladung mit 3 ist nicht korrekt");
      Assert.AreEqual(5 , willi.Pace , "Die Geschw. von 5 bei Willi ist nicht korrekt");
      Assert.AreEqual(5 , moritz.Pace , "Die Geschw. von 5 bei Moritz ist nicht korrekt");

      // Act
      saharaExpress.AddPackAnimal(willi);
      saharaExpress.AddPackAnimal(moritz);
      saharaExpress.Unload();

      // Assert after unloading
      Assert.AreEqual(0 , willi.Load , "Die Entladung von willi ist nicht korrekt");
      Assert.AreEqual(0 , moritz.Load , "Die Entladung von moritz ist nicht korrekt");
    }

    [TestMethod()]
    public void ItShouldRemovePackAnimal_GivenAnimalInCaravan()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      Logic.Camel willi = new Logic.Camel("Willi" , 15);
      willi.Load = 10;

      // Act
      saharaExpress.AddPackAnimal(willi);

      // Assert before removing
      Assert.AreEqual(saharaExpress , willi.MyCaravan , "Verweis auf eigene Karawane nicht korrekt");
      Assert.AreEqual(1 , saharaExpress.Count , "Anzahl der Tragtiere in Karawane nicht korrekt");

      saharaExpress.RemovePackAnimal(willi);

      // Assert after removing
      Assert.IsNull(willi.MyCaravan , "Verweis auf NULL in myCaravan nicht korrekt");
      Assert.AreEqual(0 , saharaExpress.Count , "Anzahl der Tragtiere in Karawane nicht korrekt");
    }

    [TestMethod()]
    public void ItShouldReturnAnimalByName_GivenAnimalNameInCaravan()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      saharaExpress.AddPackAnimal(new Logic.Camel("Willi" , 15));
      saharaExpress.AddPackAnimal(new Logic.Camel("Susi" , 16));
      saharaExpress.AddPackAnimal(new Logic.Camel("Mitzi" , 14));

      // Assert
      Assert.AreEqual("Mitzi" , saharaExpress[ "Mitzi" ]!.Name , "Indexer am ANFANG nicht korrekt");
      Assert.AreEqual("Susi" , saharaExpress[ "Susi" ]!.Name , "Indexer in der MITTE nicht korrekt");
      Assert.AreEqual("Willi" , saharaExpress[ "Willi" ]!.Name , "Indexer am ENDE nicht korrekt");
    }

    [TestMethod()]
    public void ItShouldReturnAnimalByPosition_GivenAnimalPositionInCaravan()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      saharaExpress.AddPackAnimal(new Logic.Camel("Willi" , 15));
      saharaExpress.AddPackAnimal(new Logic.Camel("Susi" , 16));
      saharaExpress.AddPackAnimal(new Logic.Camel("Mitzi" , 14));

      // Assert
      Assert.AreEqual("Willi" , saharaExpress[ 0 ]!.Name , "Indexer am ANFANG nicht korrekt");
      Assert.AreEqual("Susi" , saharaExpress[ 1 ]!.Name , "Indexer in der MITTE nicht korrekt");
      Assert.AreEqual("Mitzi" , saharaExpress[ 2 ]!.Name , "Indexer am ENDE nicht korrekt");
    }

    [TestMethod()]
    public void ItShouldUpdatePaceAndLoad_GivenAnimalsWithLoad()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      saharaExpress.AddPackAnimal(new Logic.Camel("Willi" , 15));
      saharaExpress.AddPackAnimal(new Logic.Camel("Susi" , 16));
      saharaExpress.AddPackAnimal(new Logic.Camel("Mitzi" , 14));
      saharaExpress[ "Willi" ]!.Load = 10;
      saharaExpress[ "Susi" ]!.Load = 8;
      saharaExpress[ "Mitzi" ]!.Load = 5;

      // Assert initial
      Assert.AreEqual(5 , saharaExpress.Pace , "Karawane bewegt sich --> nicht korrekt");
      Assert.AreEqual(23 , saharaExpress.Load , "Gesamtbeladung der Karawane stimmt nicht");

      // Act
      saharaExpress[ "Willi" ]!.Load = 25;

      // Assert after update
      Assert.AreEqual(0 , saharaExpress.Pace , "Karawane steht, weil Last zu hoch --> nicht korrekt");
      Assert.AreEqual(38 , saharaExpress.Load , "Gesamtbeladung der Karawane stimmt nicht");
    }

    [TestMethod()]
    public void ItShouldNotAddDuplicateAnimal_GivenAnimalAlreadyInCaravan()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      Logic.Camel willi = new Logic.Camel("Willi" , 15);
      willi.Load = 10;

      // Act
      saharaExpress.AddPackAnimal(willi);
      saharaExpress.AddPackAnimal(willi); // Versuch, Willi ein zweites Mal hinzuzufügen

      // Assert
      Assert.AreEqual(1 , saharaExpress.Count , "Willi sollte nur einmal in der Karawane sein");
      Assert.AreEqual(10 , saharaExpress.Load , "Die Gesamtbeladung sollte sich nicht ändern");
    }

    [TestMethod()]
    public void ItShouldAddLoadToAllAnimals_GivenAdditionalLoad()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      saharaExpress.AddPackAnimal(new Logic.Camel("Willi" , 15));
      saharaExpress.AddPackAnimal(new Logic.Camel("Susi" , 16));
      saharaExpress.AddPackAnimal(new Logic.Camel("Mitzi" , 14));
      saharaExpress.AddPackAnimal(new Logic.Horse("Moritz" , 45));

      saharaExpress[ "Willi" ]!.Load = 10;
      saharaExpress[ "Susi" ]!.Load = 9;
      saharaExpress[ "Mitzi" ]!.Load = 5;
      saharaExpress[ "Moritz" ]!.Load = 3;

      // Act
      saharaExpress.AddLoad(3);

      // Assert
      Assert.AreEqual(5 , saharaExpress.Pace , "Karawanengeschwindigkeit gesamt nicht korrekt");
      Assert.AreEqual(5 , saharaExpress[ "Willi" ]!.Pace , "Geschwindigkeit von Willi nicht korrekt");
      Assert.AreEqual(15 , saharaExpress[ "Moritz" ]!.Pace , "Geschwindigkeit von Moritz nicht korrekt");
    }

    [TestMethod()]
    public void ItShouldBeResilient_GivenAdditionalLoadOnAllAnimals()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      saharaExpress.AddPackAnimal(new Logic.Camel("Willi" , 17));
      saharaExpress.AddPackAnimal(new Logic.Camel("Susi" , 16));
      saharaExpress.AddPackAnimal(new Logic.Camel("Mitzi" , 13));
      saharaExpress.AddPackAnimal(new Logic.Horse("Moritz" , 45));

      saharaExpress[ "Willi" ]!.Load = 10;
      saharaExpress[ "Susi" ]!.Load = 9;
      saharaExpress[ "Mitzi" ]!.Load = 5;
      saharaExpress[ "Moritz" ]!.Load = 3;

      // Act
      saharaExpress.AddLoad(4);

      // Assert
      Assert.AreEqual(6 , saharaExpress.Pace , "Karawanengeschwindigkeit gesamt nicht korrekt");
      Assert.AreEqual(6 , saharaExpress[ "Willi" ]!.Pace , "Geschwindigkeit von Willi nicht korrekt");
      Assert.AreEqual(15 , saharaExpress[ "Moritz" ]!.Pace , "Geschwindigkeit von Moritz nicht korrekt");
    }

    [TestMethod()]
    public void ItShouldMoveAnimalsBetweenCaravans_GivenCaravanChange()
    {
      // Arrange
      Logic.Caravan saharaExpress = new Logic.Caravan();
      Logic.Caravan secondExpress = new Logic.Caravan();
      Logic.Camel willi = new Logic.Camel("Willi" , 15);
      Logic.Horse moritz = new Logic.Horse("Moritz" , 35);

      willi.Load = 4;
      moritz.Load = 3;

      saharaExpress.AddPackAnimal(willi);
      saharaExpress.AddPackAnimal(moritz);
      saharaExpress.AddPackAnimal(new Logic.Camel("Susi" , 16));
      saharaExpress.AddPackAnimal(new Logic.Camel("Mitzi" , 14));
      secondExpress.AddPackAnimal(new Logic.Camel("Franzi" , 20));

      // Act
      saharaExpress.RemovePackAnimal(willi);

      // Assert
      Assert.AreEqual(3 , saharaExpress.Count , " Sollten nur mehr 3 Tiere sein");
      Assert.IsNull(willi.MyCaravan , "Momentan keine Karawane für Willi");

      secondExpress.AddPackAnimal(willi);

      // Assert
      Assert.AreEqual(2 , secondExpress.Count , "Sollten nur mehr 3 Tiere sein");

      // Act
      secondExpress.AddPackAnimal(moritz);

      // Assert
      Assert.AreEqual(secondExpress , moritz.MyCaravan , "Moritz gehört jetzt zu secondExpress");
      Assert.AreEqual(3 , secondExpress.Count , " Sollten 3 Tiere sein");
      Assert.AreEqual(2 , saharaExpress.Count , " Sollten nur mehr 2 Tiere sein");
      Assert.AreEqual(3 , secondExpress[ "Moritz" ]!.Load , "Moritz Ladung gehört jetzt zu SecondExpress");

      // Act
      moritz.MyCaravan = saharaExpress;

      // Assert
      Assert.AreEqual(saharaExpress , moritz.MyCaravan , "Moritz gehört jetzt wieder zu Saharaexpress");
      Assert.AreEqual(3 , saharaExpress[ "Moritz" ]!.Load , "Moritz Ladung gehört jetzt auch wieder zu Saharaexpress");
      Assert.AreEqual(2 , secondExpress.Count , " Sollten 2 Tiere sein");
      Assert.AreEqual(3 , saharaExpress.Count , " Sollten 3 Tiere sein");
    }
  }
}