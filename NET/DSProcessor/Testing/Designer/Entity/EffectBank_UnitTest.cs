using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Entity;
using Designer.Manager.Helper;

namespace Testing.Designer.Entity
{
  [TestFixture()]
  public class EffectBank_UnitTest
  {
    private EffectBank eb;

    [SetUp()]
    public void Init()
    {
      eb = new EffectBank();
    }
    
    [TearDown()]
    public void DisposeT()
    { 
      //release resources
    }

    [Test()]
    public void DefaultValuesOnCreation()
    {
      Assert.That(eb.Chains.Count == 0);
      Assert.That(eb.Filename == null);
      Assert.That(eb.Name == null);
      Assert.That(eb.IsAltered == true);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NameCannotBeSetToNull()
    {
      eb.Name = null; 
    }

    [Test()]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NameCannotBeSetToBlank()
    {
      eb.Name = "";
    }

    [Test()]
    public void FilenameCannotBeMutatedViaAccessor()
    {
      eb.Filename = "filename";
      string fname = eb.Filename;
      fname += "altered!";
      Assert.That(eb.Filename.Length == 8);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentNullException))]
    public void FilenameCannotBeSetToBlank()
    {
      eb.Filename = "";
    }

    [Test()]
    public void IsAlteredOnNameChange_DiffValue()
    {
      string name = "name";
      string name2 = "name2";
      eb.Name = name;
      eb.SetUnaltered();
      eb.Name = name2;
      Assert.That(eb.IsAltered == true);
    }

    [Test()]
    public void IsAlteredOnNameChange_SameValue()
    {
      string name = "name";
      eb.Name = name;
      eb.SetUnaltered();
      eb.Name = name.Substring(0, name.Length);
      Assert.That(eb.IsAltered == false);
    }

    [Test()]
    public void IsAlteredOnFileNameChange_DiffValue()
    {
      string filename = "filename";
      string filename2 = "filename2";
      eb.Filename = filename;
      eb.SetUnaltered();
      eb.Filename = filename2;
      Assert.That(eb.IsAltered == true);
    }

    [Test()]
    public void IsAlteredOnFileNameChange_SameValue()
    {
      string filename = "filename";
      eb.Filename = filename;
      eb.SetUnaltered();
      eb.Filename = filename.Substring(0, filename.Length);
      Assert.That(eb.IsAltered == false);
    }

    [Test()]
    public void SetUnalteredValid()
    {
      Assert.That(eb.IsAltered);
      eb.SetUnaltered();
      Assert.That(!eb.IsAltered);
    }

    [Test()]
    public void AppendChainValid() // (isaltered = true also)
    {
      eb.SetUnaltered();
      for (int i = 0; i < 10; i++)
      {
        eb.AppendChain(new EffectChain());
      }
      Assert.That(eb.Chains.Count == 10);
      Assert.That(eb.IsAltered);
    }

    [Test()]
    public void AppendChainInvalid()
    {
      eb.SetUnaltered();
      for (int i = 0; i < 10; i++)
      {
        eb.AppendChain(null);
      }
      Assert.That(eb.Chains.Count == 0);
      Assert.That(!eb.IsAltered);      
    }

    [Test()]
    public void ShiftUpNullDetails() //see isaltered
    {
      for (int i = 0; i < 10; i++)
      { 
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      eb.ShiftUp(null);
      Assert.That(!eb.IsAltered);
    }

    [Test()]
    public void ShiftUpIndexZero()
    {
      for (int i = 0; i < 10; i++)
      {
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      eb.ShiftUp(eb.Chains[0].Describe());
      Assert.That(!eb.IsAltered);
    }

    [Test()]
    public void ShiftUpValid()
    {
      for (int i = 0; i < 10; i++)
      {
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      BankDetails ebDesc = eb.Describe();
      eb.ShiftUp(ebDesc.Chains[5]);
      Assert.That(eb.IsAltered);
      Assert.That(ebDesc.Chains[5].Name == eb.Chains[4].Name);
      Assert.That(ebDesc.Chains[4].Name == eb.Chains[5].Name);
    }

    [Test()]
    public void ShiftDownNullDetails() //see isaltered
    {
      for (int i = 0; i < 10; i++)
      {
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      eb.ShiftDown(null);
      Assert.That(!eb.IsAltered);
    }

    [Test()]
    public void ShiftDownIndexMax()
    {
      for (int i = 0; i < 10; i++)
      {
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      eb.ShiftDown(eb.Chains[eb.Chains.Count - 1].Describe());
      Assert.That(!eb.IsAltered);
    }

    [Test()]
    public void ShiftDownValid()
    {
      for (int i = 0; i < 10; i++)
      {
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      BankDetails ebDesc = eb.Describe();
      eb.ShiftDown(ebDesc.Chains[5]);
      Assert.That(eb.IsAltered);
      Assert.That(ebDesc.Chains[5].Name == eb.Chains[6].Name);
      Assert.That(ebDesc.Chains[6].Name == eb.Chains[5].Name);
    }

    [Test()]
    public void RemoveChainValid()
    {
      for (int i = 0; i < 10; i++)
      {
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      BankDetails ebDesc = eb.Describe();
      eb.Remove(ebDesc.Chains[5]);
      Assert.That(eb.IsAltered);
      Assert.That(eb.Chains.Count == (ebDesc.Chains.Count - 1));
      Assert.That(eb.Chains[eb.Chains.Count - 1].Name == ebDesc.Chains[ebDesc.Chains.Count - 1].Name);
    }

    [Test()]
    public void RemoveChainInvalid()
    {
      for (int i = 0; i < 10; i++)
      {
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      BankDetails ebDesc = eb.Describe();
      ChainDetails cd = ebDesc.Chains[5];
      ChainDetails cdNew = new ChainDetails(cd.Name, cd.FileName);
      cdNew.Index = 4;
      eb.Remove(cdNew);
      Assert.That(!eb.IsAltered);
    }

    [Test()]
    public void IsTrueClone()
    {
      eb.Filename = "filename";
      eb.Name = "name";
      for (int i = 0; i < 10; i++)
      {
        EffectChain ec = new EffectChain();
        ec.Filename = "test_" + i + ".zip";
        ec.Name = "test_" + i;
        eb.AppendChain(ec);
      }
      eb.SetUnaltered();
      EffectBank ebClone = eb.Clone();
      Assert.That(!object.ReferenceEquals(eb, ebClone));
      Assert.That(!object.ReferenceEquals(eb.Chains, ebClone.Chains));
      Assert.That(!object.ReferenceEquals(eb.Filename, ebClone.Filename));
      Assert.That(!object.ReferenceEquals(eb.Name, ebClone.Name));
      Assert.That(eb.Name == ebClone.Name);
      Assert.That(eb.Filename == ebClone.Filename);
      Assert.That(eb.IsAltered == ebClone.IsAltered);
      Assert.That(eb.Chains.Count == ebClone.Chains.Count);
      for (int i = 0; i < eb.Chains.Count; i++)
      {
        Assert.That(!object.ReferenceEquals(eb.Chains[i], ebClone.Chains[i]));
      }
    }
  }
}
