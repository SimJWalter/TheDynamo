using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Entity;

namespace Testing.Designer.Entity
{
  [TestFixture()]
  public class ConfigParameter_UnitTest
  {
    [SetUp()]
    public void Init()
    {
      //common refresh code here
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateConfigParamBlankName()
    {
      ConfigParameter cp = new ConfigParameter("", "test");
    }
    
    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateConfigParamNullName()
    {
      ConfigParameter cp = new ConfigParameter(null, "test");
    }

    [Test()]
    public void CreateConfigParamNullValue_BlankValue()
    {
      ConfigParameter cp = new ConfigParameter("test", null);
      Assert.That(cp.Value == "0");
      cp = new ConfigParameter("test", "");
      Assert.That(cp.Value == "0");
    }

    [Test()]
    public void NameIsCorrect()
    {
      string name = "NAME";
      string value = "VALUE";
      ConfigParameter cp = new ConfigParameter(name, value);
      Assert.That(cp.Name == name);
    }

    [Test()]
    public void NameIsReadOnly()
    {
      string name = "NAME";
      string value = "VALUE";
      ConfigParameter cp = new ConfigParameter(name, value);
      name = cp.Name;
      name = "gone!";
      Assert.That(cp.Name == "NAME");
    }

    [Test()]
    public void ValueIsCorrect()
    {
      string name = "NAME";
      string value = "VALUE";
      ConfigParameter cp = new ConfigParameter(name, value);
      Assert.That(cp.Value == value);
    }

    [Test()]
    public void ValueIsReadonly()
    {
      string name = "NAME";
      string value = "VALUE";
      ConfigParameter cp = new ConfigParameter(name, value);
      value = cp.Value;
      value = "gone!";
      Assert.That(cp.Value == "VALUE");
    }

    [Test()]
    public void EqualsTrue()
    {
      string name = "NAME";
      string valueA = "VALUEA";
      string valueB = "VALUEB";
      ConfigParameter cpA = new ConfigParameter(name, valueA);
      ConfigParameter cpB = new ConfigParameter(name, valueB);
      Assert.That(cpA == cpB);
    }

    [Test()]
    public void EqualsFalse()
    {
      string nameA = "NAMEA";
      string nameB = "NAMEB";
      string value = "VALUE";
      
      ConfigParameter cpA = new ConfigParameter(nameA, value);
      ConfigParameter cpB = new ConfigParameter(nameB, value);
      Assert.That(cpA != cpB);
    }

    [Test()]
    public void CreatesTrueClone()
    {
      string name = "name";
      string value = "value";
      ConfigParameter cpA = new ConfigParameter(name, value);
      ConfigParameter cpB = cpA.Clone();
      Assert.That(cpA.Name == cpB.Name);
      Assert.That(cpA.Value == cpB.Value);
      Assert.That(!Object.ReferenceEquals(cpA, cpB));
    }
  }
}
