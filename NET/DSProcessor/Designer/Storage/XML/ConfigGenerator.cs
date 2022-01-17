using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designer.Entity;
using System.Xml;
using System.IO;

namespace Designer.Storage.XML
{
  internal class ConfigGenerator
  {
    public EffectConfiguration ParseConfig(string config, string filename)
    {
      EffectConfiguration ret = null;
      XmlDocument doc = new XmlDocument();
      while (config[0] != '<')
      {
        config = config.Remove(0, 1);
      }
      while (config[config.Length - 1] != '>')
      {
        config = config.Remove(config.Length - 1);
      }
      doc.LoadXml(config);
      string xPathMain =  "/" + XmlStorageConstants.EffectConfigRootElement;
      string xPathParam = xPathMain.Clone() + "/" + XmlStorageConstants.EffectParameterNode;
      XmlNode mainNode = doc.SelectSingleNode(xPathMain);
      XmlNodeList paramList = doc.SelectNodes(xPathParam);
      if (mainNode.Attributes[XmlStorageConstants.EffectConfigNameAttribute] != null &&
        mainNode.Attributes[XmlStorageConstants.EffectConfigNameAttribute].InnerText != "")
      {
        ret = new EffectConfiguration(mainNode.Attributes[XmlStorageConstants.EffectConfigNameAttribute].InnerText, filename);
      }
      else
      {
        XmlParseException e = new XmlParseException("Configuration has no name associated");
        e.XML = config;
        throw e;
      }

      foreach (XmlNode param in paramList)
      {
        string xPathName = XmlStorageConstants.EffectParameterNameNode;
        string xPathValue = XmlStorageConstants.EffectParameterValueNode;
        XmlNode name = param.SelectSingleNode(xPathName);
        XmlNode value = param.SelectSingleNode(xPathValue);
        if (name == null)
        {
          XmlParseException e = new XmlParseException("Parameter has no 'name' field");
          e.XML = param.InnerXml;
          throw e;
        }
        if (value == null)
        {
          XmlParseException e = new XmlParseException("Parameter has no 'value' field");
          e.XML = param.InnerXml;
          throw e;
        }

        ret.AddParameter(new ConfigParameter(name.InnerText, value.InnerText));
      }

      return ret;
    }

    internal string GenerateConfigDoc(EffectConfiguration ec, bool startDoc)
    {
      MemoryStream ms = new MemoryStream();
      XmlTextWriter w = new XmlTextWriter(ms, System.Text.Encoding.UTF8);
      if (startDoc)
      {
        w.WriteStartDocument();
      }
      w.WriteStartElement(XmlStorageConstants.EffectConfigRootElement);
      w.WriteAttributeString(XmlStorageConstants.EffectConfigNameAttribute, ec.Name);
      foreach (ConfigParameter cp in ec.Parameters)
      {
        w.WriteStartElement(XmlStorageConstants.EffectParameterNode);
        w.WriteStartElement(XmlStorageConstants.EffectParameterNameNode);
        w.WriteString(cp.Name);
        w.WriteEndElement();
        w.WriteStartElement(XmlStorageConstants.EffectParameterValueNode);
        w.WriteString(cp.Value);
        w.WriteEndElement();
        w.WriteEndElement();
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

  internal class XmlParseException : Exception
  {
    public XmlParseException(string message) : base(message) { }
    public XmlParseException(string message, Exception inner) : base(message, inner) { }

    private string xmlString;
    public string XML
    {
      get { return xmlString; }
      set { xmlString = value; }
    }
  }
}
