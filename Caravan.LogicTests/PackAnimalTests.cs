using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caravan.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Logic.Tests;

[TestClass]
public class PackAnimalUnitTests
{
  [TestMethod()]
  public void ItShouldReturnTheCorrectPaceForHorse_GivenValidAndInvalidValue()
  {
    PackAnimal maxPlusOne = new Horse("willi" , 71);
    Assert.AreEqual(70 , maxPlusOne.Pace , $"Geschw. {maxPlusOne.Pace} nicht korrekt");

    PackAnimal max = new Horse("willi" , 70);
    Assert.AreEqual(70 , maxPlusOne.Pace , $"Geschw. {max.Pace} nicht korrekt");

    PackAnimal maxMinusOne = new Horse("willi" , 69);
    Assert.AreEqual(69 , maxMinusOne.Pace , $"Geschw. {maxMinusOne.Pace} nicht korrekt");

    PackAnimal minusOne = new Horse("Franzi" , -1);
    Assert.AreEqual(0 , minusOne.Pace , $"Geschw. {minusOne.Pace} nicht korrekt");

    PackAnimal zero = new Horse("Susi" , 0);
    Assert.AreEqual(0 , zero.Pace , $"Geschw. {zero.Pace} nicht korrekt");

    PackAnimal plusOne = new Horse("Franzi" , 1);
    Assert.AreEqual(1 , plusOne.Pace , $"Geschw. {plusOne.Pace} nicht korrekt");
  }

  [TestMethod()]
  public void ItShouldReturnTheCorrectPaceForCamel_GivenValidAndInvalidValue()
  {
    PackAnimal maxPlusOne = new Camel("willi" , 21);
    Assert.AreEqual(20 , maxPlusOne.Pace , $"Geschw. {maxPlusOne.Pace} nicht korrekt");

    PackAnimal max = new Camel("willi" , 20);
    Assert.AreEqual(20 , maxPlusOne.Pace , $"Geschw. {max.Pace} nicht korrekt");

    PackAnimal maxMinusOne = new Camel("willi" , 19);
    Assert.AreEqual(19 , maxMinusOne.Pace , $"Geschw. {maxMinusOne.Pace} nicht korrekt");

    PackAnimal minusOne = new Camel("Franzi" , -1);
    Assert.AreEqual(0 , minusOne.Pace , $"Geschw. {minusOne.Pace} nicht korrekt");

    PackAnimal zero = new Camel("Susi" , 0);
    Assert.AreEqual(0 , zero.Pace , $"Geschw. {zero.Pace} nicht korrekt");

    PackAnimal plusOne = new Camel("Franzi" , 1);
    Assert.AreEqual(1 , plusOne.Pace , $"Geschw. {plusOne.Pace} nicht korrekt");
  }
}
