using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Designer.Storage.XML
{
  [TestFixture()]
  public class ChainGenerator_UnitTest
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
    public void ParseChainNoName()
    { }

    [Test()]
    public void ParseChainInvalidDocStruct()
    { }

    [Test()]
    public void ParseChainValid()
    { }

    [Test()]
    public void GenerateChainDoc_WithStart_Valid()
    { }

    [Test()]
    public void GenerateChainDoc_WithoutStart_Valid()
    { }
  }
}
