using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designer.Entity;
using Designer.Storage;
using Designer.Manager.Helper;

namespace Designer.Manager
{
  public class BankPrototypeManager
  {
    List<EffectBank> prototypes = null;

    /// <summary>
    /// Instantiates this object with the set of available effect banks
    /// </summary>
    public BankPrototypeManager()
    {
      prototypes = new EffectBankStorage().FetchBanks();
    }

    /// <summary>
    /// Describes the available banks using a list of representation objects
    /// </summary>
    /// <returns>A list of effect-bank representations</returns>
    public List<BankDetails> DescribeBanks()
    {
      List<BankDetails> ret = null;
      if (prototypes != null)
      {
        ret = new List<BankDetails>();
        foreach (EffectBank eb in prototypes)
        {
          ret.Add(eb.Describe());
        }
      }
      return ret;
    }

    /// <summary>
    /// Collects and returns an effect bank object according to the information supplied in the parameterised argument
    /// </summary>
    /// <param name="details">The specifier for the bank-object to collect</param>
    /// <returns>The bank object if found</returns>
    public EffectBank GetBank(BankDetails bankDetails)
    {
      EffectBank ret = null;
      if (bankDetails != null)
      {
        for (int i = 0; i < prototypes.Count; i++)
        {
          if (prototypes[i].Name == bankDetails.Name && prototypes[i].Filename == bankDetails.FileName)
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
