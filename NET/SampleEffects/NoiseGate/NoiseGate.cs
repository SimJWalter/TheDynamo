using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using BlueWave.Interop.Asio;

namespace NoiseGate
{
  public class NoiseGate : EffectBase
  {
    /// <summary>
    /// The point below which any non-negative sample values must be processed for zero-ing or fading out
    /// </summary>
    private float threshold;

    /// <summary>
    /// The number of milliseconds the fade-out is required to last
    /// </summary>
    private int decay_time_ms;

    /// <summary>
    /// The number of milliseconds that must have elasped while the samples are witin the zero-able threshold
    /// </summary>
    private int attack_point_ms;

    /// <summary>
    /// The sample rate used by the driver
    /// </summary>
    private int sample_rate;
    /// <summary>
    /// The number of samples that are taken each millisecond
    /// </summary>
    private float samples_per_ms;

    /// <summary>
    /// The level which the samples much reach inorder for the gate to be reopened
    /// </summary>
    private float attack_passthrough_level;

    private bool attack_check;
    private int ms_sample_count;
    private int ms_count;

    private int fade_ms_count;
    private int fade_sample_count;

    public override void Setup(EffectConfiguration config)
    {
      threshold = float.Parse(config["Threshold"]);
      decay_time_ms = int.Parse(config["Decay_Time_Ms"]);
      attack_point_ms = int.Parse(config["Attack_Point_Ms"]);
      attack_passthrough_level = float.Parse(config["Attack_Passthrough_Level"]);
      sample_rate = (int)this.DriverInf.SampleRate;
      samples_per_ms = sample_rate / 1000;


      ms_sample_count = 0;
      fade_sample_count = 0;
      ms_count = attack_point_ms;
      fade_ms_count = decay_time_ms;      
      attack_check = true;
    }
    
    protected override void Transform(Channel buffer)
    {
      for (int i = 0; i < buffer.BufferSize; i++)
      {
        // is the signal lower than the gate?
        if (buffer[i] > -threshold && buffer[i] < threshold)
        {
          // has the required window time passed for the fade out to occur?
          if (ms_count >= attack_point_ms)
          {
            // process fade out
            if (fade_ms_count >= decay_time_ms) //if the fade has completed
            {
              buffer[i] = 0; //zero the sample and 
              attack_check = true; // set the attack-check
            }
            else
            {
              //process through the fade
              if (fade_sample_count == samples_per_ms)
              {
                fade_ms_count++;
                fade_sample_count = 0;
              }
              fade_sample_count++;

              if (fade_ms_count >= attack_point_ms) // if the fade attack point has been reached
              {
                // the number of samples through the decay_time 
                float x = ((fade_ms_count - attack_point_ms) * samples_per_ms) + fade_sample_count;
                // as a percentage
                float y = ((decay_time_ms * samples_per_ms) / 100) * x;
                // to limit the sample volume
                buffer[i] = (buffer[i] / 100) * y;
              }
            }
          }
          else //the fade attack point has not been reached so update the counters and leave the samples be
          {
            if (ms_sample_count == samples_per_ms)
            {
              ms_count++;
              ms_sample_count = 0;
            }
            ms_sample_count++;
          }
        }
        else // the signal is valid for the gate to be open
        {
          ms_sample_count = 0; //reset the counters
          ms_count = 0;
          fade_ms_count = 0;
          fade_sample_count = 0;
          if (attack_check) // if the signal has been muted then we must wait for the sample level to reach outside the passthrough threshold to reopen the gate
          {
            if (buffer[i] < attack_passthrough_level && buffer[i] > -attack_passthrough_level)
            {
              buffer[i] = 0;
            }
            else
            {
              attack_check = false;
            }
          }
        }
      }
    }
  }
}
