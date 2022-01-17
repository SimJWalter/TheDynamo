#include "stdafx.h"
#include "DriverInfo.h"

namespace Processor
{
	int DriverInfo::BufferMaxSize::get()
	{
		int ret = _bufferMaxSize;
		return ret;
	}

	void DriverInfo::BufferMaxSize::set(int value)
	{
		_bufferMaxSize = value;
	}

	int DriverInfo::BufferMinSize::get()
	{
		int ret = _bufferMinSize;
		return ret;
	}

	void DriverInfo::BufferMinSize::set(int value)
	{
		_bufferMinSize = value;
	}

	int DriverInfo::BufferPreferredSize::get()
	{
		int ret = _bufferPreferredSize;
		return ret;
	}

	void DriverInfo::BufferPreferredSize::set(int value)
	{
		_bufferPreferredSize = value;
	}

	int DriverInfo::Granularity::get()
	{
		int ret = _granularity;
		return ret;
	}

	void DriverInfo::Granularity::set(int value)
	{
		_granularity = value;
	}

	int DriverInfo::InputChannelCount::get()
	{
		int ret = _inChannelCount;
		return ret;
	}

	void DriverInfo::InputChannelCount::set(int value)
	{
		_inChannelCount = value;
	}

	String^ DriverInfo::Name::get()
	{
		String^ ret = (String^)(_name->Clone());
		return ret;
	}

	void DriverInfo::Name::set(String^ value)
	{
		_name = value;
	}

	int DriverInfo::OutputChannelCount::get()
	{
		int ret = _outChannelCount;
		return ret;
	}

	void DriverInfo::OutputChannelCount::set(int value)
	{
		_outChannelCount = value;
	}

	double DriverInfo::SampleRate::get()
	{
		double ret = _sampleRate;
		return ret;
	}

	void DriverInfo::SampleRate::set(double value)
	{
		_sampleRate = value;
	}

	int DriverInfo::Version::get()
	{
		int ret = _version;
		return ret;
	}

	void DriverInfo::Version::set(int value)
	{
		_version = value;
	}
}