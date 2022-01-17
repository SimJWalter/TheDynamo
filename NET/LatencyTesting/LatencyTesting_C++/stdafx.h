// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "EffectConfiguration.h"
#include "EffectBase.h"
#include "DummyDelayV3.h"
//#include "SampleData.h"

using namespace System;

// TODO: reference additional headers your program requires here

void HandleMe(Exception ^e);

namespace LatencyTesting
{
	public ref class SampleData
	{
	public:
		static property array<double, 1>^ Data{ array<double, 1>^ get(); };
	};
}
