using System;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Designer.Entity;
using System.Collections;
using System.Collections.Generic;

namespace Designer.Manager.Helper
{
  /// <summary>
  /// A representation of the managed effect-chain class for use on the user interfaces and during communications with the user layers of the application
  /// Used to separate the managed entities from the flow of interaction
  /// </summary>
  public class ChainDetails
  {
    private string _name;
    private string _filename;
    private List<EffectDetails> _effects;
    private bool _unsaved;
    private int _index;

    /// <summary>
    /// Constructor: requires setup with read-only attributes
    /// </summary>
    /// <param name="name">The name of the chain this represents</param>
    /// <param name="filename">The filename for the chain this represents</param>
    public ChainDetails(string name, string filename)
    {
      //if (name == null || name == "")
      //{
      //  throw new ArgumentException("Cannot instantiate a representation of an effect-chain with a null or blank name.");
      //}
      //if (filename == null || filename == "")
      //{
      //  throw new ArgumentException("Cannot instantiate a representation of an effect-chain with a null or blank filename.");
      //}

      _name = name;
      _filename = filename;
      _effects = new List<EffectDetails>();
      _unsaved = false;
    }

    /// <summary>
    /// Read-only attribute name
    /// </summary>
    public string Name
    {
      get
      {
        return _name == null ? null : String.Copy(_name);
      }
    }

    /// <summary>
    /// Read-only filename attribute
    /// </summary>
    public string FileName
    {
      get
      {
        return _filename == null ? null : String.Copy(_filename);
      }
    }

    /// <summary>
    /// Index attribute, identifies the position in a list of chains the object this represents occupies
    /// (cannot be negative)
    /// </summary>
    public int Index
    {
      get
      {
        return _index;
      }
      set
      {
        if (value < 0)
        {
          throw new IndexOutOfRangeException("Cannot assign negative values to an index attribute.");
        }
        else
        {
          _index = value;
        }
      }
    }

    /// <summary>
    /// Used to add a representation of an effect module to this object's internal list
    /// </summary>
    /// <param name="name">The name obtained from the effect module configuration, must not be null or blank</param>
    public void AppendEffect(string name)
    {
      if (name == null || name == "")
      {
        throw new ArgumentException("Cannot append an unidentified effect module to this chain.");
      }
      else
      {
        _effects.Add(new EffectDetails(name, _effects.Count));
      }
    }

    /// <summary>
    /// Allows a set of effects to be appended onto the list this object holds
    /// </summary>
    /// <param name="effects">A list of Effect-module representations</param>
    public void AppendEffects(IEnumerable<EffectDetails> effects)
    {
      if (effects == null || effects.Count() == 0)
      {
        throw new ArgumentException("Cannot append a null or empty list of effects on to this chain");
      }
      else
      {
        bool valid = true;
        foreach (EffectDetails ed in effects)
        {
          if (ed.Name == null || ed.Name == "")
          {
            valid = false;
          }
        }
        if (valid)
        {
          _effects.AddRange(effects);
        }
        else
        {
          throw new ArgumentException("This list contains invalid entries (effect representations with no name associated)");
        }      
      }      
    }

    /// <summary>
    /// This function is used to mark this representation of a chain as unsaved, inline with the actual state of the object it represents
    /// </summary>
    public void SetUnsaved()
    {
      this._unsaved = true;
    }

    /// <summary>
    /// List of effect-module representations, read-only
    /// </summary>
    public ReadOnlyCollection<EffectDetails> Effects
    {
      get
      {
        return _effects.AsReadOnly();
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
  }
}
