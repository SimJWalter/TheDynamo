#pragma once
#pragma managed

using namespace System;
using namespace System::Collections;

namespace Processor
{
	public ref class EffectConfiguration
	{
	private:
		Hashtable^ _properties;
		int _sampleRate, _bufferSize;

	public:
		EffectConfiguration(int sampleRate, int bufferSize, Hashtable^ properties);
		String^ GetValueByKey(String^ key);
		property int BufferSize{ int get(); };
		property int SampleRate{ int get(); };

		ref class EffectConfigurationException : public Exception
		{
		public:
			EffectConfigurationException(String^ message);
		};
	};
}