using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Entity;
using System.Collections;

namespace Testing.Designer.Entity
{
  [TestFixture()]
  public class EffectConfiguration_UnitTest
  {
    [SetUp()]
    public void Init()
    {
      //common refresh code here
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateEffectConfigNullName()
    {
      string name = null;
      string file = "test.zip";
      EffectConfiguration ec = new EffectConfiguration(name, file);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateEffectConfigBlankName()
    {
      string name = "";
      string file = "test.zip";
      EffectConfiguration ec = new EffectConfiguration(name, file);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateConfigBlankFilename()
    {
      string name = "test";
      string file = "";
      EffectConfiguration ec = new EffectConfiguration(name, file);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateConfigNullFilename()
    {
      string name = "test";
      string file = null;
      EffectConfiguration ec = new EffectConfiguration(name, file);
    }

    [Test()]
    public void NameFieldIsReadOnly()
    {
      string name = "test";
      string file = "test.zip";
      EffectConfiguration ec = new EffectConfiguration(name, file);
      string retrieved = ec.Name;
      retrieved += "changed";
      Assert.That(ec.Name == name);
    }

    [Test()]
    public void FilenameFieldIsReadOnly()
    {
      string name = "test";
      string file = "test.zip";
      EffectConfiguration ec = new EffectConfiguration(name, file);
      string retrieved = ec.Filename;
      retrieved += "changed";
      Assert.That(ec.Filename == file);
    }

    [Test()]
    [ExpectedException(typeof(Exception))]
    public void AddParameterExists()
    {
      string name = "name";
      string filename = "filename.zip";
      ConfigParameter cp1 = new ConfigParameter("cp1", "cp1");
      ConfigParameter cp2 = new ConfigParameter("cp2", "cp2");
      EffectConfiguration ec = new EffectConfiguration(name, filename);
      ec.AddParameter(cp1);
      ec.AddParameter(cp1);
    }

    [Test()]
    public void AddParameterDoesNotExist()
    {
      ConfigParameter cp1 = new ConfigParameter("cp1", "cp1");
      ConfigParameter cp2 = new ConfigParameter("cp2", "cp2");      
      string name = "name";
      string filename = "filename.zip";
      EffectConfiguration ec = new EffectConfiguration(name, filename);
      ec.AddParameter(cp1);
      ec.AddParameter(cp2);
      Assert.That(ec.Parameters[0].Name == cp1.Name);
      Assert.That(ec.Parameters[0].Value == cp1.Value);
      Assert.That(ec.Parameters[1].Name == cp2.Name);
      Assert.That(ec.Parameters[1].Value == cp2.Value);
    }

    [Test()]
    public void UpdateParamsAllAreRecognised()
    {
      ConfigParameter cp1 = new ConfigParameter("cp1", "cp1");
      ConfigParameter cp2 = new ConfigParameter("cp2", "cp2");
      string name = "name";
      string filename = "filename.zip";
      EffectConfiguration ec = new EffectConfiguration(name, filename);
      ec.AddParameter(cp1);
      ec.AddParameter(cp2);
      
      List<ConfigParameter> pms = new List<ConfigParameter>();
      foreach (ConfigParameter cp in ec.Parameters)
      {
        pms.Add(cp);
      }
      pms[0] = new ConfigParameter(pms[0].Name, "test");
      pms[1] = new ConfigParameter(pms[1].Name, "test23");

      ec.UpdateParameters(pms.ToArray());
      Assert.That(ec.Parameters[0].Name == cp1.Name && ec.Parameters[0].Value == "test");
      Assert.That(ec.Parameters[1].Name == cp2.Name && ec.Parameters[1].Value == "test2");
    }

    [Test()]
    public void UpdateParamsSomeNotRecognised()
    {
      ConfigParameter cp1 = new ConfigParameter("cp1", "cp1");
      ConfigParameter cp2 = new ConfigParameter("cp2", "cp2");
      string name = "name";
      string filename = "filename.zip";
      EffectConfiguration ec = new EffectConfiguration(name, filename);
      ec.AddParameter(cp1);
      ec.AddParameter(cp2);

      List<ConfigParameter> pms = new List<ConfigParameter>();
      foreach (ConfigParameter cp in ec.Parameters)
      {
        pms.Add(cp);
      }
      pms[0] = new ConfigParameter(pms[0].Name + "_", "test");
      pms[1] = new ConfigParameter(pms[1].Name, "test2");
      ec.UpdateParameters(pms.ToArray());
    }

    [Test()]
    public void CreatesTrueClone()
    {
      ConfigParameter cp1 = new ConfigParameter("cp1", "cp1");
      ConfigParameter cp2 = new ConfigParameter("cp2", "cp2");
      string name = "name";
      string filename = "filename.zip";
      EffectConfiguration ec = new EffectConfiguration(name, filename);
      ec.AddParameter(cp1);
      ec.AddParameter(cp2);

      EffectConfiguration ecClone = ec.Clone();
      Assert.That(!Object.ReferenceEquals(ecClone, ec));
      Assert.That(!Object.ReferenceEquals(ecClone.Filename, ec.Filename));
      Assert.That(!Object.ReferenceEquals(ecClone.Name, ec.Name));
      Assert.That(ecClone.Parameters.Count == ec.Parameters.Count);
      for (int i = 0; i < ec.Parameters.Count; i++)
      {
        Assert.That(!Object.ReferenceEquals(ecClone.Parameters[i], ec.Parameters[i]));
      }
    }
  }
}
