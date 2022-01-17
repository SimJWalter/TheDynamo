using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using DSProcessor;
using DSProcessor.DSProcessor;

namespace Vibrate
{
  public class Vibrate : EffectBase
  {
    private int samplesPerMS;
    private int onLength;
    private int offLength;
    private bool on;

    private int msPos;
    private int switchPos;

    public override void Setup(EffectConfiguration config)
    {
      samplesPerMS = (int)(this.DriverInf.SampleRate / 1000);
      onLength = Int32.Parse(config["onMS"]);
      offLength = Int32.Parse(config["offMS"]);
      on = true;
      msPos = 0;
      switchPos = 0;
    }

    protected override void Transform(BlueWave.Interop.Asio.Channel buffer)
    {
      for (int i = 0; i < buffer.BufferSize; i++)
      {
        if (!on)
        {
          buffer[i] = 0;
        }

        msPos++;
        if (msPos >= samplesPerMS)
        {
          msPos = 0;
          switchPos++;
          if ((on && switchPos >= onLength) || (!on && switchPos >= offLength))
          {
            on = !on;
            switchPos = 0;
          }
        }
      }
    }
  }
}
