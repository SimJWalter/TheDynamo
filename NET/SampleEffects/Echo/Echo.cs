using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using DSProcessor;
using DSProcessor.DSProcessor;
using Microsoft.DirectX.DirectSound;

namespace Echo
{
  public class Echo : EffectBase
  { 
    private bool isSet = false;
    private float[] processBuffer;
    private int[] processPointers;
    private int nextWritePoint;

    public override void Setup(EffectConfiguration config)
    {
      try
      {
        float delay = float.Parse(config["DelayMS"]);
        float decay = float.Parse(config["DecayMS"]);

        if(delay < decay)
        {
          int samplesPerMs = (int)(this.DriverInf.SampleRate / 1000);
          int delayInSamples = (int)(delay * samplesPerMs); 
          int decayInSamples = (int)(decay * samplesPerMs);
          
          int processBufferSize = this.DriverInf.BufferPreferredSize + decayInSamples;
          processBuffer = new float[processBufferSize];

          int pointerCount = (int)(decayInSamples / delayInSamples);
          processPointers = new int[pointerCount];
          for (int i = 0; i < processPointers.Length; i++)
          {
            processPointers[i] = processBufferSize - (delayInSamples * (i + 1));// this.DriverInf.BufferPreferredSize + decayInSamples //delayInSamples * (i + 1);
          }

          nextWritePoint = 0;
          isSet = true;
        }        
      }
      catch (Exception e){ }
    }

    protected override void Transform(BlueWave.Interop.Asio.Channel buffer)
    {
      if (isSet)
      {
        for (int i = 0; i < buffer.BufferSize; i++)
        {
          //calculate new sample value
          float newSampleValue = buffer[i];
          for (int j = 0; j < processPointers.Length; j++)
          {
            newSampleValue += processBuffer[processPointers[j]] / (j+2);
            //increment all echo pointers
            processPointers[j]++;
            if (processPointers[j] >= processBuffer.Length - 1)
            {
              processPointers[j] = 0;
            }
          }

          //set the next process sample
          processBuffer[nextWritePoint] = buffer[i];
          nextWritePoint++;
          if (nextWritePoint >= processBuffer.Length - 1)
          {
            nextWritePoint = 0;
          }

          //assign new value to output buffer index point
          buffer[i] = newSampleValue;
        }        
      }      
    }
  }
}
