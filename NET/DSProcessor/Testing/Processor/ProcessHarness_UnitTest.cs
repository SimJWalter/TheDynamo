using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Processor
{
  [TestFixture()]
  public class ProcessHarness_UnitTest
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
    public void SelectDriverNull()
    { }

    [Test()]
    public void SelectDriverValid()
    { }

    [Test()]
    public void GetCurrentDriverInfoValid()
    { }

    [Test()]
    public void LoadBankNoDriver()
    { }

    [Test()]
    public void LoadBankNullDescriptor()
    { }

    [Test()]
    public void LoadBankValid()
    { }
  }
}
