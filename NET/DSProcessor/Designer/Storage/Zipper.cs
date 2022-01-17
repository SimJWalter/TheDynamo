using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Designer.Storage
{
  public class Zipper
  {
    public string ExtractConfiguration(FileInfo fInfo)
    {
      try
      {
        ZipFile file = new ZipFile(fInfo.FullName);
        ZipEntry configFile = file.GetEntry(FileStorageConstants.ConfigFileName);
        Stream stream = file.GetInputStream(configFile);
        Byte[] content = new Byte[configFile.Size];
        stream.Read(content, 0, content.Length);
        stream.Close();
        return System.Text.Encoding.Default.GetString(content);
      }
      catch (Exception e)
      {
        ZipFileException ze = new ZipFileException("Corrupt resource, unable to read archive: " + fInfo.FullName, e);
        ze.ZipFile = fInfo;
        throw ze;
      }
    }

    private class ZipFileException : Exception
    {
      private FileInfo _zipFile;

      public ZipFileException(string message) : base(message) { }
      public ZipFileException(string message, Exception inner) : base(message, inner) { }

      public FileInfo ZipFile
      {
        get { return _zipFile; }
        set { _zipFile = value; }
      }
    }
  }
}
