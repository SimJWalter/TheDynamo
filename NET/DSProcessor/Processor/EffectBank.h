#pragma once
#pragma managed
#include "EffectBase.h"

using namespace System;
using namespace System::Collections;
using namespace System::Collections::Generic;

namespace Processor
{
	public ref class EffectBank
	{
	private:
		List<Processor::EffectBase^>^ _chains;
		List<String^>^ _names;
		String^ _name;
		int _currentIndex;

	public:
		EffectBank(String^ name);
		void ApppendChain(EffectBase^ chain, String^ name);
		void MoveNext();		
		property String^ NameOfCurrent{ String^ get(); };		
		property EffectBase^ HandleToCurrent{ EffectBase^ get(); };
		void Reset();
	};
}