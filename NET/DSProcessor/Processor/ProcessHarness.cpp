#include "stdafx.h"
#include "Processor.h"

using namespace System;

namespace Processor
{
	array<InstalledDriver^>^ ProcessHarness::GetDriverDetails()
	{
		return AsioDriver::InstalledDrivers;
	}
	
	void ProcessHarness::Destroy()
	{
		if(this->IsRunning)
		{
			this->Stop();
		}
		this->_bank = nullptr;	
		if(this->_driver != nullptr)
		{
			this->_driver->Release();
			this->_driver = nullptr;
		}		
	}

	void ProcessHarness::SelectDriver(InstalledDriver^ driver)
	{
		if(driver == nullptr)
		{
			throw gcnew ArgumentNullException("Invalid driver");
		}
		this->_driver = AsioDriver::SelectDriver(driver);
		this->_driver->CreateBuffers(false);
		_driver->BufferUpdate += gcnew EventHandler(this, &ProcessHarness::BufferUpdate);
		_driver->ShowControlPanel();		
	}

	DriverInfo^ ProcessHarness::GetCurrentDriverInfo()
	{
		DriverInfo^ ret = nullptr;
		if(this->_driver != nullptr)
		{
			ret = gcnew DriverInfo();
			ret->Name = _driver->DriverName;
			ret->Version = _driver->Version;
			ret->InputChannelCount = _driver->InputChannelCount;
			ret->OutputChannelCount = _driver->OutputChannelCount;
			ret->BufferMaxSize = _driver->BufferSizex->MaxSize;
			ret->BufferMinSize = _driver->BufferSizex->MinSize;
			ret->BufferPreferredSize = _driver->BufferSizex->PreferredSize;
			ret->Granularity = _driver->BufferSizex->Granularity;
			ret->SampleRate = _driver->SampleRate;			
		}
		return ret; 
	}

	void ProcessHarness::LoadBank(Designer::Entity::EffectBank ^bankDescriptor)
	{
		if(_driver == nullptr)
		{
			throw gcnew ProcessHarnessException("You must select a driver before attempting to load up an effect bank because the effects need information about the drivers in order to be instantiated.");
		}
		else if(bankDescriptor == nullptr)
		{
			throw gcnew ArgumentNullException("Invalid bank descriptor object");
		}
		else
		{
			EffectBankFactory^ factory = gcnew EffectBankFactory(GetCurrentDriverInfo());
			this->_bank = factory->BuildBank(bankDescriptor);
		}
	}
	
	void ProcessHarness::Go()
	{
		_driver->Start();
		_isRunning = true;
	}	

	void ProcessHarness::Stop()
	{
		_driver->Stop();
		_isRunning = false;
	}

	bool ProcessHarness::IsRunning::get()
	{
		return _isRunning;
	}

	void ProcessHarness::BufferUpdate(System::Object ^sender, System::EventArgs ^e)
	{
		AsioDriver^ driver = dynamic_cast<AsioDriver^>(sender);
		if(driver)
		{
			Channel ^input = driver->InputChannels[0];
			Channel ^leftOutput = driver->OutputChannels[0];
			Channel ^rightOutput = driver->OutputChannels[1];
			this->_bank->HandleToCurrent->Process(input);
			for(int i = 0; i < input->BufferSize; i++)
			{
				leftOutput[i] = input[i];
				rightOutput[i] = input[i];
			}
		}		
	}

	void ProcessHarness::SwitchToNext()
	{
		this->_bank->MoveNext();
	}		

	void ProcessHarness::ResetBank()
	{
		this->_bank->Reset();
	}
}