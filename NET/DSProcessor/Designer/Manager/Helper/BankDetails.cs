using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Designer.Manager.Helper
{
  public class BankDetails
  {
    private string _name;
    private string _filename;
    private List<ChainDetails> _chains;
    private bool _unsaved;

    /// <summary>
    /// Constructor: requires setup with read-only attributes
    /// </summary>
    /// <param name="name">The name of the bank this represents</param>
    /// <param name="filename">The filename for the bank this represents</param>
    public BankDetails(string name, string filename)
    {
      //if (name == null || name == "")
      //{
      //  throw new ArgumentException("Cannot instantiate a representation of an effect-bank with a null or blank name.");
      //}
      //if (filename == null || filename == "")
      //{
      //  throw new ArgumentException("Cannot instantiate a representation of an effect-bank with a null or blank filename.");
      //}
      _name = name;
      _filename = filename;
      _unsaved = false;
      _chains = new List<ChainDetails>();
    }

    /// <summary>
    /// Used to add a representation of an effect chain to this object's internal list
    /// </summary>
    /// <param name="chain">The representation of the chain to append to the list</param>
    public void AppendChain(ChainDetails chain)
    {
      if (chain == null)
      {
        throw new ArgumentNullException("Cannot use a null object as an effect-chain within a bank");
      }
      else
      {
        _chains.Add(chain);
      }
    }

    /// <summary>
    /// Allows a set of effect-chains to be appended onto the list this object holds
    /// </summary>
    /// <param name="chains">A list of Effect-chain representations</param>
    public void AppendChains(IEnumerable<ChainDetails> chains)
    {
      if (chains == null || chains.Count() == 0)
      {
        throw new ArgumentException("Cannot append a null list or a list of 0 objects to the chains in this bank");
      }
      else
      {
        _chains.AddRange(chains);
      }
    }

    /// <summary>
    /// Allows this representation of an effect bank to be marked as unsaved
    /// </summary>
    public void SetUnsaved()
    {
      this._unsaved = true;
    }

    /// <summary>
    /// List of effect-chain representations, read-only
    /// </summary>
    public ReadOnlyCollection<ChainDetails> Chains
    {
      get
      {
        return _chains.AsReadOnly();
      }
    }

    /// <summary>
    /// Simple property used to query whether or not this object has been marked as saved or not
    /// </summary>
    public bool Unsaved
    {
      get
      {
        return _unsaved;
      }
    }

    /// <summary>
    /// Accessor to a read-only property holding the name of this object
    /// </summary>
    public string Name
    {
      get
      {
        return _name == null ? null : String.Copy(_name);
      }
    }

    /// <summary>
    /// Accessor to a read-only property holding the filename of this object
    /// </summary>
    public string FileName
    {
      get
      {
        return _filename == null ? null : String.Copy(_filename);
      }
    }

    /// <summary>
    /// String representation of this object, typical,ly used for ui representation
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return Name.Trim();
    }
  }
}
