using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designer.Entity;
using System.Xml;
using System.IO;

namespace Designer.Storage.XML
{
  class BankGenerator
  {
    internal EffectBank ParseBank(string chain)
    {
      EffectBank ret = null;
      try
      {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(chain);  // does not work if the xml is invalid
        ret = new EffectBank();

        string xPathMain = "/" + XmlStorageConstants.BankDocumentRootElement;
        string xPathChain = xPathMain.Clone() + "/" + XmlStorageConstants.ChainDocumentRootElement;
        XmlNode mainNode = doc.SelectSingleNode(xPathMain);
        XmlNodeList chains = doc.SelectNodes(xPathChain);

        if (mainNode.Attributes[XmlStorageConstants.BankNameAttribute] != null)
        {
          ret.Name = mainNode.Attributes[XmlStorageConstants.BankNameAttribute].InnerText;
        }
        else
        {
          XmlParseException e = new XmlParseException("Effect bank has no name associated");
          e.XML = chain;
          throw e;
        }

        foreach (XmlNode xChain in chains)
        {
          ret.AppendChain(new ChainGenerator().ParseChain(xChain.OuterXml));
        }
      }
      catch (XmlParseException e)
      {
        throw e;
      }
      catch { }

      return ret;
    }
    
    internal string GenerateBankDoc(EffectBank bank, bool startDoc)
    {
      MemoryStream ms = new MemoryStream();
      XmlTextWriter w = new XmlTextWriter(ms, System.Text.Encoding.UTF8);
      ChainGenerator chainGenerator = new ChainGenerator();
      if (startDoc)
      {
        w.WriteStartDocument();
      }
      w.WriteStartElement(XmlStorageConstants.BankDocumentRootElement);
      w.WriteAttributeString(XmlStorageConstants.BankNameAttribute, bank.Name);
      foreach (EffectChain ec in bank.Chains)
      {
        w.WriteRaw(chainGenerator.GenerateChainDoc(ec, false));
      }
      w.WriteEndElement();
      if (startDoc)
      {
        w.WriteEndDocument();
      }
      w.Flush();

      ms.Position = 0;

      XmlTextReader reader = new XmlTextReader(ms);
      XmlDocument xDoc = new XmlDocument();
      xDoc.Load(reader);
      return xDoc.OuterXml;
    }
  }
}
