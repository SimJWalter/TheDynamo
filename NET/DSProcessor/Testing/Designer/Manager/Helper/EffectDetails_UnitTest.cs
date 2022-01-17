using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Manager.Helper;

namespace Testing.Designer.Manager.Helper
{
  [TestFixture()]
  public class EffectDetails_UnitTest
  {
    private EffectDetails ed;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      ed = new EffectDetails("test", 0);
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
      ed = null;
    }
    
    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateObjectNameNull()
    {
      EffectDetails ed2 = new EffectDetails(null, 0);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateObjectNameBlank()
    {
      EffectDetails ed2 = new EffectDetails("", 0);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateObjectIndexInvalid()
    {
      EffectDetails ed2 = new EffectDetails("test", -1);
    }

    [Test()]
    public void CreateObjectValid()
    {
      EffectDetails ed2 = new EffectDetails("test", 0);
      Assert.That(ed2 != null);
    }

    [Test()]
    public void NameAccessorIsReadOnly()
    {
      string tmp = ed.Name;
      int len = ed.Name.Length;
      tmp += "test";
      Assert.That(ed.Name.Length == len);
    }

    [Test()]
    public void NameAccessorValid()
    {
      Assert.That(ed.Name == "test");
    }

    [Test()]
    public void IndexAccessorValid()
    {
      Assert.That(ed.Index == 0);
    }

    [Test()]
    public void EqualsTrue()
    {
      EffectDetails ed2 = new EffectDetails("test", 0);
      Assert.That(ed == ed2);
    }

    [Test()]
    public void EqualsFalse()
    {
      EffectDetails ed2 = new EffectDetails("test", 1);
      Assert.That(ed != ed2);
      ed2 = new EffectDetails("t", 0);
      Assert.That(ed != ed2);
      ed2 = new EffectDetails("t", 1);
      Assert.That(ed != ed2);
    }

    [Test()]
    public void ToStringIsNameOnly()
    {
      Assert.That(ed.ToString() == ed.Name);
    }
  }
}
