#pragma once
#pragma managed

using namespace System;
using namespace System::Collections;
using namespace System::Collections::Generic;

namespace Processor
{
	public ref class EffectConfiguration
	{
	private:
		Dictionary<String^, String^>^ _properties;

	public:
		EffectConfiguration();
		void Add(String^ key, String^ value);
		property String^ default[String^] { String^ get(String^ key); }

		ref class EffectConfigurationException : public Exception
		{
		public:
			EffectConfigurationException(String^ message) : Exception(message) {};
		};
	};
}