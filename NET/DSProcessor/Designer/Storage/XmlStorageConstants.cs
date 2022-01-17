using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Storage
{
  public class XmlStorageConstants
  {
    private static string _ChainDocumentRootElement = "effectChain";
    public static string ChainDocumentRootElement
    {
      get { return _ChainDocumentRootElement; }
    }

    private static string _BankDocumentRootElement = "effectBank";
    public static string BankDocumentRootElement
    {
      get { return _BankDocumentRootElement; }
    }

    private static string _ChainNameAttribute = "chainName";
    public static string ChainNameAttribute
    {
      get { return _ChainNameAttribute; }
    }

    private static string _BankNameAttribute = "bankName";
    public static string BankNameAttribute
    {
      get { return _BankNameAttribute; }
    }

    private static string _EffectConfigRootElement = "effect";
    public static string EffectConfigRootElement
    {
      get { return _EffectConfigRootElement; }
    }

    private static string _EffectConfigNameAttribute = "name";
    public static string EffectConfigNameAttribute
    {
      get { return _EffectConfigNameAttribute; }
    }

    private static string _EffectParameterNode = "param";
    public static string EffectParameterNode
    {
      get { return _EffectParameterNode; }
    }

    private static string _EffectParameterNameNode = "name";
    public static string EffectParameterNameNode
    {
      get { return _EffectParameterNameNode; }
    }

    private static string _EffectParameterValueNode = "value";
    public static string EffectParameterValueNode
    {
      get { return _EffectParameterValueNode; }
    }
  }
}
