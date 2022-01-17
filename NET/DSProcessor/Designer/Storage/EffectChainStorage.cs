using System;
using System.Collections.Generic;
using System.Text;
using Designer.Entity;
using System.IO;
using Designer.Storage.XML;
using Designer.Manager.Helper;

namespace Designer.Storage
{
  class EffectChainStorage
  {
    /// <summary>
    /// Class for handling access to the stored effect chains
    /// </summary>
    /// <returns>The list of EffectChain objects</returns>
    internal List<EffectChain> FetchChains()
    {
      List<EffectChain> ret = new List<EffectChain>();

      if (!Directory.Exists(FileStorageConstants.CustomChainsLocation))
      {
        //create directory if it does not exist
        Directory.CreateDirectory(FileStorageConstants.CustomChainsLocation);
      }

      List<FileInfo> chainFiles = new List<FileInfo>();
      foreach (string file in Directory.GetFiles(FileStorageConstants.CustomChainsLocation))
      {
        FileInfo fInfo = new FileInfo(file);
        if (string.Compare(fInfo.Extension, FileStorageConstants.StorageFileExtension, true) == 0)
        {
          chainFiles.Add(fInfo);
        }
      }

      ChainGenerator chainGenerator = new ChainGenerator();
      foreach (FileInfo fInfo in chainFiles)
      {
        StreamReader reader = File.OpenText(fInfo.FullName);
        string chain = reader.ReadToEnd();
        reader.Close();
        EffectChain add = chainGenerator.ParseChain(chain);
        if (add != null)
        {
          foreach (EffectChain ec in ret)
          {
            if (add.Name == ec.Name)
            {
              throw new EffectChainStorageException("There are duplicate entries in the storage location with the name: " + ec.Name);
            }
          }
          add.Filename = fInfo.Name;
          ret.Add(add);
        }        
      }

      return ret;
    }

    internal void Save(EffectChain chain)
    {
      if (!Directory.Exists(FileStorageConstants.CustomChainsLocation))
      {
        Directory.CreateDirectory(FileStorageConstants.CustomChainsLocation);
      }
      string chainFileContent = new ChainGenerator().GenerateChainDoc(chain, true);

      if ((chain.Filename == null || chain.Filename == "") && (chain.Name == null || chain.Name == ""))
      {
        EffectChainStorageException e = new EffectChainStorageException("Cannot persist chain to file as it has no valid name or filename");
        e.Chain = chain;
        throw e;
      }      
      else if ((chain.Filename == null || chain.Filename == "") && (chain.Name != null && chain.Name != ""))
      {
        chain.Filename = chain.Name + FileStorageConstants.StorageFileExtension;
      }
      else if ((chain.Filename != null && chain.Filename != "") && (chain.Name == null || chain.Name == ""))
      { 
        chain.Name = chain.Filename.Substring(0, chain.Filename.Length - FileStorageConstants.StorageFileExtension.Length);
      }

      FileInfo saveFile = new FileInfo(FileStorageConstants.CustomChainsLocation + chain.Filename);

      FileStream fstream = File.Create(saveFile.FullName); //File.OpenWrite(saveFile.FullName);
      StreamWriter writer = new StreamWriter(fstream);
      writer.Write(chainFileContent);
      writer.Flush();
      writer.Close();
    }

    internal EffectChain Retrieve(ChainDetails chainDetails)
    {
      if (chainDetails == null)
      {
        throw new ArgumentNullException("Details of chain are null");
      }
      EffectChain ret = null;
      List<EffectChain> chains = FetchChains();
      for (int i = 0; i < chains.Count; i++)
      {
        if (chains[i].Filename == chainDetails.FileName && chains[i].Name == chainDetails.Name)
        {
          ret = chains[i];
          i = chains.Count;
        }
      }
      return ret;
    }

    public class EffectChainStorageException : Exception
    {
      private EffectChain _chain;
      private string _chainString;

      public EffectChainStorageException(string message)
        : base(message)
      { }

      public EffectChain Chain
      {
        get { return _chain; }
        set { _chain = value; }
      }

      public string ChainString
      {
        get { return _chainString; }
        set { _chainString = value; }
      }
    }
  }
}
