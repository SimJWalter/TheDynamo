#include "stdafx.h"
#include "EffectBase.h"

namespace Processor
{
	EffectBase::EffectBase()
	{
			_decorate = nullptr;
			_dInfo = nullptr;
	}

	void EffectBase::Process(Channel^ buffer)
	{
		if(buffer != nullptr)
		{
			Transform(buffer);
			if(_decorate != nullptr)
			{
				_decorate->Process(buffer);
			}
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

	DriverInfo^ EffectBase::DriverInf::get()
	{
		return _dInfo;
	}

	void EffectBase::DriverInf::set(DriverInfo^ value)
	{
		_dInfo = value;
	}

	array<float>^ EffectBase::MatlabFilter(array<float>^ b, array<float>^ a, array<float>^ x)
	{
		int ord = b->Length;
		int np = x->Length;
		array<float>^ y = gcnew array<float>(np);

		y[0] = b[0] * x[0];

		for (int i = 1; i < ord; i++)
		{
			y[i] = 0.0f;
			for (int j = 0; j < i + 1; j++)
			{
				y[i] = y[i] + b[j] * x[i - j];
			}
			for (int j = 0; j < i; j++)
			{
				y[i] = y[i] - a[j + 1] * y[i - j - 1];
			}
		}

		for (int i = ord; i < np; i++)
		{
			y[i] = 0.0f;
			for (int j = 0; j < ord; j++)
			{
				y[i] = y[i] + b[j] * x[i - j];
			}
			for (int j = 0; j < ord-1; j++)
			{
				y[i] = y[i] - a[j + 1] * y[i - j - 1];
			}
		}

		return y;

	}

}