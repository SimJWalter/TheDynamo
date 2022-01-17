using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designer.Entity;

namespace Designer.Manager.Helper
{
  /// <summary>
  /// A representation of the managed effect-configuration class for use on the user interfaces and during communications with the user layers of the application
  /// Used to separate the managed entities from the flow of interaction
  /// </summary>
  public class EffectDetails
  {
    private string _name;
    private int _index;

    /// <summary>
    /// Constructor: requires setup with read-only attributes
    /// </summary>
    /// <param name="name">The name of the effect this represents</param>
    /// <param name="index">The index in the managed list of the effect this object represents</param>
    public EffectDetails(string name, int index)
    {
      if (name == null || name == "")
      {
        throw new ArgumentException("Cannot instantiate a representation of an effect unit with a blank or null name field.");
      }
      if (index < 0)
      {
        throw new ArgumentException(String.Format("Cannot instantiate a representation of an effect with an invalid list index: {0}", index));
      }
      _name = name;
      _index = index;
    }

    /// <summary>
    /// Read-only accessor to the name of the effect this object represents
    /// </summary>
    public string Name
    {
      get
      {
        return _name == null ? null : String.Copy(_name);
      }
    }

    /// <summary>
    /// Read-only accessor to the index in the managed list of the effect this represents
    /// </summary>
    public int Index
    {
      get
      {
        return _index;
      }
    }

    /// <summary>
    /// Determines whether this objects is equal to another EffectDetails object by testing on equality the value of the name & index fields
    /// </summary>
    /// <param name="ed">The effect to make the comparison with</param>
    /// <returns>true if equal, false if not</returns>
    //public bool Equals(EffectDetails ed)
    //{
    //  return (ed.Name == Name && ed.Index == Index);
    //}

    public static bool operator ==(EffectDetails edA, EffectDetails edB)
    {
      bool ret = true;
      if ((object)edA == null || (object)edB == null)
      {
        ret = false;
      }
      else
      { 
        ret = (edA.Name == edB.Name && edA.Index == edB.Index);
      }
      return ret;
    }

    public static bool operator !=(EffectDetails edA, EffectDetails edB)
    {
      bool ret = false;
      if ((object)edA == null || (object)edB == null)
      {
        ret = true;
      }
      else
      {
        ret = (edA.Name != edB.Name || edA.Index != edB.Index); 
      }
      return ret;
    }

    /// <summary>
    /// Expresses this object in terms of its name field stripped of trailing whitespaces
    /// </summary>
    /// <returns>name string</returns>
    new public string ToString()
    {
      return Name.Trim();
    }
  }
}
