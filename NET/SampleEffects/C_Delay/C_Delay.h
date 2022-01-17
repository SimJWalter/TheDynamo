// C_Delay.h

#pragma once

using namespace System;
using namespace BlueWave::Interop::Asio;
using namespace Processor;

namespace C_Delay {

	public ref class C_Delay : Processor::EffectBase
	{
	protected:
		virtual void Transform(Channel^ buffer) override;

	public:
		virtual void Setup(EffectConfiguration^ config) override;
	};
}
