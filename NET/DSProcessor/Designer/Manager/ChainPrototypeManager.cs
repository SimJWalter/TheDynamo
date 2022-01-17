using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designer.Entity;
using Designer.Storage;
using Designer.Manager.Helper;

namespace Designer.Manager
{
  /// <summary>
  /// Responsible for cataloguing and providing access to the EffectChains stored in the file system
  /// </summary>
  public class ChainPrototypeManager
  {
    List<EffectChain> prototypes = null;

    /// <summary>
    /// Instantiates this object with the set of available effect chains
    /// </summary>
    public ChainPrototypeManager()
    {
      prototypes = new EffectChainStorage().FetchChains();
    }

    /// <summary>
    /// Describes the available chains using a list of representation objects
    /// </summary>
    /// <returns>A list of effect-chain representations</returns>
    public List<ChainDetails> DescribeChains()
    {
      List<ChainDetails> ret = new List<ChainDetails>(); // this function must not return null!
      if (prototypes != null && prototypes.Count > 0)
      {
        foreach (EffectChain ec in prototypes)
        {
          ret.Add(ec.Describe());
        }
      }
      return ret;
    }

    /// <summary>
    /// Collects and returns an effect chain object according to the information supplied in the parameterised argument
    /// </summary>
    /// <param name="details">The specifier for the chain-object to collect</param>
    /// <returns>The chain object if found</returns>
    internal EffectChain GetChain(ChainDetails chainDetails)
    {
      EffectChain ret = null;
      if (chainDetails != null)
      {
        for (int i = 0; i < prototypes.Count; i++)
        {
          if (prototypes[i].Name == chainDetails.Name && prototypes[i].Filename == chainDetails.FileName)
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
