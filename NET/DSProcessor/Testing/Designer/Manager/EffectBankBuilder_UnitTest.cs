using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Manager;
using System.IO;
using Designer.Storage;
using Designer.Manager.Helper;

namespace Testing.Designer.Manager.Helper
{
  [TestFixture()]
  public class EffectBankBuilder_UnitTest
  {
    private EffectBankBuilder ebb;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      ASCIIEncoding encoding = new ASCIIEncoding();
      string file1 = global::Testing.Properties.Resources.TestChain1;
      string file2 = global::Testing.Properties.Resources.TestChain2;
      File.WriteAllBytes(FileStorageConstants.CustomChainsLocation + "\\TestChain1.xml", encoding.GetBytes(file1));
      File.WriteAllBytes(FileStorageConstants.CustomChainsLocation + "\\TestChain2.xml", encoding.GetBytes(file2));

      file1 = global::Testing.Properties.Resources.TestBank1;
      file2 = global::Testing.Properties.Resources.TestBank2;
      File.WriteAllBytes(FileStorageConstants.ChainBanksLocation + "\\TestBank1.xml", encoding.GetBytes(file1));
      File.WriteAllBytes(FileStorageConstants.ChainBanksLocation + "\\TestBank2.xml", encoding.GetBytes(file2));

      ebb = new EffectBankBuilder();
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
      ebb = null;

      string[] chainFiles = Directory.GetFiles(FileStorageConstants.CustomChainsLocation);
      string[] bankFiles = Directory.GetFiles(FileStorageConstants.ChainBanksLocation);

