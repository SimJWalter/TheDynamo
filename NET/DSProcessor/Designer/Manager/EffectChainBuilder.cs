using System;
using System.Collections.Generic;
using System.Text;
using Designer.Storage;
using Designer.Entity;
using Designer.Manager.Helper;
using System.IO;

namespace Designer.Manager
{
  /// <summary>
  /// Manages the creation, editing and storage of effect-chains in the system
  /// </summary>
  public class EffectChainBuilder
  {
    private EffectChain currentEdit;

    /// <summary>
    /// Instantiates a new Effect-Chain object to work on
    /// </summary>
    public void CreateNew()
    {
      currentEdit = new EffectChain();
    }

    /// <summary>
    /// Determines whether the EffectChain being worked on has been altered since it was last saved
    /// </summary>
    public bool UnsavedChanges
    {
      get
      {
        bool ret = false;
        if (currentEdit != null)
        {
          ret = currentEdit.IsAltered;
        }
        return ret;
      }
    }

    /// <summary>
    /// Fashions a description object about the chain currently being worked on
    /// </summary>
    public ChainDetails CurrentEditDetails
    {
      get
      {
        ChainDetails ret = null;
        if (currentEdit != null)
        {
          ret = currentEdit.Describe();
        }
        return ret;
      }
    }

    public EffectChain CurrentEdit
    {
      get 
      {
        return currentEdit.Clone();
      }
    }

    /// <summary>
    /// Appends the specified effect configuration onto the end of the list of ewffects represented by the current chain in edit
    /// </summary>
    /// <param name="details">Object describing the effect to add to the list</param>
    /// <param name="pManager">The resource manager from which to collect the configuration object</param>
    public void AddEffect(EffectDetails details, EffectPrototypeManager pManager)
    {
      EffectConfiguration config = pManager.GetConfig(details);
      if (config == null)
      {
        ChainMgtException e = new ChainMgtException("The requested prototype no longer exists in the list");
        e.P_Details = details;
        throw e;
      }
      else
      {
        currentEdit.AppendEffect(config);
      }
    }

    /// <summary>
    /// Requests that the current chain in edit places the effect object specified in the argument list at one index higher than its current location
    /// </summary>
    /// <param name="details">The object describing the effect to be relocated</param>
    public void IncrementIndex(EffectDetails details)
    {
      if (details != null)
      {
        currentEdit.ShiftUp(details);
      }
    }

    /// <summary>
    /// Requests that the current chain in edit places the effect object specified in the argument list at one index lower than its current location
    /// </summary>
    /// <param name="details">The object describing the effect to be relocated</param>
    public void DecrementIndex(EffectDetails details)
    {
      if (details != null)
      {
        currentEdit.ShiftDown(details);
      }
    }

    /// <summary>
    /// Requests that the current chain in edit removes the effect specified in the argument list from its internal list
    /// </summary>
    /// <param name="details">The object describing the effect to be removed</param>
    public void RemoveFromChain(EffectDetails details)
    {
      currentEdit.Remove(details);
    }

    /// <summary>
    /// Determines whether the chain currently in edit has a filename applied to it (whether or not it has been saved before at any point)
    /// </summary>
    public bool CurrentHasFile
    {
      get
      {
        return !(currentEdit.Filename == "" || currentEdit.Filename == null);
      }      
    }

    /// <summary>
    /// Requests that the chain in edit currently is persisted using the filename associated with it
    /// </summary>
    public void SaveCurrent()
    {
      if ((currentEdit.Name == "" || currentEdit.Name == null) && (currentEdit.Filename == "" || currentEdit.Filename == null))
      {
        ChainPersistenceException e = new ChainPersistenceException("An effect chain must have a name associated with it in order for the");
        e.Details = currentEdit.Describe();
        throw e;
      }
      new EffectChainStorage().Save(currentEdit);
      currentEdit.SetUnaltered();
    }

    /// <summary>
    /// Requests that the chain currently in edit is persisted using the name supplied
    /// </summary>
    /// <param name="name">The name to apply to this chain</param>
    public void SaveCurrentAs(string name)
    {
      if (name == "" || name == null)
      {
        ChainPersistenceException e = new ChainPersistenceException("No name is associated with theis chain, unable to perform persistence.");
        e.Details = currentEdit.Describe();
        throw e;
      }
      else
      {
        currentEdit.Name = name;
        currentEdit.Filename = null;
        new EffectChainStorage().Save(currentEdit);
        currentEdit.SetUnaltered();
      }      
    }

    /// <summary>
    /// Sets the specified chain as the current edit
    /// </summary>
    /// <param name="chainDetails">A description object specifying the chain to load as the current edit</param>
    public void LoadChain(ChainDetails chainDetails)
    {
      EffectChain load = new EffectChainStorage().Retrieve(chainDetails);
      if (load == null)
      {
        ChainPersistenceException e = new ChainPersistenceException("Requested effect chain could not be located for instantiation");
        e.Details = chainDetails;
        throw e;
      }
      else
      {
        currentEdit = load;
        currentEdit.SetUnaltered();
      }      
    }

    /// <summary>
    /// Retrieves a list of configuration parameters for a specific effect module
    /// </summary>
    /// <param name="details">An object describing the effect module from which to collect the configuration parameters</param>
    /// <returns>Set of configuration options</returns>
    public ConfigParameter[] GetConfigParams(EffectDetails details)
    {
      ConfigParameter[] ret = null;
      if (details != null)
      {
        ret = currentEdit.GetConfigParams(details);
      }
      return ret;
    }

    /// <summary>
    /// Requests that the chain currently in edit updates the configuration options of the specified effect with the values specified
    /// </summary>
    /// <param name="cMember">Object describing the effect momdule to update</param>
    /// <param name="cps">The list of names and values to apply to the effect module</param>
    public void UpdateChainMemberConfig(EffectDetails cMember, ConfigParameter[] cps)
    {
      currentEdit.AlterConfig(cMember, cps);
    }

    private void SetName(string name)
    {
      this.currentEdit.Name = name;
    }

    public class ChainPersistenceException : Exception
    {
      private ChainDetails details;

      public ChainPersistenceException(string message) : base(message)
      { }

      public ChainDetails Details
      {
        get { return details; }
        set { details = value; }
      }
    }

    public class ChainMgtException : Exception
    {
      private EffectDetails p_details;
      public ChainMgtException(string message)
        : base(message)
      { }

      public EffectDetails P_Details
      {
        get { return p_details; }
        set { p_details = value; }
      }
    }
  }
}
