#include "stdafx.h"
#include "EffectBase.h"

namespace Processor
{
	EffectBase::EffectBase()
	{
		_decorate = nullptr;
	}
	
	void EffectBase::Process(array<float>^ buffer)
	{
		Transform(buffer);
		if(_decorate != nullptr)
		{
			_decorate->Process(buffer);
		}
	}

	void EffectBase::Decorator::set(EffectBase^ value)
	{
		if(_decorate == nullptr)
		{
			_decorate = value;
		}
		else
		{
			_decorate->Decorator = value;
		}
	}
}