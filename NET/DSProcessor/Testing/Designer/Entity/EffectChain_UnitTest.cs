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
  public class EffectChain_UnitTest
  {
    private EffectChain ec;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      ec = new EffectChain();
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
    }

    [Test()]
    public void IsAlteredOnCreation()
    {
      Assert.That(ec.IsAltered);
    }

    [Test()]
    public void NameIsNullOnCreation()
    {
      Assert.That(ec.Name == null);
    }

    [Test()]
    public void FilenameIsNullOnCreation()
    {
      Assert.That(ec.Filename == null);
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void NameCannotBeNull()
    {
      ec.Name = "test";
      ec.Name = null;  
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void NameCannotBeBlank()
    {
      ec.Name = "test";
      ec.Name = "";
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void FileNameCannotBeBlank()
    {
      ec.Filename = "test";
      ec.Filename = "";
    }

    [Test()]
    [ExpectedException(typeof(ArgumentException))]
    public void FileNameCannotBeNull()
    {
      ec.Filename = "test";
      ec.Filename = null;
    }

    [Test()]
    public void NameCannotBeAlteredViaAccessor()
    {
      string name = "test";
      ec.Name = name;
      Assert.That(!Object.ReferenceEquals(name, ec.Name));
    }

    [Test()]
    public void FileNameCannotBeAlteredViaAccessor()
    {
      string filename = "test";
      ec.Filename = filename;
      Assert.That(!Object.ReferenceEquals(filename, ec.Filename));
    }

    [Test()]
    public void IsAlteredOnNameChange_DiffValue()
    {
      ec.Name = "test";
      ec.SetUnaltered();
      Assert.That(!ec.IsAltered);
      ec.Name = "test2";
      Assert.That(ec.IsAltered);
    }

    [Test()]
    public void IsAlteredOnNameChange_SameValue()
    {
      ec.Name = "test";
      ec.SetUnaltered();
      Assert.That(!ec.IsAltered);
      ec.Name = "test";
      Assert.That(!ec.IsAltered);
    }

    [Test()]
    public void IsAlteredOnFileNameChange_DiffValue()
    {
      ec.Filename = "test";
      ec.SetUnaltered();
      Assert.That(!ec.IsAltered);
      ec.Filename = "test2";
      Assert.That(ec.IsAltered);
    }

    [Test()]
    public void IsAlteredOnFileNameChange_SameValue()
    {
      ec.Filename = "test";
      ec.SetUnaltered();
      Assert.That(!ec.IsAltered);
      ec.Filename = "test";
      Assert.That(!ec.IsAltered);
    }
        
    [Test()]
    public void IsTrueClone()
    {
      string name = "test";
      string filename = "testFile";
      ec.Name = name;
      ec.Filename = filename;
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      EffectChain ecClone = ec.Clone();
      Assert.That(!object.ReferenceEquals(ecClone, ec));
      Assert.That(!object.ReferenceEquals(ecClone.Filename, ec.Filename));
      Assert.That(!object.ReferenceEquals(ecClone.Name, ec.Name));
      Assert.That(!object.ReferenceEquals(ecClone.Configurations, ec.Configurations));
      Assert.That(ecClone.Configurations.Count == ec.Configurations.Count);
      for (int i = 0; i < ecClone.Configurations.Count; i++)
      {
        Assert.That(!object.ReferenceEquals(ecClone.Configurations[i], ec.Configurations[i]));
        Assert.That(ecClone.Configurations[i].Name == ec.Configurations[i].Name);
        Assert.That(ecClone.Configurations[i].Filename == ec.Configurations[i].Filename);
      }
    }

    [Test()]
    public void SetUnalteredValid()
    {
      Assert.That(ec.IsAltered);
      ec.SetUnaltered();
      Assert.That(!ec.IsAltered);
    }

    [Test()]
    public void AppendEffectValid() // (isaltered = true also)
    {
      Assert.That(ec.IsAltered);
      ec.SetUnaltered();
      Assert.That(!ec.IsAltered);
      Assert.That(ec.Configurations.Count == 0);
      ec.AppendEffect(new EffectConfiguration("test", "test"));
      Assert.That(ec.Configurations.Count == 1);
      Assert.That(ec.IsAltered);
    }
    
    [Test()]
    public void AppendEffectInvalid()
    {
      Assert.That(ec.Configurations.Count == 0);
      Assert.That(ec.IsAltered);
      ec.SetUnaltered();
      Assert.That(!ec.IsAltered);
      ec.AppendEffect(null);
      Assert.That(!ec.IsAltered);
      Assert.That(ec.Configurations.Count == 0);
    }

    [Test()]
    public void ShiftUpNullDetails() //see isaltered
    {
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();

      ec.ShiftUp(null);

      for (int i = 0; i < cd.Effects.Count; i++)
      {
        Assert.That(ec.Configurations[i].Name == cd.Effects[i].Name);
        Assert.That(i == cd.Effects[i].Index);
      }
      Assert.That(!ec.IsAltered);
    }

    [Test()]
    public void ShiftDownNullDetails() //see isaltered
    {
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();

      ec.ShiftDown(null);

      for (int i = 0; i < cd.Effects.Count; i++)
      {
        Assert.That(ec.Configurations[i].Name == cd.Effects[i].Name);
        Assert.That(i == cd.Effects[i].Index);
      }
      Assert.That(!ec.IsAltered);
    }

    [Test()]
    public void ShiftUpIndexZero()
    {
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();

      ec.ShiftUp(cd.Effects[0]);

      for (int i = 0; i < cd.Effects.Count; i++)
      {
        Assert.That(ec.Configurations[i].Name == cd.Effects[i].Name);
        Assert.That(i == cd.Effects[i].Index);
      }
      Assert.That(!ec.IsAltered);
    }

    [Test()]
    public void ShiftDownIndexMax()
    {
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();

      ec.ShiftDown(cd.Effects[cd.Effects.Count-1]);

      for (int i = 0; i < cd.Effects.Count; i++)
      {
        Assert.That(ec.Configurations[i].Name == cd.Effects[i].Name);
        Assert.That(i == cd.Effects[i].Index);
      }
      Assert.That(!ec.IsAltered);
    }

    [Test()]
    public void ShiftUpValid()
    {
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();

      ec.ShiftUp(cd.Effects[5]);

      Assert.That(cd.Effects[5].Name == ec.Configurations[4].Name);
      Assert.That(cd.Effects[4].Name == ec.Configurations[5].Name);
      Assert.That(ec.IsAltered);
    }

    [Test()]
    public void ShiftDownValid()
    {
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();

      ec.ShiftDown(cd.Effects[5]);

      Assert.That(cd.Effects[5].Name == ec.Configurations[6].Name);
      Assert.That(cd.Effects[6].Name == ec.Configurations[5].Name);
      Assert.That(ec.IsAltered);
    }

    [Test()]
    public void RemoveEffectValid()
    {
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();

      ec.Remove(cd.Effects[5]);
      Assert.That(cd.Effects.Count == (ec.Configurations.Count +1));
      Assert.That(cd.Effects[cd.Effects.Count-1].Name == ec.Configurations[ec.Configurations.Count -1].Name);
      Assert.That(ec.IsAltered);
    }

    [Test()]
    public void RemoveEffectInvalid()
    {
      Assert.That(!ec.IsAltered);
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();
      Assert.That(!ec.IsAltered);

      ec.Remove(null);
      Assert.That(!ec.IsAltered);

      ec.Remove(new EffectDetails("test", 0));
      Assert.That(!ec.IsAltered);

      EffectDetails ed = new EffectDetails(cd.Effects[5].Name, cd.Effects[5].Index-1);
      ec.Remove(ed);
      Assert.That(!ec.IsAltered);
    }

    [Test()]
    public void GetConfigParamsNullEffect()
    {
      Assert.That(!ec.IsAltered);
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        ec.AppendEffect(new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip"));
      }
      ec.SetUnaltered();

      Assert.That(ec.GetConfigParams(null) == null);
    }

    [Test()]
    public void GetConfigParamsValidEffect()
    {
      Assert.That(!ec.IsAltered);
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        EffectConfiguration eConf = new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip");
        eConf.AddParameter(new ConfigParameter("test", "test"));
        ec.AppendEffect(eConf);
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();

      ConfigParameter[] prms = ec.GetConfigParams(cd.Effects[5]);
      Assert.That(prms != null);
      Assert.That(prms.Count() == 1);
    }

    [Test()]
    public void AlterConfigInvalid()
    {
      ec.SetUnaltered();
      Assert.That(!ec.IsAltered);
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        EffectConfiguration eConf = new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip");
        eConf.AddParameter(new ConfigParameter("test", "test"));
        ec.AppendEffect(eConf);
      }
      ec.SetUnaltered();

      ChainDetails cd = ec.Describe();
      ec.AlterConfig(null, new ConfigParameter[0]);
      Assert.That(!ec.IsAltered);

      ConfigParameter[] confs = ec.GetConfigParams(cd.Effects[cd.Effects.Count-1]);
      ConfigParameter cpm = new ConfigParameter("test2", confs[0].Value + "test");
      confs[0] = cpm;
      ec.AlterConfig(cd.Effects[cd.Effects.Count - 1], confs);
      Assert.That(!ec.IsAltered);
    }

    [Test()]
    public void AlterConfigValid()
    {
      Assert.That(!ec.IsAltered);
      ec.Name = "test";
      ec.Filename = "test.zip";
      for (int i = 0; i < 10; i++)
      {
        EffectConfiguration eConf = new EffectConfiguration("Effect_" + i, "Effect_" + i + ".zip");
        eConf.AddParameter(new ConfigParameter("test", "test"));
        ec.AppendEffect(eConf);
      }
      ec.SetUnaltered();
      ChainDetails cd = ec.Describe();
      ConfigParameter[] confs = ec.GetConfigParams(cd.Effects[cd.Effects.Count - 1]);
      ConfigParameter cpm = new ConfigParameter(confs[0].Name, "test2");
      confs[0] = cpm;
      ec.AlterConfig(cd.Effects[cd.Effects.Count - 1], confs);
      Assert.That(ec.IsAltered);
    }
  }
}
