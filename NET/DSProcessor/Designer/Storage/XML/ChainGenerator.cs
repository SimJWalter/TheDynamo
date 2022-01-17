using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designer.Entity;
using System.Xml;
using System.IO;

namespace Designer.Storage.XML
{
  class ChainGenerator
  {
    internal EffectChain ParseChain(string chain)
    {
      EffectChain ret = null;
      try
      {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(chain);  // does not work if the xml is invalid
        ret = new EffectChain();

        string xPathMain = "/" + XmlStorageConstants.ChainDocumentRootElement;
        string xPathChain = xPathMain.Clone() + "/" + XmlStorageConstants.EffectConfigRootElement;
        XmlNode mainNode = doc.SelectSingleNode(xPathMain);
        XmlNodeList effects = doc.SelectNodes(xPathChain);

        if (mainNode.Attributes[XmlStorageConstants.ChainNameAttribute] != null)
        {
          ret.Name = mainNode.Attributes[XmlStorageConstants.ChainNameAttribute].InnerText;
        }
        else
        {
          XmlParseException e = new XmlParseException("Effect chain has no name associated");
          e.XML = chain;
          throw e;
        }

        foreach (XmlNode effect in effects)
        {
          ret.AppendEffect(new ConfigGenerator().ParseConfig(effect.OuterXml, null));
        }
      }
      catch (XmlParseException e)
      {
        throw e;
      }
      catch { }
          
      return ret;
    }

    internal string GenerateChainDoc(EffectChain chain, bool startDoc)
    {
      MemoryStream ms = new MemoryStream();
      XmlTextWriter w = new XmlTextWriter(ms, System.Text.Encoding.UTF8);
      ConfigGenerator configGenerator = new ConfigGenerator();
      if (startDoc)
      {
        w.WriteStartDocument();
      }      
      w.WriteStartElement(XmlStorageConstants.ChainDocumentRootElement);
      w.WriteAttributeString(XmlStorageConstants.ChainNameAttribute, chain.Name);
      foreach (EffectConfiguration ec in chain.Configurations)
      {
        w.WriteRaw(configGenerator.GenerateConfigDoc(ec, false));
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