      string[] files = new string[chainFiles.Length + bankFiles.Length];
      chainFiles.CopyTo(files, 0);
      bankFiles.CopyTo(files, chainFiles.Length);

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
      Assert.That(ebb.CurrentEditDetails == null);
      ebb.CreateNew();
      Assert.That(ebb.CurrentEditDetails != null);
      Assert.That(ebb.UnsavedChanges);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectBankBuilder.BankMgtException))]
    public void AppendChainNotAvailable()
    {
      BankPrototypeManager bpm = new BankPrototypeManager();
      Assert.That(bpm.DescribeBanks().Count > 0);
      ebb.LoadBank(bpm.DescribeBanks()[0]);

      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);

      ChainDetails cd = cpm.DescribeChains()[0];

      foreach (string file in Directory.GetFiles(FileStorageConstants.CustomChainsLocation))
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
      ebb.AddChain(cd, cpm);
    }

    [Test()]
    public void AppendChainValid()
    {
      BankPrototypeManager bpm = new BankPrototypeManager();
      Assert.That(bpm.DescribeBanks().Count > 0);
      ebb.LoadBank(bpm.DescribeBanks()[0]);
      Assert.That(ebb.CurrentHasFile && !ebb.UnsavedChanges);

      ChainPrototypeManager cpm = new ChainPrototypeManager();
      Assert.That(cpm.DescribeChains().Count > 0);

      ChainDetails cd = cpm.DescribeChains()[0];
      int numHeld = ebb.CurrentEditDetails.Chains.Count;
      ebb.AddChain(cd, cpm);

      Assert.That(ebb.CurrentEditDetails.Chains.Count == (numHeld + 1));
      Assert.That(ebb.UnsavedChanges);
    }

    [Test()]
    public void IncrementIndexValid()
    {
      BankPrototypeManager bpm = new BankPrototypeManager();
      Assert.That(bpm.DescribeBanks().Count > 0);
      ebb.LoadBank(bpm.DescribeBanks()[0]);
      Assert.That(ebb.CurrentHasFile && !ebb.UnsavedChanges);

      ChainPrototypeManager cpm = new ChainPrototypeManager();
      ChainDetails cd1 = cpm.DescribeChains()[0];
      ChainDetails cd2 = cpm.DescribeChains()[1];
      int startIndex = ebb.CurrentEditDetails.Chains.Count - 1;

      ebb.AddChain(cd1, cpm);
      ebb.AddChain(cd2, cpm);
      Assert.That(ebb.CurrentEditDetails.Chains[startIndex].Name == cd1.Name && ebb.CurrentEditDetails.Chains[startIndex + 1].Name == cd2.Name);
      ebb.IncrementIndex(ebb.CurrentEditDetails.Chains[startIndex + 1]);
      Assert.That(ebb.CurrentEditDetails.Chains[startIndex].Name == cd2.Name && ebb.CurrentEditDetails.Chains[startIndex + 1].Name == cd1.Name);
      Assert.That(ebb.UnsavedChanges);
    }

    [Test()]
    public void DecrementIndexValid()
    {
      BankPrototypeManager bpm = new BankPrototypeManager();
      Assert.That(bpm.DescribeBanks().Count > 0);
      ebb.LoadBank(bpm.DescribeBanks()[0]);
      Assert.That(ebb.CurrentHasFile && !ebb.UnsavedChanges);

      ChainPrototypeManager cpm = new ChainPrototypeManager();
      ChainDetails cd1 = cpm.DescribeChains()[0];
      ChainDetails cd2 = cpm.DescribeChains()[1];
      int startIndex = ebb.CurrentEditDetails.Chains.Count - 1;

      ebb.AddChain(cd1, cpm);
      ebb.AddChain(cd2, cpm);
      Assert.That(ebb.CurrentEditDetails.Chains[startIndex].Name == cd1.Name && ebb.CurrentEditDetails.Chains[startIndex + 1].Name == cd2.Name);
      ebb.DecrementIndex(ebb.CurrentEditDetails.Chains[startIndex]);
      Assert.That(ebb.CurrentEditDetails.Chains[startIndex].Name == cd2.Name && ebb.CurrentEditDetails.Chains[startIndex + 1].Name == cd1.Name);
      Assert.That(ebb.UnsavedChanges);
    }

    [Test()]
    public void RemoveChainValid()
    {
      BankPrototypeManager bpm = new BankPrototypeManager();
      Assert.That(bpm.DescribeBanks().Count > 0);
      ebb.LoadBank(bpm.DescribeBanks()[0]);
      Assert.That(ebb.CurrentHasFile && !ebb.UnsavedChanges);

      ChainPrototypeManager cpm = new ChainPrototypeManager();
      ChainDetails cd1 = cpm.DescribeChains()[0];
      ChainDetails cd2 = cpm.DescribeChains()[1];
      int startIndex = ebb.CurrentEditDetails.Chains.Count - 1;
      ebb.AddChain(cd1, cpm);
      ebb.AddChain(cd2, cpm);
      Assert.That(ebb.CurrentEditDetails.Chains[startIndex].Name == cd1.Name && ebb.CurrentEditDetails.Chains[startIndex + 1].Name == cd2.Name);
      ebb.RemoveFromBank(ebb.CurrentEditDetails.Chains[startIndex]);
      Assert.That(ebb.CurrentEditDetails.Chains[startIndex].Name == cd2.Name);
      Assert.That(ebb.UnsavedChanges);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectBankBuilder.BankPersistenceException))]
    public void SaveCurrentHasNoFilenameOrName()
    {
      ebb.CreateNew();
      ebb.SaveCurrent();
    }

    [Test()]
    public void SaveCurrentValid()
    {
      BankPrototypeManager bpm = new BankPrototypeManager();
      Assert.That(bpm.DescribeBanks().Count > 0);
      ebb.LoadBank(bpm.DescribeBanks()[0]);
      Assert.That(ebb.CurrentHasFile && !ebb.UnsavedChanges);

      ChainPrototypeManager cpm = new ChainPrototypeManager();
      ChainDetails cd1 = cpm.DescribeChains()[0];
      ebb.AddChain(cd1, cpm);

      Assert.That(ebb.UnsavedChanges);
      ebb.SaveCurrent();
      Assert.That(!ebb.UnsavedChanges);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectBankBuilder.BankPersistenceException))]
    public void SaveCurrentWithSpecifiedNameNull()
    {
      ebb.CreateNew();
      ebb.SaveCurrentAs(null);
    }

    [Test()]
    [ExpectedException(typeof(global::Designer.Manager.EffectBankBuilder.BankPersistenceException))]
    public void SaveCurrentWithSpecifiedNameBlank()
    {
      ebb.CreateNew();
      ebb.SaveCurrentAs("");
    }

    [Test()]
    public void SaveCurrentWithSpecifiedNameValid()
    {
      ebb.CreateNew();
      Assert.That(ebb.UnsavedChanges);
      ebb.SaveCurrentAs("TestBank");
      Assert.That(!ebb.UnsavedChanges);
    }

    [Test()]
    public void LoadBankInvalid()
    {
      ebb.LoadBank(new BankDetails("test", "test.xml"));
    }

    [Test()]
    public void LoadBankValid()
    {
      BankPrototypeManager bpm = new BankPrototypeManager();
      Assert.That(bpm.DescribeBanks().Count > 0);
      ebb.LoadBank(bpm.DescribeBanks()[0]);
      Assert.That(ebb.CurrentHasFile && !ebb.UnsavedChanges && ebb.CurrentEditDetails != null);
    }
  }
}
