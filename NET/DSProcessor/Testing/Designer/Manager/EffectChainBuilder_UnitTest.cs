using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Manager;
using Designer.Storage;
using Designer.Manager.Helper;
using System.IO;
using Designer.Entity;

namespace Testing.Designer.Manager
{
  [TestFixture()]
  public class EffectChainBuilder_UnitTest
  {
    private EffectChainBuilder ecb;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      ASCIIEncoding encoding = new ASCIIEncoding();
      string file1 = global::Testing.Properties.Resources.TestChain1;
      string file2 = global::Testing.Properties.Resources.TestChain2;
      File.WriteAllBytes(FileStorageConstants.CustomChainsLocation + "\\TestChain1.xml", encoding.GetBytes(file1));
      File.WriteAllBytes(FileStorageConstants.CustomChainsLocation + "\\TestChain2.xml", encoding.GetBytes(file2));
 
      File.WriteAllBytes(FileStorageConstants.PrototypesLocation + "\\vibrate.zip", global::Testing.Properties.Resources.vibrate);
      File.WriteAllBytes(FileStorageConstants.PrototypesLocation + "\\vibrate2.zip", global::Testing.Properties.Resources.vibrate2);

      ecb = new EffectChainBuilder();
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
      ecb = null;      
      string[] protoFiles = Directory.GetFiles(FileStorageConstants.PrototypesLocation);
      string[] chainFiles = Directory.GetFiles(FileStorageConstants.CustomChainsLocation);

      string[] files = new string[chainFiles.Length + protoFiles.Length];
      chainFiles.CopyTo(files, 0);
      protoFiles.CopyTo(files, chainFiles.Length);

      foreach (string file in files)
      {
        while (File.Exists(file))
        {
          try
          {
            File.Delete(file);
          }
          catch (Exception e) { }
        }
      }
    }

