#include "stdafx.h"
#include "EffectConfiguration.h"

namespace Processor
{
	EffectConfiguration::EffectConfiguration(int sampleRate, int bufferSize, System::Collections::Hashtable ^properties)
	{
		_sampleRate = sampleRate;
		_bufferSize = bufferSize;
		_properties = properties;
	}

	String^ EffectConfiguration::GetValueByKey(System::String ^key)
	{
		String^ ret = nullptr;
		IDictionaryEnumerator^ enumerator = _properties->GetEnumerator();
		enumerator->Reset();
		while(enumerator->MoveNext())
		{
			DictionaryEntry ^entry = enumerator->Entry;
			if(entry->Key->ToString() == key->ToString())
			{
				ret = entry->Value->ToString();
				break;
			}
		}
		if(ret == nullptr)
		{
			throw gcnew EffectConfigurationException("Key does not exist in this Config object");
		}
		return ret;
	}

	int EffectConfiguration::BufferSize::get()
	{
		return _bufferSize;
	}

	int EffectConfiguration::SampleRate::get()
	{
		return _sampleRate;
	}

	EffectConfiguration::EffectConfigurationException::EffectConfigurationException(System::String ^message) : Exception(message)
	{}
}