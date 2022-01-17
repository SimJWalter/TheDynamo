#pragma once
#pragma managed

#include "EffectConfiguration.h"
#include "DriverInfo.h"

using namespace System;
using namespace BlueWave::Interop::Asio;

namespace Processor
{
	// Abstract base class for use in creating effect modules and chaining them together
	public ref class EffectBase abstract
	{
	private:
		EffectBase^ _decorate;
		DriverInfo^ _dInfo;

	protected:
		// called by the Process method when the end of the chain has been exhausted
		virtual void Transform(Channel^ buffer) = 0;

		array<float>^ MatlabFilter(array<float>^ b, array<float>^ a, array<float>^ x);

	public:
		//force single constructor
		EffectBase(); 

		//setup the effect module ready for processing
		virtual void Setup(EffectConfiguration^ config) = 0;

		//call to execute chain
		void Process(Channel^ buffer);

		//call Set to nest an effect on the end of the chain
		property EffectBase^ Decorator { void set(EffectBase^ value); };	

		//retrieve the driver details for such fields as samplerate
		property DriverInfo^ DriverInf { DriverInfo^ get(); void set(DriverInfo^ value); };

	};
}