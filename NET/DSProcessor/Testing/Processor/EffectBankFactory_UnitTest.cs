using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Processor
{
  [TestFixture()]
  public class EffectBankFactory_UnitTest
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
    public void InstantiateNullDriver()
    { }

    [Test()]
    public void InstantiateValid()
    { }

    [Test()]
    public void BuildBankNullDescriptor()
    { }
    
    [Test()]
    public void BuildBankValid()
    { }
  }
}
