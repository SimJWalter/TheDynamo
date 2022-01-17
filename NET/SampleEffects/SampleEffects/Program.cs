using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;


namespace SampleEffects
{
  public class Effect1 : EffectBase
  {
    public override void Setup(EffectConfiguration config)
    {
      throw new NotImplementedException();
    }

    protected override void Transform(BlueWave.Interop.Asio.Channel buffer)
    {
      throw new NotImplementedException();
    }
  }

}
