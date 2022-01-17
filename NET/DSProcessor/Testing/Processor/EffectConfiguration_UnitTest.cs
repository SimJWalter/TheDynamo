using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Processor
{
  [TestFixture()]
  public class EffectConfiguration_UnitTest
  {
    [SetUp()]
    public void Init()
    {
      //common refresh code here
    }

    [TearDown()]
    public void DisposeT()
    {
      //release resources
    }

    [Test()]
    public void InstantiateValid()
    { }

    [Test()]
    public void AddNullKey()
    { }

    [Test()]
    public void AddNullValue()
    { }

    [Test()]
    public void AddValid()
    { }

    [Test()]
    public void DefaultNullKey()
    { }

    [Test()]
    public void DefaultValid()
    { }
  }
}
