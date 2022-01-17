using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Designer.Storage;
using System.IO;
using System.Xml;

namespace Testing.Designer.Storage
{
  [TestFixture()]
  public class Zipper_UnitTest
  {
    private Zipper zip;

    [SetUp()]
    public void Init()
    {
      //common refresh code here
      File.WriteAllBytes(FileStorageConstants.PrototypesLocation + "\\vibrate.zip", global::Testing.Properties.Resources.vibrate);
      zip = new Zipper();
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
      zip = null;
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
    public void ExtractConfigurationValid()
    {
      FileInfo file = new FileInfo(Directory.GetFiles(FileStorageConstants.PrototypesLocation)[0]);
      string config = zip.ExtractConfiguration(file);
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(config);
    }
  }
}
