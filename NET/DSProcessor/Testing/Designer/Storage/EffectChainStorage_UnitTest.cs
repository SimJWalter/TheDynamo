using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Designer.Storage;

namespace Testing.Designer.Storage
{
  [TestFixture()]
  public class EffectChainStorage_UnitTest
  {
    [SetUp()]
    public void Init()
    {
      ASCIIEncoding encoding = new ASCIIEncoding();
      string file1 = global::Testing.Properties.Resources.TestChain1;
      string file2 = global::Testing.Properties.Resources.TestChain2;
      File.WriteAllBytes(FileStorageConstants.CustomChainsLocation + "\\TestChain1.xml", encoding.GetBytes(file1));
      File.WriteAllBytes(FileStorageConstants.CustomChainsLocation + "\\TestChain2.xml", encoding.GetBytes(file2));
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
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
    public void FetchChainsNonePresent()
    { 
      
    }

    [Test()]
    public void FetchChainsMultiple_WithDuplicate()
    { }

    [Test()]
    public void FetchChainsMultipleValid()
    { }

    [Test()]
    public void SaveChainNameNullOrBlank()
    { }

    [Test()]
    public void SaveChainFilenameNullOrBlank()
    { }

    [Test()]
    public void SaveChainValid()
    { }

    [Test()]
    public void RetrieveChainNullDetails()
    { }

    [Test()]
    public void RetrieveChainNotAvailable()
    { }

    [Test()]
    public void RetrieveChainValid()
    { }
  }
}
