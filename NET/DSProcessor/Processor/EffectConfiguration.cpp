#include "stdafx.h"
#include "EffectConfiguration.h"

namespace Processor
{
	EffectConfiguration::EffectConfiguration()
	{
		this->_properties = gcnew Dictionary<String^, String^>();
	}

	void EffectConfiguration::Add(String^ key, String^ value)
	{
		this->_properties->Add(key, value);
	}

	String^ EffectConfiguration::default::get(String^ key)
	{
		return this->_properties[key];
	}
}