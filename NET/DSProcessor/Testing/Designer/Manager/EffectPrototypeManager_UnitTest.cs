using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Designer.Manager;
using Designer.Storage;
using Designer.Entity;
using Designer.Manager.Helper;

namespace Testing.Designer.Manager
{
  [TestFixture()]
  public class EffectPrototypeManager_UnitTest
  {
    EffectPrototypeManager epm;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      File.WriteAllBytes(FileStorageConstants.PrototypesLocation + "\\vibrate.zip", global::Testing.Properties.Resources.vibrate);      
      File.WriteAllBytes(FileStorageConstants.PrototypesLocation + "\\vibrate2.zip", global::Testing.Properties.Resources.vibrate2); 
      epm = new EffectPrototypeManager();
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
      epm = null;
      foreach (string file in Directory.GetFiles(FileStorageConstants.PrototypesLocation))
      {
        while (File.Exists(file))
        {
          try
          {
            File.Delete(file);
          }
          catch (Exception e){ }
        }        
      }
    }

    [Test()]
    public void DescribePrototypesNoneAvailable()
    {
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
      epm = new EffectPrototypeManager();
      Assert.That(epm.DescribePrototypes() == null);
    }

    [Test()]
    public void DescribePrototypesMultiplePresent()
    {
      int filecount = Directory.GetFiles(FileStorageConstants.PrototypesLocation).Count();
      Assert.That(epm.DescribePrototypes().Count == filecount);
      for (int i = 0; i < filecount; i++)
      {
        Assert.That(epm.DescribePrototypes()[i] != null);
      }
    }

    [Test()]
    public void GetConfigNullParam()
    {
      EffectConfiguration ec = epm.GetConfig(null);
      Assert.That(ec == null);
    }

    [Test()]
    public void GetConfigValid()
    {
      EffectDetails[] eda = epm.DescribePrototypes().ToArray();
      Assert.That(eda.Count() > 0);
      EffectConfiguration ec = epm.GetConfig(eda[0]);
      Assert.That(ec != null);
    }
  }
}
