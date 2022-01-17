#pragma once
#pragma managed

#include "EffectBase.h"

using namespace System;

namespace LatencyTesting
{
	public ref class DummyDelayV3 : public EffectBase
	{
	private:
		static const int CORR = 0;

		int tapQuantity, delayLength, delayGap, copyTo;
		array<float, 1>^ delayBuffer;
		array<int, 1>^ volumeDifferential;
		array<int, 1>^ readFrom;

	protected:
		virtual void Transform(array<float>^ buffer) override;

	public:
		virtual void Setup(EffectConfiguration^ config) override;

		ref class DummyDelayException : public Exception
		{
		public:
			DummyDelayException(String^ message);
		};
	};
}