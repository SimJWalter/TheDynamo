using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Designer.Storage;
using Designer.Entity;

namespace Testing.Designer.Storage
{
  [TestFixture()]
  public class EffectPrototypeStorage_UnitTest
  {
    private EffectPrototypeStorage eps;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      File.WriteAllBytes(FileStorageConstants.PrototypesLocation + "\\vibrate.zip", global::Testing.Properties.Resources.vibrate);
      File.WriteAllBytes(FileStorageConstants.PrototypesLocation + "\\vibrate2.zip", global::Testing.Properties.Resources.vibrate2);
      eps = new EffectPrototypeStorage();
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
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
    }

    [Test()]
    public void FetchConfigurationsNonePresent()
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
      List<EffectConfiguration> list = eps.FetchConfigurations();
      Assert.That(list != null && list.Count == 0);
    }

    [Test()]
    [ExpectedException(typeof(PrototypeStorageException))]
    public void FetchConfigurationsMultiple_WithDuplicate()
    {
      File.WriteAllBytes(FileStorageConstants.PrototypesLocation + "\\vibrate.zip", global::Testing.Properties.Resources.vibrate);
      List<EffectConfiguration> list = eps.FetchConfigurations();
    }

    [Test()]
    public void FetchConfigurationsMultipleValid()
    {
      int numAvailable = Directory.GetFiles(FileStorageConstants.PrototypesLocation).Length;
      List<EffectConfiguration> list = eps.FetchConfigurations();
      Assert.That(list.Count == numAvailable);
    }
  }
}
