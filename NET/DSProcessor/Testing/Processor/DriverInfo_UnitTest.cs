using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Testing.Processor
{
  [TestFixture()]
  public class DriverInfo_UnitTest
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
    public void NameIsReadOnly()
    { }

    [Test()]
    public void GetNameIsValid()
    { }

    [Test()]
    public void VersionValid()
    { }

    [Test()]
    public void InputChannelCountValid()
    { }

    [Test()]
    public void OutputChannelCountValid()
    { }

    [Test()]
    public void BufferMinSizeValid()
    { }

    [Test()]
    public void BufferMaxSizeValid()
    { }

    [Test()]
    public void BufferPreferredSizeValid()
    { }

    [Test()]
    public void GranularityValid()
    { }
    
    [Test()]
    public void SampleRateValid()
    { }
    
  }
}
