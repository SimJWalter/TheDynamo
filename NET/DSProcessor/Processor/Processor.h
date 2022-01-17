#pragma once
#pragma managed
#include "stdafx.h"
#include "EffectBank.h"
#include "DriverInfo.h"

using namespace System;
using namespace System::Collections;
using namespace System::IO;
using namespace BlueWave::Interop::Asio;

namespace Processor
{
	// Class for handling the instantiation and live processing effect banks with sound drivers
	public ref class ProcessHarness
	{
	private:
		Processor::EffectBank^ _bank;
		AsioDriver^ _driver;
		bool _isRunning;

	public:	
		// function to tie to the event handler for the buffer filled event in the ASIO driver
		void BufferUpdate(System::Object^ sender, System::EventArgs^ e);

		// release all handles to objects in memory
		void Destroy();

		// inform the caller of what drivers are available on the host system
		array<InstalledDriver^>^ GetDriverDetails();

		// inform the processor of which driver to use for processing
		void SelectDriver(InstalledDriver^ driver);

		// provide details of the selected driver
		DriverInfo^ GetCurrentDriverInfo();

		// instantiate the specified bank of effects
		void LoadBank(Designer::Entity::EffectBank^ bankDescriptor);

		// set the next bank in the running list as the current one for processing, reset the selected to 0 if the current is the last
		void SwitchToNext();

		// start the live processing
		void Go();

		// stop the live processing
		void Stop();

		// set the selected index of the chains in the bank to 0
		void ResetBank();

		// specifies whether or not the processor is currently running
		property bool IsRunning
		{ 
			bool get(); 
		};

		ref class ProcessHarnessException : Exception
		{
			public:
				ProcessHarnessException(String^ message) : Exception(message) {};
		};		
	};

	public ref class EffectBankFactory
	{
	private:
		DriverInfo^ _dInfo;
		Dictionary<String^, FileInfo^>^ _prototypes;
		Designer::Storage::EffectPrototypeStorage^ _effectStore;
		Processor::EffectBase^ FormChain(Designer::Entity::EffectChain^ chainDescriptor);
		Processor::EffectBase^ LoadEffectConfiguration(Designer::Entity::EffectConfiguration^ configDescriptor);
		Processor::EffectBase^ InstantiateEffect(FileInfo ^zipFile);
		void LoadPrototypes();

	public:
		EffectBankFactory(DriverInfo^ dInfo);
		Processor::EffectBank^ BuildBank(Designer::Entity::EffectBank^ bankDescriptor);

		ref class BankFactoryException : Exception
		{
		private:
			String^ _XML;
			String^ _filename;
		
		public:
			BankFactoryException(String^ message) : Exception(message) {};

			property String^ XML
			{ 
				String^ get(); 
				void set(String^ value);
			};
			property String^ Filename
			{
				String^ get();
				void set(String^ value);
			};
		};
	};
}