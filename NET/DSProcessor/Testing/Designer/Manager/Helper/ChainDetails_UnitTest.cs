using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Manager.Helper;

namespace Testing.Designer.Manager.Helper
{
  [TestFixture()]
  public class ChainDetails_UnitTest
  {
    private ChainDetails cd;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      cd = new ChainDetails("test", "test.zip");
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
      cd = null;
    }

    [Test()]
    public void CreateObjectNameBlank()
    {
      ChainDetails cd2 = new ChainDetails("", "test");
      Assert.That(cd2 != null);
    }

    [Test()]
    public void CreateObjectNameNull()
    {
      ChainDetails cd2 = new ChainDetails(null, "test");
      Assert.That(cd2 != null);
    }

    [Test()]
    public void CreateObjectFilenameBlank()
    {
      ChainDetails cd2 = new ChainDetails("test", "");
      Assert.That(cd2 != null);
    }

    [Test()]
    public void CreateObjectFilenameNull()
    {
      ChainDetails cd2 = new ChainDetails("test", null);
      Assert.That(cd2 != null);
    }

    [Test()]
    public void CreatObjectIsValid()
    {
      ChainDetails cd2 = new ChainDetails("test", "test");
      Assert.That(cd2.FileName == "test");
      Assert.That(cd2.Name == "test");
      Assert.That(cd2.Effects != null);
      Assert.That(cd2.Effects.Count == 0);
      Assert.That(!cd2.Unsaved);
    }

    [Test()]
    public void NameAccessorIsReadOnly()
    {
      string name = cd.Name;
      int len = cd.Name.Length;
      name += "test";
      Assert.That(cd.Name.Length == len);
      Assert.That(!object.ReferenceEquals(name, cd.Name));
      Assert.That(name != cd.Name);
    }

    [Test()]
    public void NameAccessorValid()
    {
      Assert.That(cd.Name == null);
    }

    [Test()]
    public void IndexAccessorValid()
    {
      Assert.That(cd.Index == 0);
    }

    [Test()]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void IndexMutatorInValid()
    {
      cd.Index = -10;
    }

    [Test()]
    public void IndexMutatorValid()
    {
      cd.Index = 10;
      Assert.That(cd.Index == 10);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void AppendEffectNullName()
    {
      cd.AppendEffect(null);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void AppendEffectBlankName()
    {
      cd.AppendEffect("");
    }

    [Test()]
    public void AppendEffectValid()
    {
      cd.AppendEffect("test");
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void AppendEffectsNoEntries()
    {
      cd.AppendEffects(new List<EffectDetails>());
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void AppendEffectsNullList()
    {
      cd.AppendEffects(null);
    }

    // uncodeable due to the restrictions in place in the effectDetails class
    //[Test()]
    //[ExpectedException(typeof(ArgumentException))]
    //public void AppendEffectsContainsInvalidEntries()
    //{
    //}

    [Test()]
    public void AppendEffectsValid()
    {
      List<EffectDetails> led = new List<EffectDetails>();
      for (int i = 0; i < 10; i++)
      {
        led.Add(new EffectDetails("effect_" + i, i));
      }
      cd.AppendEffects(led);
    }

    [Test()]
    public void SetUnsavedValid()
    {
      Assert.That(!cd.Unsaved);
      cd.SetUnsaved();
      Assert.That(cd.Unsaved);
    }
  }
}
