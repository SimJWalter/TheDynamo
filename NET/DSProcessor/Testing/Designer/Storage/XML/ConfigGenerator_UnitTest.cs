using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Designer.Storage.XML
{
  public class ConfigGenerator_UnitTest
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
    public void ParseConfigNoName()
    { }

    [Test()]
    public void ParseConfigInvalidDocStruct()
    { }

    [Test()]
    public void ParseConfigValid()
    { }

    [Test()]
    public void GenerateConfigDoc_WithStart_Valid()
    { }

    [Test()]
    public void GenerateConfigDoc_WithoutStart_Valid()
    { }
  }
}
