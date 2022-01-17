using System;
using System.Collections.Generic;
using System.Text;

namespace LatencyTesting
{
    public abstract class EffectBase
    {
        private EffectBase decorate;

        public void Process(float[] buffer)
        {
            Transform(buffer);
            if (decorate != null)
            {
                decorate.Process(buffer);
            }
        }

        public EffectBase Decorator
        {
            set 
            {
                if (decorate == null)
                {
                    decorate = value;
                }
                else
                {
                    decorate.Decorator = value;
                }
            }
        }

        public abstract void Setup(EffectConfiguration config);
        protected abstract void Transform(float[] buffer);
    }
}
