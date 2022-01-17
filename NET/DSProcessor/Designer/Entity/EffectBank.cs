using System;
using System.Collections.Generic;
using System.Text;
using Designer.Manager.Helper;
using System.Collections.ObjectModel;

namespace Designer.Entity
{
  /// <summary>
  /// Represents a managed list of effect-chains which can be used for live processing
  /// </summary>
  public class EffectBank
  {
    private List<EffectChain> chains;
    private string name;
    private string filename;
    private bool isAltered;

    /// <summary>
    /// Creates a new EffectBank object with state set to altered (unsaved changes)
    /// name and filename are set to null
    /// </summary>
    public EffectBank()
    {
      chains = new List<EffectChain>();
      isAltered = true;
      name = null;
      filename = null;
    }

    /// <summary>
    /// Returns a read-only collection of the Effect-Chain objects in the list stored in this object
    /// (this is a convenience class for use by the storage componenets -- design would benefit from decoupling here)
    /// </summary>
    public ReadOnlyCollection<EffectChain> Chains
    {
      get { return chains.AsReadOnly(); }
    }

    /// <summary>
    /// Get: returns read-only copy of the name string
    /// Set: will not accept null or blank, update is actioned if the new value is not equal to the old one.
    ///     Sets the state of this object to altered (unsaved) if the action goes ahead successfully
    /// </summary>
    public string Name
    {
      get { return name == null ? null : String.Copy(name); }
      set 
      {
        if (value == null || value == "")
        {
          throw new ArgumentNullException("Cannot set the value of an EffectBanks name to blank or null");
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
    /// Property accessor, specifies whether or not this object has undergone changes since it was last persisted (or has not yet been persisted)
    /// </summary>
    public bool IsAltered
    {
      get { return isAltered; }
    }

    /// <summary>
    /// Get: returns read-only copy of the filename string
    /// Set: will not accept null or blank, update is actioned if the new value is not equal to the old one.
    ///     Sets the state of this object to altered (unsaved) if the action goes ahead successfully
    /// </summary>
    public string Filename
    {
      get 
      { return filename == null ? null : String.Copy(filename); }
      set
      {
        if (value == "")
        {
          throw new ArgumentNullException("Cannot set the filename of this Effect-Bank to blank or null");
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
    /// <returns>A BankDetails object set to represent the state and content of this object</returns>
    public BankDetails Describe()
    {
      BankDetails ret = new BankDetails(Name, Filename);
      if (isAltered)
      {
        ret.SetUnsaved();
      }
      for (int i = 0; i < Chains.Count; i++)
      {
        ChainDetails cd = chains[i].Describe();
        cd.Index = i;
        ret.AppendChain(cd);
      }
      return ret;
    }

    /// <summary>
    /// Appends an EffectChain object to the end of the list for this entity
    /// </summary>
    /// <param name="chain">The effect-chain object (must not be null)</param>
    public void AppendChain(EffectChain chain)
    {
      if (chain != null)
      {
        chains.Add(chain);
        isAltered = true;
      }
    }

    /// <summary>
    /// Performs a deep clone on the current object by instantiating a new copy and inserting clones of the contents herein
    /// </summary>
    /// <returns>The cloned copy</returns>
    internal EffectBank Clone()
    {
      EffectBank ret = new EffectBank();
      if (this.Name != null)
      {
        ret.Name = (string)this.Name.Clone();
      }
      if (this.Filename != null)
      {
        ret.Filename = (string)this.Filename.Clone();
      }
      foreach (EffectChain ec in chains)
      {
        ret.AppendChain(ec.Clone());
      }
      if (!isAltered)
      {
        ret.SetUnaltered();
      }
      return ret;
    }

    /// <summary>
    /// Uses a representation of an effect-chain object to shift its "real" counterpart up by one index in the list held by this object
    /// Sets the state of the object as altered if the action is valid and is carried out
    /// </summary>
    /// <param name="details">The representation of an effect-chain object</param>
    internal void ShiftUp(ChainDetails details)
    {
      if (details != null)
      {
        if (chains[details.Index].Name == details.Name && details.Index > 0)
        {
          EffectChain shiftDown = chains[details.Index - 1];
          chains[details.Index - 1] = chains[details.Index];
          chains[details.Index] = shiftDown;
          isAltered = true;
        }
      }
    }

    /// <summary>
    /// Uses a representation of an effect-chain object to shift its "real" counterpart down by one index in the list held by this object
    /// Sets the state of the object as altered if the action is valid and is carried out
    /// </summary>
    /// <param name="details">The representation of an effect-chain object</param>
    internal void ShiftDown(ChainDetails details)
    {
      if (details != null)
      {
        if (chains[details.Index].Name == details.Name && details.Index < chains.Count - 1)
        {
          EffectChain shiftUp = chains[details.Index + 1];
          chains[details.Index + 1] = chains[details.Index];
          chains[details.Index] = shiftUp;
          isAltered = true;
        }
      }
    }

    /// <summary>
    /// Uses a representation of an effect chain object to remove its "real" counterpart from the list stored in this object
    /// Sets the object state to altered of the action is valid and successful
    /// </summary>
    /// <param name="details">The details of the effect-chain to remove from the list</param>
    internal void Remove(ChainDetails details)
    {
      if (details != null && chains.Count > 0)
      {
        if (chains[details.Index].Name == details.Name)
        {
          chains.RemoveAt(details.Index);
          isAltered = true;
        }
      }
    }

    /// <summary>
    /// Marks this object as persisted (no chanegs have been made since last save)
    /// </summary>
    internal void SetUnaltered()
    {
      isAltered = false;
    }
  }
}
