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
  /// Manages the creation, editing and storage of effect-banks in the system
  /// </summary>
  public class EffectBankBuilder
  {
    private EffectBank currentEdit;

    /// <summary>
    /// Instantiates a new Effect-Bank object to work on
    /// </summary>
    public void CreateNew()
    {
      currentEdit = new EffectBank();
    }

    /// <summary>
    /// Determines whether the EffectBank being worked on has been altered since it was last saved
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
    public BankDetails CurrentEditDetails
    {
      get
      {
        BankDetails ret = null;
        if (currentEdit != null)
        {
          ret = currentEdit.Describe();
        }
        return ret;
      }
    }

    /// <summary>
    /// Appends the specified effect chain onto the end of the list of chains represented by the current bank in edit
    /// </summary>
    /// <param name="chainDetails">Object describing the effect-chain to add to the list</param>
    /// <param name="protManager">The resource manager from which to collect the chain object</param>
    public void AddChain(ChainDetails chainDetails, ChainPrototypeManager protManager)
    {
      EffectChain chain = protManager.GetChain(chainDetails);
      if (chain == null)
      {
        BankMgtException e = new BankMgtException("The requested chain no longer exists in the list");
        e.C_Details = chainDetails;
        throw e;
      }
      else
      {
        currentEdit.AppendChain(chain);
      }
    }

    /// <summary>
    /// Requests that the current bank in edit places the chain object specified in the argument list at one index higher than its current location
    /// </summary>
    /// <param name="details">The object describing the chain to be relocated</param>
    public void IncrementIndex(ChainDetails details)
    {
      if (details != null)
      {
        currentEdit.ShiftUp(details);
      }      
    }

    /// <summary>
    /// Requests that the current bank in edit places the chain object specified in the argument list at one index lower than its current location
    /// </summary>
    /// <param name="details">The object describing the chain to be relocated</param>
    public void DecrementIndex(ChainDetails details)
    {
      if (details != null)
      {
        currentEdit.ShiftDown(details);
      }
    }

    /// <summary>
    /// Requests that the current bank in edit removes the chain specified in the argument list from its internal list
    /// </summary>
    /// <param name="details">The object describing the chain to be removed</param>
    public void RemoveFromBank(ChainDetails details)
    {
      if (details != null)
      {
        currentEdit.Remove(details);
      }      
    }

    /// <summary>
    /// Determines whether the bank currently in edit has a filename applied to it (whether or not it has been saved before at any point)
    /// </summary>
    public bool CurrentHasFile
    {
      get
      {
        return !(currentEdit.Filename == "" || currentEdit.Filename == null);
      }
    }

    /// <summary>
    /// Requests that the bank in edit currently is persisted using the filename associated with it
    /// </summary>
    public void SaveCurrent()
    {
      if ((currentEdit.Name == "" || currentEdit.Name == null) && (currentEdit.Filename == "" || currentEdit.Filename == null))
      {
        BankPersistenceException e = new BankPersistenceException("No filename or name is associated with this bank, unable to perform persistence.");
        e.Details = currentEdit.Describe();
        throw e;
      }
      new EffectBankStorage().Save(currentEdit);
      currentEdit.SetUnaltered();
    }

    /// <summary>
    /// Requests that the bank currently in edit is persisted using the name supplied
    /// </summary>
    /// <param name="name">The name to apply to this bank</param>
    public void SaveCurrentAs(string name)
    {
      if (name == "" || name == null)
      {
        BankPersistenceException e = new BankPersistenceException("No filename or name is associated with theis chain, unable to perform persistence.");
        e.Details = currentEdit.Describe();
        throw e;
      }
      else
      {
        currentEdit.Name = name;
        currentEdit.Filename = null;
        new EffectBankStorage().Save(currentEdit);
        currentEdit.SetUnaltered();
      }      
    }

    /// <summary>
    /// Sets the specified bank as the current edit
    /// </summary>
    /// <param name="chainDetails">A description object specifying the bank to load as the current edit</param>
    public void LoadBank(BankDetails bankDetails)
    {
      EffectBank load = new EffectBankStorage().Retrieve(bankDetails);
      if (load == null)
      {
        BankPersistenceException e = new BankPersistenceException("Requested effect bank could not be located for instantiation");
        e.Details = bankDetails;
        throw e;
      }
      else
      {
        currentEdit = load;
        currentEdit.SetUnaltered();
      }
    }

    public class BankMgtException : Exception
    {
      private BankDetails details;
      private ChainDetails c_details;
      public BankMgtException(string msg) : base(msg) { }

      public BankDetails B_Details
      {
        get { return details; }
        set { details = value; }
      }

      public ChainDetails C_Details
      {
        get { return c_details; }
        set { c_details = value; }
      }
    }

    public class BankPersistenceException : Exception
    {
      private BankDetails details;

      public BankPersistenceException(string message)
        : base(message)
      { }

      public BankDetails Details
      {
        get { return details; }
        set { details = value; }
      }
    }
  }
}
