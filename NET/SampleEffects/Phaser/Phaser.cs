using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Processor;
using DSProcessor;
using DSProcessor.DSProcessor;
using System.ComponentModel;

namespace Phaser
{
  public class Phaser : EffectBase
  {

    // media parameters
    private int m_SamplingRate;

    // filter parameters
    private float m_Dry;		// dry mix
    private float m_Wet;		// Wet mix
    private float m_Feedback;	// Feedback gain
    private float m_SweepRate;	// Sweep rate (Hz / s)
    private float m_SweepRange;	// Sweep range (octaves)
    private float m_Frequency;	// Sweep frequency (Hz)

    // filter delays: x = input, y = output
    private float m_x1;
    private float m_x2;
    private float m_x3;
    private float m_x4;

    private float m_y1;
    private float m_y2;
    private float m_y3;
    private float m_y4;

    // phaser state
    private float m_MinWp;
    private float m_MaxWp;
    private float m_Wp;
    private float m_Rate;
    private float m_SweepFac;


    public Phaser()
    {}

    public override void Setup(EffectConfiguration config)
    {
      m_Dry = float.Parse(config["Dryness"]);
      m_Feedback = float.Parse(config["Feedback"]);
      m_Frequency = float.Parse(config["Frequency"]);
      m_SweepRange = float.Parse(config["SweepRange"]);
      m_SweepRate = float.Parse(config["SweepRate"]);
      m_Wet = float.Parse(config["Wetness"]);
      m_SamplingRate = (int)this.DriverInf.SampleRate;
      UpdateParams();
    }

    protected override void Transform(BlueWave.Interop.Asio.Channel buffer)
    {
      for (int i = 0; i < buffer.BufferSize; i++)
      {
        buffer[i] = FilterSample(buffer[i]);
      }        
    }

    public float Dry
    {
      get { return m_Dry; }
      set { m_Dry = value; }
    }

    public float Wet
    {
      get { return m_Wet; }
      set { m_Wet = value; }
    }

    public float Feedback
    {
      get { return m_Feedback; }
      set { m_Feedback = value; }
    }

    //Frequency SweepRange SweepRate
    public float SweepRate
    {
      get { return m_SweepRate; }
      set 
      { 
        m_SweepRate = value; 
        UpdateParams(); 
      }
    }

    public float SweepRange
    {
      get { return m_SweepRange; }
      set
      {
        m_SweepRange = value;
        UpdateParams();
      }
    }

    public float Frequency
    {
      get { return m_Frequency; }
      set
      {
        m_Frequency = value;
        UpdateParams();
      }
    }

    /// <summary>
    /// Apply the effect to one sample
    /// </summary>
    /// <param name="x">Input sample</param>
    /// <returns></returns>
    public float FilterSample(float x)
    {
      float K = (1.0f - m_Wp) / (1.0f + m_Wp);

      float x1 = x + m_Feedback * m_y4;
      m_y1 = K * (m_y1 + x1) - m_x1; // 1st filter
      m_x1 = x1;
      m_y2 = K * (m_y2 + m_y1) - m_x2; // 2nd filter
      m_x2 = m_y1;
      m_y3 = K * (m_y3 + m_y2) - m_x3; // 3rd filter
      m_x3 = m_y2;
      m_y4 = K * (m_y4 + m_y3) - m_x4; // 4th filter
      m_x4 = m_y3;

      float y = m_y4 * m_Wet + x * m_Dry;

      m_Wp *= m_SweepFac;
      if (m_Wp > m_MaxWp)
        m_SweepFac = 1.0f / m_Rate;
      else if (m_Wp < m_MinWp)
        m_SweepFac = m_Rate;

      return y;
    }

    private void UpdateParams()
    {
      m_MinWp = (float)(Math.PI * m_Frequency / m_SamplingRate);
      double Range = Math.Pow(2.0, m_SweepRange);
      m_MaxWp = (float)(Math.PI * m_Frequency * Range / m_SamplingRate);
      m_Rate = (float)Math.Pow(Range, 2.0f * m_SweepRate / m_SamplingRate);

      m_SweepFac = m_Rate;
      m_Wp = m_MinWp;
    }
  }
}
