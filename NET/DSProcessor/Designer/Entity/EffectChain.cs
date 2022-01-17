using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Designer.Manager.Helper;

namespace Designer.Entity
{
  /// <summary>
  /// Represents a chain of effect modules for transforming sound signals, each of which may have any sort of customised configuration associated
  /// </summary>
  public class EffectChain
  {
    private List<EffectConfiguration> configs;
    private string name;
    private string filename;
    private bool isAltered;

    /// <summary>
    /// Constructs a default representation of an effectchain for use in building a chain
    /// sets the object state to altered=true (as it hasnt been saved yet)
    /// sets both the filename and name values to null
    /// </summary>
    public EffectChain()
    {
      configs = new List<EffectConfiguration>();
      isAltered = true; 
      name = null;
      filename = null;
    }

    /// <summary>
    /// Performs a deep clone on the current object by instantiating a new copy and inserting clones of the contents herein
    /// </summary>
    /// <returns>The cloned copy</returns>
    internal EffectChain Clone()
    {
      EffectChain ret = new EffectChain();
      if (this.Name != null)
      {
        ret.Name = (string)(this.Name.Clone());
      }
      if (this.Filename != null)
      {
        ret.Filename = (string)(this.Filename.Clone());
      }      
      if (!isAltered)
      {
        ret.SetUnaltered();
      }
      foreach (EffectConfiguration ec in configs)
      {
        ret.AppendEffect(ec.Clone());
      }
      return ret;
    }

    /// <summary>
    /// returns a read-only copy of the configurations collection (effect modules)
    /// </summary>
    public ReadOnlyCollection<EffectConfiguration> Configurations
    {
      get { return configs.AsReadOnly(); }
    }

    /// <summary>
    /// Property for identifying whether or not the state of this object has been altered (whether it needs to be saved or not - discarded without losing changes)
    /// </summary>
    public bool IsAltered
    {
      get { return isAltered; }
    }

    /// <summary>
    /// get: Default accessor, read only
    /// set: requires non-null, non-blank value for association, sets object state to altered if the new value is different to the old one
    /// </summary>
    public string Name
    {
      get { return name == null ? null : String.Copy(name); }
      set
      {
        if (value == null || value == "")
        {
          throw new ArgumentException("Cannot alter the name of an effect chain to a blank or null value!");
        }
        else
        {
          if (name != value)
          {
            name = value;
            isAltered = true;
          }
        }
      }
    }

    /// <summary>
    /// get: Default accessor, read only
    /// set: requires non-null, non-blank value for association, sets object state to altered if the new value is different to the old one
    /// </summary>
    public string Filename
    {
      get { return filename == null ? null : String.Copy(filename); }
      set
      {
        if (value == "")
        {
          throw new ArgumentException("Cannot alter the filename of an effect chain to blank!");
        }
        else
        {
          if (filename != value)
          {
            filename = value;
            isAltered = true;
          }
        }        
      }
    }

    /// <summary>
    /// Provides a descriptor object for use on the front end to represent the state of the object herein
    /// </summary>
    /// <returns>A ChainDetails object set to represent the state and content of this object</returns>
    public ChainDetails Describe()
    {
      ChainDetails ret = new ChainDetails(Name, Filename);
      if (isAltered)
      {
        ret.SetUnsaved();
      }
      foreach (EffectConfiguration ec in this.Configurations)
      {
        ret.AppendEffect(ec.Name);
      }
      return ret;
    }

    /// <summary>
    /// Appends an EffectConfiguration object to the end of the list for this entity
    /// </summary>
    /// <param name="config">The configuration/effect-module object (must not be null)</param>
    public void AppendEffect(EffectConfiguration config)
    {
      if (config != null)
      {
        configs.Add(config);
        isAltered = true;
      }
    }

    /// <summary>
    /// Marks this object as persisted (no chanegs have been made since last save)
    /// </summary>
    public void SetUnaltered()
    {
      isAltered = false;
    }

    /// <summary>
    /// Uses a representation of an effect-configuration object to shift its "real" counterpart up by one index in the list held by this object, thereby making it closer to the source of the sound-signals by 1 place
    /// Sets the state of the object as altered if the action is valid and is carried out
    /// </summary>
    /// <param name="details">The representation of an effect-configuration object</param>
    internal void ShiftUp(EffectDetails details)
    {
      if (details != null)
      {
        if (configs[details.Index].Name == details.Name &&
              details.Index > 0)
        {
          EffectConfiguration shiftDown = configs[details.Index - 1];
          configs[details.Index - 1] = configs[details.Index];
          configs[details.Index] = shiftDown;
          isAltered = true;
        }
      }
    }

    /// <summary>
    /// Uses a representation of an effect-configuration object to shift its "real" counterpart down by one index in the list held by this object, thereby making it further from the source of the sound-signals by 1 place
    /// Sets the state of the object as altered if the action is valid and is carried out
    /// </summary>
    /// <param name="details">The representation of an effect-configuration object</param>
    internal void ShiftDown(EffectDetails details)
    {
      if (details != null)
      {
        if (configs[details.Index].Name == details.Name &&
            details.Index < configs.Count - 1)
        {
          EffectConfiguration shiftUp = configs[details.Index + 1];
          configs[details.Index + 1] = configs[details.Index];
          configs[details.Index] = shiftUp;
          isAltered = true;
        }
      }
    }

    /// <summary>
    /// Uses a representation of an effect configuration object to remove its "real" counterpart from the list stored in this object
    /// Sets the object state to altered of the action is valid and successful
    /// </summary>
    /// <param name="details">The details of the effectConfiguration to remove from the list</param>
    internal void Remove(EffectDetails details)
    {
      if (details != null && configs.Count > 0)
      {
        if (configs[details.Index].Name == details.Name)
        {
          configs.RemoveAt(details.Index);
          isAltered = true;
        }
      }
    }

    /// <summary>
    /// Generates a copy of the config parameters associated with the specified effect into the resulting array
    /// </summary>
    /// <param name="details">The effect from which to derive the set of configuration parameters</param>
    /// <returns>An array of configuration paramteters that are oasiciated with the specified effect in the chain held herein</returns>
    internal ConfigParameter[] GetConfigParams(EffectDetails details)
    {
      ConfigParameter[] ret = null;
      if (details != null)
      {
        if (configs[details.Index].Name == details.Name)
        {
          ret = new ConfigParameter[configs[details.Index].Parameters.Count];
          configs[details.Index].Parameters.CopyTo(ret, 0);
        }
      }
      return ret;
    }

    /// <summary>
    /// Uses a chain-member specifier and a set of config-parameters to update that member of the chain, the state of the object is set to altered if the action is successful
    /// </summary>
    /// <param name="cMember">The object representing the effect at a specified point in the chain</param>
    /// <param name="cps">The set of configuration parameters to be taken up by the specified effect module</param>
    internal void AlterConfig(EffectDetails cMember, ConfigParameter[] cps)
    {
      if (cMember != null)
      {
        for (int i = 0; i < configs.Count; i++)
        {
          if (i == cMember.Index && configs[i].Name == cMember.Name)
          {
            if (configs[i].UpdateParameters(cps))
            {
              isAltered = true;
            }
          }
        }
      }      
    }
  }
}
