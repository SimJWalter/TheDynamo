#pragma once
#pragma managed

using namespace System;

namespace Processor
{
	public ref class DriverInfo
	{
	private:
		String^ _name;
		int _version;
		int _inChannelCount;
		int _outChannelCount;
		int _bufferMinSize;
		int _bufferMaxSize;
		int _bufferPreferredSize;
		int _granularity;
		double _sampleRate;

	public:
		property String^ Name
		{ 
			String^ get(); 
			void set(String^ value);
		};
		
		property int Version
		{ 
			int get(); 
			void set(int value);
		};

		property int InputChannelCount
		{ 
			int get(); 
			void set(int value);
		};

		property int OutputChannelCount
		{ 
			int get(); 
			void set(int value);
		};

		property int BufferMinSize
		{ 
			int get(); 
			void set(int value);
		};

		property int BufferMaxSize
		{ 
			int get(); 
			void set(int value);
		};

		property int BufferPreferredSize
		{ 
			int get(); 
			void set(int value);
		};

		property int Granularity
		{ 
			int get(); 
			void set(int value);
		};

		property double SampleRate
		{ 
			double get(); 
			void set(double value);
		};
	};
}