using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Manager;
using System.IO;
using Designer.Storage;
using Designer.Manager.Helper;
using Designer.Entity;

namespace Testing.Designer.Manager
{
  [TestFixture()]
  public class ChainPrototypeManager_UnitTest
  {
    private ChainPrototypeManager cpm;

    [SetUp()]
    public void Init()
    {
      cpm = new ChainPrototypeManager();
      ASCIIEncoding encoding = new ASCIIEncoding();
      string file1 = global::Testing.Properties.Resources.TestChain1;
      string file2 = global::Testing.Properties.Resources.TestChain2;
      File.WriteAllBytes(FileStorageConstants.CustomChainsLocation + "\\TestChain1.xml", encoding.GetBytes(file1));
      File.WriteAllBytes(FileStorageConstants.CustomChainsLocation + "\\TestChain2.xml", encoding.GetBytes(file2));
    }

    [TearDown()]
    public void DisposeT()
    {
      cpm = null;
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
    }

    [Test()]
    public void DescribePrototypesNoneAvailable()
    {
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
      Assert.That(cpm.DescribeChains().Count == 0);
    }

    [Test()]
    public void DescribePrototypesMultiplePresent()
    {
      int numAvailable = Directory.GetFiles(FileStorageConstants.CustomChainsLocation).Count();
      Assert.That(cpm.DescribeChains().Count == numAvailable);
    }

    [Test()]
    public void GetChainNullParam()
    {
      Assert.That(cpm.GetChain(null) == null);
    }

    [Test()]
    public void GetChainValid()
    {
      List<ChainDetails> list = cpm.DescribeChains();
      Assert.That(list.Count > 0);
      EffectChain chn = cpm.GetChain(list[0]);
      Assert.That(chn.Name == list[0].Name && chn.Filename == list[0].FileName);
    }
  }
}
