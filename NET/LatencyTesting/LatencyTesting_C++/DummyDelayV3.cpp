#include "DummyDelayV3.h"
#include "EffectConfiguration.h"

using namespace System;

namespace LatencyTesting
{
	void DummyDelayV3::Setup(LatencyTesting::EffectConfiguration ^config)
	{
		tapQuantity = Int32::Parse(config->GetValueByKey("tapQuantity"));
		delayLength = Int32::Parse(config->GetValueByKey("delayLength"));
		if(tapQuantity < 1)
		{
			throw gcnew DummyDelayException("Cannot configure a delay unit with less than 1 tap on it.");
		}
		int bufferSize = config->BufferSize;
		int sampleRate = config->SampleRate;
		delayGap = (int)((sampleRate / 1000) * (delayLength / tapQuantity));
		int volumeDiff = (int)(100 / (tapQuantity + 1));

		delayBuffer = gcnew array<float, 1>(bufferSize + (delayGap * tapQuantity));
		copyTo = delayBuffer->Length - bufferSize - 1;
		readFrom = gcnew array<int, 1>(tapQuantity);
		volumeDifferential = gcnew array<int, 1>(tapQuantity);

		for (int i = 0; i < tapQuantity; i++)
		{
			if (i > 0)
			{
				readFrom[i] = (i * delayGap) - 1;
			}
			else
			{
				readFrom[i] = 0;
			}

			volumeDifferential[i] = 100 - (volumeDiff * (i + 1));
		}
	}

	void DummyDelayV3::Transform(array<float, 1> ^buffer)
	{
		if (delayGap < 1)
		{
			return;
		}

		// loop once for each sample in the buffer
		for (int i = 0; i < buffer->Length; i++)
		{
			// copy out original sample to delayBuffer for replaying
			delayBuffer[copyTo] = buffer[i];
			// increment and test copyTo index
			if (++copyTo > (delayBuffer->Length - 1))
			{
				copyTo = 0;
			}

			// run through tap-offs
			for (int x = 0; x < tapQuantity; x++)
			{
				buffer[i] = (float)(Math::Sqrt(
					(
						(((buffer[i] + CORR) * (buffer[i] + CORR)) +
						((((delayBuffer[readFrom[x]] / 100) * volumeDifferential[x]) + CORR) *
							(((delayBuffer[readFrom[x]] / 100) * volumeDifferential[x]) + CORR))
					))
					)
					) - CORR;
				/*buffer[i] = (float)Math::Sqrt(((buffer[i] * buffer[i]) +
				(((delayBuffer[readFrom[x]] / 100) * volumeDifferential[x]) * ((delayBuffer[readFrom[x]] / 100) * volumeDifferential[x]))) / 2);*/

				if (++readFrom[x] > delayBuffer->Length - 1)
				{
					readFrom[x] = 0;
				}
			}
		}
	}

	DummyDelayV3::DummyDelayException::DummyDelayException(System::String ^message) : Exception(message)
	{}
}