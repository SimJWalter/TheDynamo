using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Manager.Helper;

namespace Testing.Designer.Manager.Helper
{
  [TestFixture()]
  public class BankDetails_UnitTest
  {
    private BankDetails bd;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      bd = new BankDetails("test", "test");
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
      bd = null;
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateObjectNameBlank()
    {
      bd = new BankDetails("", "test");
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateObjectNameNull()
    {
      bd = new BankDetails(null, "test");
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateObjectFilenameBlank()
    {
      bd = new BankDetails("test", "");
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateObjectFilenameNull()
    {
      bd = new BankDetails("test", null);
    }

    [Test()]
    public void CreatObjectValid()
    {
      bd = new BankDetails("test", "test");
      Assert.That(bd.Name == "test");
      Assert.That(bd.FileName == "test");
      Assert.That(!bd.Unsaved);
      Assert.That(bd.Chains != null);
      Assert.That(bd.Chains.Count == 0);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AppendChainNull()
    {
      bd.AppendChain(null);
    }

    [Test()]
    public void AppendChainValid()
    {
      ChainDetails cd = new ChainDetails("test", "test");
      bd.AppendChain(cd);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void AppendChainsNull()
    {
      bd.AppendChains(null);
    }

    [Test()]
    public void AppendChainsValid()
    {
      List<ChainDetails> cdl = new List<ChainDetails>();
      for (int i = 0; i < 10; i++)
      {
        cdl.Add(new ChainDetails("test_" + i, "test_" + i + ".xyz"));
      }
      bd.AppendChains(cdl);
    }

    [Test()]
    public void SetUnsavedValid()
    {
      Assert.That(!bd.Unsaved);
      bd.SetUnsaved();
      Assert.That(bd.Unsaved);
    }

    [Test()]
    public void NameIsReadOnly()
    {
      string name = bd.Name;
      Assert.That(!object.ReferenceEquals(name, bd.Name));
      name += "test";
      Assert.That(bd.Name != name);
    }

    [Test()]
    public void FilenameIsReadOnly()
    {
      string filename = bd.FileName;
      Assert.That(!object.ReferenceEquals(filename, bd.FileName));
      filename += "test";
      Assert.That(bd.FileName != filename);
    }
  }
}