    [Test()]
    public void CreateNewValid()
    {
      Assert.That(ecb.CurrentEditDetails == null);
      ecb.CreateNew();
      Assert.That(ecb.CurrentEditDetails != null);
      Assert.That(ecb.UnsavedChanges);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectChainBuilder.ChainMgtException))]
    public void AppendEffectNotAvailable()
    {
      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);
      ecb.LoadChain(cpm.DescribeChains()[0]);

      EffectPrototypeManager epm = new EffectPrototypeManager();
      Assert.That(epm.DescribePrototypes().Count > 0);
      
      EffectDetails ed = epm.DescribePrototypes()[0];

      foreach (string file in Directory.GetFiles(FileStorageConstants.PrototypesLocation))
      {
        while (File.Exists(file))
        {
          try
          {
            File.Delete(file);
          }
          catch (Exception e) { }
        }
      }

      ecb.AddEffect(ed, new EffectPrototypeManager());
    }

    [Test()]
    public void AppendEffectValid()
    {
      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);
      ecb.LoadChain(cpm.DescribeChains()[0]);
      Assert.That(ecb.CurrentHasFile && !ecb.UnsavedChanges);

      EffectPrototypeManager epm = new EffectPrototypeManager();
      Assert.That(epm.DescribePrototypes().Count > 0);

      EffectDetails ed = epm.DescribePrototypes()[0];
      int numHeld = ecb.CurrentEditDetails.Effects.Count;
      ecb.AddEffect(ed, epm);
      Assert.That(ecb.CurrentEditDetails.Effects.Count == (numHeld + 1));
      Assert.That(ecb.UnsavedChanges);
    }

    [Test()]
    public void IncrementIndexValid()
    {
      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);
      ecb.LoadChain(cpm.DescribeChains()[0]);
      Assert.That(ecb.CurrentHasFile && !ecb.UnsavedChanges);

      EffectPrototypeManager epm = new EffectPrototypeManager();
      EffectDetails ed1 = epm.DescribePrototypes()[0];
      EffectDetails ed2 = epm.DescribePrototypes()[1];
      int startIndex = ecb.CurrentEditDetails.Effects.Count - 1;

      ecb.AddEffect(ed1, epm);
      ecb.AddEffect(ed2, epm);
      Assert.That(ecb.CurrentEditDetails.Effects[startIndex].Name == ed1.Name && ecb.CurrentEditDetails.Effects[startIndex + 1].Name == ed2.Name);
      ecb.IncrementIndex(ecb.CurrentEditDetails.Effects[startIndex + 1]);
      Assert.That(ecb.CurrentEditDetails.Effects[startIndex].Name == ed2.Name && ecb.CurrentEditDetails.Effects[startIndex + 1].Name == ed1.Name);
      Assert.That(ecb.UnsavedChanges);
    }

    [Test()]
    public void DecrementIndexValid()
    {
      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);
      ecb.LoadChain(cpm.DescribeChains()[0]);
      Assert.That(ecb.CurrentHasFile && !ecb.UnsavedChanges);

      EffectPrototypeManager epm = new EffectPrototypeManager();
      EffectDetails ed1 = epm.DescribePrototypes()[0];
      EffectDetails ed2 = epm.DescribePrototypes()[1];
      int startIndex = ecb.CurrentEditDetails.Effects.Count - 1;

      ecb.AddEffect(ed1, epm);
      ecb.AddEffect(ed2, epm);
      Assert.That(ecb.CurrentEditDetails.Effects[startIndex].Name == ed1.Name && ecb.CurrentEditDetails.Effects[startIndex + 1].Name == ed2.Name);
      ecb.DecrementIndex(ecb.CurrentEditDetails.Effects[startIndex]);
      Assert.That(ecb.CurrentEditDetails.Effects[startIndex].Name == ed2.Name && ecb.CurrentEditDetails.Effects[startIndex + 1].Name == ed1.Name);
      Assert.That(ecb.UnsavedChanges);
    }

    [Test()]
    public void RemoveEffectValid()
    {
      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);
      ecb.LoadChain(cpm.DescribeChains()[0]);
      Assert.That(ecb.CurrentHasFile && !ecb.UnsavedChanges);

      EffectPrototypeManager epm = new EffectPrototypeManager();
      EffectDetails ed1 = epm.DescribePrototypes()[0];
      EffectDetails ed2 = epm.DescribePrototypes()[1];
      int startIndex = ecb.CurrentEditDetails.Effects.Count - 1;
      ecb.AddEffect(ed1, epm);
      ecb.AddEffect(ed2, epm);
      Assert.That(ecb.CurrentEditDetails.Effects[startIndex].Name == ed1.Name && ecb.CurrentEditDetails.Effects[startIndex + 1].Name == ed2.Name);
      ecb.RemoveFromChain(ecb.CurrentEditDetails.Effects[startIndex]);
      Assert.That(ecb.CurrentEditDetails.Effects[startIndex].Name == ed2.Name);
      Assert.That(ecb.UnsavedChanges);
    }

    [Test()]
    public void GetConfigParamsExists()
    {
      ecb.CreateNew();
      EffectPrototypeManager epm = new EffectPrototypeManager();
      EffectDetails ed1 = epm.DescribePrototypes()[0];
      ecb.AddEffect(ed1, epm);
      ConfigParameter[] cps = ecb.GetConfigParams(ecb.CurrentEditDetails.Effects[0]);
      Assert.That(cps != null);
    }

    [Test()]
    public void GetConfigParamsDoesNotExist()
    {
      ecb.CreateNew();
      EffectPrototypeManager epm = new EffectPrototypeManager();
      EffectDetails ed1 = epm.DescribePrototypes()[0];
      ecb.AddEffect(ed1, epm);
      EffectDetails ed = ecb.CurrentEditDetails.Effects[0];
      ecb.RemoveFromChain(ed);
      ConfigParameter[] cps = ecb.GetConfigParams(ed);
      Assert.That(cps == null);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectChainBuilder.ChainPersistenceException))]
    public void SaveCurrentHasNoFilenameOrName()
    {
      ecb.CreateNew();
      ecb.SaveCurrent();
    }

    [Test()]
    public void SaveCurrentValid()
    {
      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);
      ecb.LoadChain(cpm.DescribeChains()[0]);
      Assert.That(ecb.CurrentHasFile && !ecb.UnsavedChanges);

      EffectPrototypeManager epm = new EffectPrototypeManager();
      EffectDetails ed1 = epm.DescribePrototypes()[0];
      ecb.AddEffect(ed1, epm);

      Assert.That(ecb.UnsavedChanges);
      ecb.SaveCurrent();
      Assert.That(!ecb.UnsavedChanges);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectChainBuilder.ChainPersistenceException))]
    public void SaveCurrentWithSpecifiedNameNull()
    {
      ecb.CreateNew();
      ecb.SaveCurrentAs(null);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectChainBuilder.ChainPersistenceException))]
    public void SaveCurrentWithSpecifiedNameBlank()
    {
      ecb.CreateNew();
      ecb.SaveCurrentAs("");
    }

    [Test()]
    public void SaveCurrentWithSpecifiedNameValid()
    {
      ecb.CreateNew();
      Assert.That(ecb.UnsavedChanges);
      ecb.SaveCurrentAs("TestChain");
      Assert.That(!ecb.UnsavedChanges);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectChainBuilder.ChainPersistenceException))]
    public void LoadChainInvalid()
    {
      ecb.LoadChain(new ChainDetails("test", "test.xml"));
    }

    [Test()]
    public void LoadChainValid()
    {
      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);
      ecb.LoadChain(cpm.DescribeChains()[0]);
      Assert.That(ecb.CurrentHasFile && !ecb.UnsavedChanges && ecb.CurrentEditDetails != null);
    }
  }
}
