using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using DSProcessor;
using DSProcessor.DSProcessor;

namespace Delay
{
  public class Delay : EffectBase
  {
    private int bufPos, delay, rate, delay_s, delayBufferSize;
    private long decayInt;
    private float decayMS, s;
    private float[] delayBuffer;

    //private long k;
    private int /*i,*/ j;

    public override void Setup(EffectConfiguration config)
    {
      bufPos = 0;
      decayMS = Int32.Parse(config["decayMS"]);
      rate = Int32.Parse(config["rate"]);
      delay = Int32.Parse(config["delay"]);
      delay_s = SampleDelay(delay);
      decayInt = decayInt * 128;
      delayBufferSize = (int)(((int)(DSProcessorController.Instance.DriverInfo.SampleRate / 1000)) * decayMS);
      delayBuffer = new float[delayBufferSize];
      for (int i = 0; i < delayBufferSize; i++)
      {
        delayBuffer[i] = 0;
      }
    }

    private int SampleDelay(int delay)
    {
      float ret;
      if (delay <= 0)
      {
        ret = 0.0f;
      }
      else
      { 
        ret = (float)((delay * (float)rate)/1000.0);
      }
      return (int)ret;
    }

    protected override void Transform(BlueWave.Interop.Asio.Channel buffer)
    {
      int BLOCK_LEN = buffer.BufferSize;
      for (int i = 0; i < BLOCK_LEN; i++)
      {
        if (bufPos > delay_s)
        {
          j = bufPos - delay_s;
        }
        else
        {
          j = delayBufferSize - 1 - delay_s + bufPos;
        }

        s = (long)delayBuffer[j] * decayInt;
        s = s / 128;

        s = buffer[i] + s;

        delayBuffer[bufPos] = s;
        buffer[i] = s;

        bufPos++;
        if (bufPos > delayBufferSize - 1)
        {
          bufPos = 0;
        }
      }
    }
  }
}
