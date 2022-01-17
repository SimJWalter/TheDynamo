using System;
using System.Collections.Generic;
using System.Text;

namespace LatencyTesting
{
    public class DummyDelayV3 : EffectBase
    {
        private const int CORR = 0; 
        
        private int tapQuantity;
        private int delayLength;
        private int delayGap;

        private float[] delayBuffer;
        private int copyTo;
        private int[] volumeDifferential;
        private int[] readFrom;

        public override void Setup(EffectConfiguration config)
        {
            tapQuantity = Int32.Parse(config.GetValueByKey("tapQuantity"));
            delayLength = Int32.Parse(config.GetValueByKey("delayLength"));
            if (tapQuantity < 1)
            {
                throw new DummyDelayException(
                        "Cannot configure a delay unit with less than 1 tap on it");
            }

            int bufferSize = config.BufferSize;
            int sampleRate = config.SampleRate;
            delayGap = (int)((sampleRate / 1000) * (delayLength / tapQuantity));
            int volumeDiff = (int)(100 / (tapQuantity + 1));

            delayBuffer = new float[bufferSize + (delayGap * tapQuantity)];
            copyTo = delayBuffer.Length - bufferSize - 1;
            readFrom = new int[tapQuantity];
            volumeDifferential = new int[tapQuantity];

            for (int i = 0; i < tapQuantity; i++)
            {
                if (i > 0)
                {
                    readFrom[i] = (i * delayGap) - 1;
                }
                else
                {
                    readFrom[i] = 0;
                }

                volumeDifferential[i] = 100 - (volumeDiff * (i + 1));
            }
        }

        protected override void Transform(float[] buffer)
        {
            if (delayGap < 1)
            {
                return;
            }

            // loop once for each sample in the buffer
            for (int i = 0; i < buffer.Length; i++)
            {
                // copy out original sample to delayBuffer for replaying
                delayBuffer[copyTo] = buffer[i];
                // increment and test copyTo index
                if (++copyTo > (delayBuffer.Length - 1))
                {
                    copyTo = 0;
                }

                // run through tap-offs
                for (int x = 0; x < tapQuantity; x++)
                {
                    buffer[i] = (float)(Math.Sqrt(
                                    (
                                    (((buffer[i] + CORR) * (buffer[i] + CORR)) +
                                    ((((delayBuffer[readFrom[x]] / 100) * volumeDifferential[x]) + CORR) *
                                            (((delayBuffer[readFrom[x]] / 100) * volumeDifferential[x]) + CORR))
                                    ))
                                    )
                                ) - CORR;
                    
                    //buffer[i] = (float)Math.Sqrt(((buffer[i] * buffer[i]) +
                    //        (((delayBuffer[readFrom[x]] / 100) * volumeDifferential[x]) * ((delayBuffer[readFrom[x]] / 100) * volumeDifferential[x]))) / 2);

                    if (++readFrom[x] > delayBuffer.Length - 1)
                    {
                        readFrom[x] = 0;
                    }
                }
            }
        }

        public class DummyDelayException : Exception
        {
            public DummyDelayException(String msg)
                : base(msg)
            { }
        }
    }
}
