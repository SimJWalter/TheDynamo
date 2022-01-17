using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Designer.Storage
{
  [TestFixture()]
  public class EffectBankStorage_UnitTest
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
    public void FetchBanksNonePresent()
    { }

    [Test()]
    public void FetchBanksMultiple_WithDuplicate()
    { }

    [Test()]
    public void FetchBanksMultipleValid()
    { }

    [Test()]
    public void SaveBankNameNullOrBlank()
    { }

    [Test()]
    public void SaveBankFilenameNullOrBlank()
    { }

    [Test()]
    public void SaveBankValid()
    { }

    [Test()]
    public void RetrieveBankNullDetails()
    { }

    [Test()]
    public void RetrieveBankNotAvailable()
    { }

    [Test()]
    public void RetrieveBankValid()
    { }
  }
}
