using System;
using System.Collections.Generic;
using System.Text;
using Designer.Entity;
using System.IO;
using Designer.Storage.XML;

using ICSharpCode.SharpZipLib.Zip;
using System.Reflection;

namespace Designer.Storage
{
  /// <summary>
  /// Class for handling access to the stored effect modules
  /// </summary>
  public class EffectPrototypeStorage
  {
    /// <summary>
    /// Collects together a list of effectConfiguration entities representing the set of 
    /// available effect module prototypes available in the prototypes storage location
    /// </summary>
    /// <returns>The list of available Effects</returns>
    public List<Designer.Entity.EffectConfiguration> FetchConfigurations()
    {
      List<EffectConfiguration> ret = new List<EffectConfiguration>();

      if (!Directory.Exists(FileStorageConstants.PrototypesLocation))
      {
        //create directory if it does not exist
        Directory.CreateDirectory(FileStorageConstants.PrototypesLocation);
      }

      List<FileInfo> protoFiles = new List<FileInfo>();
      foreach (string file in Directory.GetFiles(FileStorageConstants.PrototypesLocation))
      {
        FileInfo fInfo = new FileInfo(file);

        if (string.Compare(fInfo.Extension, FileStorageConstants.ArchiveExtension, true) == 0)
        {
          protoFiles.Add(fInfo);
        }
      }

      ConfigGenerator cg = new ConfigGenerator();
      foreach (FileInfo fInfo in protoFiles)
      {
        EffectConfiguration ec = cg.ParseConfig(new Zipper().ExtractConfiguration(fInfo), fInfo.Name);
        ret.Add(ec);
      }

      //check for duplicates
      foreach (EffectConfiguration ec in ret)
      {
        foreach (EffectConfiguration ec2 in ret)
        {
          if (ec.Filename != ec2.Filename && ec.Name == ec2.Name)
          {
            throw new PrototypeStorageException("Your set of effect configuration prototypes contains duplicates: " + ec + "  " + ec2);
          }
        }
      }
      return ret;
    }
  }

  public class PrototypeStorageException : Exception
  {
    public PrototypeStorageException(String msg)
      : base(msg)
    { }
  }
}
