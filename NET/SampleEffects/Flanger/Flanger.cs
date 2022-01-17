using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using BlueWave.Interop.Asio;

namespace Flanger
{
  public class Flanger : EffectBase
  {

    public override void Setup(EffectConfiguration config)
    {
      
    }

    protected override void Transform(Channel buffer)
    {
      float[] newOut = new float[((int)(buffer.BufferSize/2)) + 1];

      for (int i = 0; i < buffer.BufferSize; i++)
      {
        if (i % 2 == 0)
        {
          newOut[i / 2] = buffer[i];
        }
      }

      for (int i = 0; i < buffer.BufferSize; i++)
      {
        buffer[i] = 0;
      }

      for (int i = 0; i < newOut.Length; i++)
      {
        buffer[i] = newOut[i];
      }
    }
  }
}
