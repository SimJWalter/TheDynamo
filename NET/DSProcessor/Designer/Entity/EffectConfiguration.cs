using System;
using System.Collections.Generic;
using System.Text;
using Designer.Manager.Helper;
using System.Collections.ObjectModel;

namespace Designer.Entity
{
  /// <summary>
  /// Represents a single effect that exists within a chain of effects and contains 
  /// a list of configration parameters which provide the real-time processing routine 
  /// with the data it needs to run
  /// </summary>
  public class EffectConfiguration
  {
    private string _name;
    private string _filename;
    private List<ConfigParameter> _params;

    /// <summary>
    /// Construct a new EffectConfiguration object, will not allow null or blank name parameter
    /// </summary>
    /// <param name="effectName">The name to assign to the effect object</param>
    public EffectConfiguration(string effectName, string zipFile)
    {
      //if (effectName == null || effectName == "")
      //{
      //  throw new ArgumentException("Cannot instantiate a representation of an effect with which there is no name associated");
      //}
      //if (zipFile == null || zipFile == "")
      //{
      //  throw new ArgumentException("Cannot instantiate an effect configuration object with a null or blank filename");
      //}
      //else
      //{
        _params = new List<ConfigParameter>();
        _name = effectName;
        _filename = zipFile;
      //}
    }

    /// <summary>
    /// Get the effect-name (as defined in the name attribute of the XML that represents it), read-only
    /// </summary>
    public string Name
    {
      get { return _name == null ? null : String.Copy(_name); }
    }

    /// <summary>
    /// Get the filename of the zip file containing the effect and the configuraton document, read only
    /// </summary>
    public string Filename
    {
      get { return _filename == null ? null : String.Copy(_filename); }
    }

    /// <summary>
    /// Get a list of the configuration parameters as a read-only collection (parameters cannot be added or removed by way of this wrapper)
    /// </summary>
    public ReadOnlyCollection<ConfigParameter> Parameters
    {
      get { return _params.AsReadOnly(); }
    }

    /// <summary>
    /// Function to add a parameter to the list for this configuration
    /// </summary>
    /// <param name="param">The parameter entity containing the required data</param>
    public void AddParameter(ConfigParameter param)
    {
      bool exists = false;
      foreach (ConfigParameter cp in _params)
      {
        if (cp == param)
        {
          exists = true;
        }
      }
      if (exists)
      {
        throw new Exception("Cannot add a parameter to the list when that parameter already exists, try the updateparams function instead..");
      }
      else
      {
        _params.Add(param);
      }      
    }

    /// <summary>
    /// Updates the configuration values for any internal parameters that match those sent in the 
    /// parameterised array by name, all parameterised config values must match up with an entry 
    /// contained herein for the update to go ahead, 
    /// otherwise it is assumed that the config-attributes were meant for a different entity
    /// </summary>
    /// <param name="cps">The set of configuration parameters to inherit values from</param>
    /// <returns>Whether or not all the values in the parameterised array were recognised and used by this object</returns>
    internal bool UpdateParameters(ConfigParameter[] cps)
    {
      int matchCount = 0;
      foreach (ConfigParameter cfg in cps)
      {
        bool matchFound = false;
        foreach(ConfigParameter cfgI in _params)
        {
          if (cfgI == cfg)
          {
            matchFound = true;
          }
        }
        if (matchFound)
        {
          matchCount++;
        }
      }
           
      bool validUpdate = (matchCount == cps.Length);
      if (validUpdate)
      {
        foreach (ConfigParameter config in cps)
        {
          for (int i = 0; i < _params.Count; i++)
          {
            if (_params[i] == config && _params[i].Value != config.Value)
            {
              _params[i] = new ConfigParameter(_params[i].Name, config.Value);
            }
          }
        }
      }       
      return validUpdate;
    }

    /// <summary>
    /// Creates a recursive copy of this effect config object with matching values but non-matching references
    /// </summary>
    /// <returns>The new EC object</returns>
    public EffectConfiguration Clone()
    {
      EffectConfiguration ret = new EffectConfiguration(Name, Filename);
      foreach (ConfigParameter cp in _params)
      {
        ret.AddParameter(cp.Clone());
      }
      return ret;
    }
  }

  /// <summary>
  /// Represents a single configuration attribute on an effect module, 
  /// it consists of a name and a value both of which are strings at this 
  /// point but should be parsed to their esxpected types when the 
  /// effect module is set up for processing
  /// </summary>
  public class ConfigParameter
  {
    /// <summary>
    /// Forced single constructor, expects 2 parameters
    /// </summary>
    /// <param name="name">the Name of the configuration attribute</param>
    /// <param name="value">the Value of the configuration attribute</param>
    public ConfigParameter(string name, string value)
    {
      if (name == null || name == "")
      {
        throw new ArgumentException("Cannot have a configuration attribute with no name");
      }
      else
      {
        _name = name;
      }
      if (value == null || value == "")
      {
        _value = "0";
      }
      else
      {
        _value = value;
      }      
    }

    private string _name;
    /// <summary>
    /// Read-only Name property
    /// </summary>
    public string Name
    {
      get { return _name.Substring(0, _name.Length); }
    }

    private string _value;
    /// <summary>
    /// Read-only Value property
    /// </summary>
    public string Value
    {
      get { return _value.Substring(0, _value.Length); }
    }

    /// <summary>
    /// Tests whether two parameters are the same according to their name attributes, (names of config attributes are unique in each effect module instantiation)
    /// </summary>
    /// <param name="param">The other 'parameter'</param>
    /// <returns>True on equal, false on unequal</returns>
    //public bool Equals(ConfigParameter param)
    //{
    //  return param.Name == this.Name;
    //}

    public static bool operator ==(ConfigParameter cpA, ConfigParameter cpB)
    {
      bool ret = true;
      if((object)cpA == null || (object)cpB == null)
      {
        ret = false;
      }
      else
      {
        ret = (cpA.Name == cpB.Name);
      }
      return ret;
    }

    public static bool operator !=(ConfigParameter cpA, ConfigParameter cpB)
    {
      bool ret = false;
      if ((object)cpA == null || (object)cpB == null)
      {
        ret = true;
      }
      else
      {
        ret = (cpA.Name != cpB.Name);
      }
      return ret;
    }

    /// <summary>
    /// Creates a copy of this config parameter by replicating the values herein and instantiating a new object with them
    /// </summary>
    /// <returns>The cloned copy of this config parameter</returns>
    public ConfigParameter Clone()
    {
      return new ConfigParameter(_name.Substring(0, _name.Length), _value.Substring(0, _value.Length));
    }
  }
}
