using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Manager;
using Designer.Storage;
using System.IO;
using Designer.Manager.Helper;
using Designer.Entity;

namespace Testing.Designer.Manager
{
  [TestFixture()]
  public class BankPrototypeManager_UnitTest
  {
    private BankPrototypeManager bpm;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      bpm = new BankPrototypeManager();
      ASCIIEncoding encoding = new ASCIIEncoding();
      string file1 = global::Testing.Properties.Resources.TestBank1;
      string file2 = global::Testing.Properties.Resources.TestBank2;
      File.WriteAllBytes(FileStorageConstants.ChainBanksLocation + "\\TestBank1.xml", encoding.GetBytes(file1));
      File.WriteAllBytes(FileStorageConstants.ChainBanksLocation + "\\TestBank2.xml", encoding.GetBytes(file2));
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
      bpm = null;
      foreach (string file in Directory.GetFiles(FileStorageConstants.ChainBanksLocation))
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
    public void DescribePrototypesNoneAvailable()
    {
      foreach (string file in Directory.GetFiles(FileStorageConstants.ChainBanksLocation))
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
      Assert.That(bpm.DescribeBanks().Count == 0);
    }

    [Test()]
    public void DescribePrototypesMultiplePresent()
    {
      int numAvail = Directory.GetFiles(FileStorageConstants.ChainBanksLocation).Length;
      Assert.That(bpm.DescribeBanks().Count == numAvail);      
    }

    [Test()]
    public void GetBankNullParam()
    {
      Assert.That(bpm.GetBank(null) == null);
    }

    [Test()]
    public void GetBankValid()
    {
      List<BankDetails> list = bpm.DescribeBanks();
      Assert.That(list.Count > 0);
      EffectBank eb = bpm.GetBank(list[0]);
      Assert.That(eb.Name == list[0].Name && eb.Filename == list[0].FileName);
    }
  }
}
