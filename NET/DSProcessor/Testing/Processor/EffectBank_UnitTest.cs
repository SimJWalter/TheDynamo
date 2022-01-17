using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Processor
{
  [TestFixture()]
  public class EffectBank_UnitTest
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
    public void InstantiateNullOrBlankName()
    { }

    [Test()]
    public void InstantiateValid()
    { }

    [Test()]
    public void AppendChainNullOrBlankName()
    { }

    [Test()]
    public void AppendChainNullChain()
    { }

    [Test()]
    public void AppendChainValid()
    { }

    [Test()]
    public void MoveNextValid()
    { }

    [Test()]
    public void ResetValid()
    { }

    [Test()]
    public void NameOfCurrentValid()
    { }

    [Test()]
    public void HandleToCurrentValid()
    { }
  }
}
