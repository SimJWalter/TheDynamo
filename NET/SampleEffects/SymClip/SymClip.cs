using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using BlueWave.Interop.Asio;

namespace SymClip
{
  public class SymClip : EffectBase
  {
    double th;
    int N;
    float[] y;

    public override void Setup(EffectConfiguration config)
    {
      th = 1 / (int)(float.Parse(config["Clip_Threshold"]));
      N = this.DriverInf.BufferPreferredSize;
      y = new float[N];
    }

    protected override void Transform(Channel buffer)
    {
      for (int i = 0; i < N; i++)
      {
        if (Math.Abs(buffer[i]) < th)
        {
          y[i] = 2 * buffer[i];
        }

        if (Math.Abs(buffer[i]) >= th)
        {
          if (buffer[i] > 0)
          {
            y[i] = (float)Math.Pow(3 - (2 - buffer[i] * 3), 2) / 3;
          }
          if (buffer[i] < 0)
          {
            y[i] = (float)(-(Math.Pow(3 - (2 - Math.Abs(buffer[i]) * 3), 2) / 3));
          }
        }

        if (Math.Abs(buffer[i]) > (2 * th))
        {
          if (buffer[i] > 0)
          {
            y[i] = 1;
          }
          if (buffer[i] < 0)
          {
            y[i] = -1;
          }
        }
      }

      for (int i = 0; i < buffer.BufferSize; i++)
      {
        buffer[i] = y[i];
        y[i] = 0;
      }
    }
  }
}
