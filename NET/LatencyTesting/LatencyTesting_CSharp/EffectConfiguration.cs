using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace LatencyTesting
{
    public class EffectConfiguration
    {
        private Hashtable properties;
        private int sampleRate, bufferSize;

        public EffectConfiguration(int _sampleRate, int _bufferSize, Hashtable _properties)
        {
            properties = _properties;
            sampleRate = _sampleRate;
            bufferSize = _bufferSize;
        }

        public String GetValueByKey(String _key)
        {
            String ret = null;
            IDictionaryEnumerator enumerator = properties.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                if (enumerator.Entry.Key.ToString() == _key)
                {
                    ret = enumerator.Entry.Value.ToString();
                    break;
                }
            }             
            if(ret == null)
            {
                throw new EffectConfigurationException("No configuration properties exist with that name/key");
            }
            return ret;
        }

        public int BufferSize
        {
            get
            {
                return bufferSize;
            }
        }

        public int SampleRate
        {
            get
            {
                return sampleRate;
            }
        }

        public class EffectConfigurationException : Exception
        {
            public EffectConfigurationException(String msg)
                : base(msg)
            { }
        }
    }
}
