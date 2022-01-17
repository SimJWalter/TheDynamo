using System;
using System.Collections.Generic;
using System.Text;
using Designer.Entity;
using System.IO;
using Designer.Storage.XML;
using Designer.Manager.Helper;

namespace Designer.Storage
{
  /// <summary>
  /// Class for handling access to the stored effect banks
  /// </summary>
  class EffectBankStorage
  {
    internal List<EffectBank> FetchBanks()
    {
      List<EffectBank> ret = new List<EffectBank>();

      if (!Directory.Exists(FileStorageConstants.ChainBanksLocation))
      {
        //create directory if it does not exist
        Directory.CreateDirectory(FileStorageConstants.ChainBanksLocation);
      }

      List<FileInfo> bankFiles = new List<FileInfo>();
      foreach (string file in Directory.GetFiles(FileStorageConstants.ChainBanksLocation))
      {
        FileInfo fInfo = new FileInfo(file);
        if (string.Compare(fInfo.Extension, FileStorageConstants.StorageFileExtension, true) == 0)
        {
          bankFiles.Add(fInfo);
        }
      }

      BankGenerator bankGenerator = new BankGenerator();
      foreach (FileInfo fInfo in bankFiles)
      {
        StreamReader reader = File.OpenText(fInfo.FullName);
        string chain = reader.ReadToEnd();
        reader.Close();
        EffectBank add = bankGenerator.ParseBank(chain);
        if (add != null)
        {
          add.Filename = fInfo.Name;
          ret.Add(add);
        }
      }
      return ret;
    }

    internal void Save(EffectBank bank)
    {
      if (!Directory.Exists(FileStorageConstants.ChainBanksLocation))
      {
        Directory.CreateDirectory(FileStorageConstants.ChainBanksLocation);
      }
      string bankFileContent = new BankGenerator().GenerateBankDoc(bank, true);
      if ((bank.Filename == null || bank.Filename == "") && (bank.Name == null || bank.Name == ""))
      {
        EffectBankStorageException e = new EffectBankStorageException("Cannot persist bank to file as it has no valid name or filename");
        e.Bank = bank;
        throw e;
      }
      else if ((bank.Filename == null || bank.Filename == "") && (bank.Name != null && bank.Name != ""))
      {
        bank.Filename = bank.Name + FileStorageConstants.StorageFileExtension;
      }
      else if ((bank.Filename != null && bank.Filename != "") && (bank.Name == null || bank.Name == ""))
      {
        bank.Name = bank.Filename.Substring(0, bank.Filename.Length - FileStorageConstants.StorageFileExtension.Length);
      }

      FileInfo saveFile = new FileInfo(FileStorageConstants.ChainBanksLocation + bank.Filename);

      FileStream fstream = File.Create(saveFile.FullName);
      StreamWriter writer = new StreamWriter(fstream);
      writer.Write(bankFileContent);
      writer.Flush();
      writer.Close();
    }

    internal EffectBank Retrieve(BankDetails bankDetails)
    {
      if (bankDetails == null)
      {
        throw new ArgumentNullException("Details of bank are null");
      }
      EffectBank ret = null;
      List<EffectBank> banks = FetchBanks();
      for (int i = 0; i < banks.Count; i++)
      {
        if (banks[i].Filename == bankDetails.FileName && banks[i].Name == bankDetails.Name)
        {
          ret = banks[i];
          i = banks.Count;
        }
      }
      return ret;
    }
    
    public class EffectBankStorageException : Exception
    {
      private EffectBank _bank;
      private string _bankString;

      public EffectBankStorageException(string message)
        : base(message)
      { }

      public EffectBank Bank
      {
        get { return _bank; }
        set { _bank = value; }
      }

      public string BankString
      {
        get { return _bankString; }
        set { _bankString = value; }
      }
    }
  }
}
