using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;

namespace ExpFuzz
{
  public class ExpFuzz : EffectBase
  {
    private float gain;
    private float mix;
    private float[] inProcess_q;
    private float[] inProcess_z;
    private float[] inProcess_y;

    public override void Setup(EffectConfiguration config)
    {
      gain = float.Parse(config["Gain"]);
      mix = float.Parse(config["Mix"]);
      inProcess_q = new float[DriverInf.BufferPreferredSize];
      inProcess_z = new float[DriverInf.BufferPreferredSize];
      inProcess_y = new float[DriverInf.BufferPreferredSize];
    }

    protected override void Transform(BlueWave.Interop.Asio.Channel buffer)
    {
      float maxAbs_x = 0.0f;
      for (int i = 0; i < buffer.BufferSize; i++)
      {
        float t = Math.Abs(buffer[i]);
        if (t > maxAbs_x)
        {
          maxAbs_x = t;
        }
      }
      for (int i = 0; i < buffer.BufferSize; i++)
      {
        inProcess_q[i] = buffer[i] * gain / maxAbs_x;
      }

      for (int i = 0; i < buffer.BufferSize; i++)
      {
        if (-inProcess_q[i] > 0)
        {
          inProcess_z[i] = 1.0f;
        }
        else if (-inProcess_q[i] < 0)
        {
          inProcess_z[i] = -1.0f;
        }
        else
        {
          inProcess_z[i] = 0.0f;
        }

        inProcess_z[i] = (float)(inProcess_z[i] * (1 - Math.Exp(inProcess_z[i] * inProcess_q[i])));
      }

      float maxAbs_z = 0.0f;
      for (int i = 0; i < buffer.BufferSize; i++)
      {
        float t = Math.Abs(inProcess_z[i]);
        if (t > maxAbs_z)
        {
          maxAbs_z = t;
        }
      }

      for (int i = 0; i < buffer.BufferSize; i++)
      {
        inProcess_y[i] = mix * inProcess_z[i] * maxAbs_x / maxAbs_z + (1 - mix) * buffer[i];
      }

      float maxAbs_y = 0.0f;
      for (int i = 0; i < buffer.BufferSize; i++)
      {
        float t = Math.Abs(inProcess_y[i]);
        if (t > maxAbs_y)
        {
          maxAbs_y = t;
        }
      }

      for (int i = 0; i < buffer.BufferSize; i++)
      {
        buffer[i] = inProcess_y[i] * maxAbs_x / maxAbs_y;
      }
    }
  }
}
