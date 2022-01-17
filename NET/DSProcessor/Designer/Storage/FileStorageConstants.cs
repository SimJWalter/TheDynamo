using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Storage
{
  public class FileStorageConstants
  {
    private static string _PrototypesLocation = "01_EffectModules\\";
    public static string PrototypesLocation
    {
      get { return _PrototypesLocation; }
    }

    private static string _CustomChainsLocation = "02_ChainedEffects\\";
    internal static string CustomChainsLocation
    {
      get { return _CustomChainsLocation; }
    }

    private static string _ChainBanksLocation = "03_EffectBanks\\";
    internal static string ChainBanksLocation
    {
      get { return _ChainBanksLocation; }
    }

    private static string _ArchiveExtension = ".zip";
    public static string ArchiveExtension
    {
      get { return _ArchiveExtension; }
    }

    private static string _ConfigFileName = "config.xml";
    internal static string ConfigFileName
    {
      get { return _ConfigFileName; }
    }

    private static string _StorageFileExtension = ".xml";
    internal static string StorageFileExtension
    {
      get { return _StorageFileExtension; }
    }

    private static string _AssemblyExtension = ".dll";
    public static string AssemblyExtension
    {
      get { return _AssemblyExtension; }
    }
  }
}
