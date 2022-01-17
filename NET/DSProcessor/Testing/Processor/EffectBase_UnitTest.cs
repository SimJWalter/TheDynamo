using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Processor
{
  [TestFixture()]
  public class EffectBase_UnitTest
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
    public void ProcessNullBuffer()
    { }

    [Test()]
    public void ProcessBufferValid()
    { }
  }
}
