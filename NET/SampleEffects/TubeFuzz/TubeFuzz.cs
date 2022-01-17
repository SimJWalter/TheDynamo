using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using Utils;

namespace TubeFuzz
{
  public class TubeFuzz : EffectBase
  {
    private float gain;
    private float Q;
    private int dist;
    private float mix;
    
    private float RH;
    private float RL;

    private float[] inProcess_Q;
    private float[] inProcess_Z;
    private float[] inProcess_Y;

    private float[] HP_b;
    private float[] HP_a;
    private float[] LP_b;
    private float[] LP_a;

    public override void Setup(EffectConfiguration config)
    {
      gain = float.Parse(config["Distortion_Gain"]);
      Q = float.Parse(config["Amplitude_Attack_Point"]);
      dist = int.Parse(config["Distortion_Clip_Hardness"]);
      mix = float.Parse(config["Mix"]);
      RH = 0.5f;
      RL = 0.5f;
      inProcess_Q = new float[this.DriverInf.BufferPreferredSize];
      inProcess_Z = new float[this.DriverInf.BufferPreferredSize];
      inProcess_Y = new float[this.DriverInf.BufferPreferredSize];

      HP_b = new float[3];
      HP_b[0] = 1;
      HP_b[1] = -2;
      HP_b[2] = 1;

      HP_a = new float[3];
      HP_a[0] = 1;
      HP_a[1] = -2 * RH;
      HP_a[2] = (float)Math.Pow((double)RH, 2.0);

      LP_b = new float[1];
      LP_b[0] = 1 - RL;

      LP_a = new float[2];
      LP_a[0] = 1;
      LP_a[1] = -RL;
    }

    protected override void Transform(BlueWave.Interop.Asio.Channel buffer)
    {
      float maxAbs_x = 0.0f;
      for(int i = 0; i < buffer.BufferSize; i++)
      {
        float t = Math.Abs(buffer[i]);
        if(t > maxAbs_x)
        {
          maxAbs_x = t;
        }
      }

      for (int i = 0; i < buffer.BufferSize; i++)
      {
        inProcess_Q[i] = (buffer[i] * gain) / maxAbs_x;
      }

      if (Q == 0)
      {
        for (int i = 0; i < inProcess_Q.Length; i++)
        {
          inProcess_Z[i] = (float)(inProcess_Q[i] / (1 - (Math.Exp(-dist * inProcess_Q[i]))));
        }
        for (int i = 0; i < inProcess_Q.Length; i++)
        {
          if (inProcess_Q[i] == Q)
          {
            inProcess_Z[i] = 1 / dist;
          }
        }
      }
      else
      {
        for (int i = 0; i < inProcess_Z.Length; i++)
        {
          inProcess_Z[i] = (float)((inProcess_Q[i] - Q) / (1 - Math.Exp(-dist * (inProcess_Q[i] - Q))) + Q / (1 - Math.Exp(dist * Q)));

          if (inProcess_Q[i] == Q)
          {
            inProcess_Z[i] = (float)(1 / dist + Q / (1 - Math.Exp(dist * Q)));
          }
        }
      }
      float maxAbs_z = 0.0f;
      for(int i = 0; i < inProcess_Z.Length; i++)
      {
        float t = Math.Abs(inProcess_Z[i]);
        if(t > maxAbs_z)
        {
          maxAbs_z = t;
        }
      }

      float maxAbs_y = 0;
      for (int i = 0; i < inProcess_Y.Length; i++)
      {
        inProcess_Y[i] = mix * 2 * maxAbs_x / maxAbs_z + (1 - mix) * buffer[i];
        for (int j = 0; j < inProcess_Y.Length; j++)
        {
          float t = Math.Abs(inProcess_Y[j]);
          if (t > maxAbs_y)
          {
            maxAbs_y = t;
          }
        }
        inProcess_Y[i] = inProcess_Y[i] * maxAbs_x / maxAbs_y;
      }

      //inProcess_Y = this.MatlabFilter(HP_b, HP_a, inProcess_Y);
      //inProcess_Y = this.MatlabFilter(LP_b, LP_a, inProcess_Y);

      for (int i = 0; i < inProcess_Y.Length; i++)
      {
        buffer[i] = inProcess_Y[i];
      }
    }
  }
}
