using System;
using System.Collections.Generic;
using System.Text;
using Designer.Entity;
using Designer.Storage;
using Designer.Manager.Helper;

namespace Designer.Manager
{
  public class EffectPrototypeManager
  {
    List<EffectConfiguration> prototypes = null;

    /// <summary>
    /// Instantiates this object with the set of available effect units
    /// </summary>
    public EffectPrototypeManager() 
    {
      prototypes = new EffectPrototypeStorage().FetchConfigurations();
    }

    /// <summary>
    /// Describes the available effects using a list of representation objects
    /// </summary>
    /// <returns>A list of effect-module representations</returns>
    public List<EffectDetails> DescribePrototypes()
    {
      List<EffectDetails> ret = new List<EffectDetails>(); // this function must not return null!!
      if (prototypes != null && prototypes.Count > 0)
      {
        for (int i = 0; i < prototypes.Count; i++)
        {
          ret.Add(new EffectDetails(prototypes[i].Name, i));
        }
      }
      return ret;
    }

    /// <summary>
    /// Collects and returns an effect configuration object according to the information supplied in the parameterised argument
    /// </summary>
    /// <param name="details">The specifier for the config-object to collect</param>
    /// <returns>The config object if found</returns>
    internal EffectConfiguration GetConfig(EffectDetails details)
    {
      EffectConfiguration ret = null;
      if (details != null)
      {
        for (int i = 0; i < prototypes.Count; i++)
        {
          if (prototypes[i].Name == details.Name && details.Index == i)
          {
            ret = prototypes[i].Clone();
            i = prototypes.Count;
          }
        }
      }      
      return ret;
    }
  }
}