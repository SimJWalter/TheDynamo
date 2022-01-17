using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Designer.Storage.XML
{
  [TestFixture()]
  public class BankGenerator_UnitTest
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
    public void ParseBankNoName()
    { }

    [Test()]
    public void ParseBankInvalidDocStruct()
    { }

    [Test()]
    public void ParseBankValid()
    { }

    [Test()]
    public void GenerateBankDoc_WithStart_Valid()
    { }

    [Test()]
    public void GenerateBankDoc_WithoutStart_Valid()
    { }
  }
}
