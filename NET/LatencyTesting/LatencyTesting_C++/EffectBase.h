#pragma once
#pragma managed

#include "EffectConfiguration.h"

using namespace System;

namespace Processor
{
	public ref class EffectBase abstract
	{
	private:
		EffectBase^ _decorate;
	
	protected:
		// called by the Process method when the end of the chain has been exhausted
		virtual void Transform(array<float>^ buffer) = 0;

	public:
		//default constructor
		EffectBase();
		
		//setup the effect module ready for processing
		virtual void Setup(EffectConfiguration^ config) = 0;

		//call to execute chain
		void Process(array<float>^ buffer);

		//call Set to nest an effect on the end of the chain
		property EffectBase^ Decorator { void set(EffectBase^ value); };		
	};
}