#include "stdafx.h"
#include "EffectBank.h"

using namespace System;

namespace Processor
{
	EffectBank::EffectBank(System::String^ name)
	{
		if(name == nullptr || name == "")
		{
			throw gcnew ArgumentNullException("The name applied to this effect-bank is either null or blank");
		}
		_name = name;
		_chains = gcnew List<EffectBase^>();
		_names = gcnew List<String^>();
		_currentIndex = 0;
	}

	void EffectBank::ApppendChain(EffectBase ^chain, String ^name)
	{
		if(chain == nullptr)
		{
			throw gcnew ArgumentNullException("The chain is unassigned and cannot be used for processing");
		}
		if(name == nullptr || name == "")
		{
			throw gcnew ArgumentNullException("The name to be applied to this effect-bank is invalid");
		}
		_chains->Add(chain);
		_names->Add(name);
	}

	void EffectBank::MoveNext()
	{
		if(++_currentIndex >= _chains->Count)
		{
			_currentIndex = 0;
		}
	}

	void EffectBank::Reset()
	{
		_currentIndex = 0;
	}

	String^ EffectBank::NameOfCurrent::get()
	{
		return _names[_currentIndex];
	}

	EffectBase^ EffectBank::HandleToCurrent::get()
	{
		return _chains[_currentIndex];
	}
}