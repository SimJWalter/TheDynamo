using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using BlueWave.Interop.Asio;
using Utils;

namespace TransformationTester
{
  class Program
  {
    static void Main(string[] args)
    {
      //SymClip.SymClip s = new SymClip.SymClip();
      //s.Setup(new EffectConfiguration());
      //float[] buffer = new float[100];
      //Random rand = new Random();
      //for (int i = 0; i < buffer.Length; i++)
      //{
      //  buffer[i] = (float)rand.NextDouble();
      //}
      //s.Tr_2(buffer);

      //float[] b = new float[4];


      //float[] a = new float[4];


      //b[0] = 0.0007f;
      //b[1] = 0.0021f;
      //b[2] = 0.0021f;
      //b[3] = 0.0007f;
      //a[0] = 1.0000f;
      //a[1] = -2.6236f;
      //a[2] = 2.3147f;
      //a[3] = -0.6855f;

      //float[] x = new float[100];
      //Random r = new Random();
      //for (int i = 0; i < x.Length; i++)
      //{
      //  x[i] = (float)r.NextDouble();
      //  if (r.Next(0, 1) == 1)
      //  {
      //    x[i] = 0 - x[i];
      //  }
      //}

      //float[] test = MatlabFilter.Filter(b, a, x);
      //Console.WriteLine("\tx\t|\ty");
      //for (int i = 0; i < x.Length; i++)
      //{
      //  Console.WriteLine(x[i] + "\t" + test[i]);
      //}
      //Console.ReadKey();

      NoiseGate.NoiseGate ng = new NoiseGate.NoiseGate();
      EffectConfiguration ec = new EffectConfiguration();
      ec.Add("Threshold", "0.0045");
      ec.Add("Decay_Time_Ms", "20");
      ec.Add("Attack_Point_Ms", "2");
      ec.Add("Attack_Passthrough_Level", "0.04");
      ng.Setup(ec);

      //float[] x = new float[100];
      //Random r = new Random();
      //for (int i = 0; i < x.Length; i++)
      //{
      //  x[i] = (float)r.NextDouble();
      //  if (r.Next(0, 1) == 1)
      //  {
      //    x[i] = 0 - x[i];
      //  }
      //}
      
    }
  }
}
