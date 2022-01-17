#include "stdafx.h"
#include "Processor.h"

using namespace System;
using namespace System::IO;
using namespace System::Xml;
using namespace ICSharpCode::SharpZipLib::Zip;
using namespace System::Reflection;

namespace Processor
{
	EffectBankFactory::EffectBankFactory(DriverInfo^ dInfo)
	{
		if(dInfo == nullptr)
		{
			throw gcnew ArgumentNullException("Invalid driver object");
		}
		_dInfo = dInfo;
		_effectStore = gcnew Designer::Storage::EffectPrototypeStorage();
		this->LoadPrototypes();
	}

	Processor::EffectBank^ EffectBankFactory::BuildBank(Designer::Entity::EffectBank^ bankDescriptor)
	{
		if(bankDescriptor == nullptr)
		{
			throw gcnew ArgumentNullException("Invalid bank description object");
		}
		EffectBank^ ret = gcnew EffectBank(bankDescriptor->Name);
		for(int i = 0; i < bankDescriptor->Chains->Count; i++)
		{
			EffectBase^ toAdd = this->FormChain(bankDescriptor->Chains[i]);
			if(toAdd != nullptr)
			{
				ret->ApppendChain(toAdd, bankDescriptor->Chains[i]->Name);	
			}
		}		
		return ret;
	}

	Processor::EffectBase^ EffectBankFactory::FormChain(Designer::Entity::EffectChain ^chainDescriptor)
	{
		EffectBase^ ret = nullptr;
		for(int i = 0; i < chainDescriptor->Configurations->Count; i++)
		{
			try
			{
				EffectBase^ toAdd = this->LoadEffectConfiguration(chainDescriptor->Configurations[i]);
				if(ret == nullptr)
				{
					//initialise ret
					ret = toAdd;
				}
				else
				{
					//append onto ret
					ret->Decorator = toAdd;
				}
			}
			catch(Exception^ e){}
		}
		return ret;
	}

	Processor::EffectBase^ EffectBankFactory::LoadEffectConfiguration(Designer::Entity::EffectConfiguration ^configDescriptor)
	{
		EffectBase^ ret = InstantiateEffect(_prototypes[configDescriptor->Name]);
		if(ret == nullptr)
		{
			throw gcnew BankFactoryException(String::Format("Could not instantiate effect: {0}.  From file: {1}", configDescriptor->Name, _prototypes[configDescriptor->Name]->FullName));
		}
		else
		{
			ret->DriverInf = _dInfo;
			EffectConfiguration^ config = gcnew EffectConfiguration();
			for(int i = 0; i < configDescriptor->Parameters->Count; i++)
			{
				config->Add(configDescriptor->Parameters[i]->Name, configDescriptor->Parameters[i]->Value);
			}
			ret->Setup(config);
		}
		return ret;
	}

	EffectBase^ EffectBankFactory::InstantiateEffect(FileInfo ^zipFile)
	{
		EffectBase^ ret = nullptr;
		ZipFile^ _ZFILE = gcnew ZipFile(zipFile->FullName);
		for(int i = 0; i < _ZFILE->Count; i++)
		{
			FileInfo^ temp = gcnew FileInfo(_ZFILE[i]->Name);
			if(temp->Extension == Designer::Storage::FileStorageConstants::AssemblyExtension)
			{
				Stream^ stream = _ZFILE->GetInputStream(_ZFILE[i]);
				array<unsigned char>^ content = gcnew array<unsigned char>(_ZFILE[i]->Size);
				stream->Read(content, 0, _ZFILE[i]->Size);
				Assembly^ assembled = Assembly::Load(content);
				array<Type^, 1>^ types = assembled->GetTypes();
				Type^ type = types[0];
				ConstructorInfo^ cInfo = type->GetConstructor(gcnew array<Type^>(0));
				Object^ obj = cInfo->Invoke(gcnew array<Object^>(0));
				ret = (EffectBase^)obj;
				i = _ZFILE->Count;
			}
		}
		return ret;
	}

	void EffectBankFactory::LoadPrototypes()
	{
		List<Designer::Entity::EffectConfiguration^>^ configs = _effectStore->FetchConfigurations();
		this->_prototypes = gcnew Dictionary<String^, FileInfo^>();
		for(int i = 0; i < configs->Count; i++)
		{
			FileInfo^ finfo = gcnew FileInfo(Designer::Storage::FileStorageConstants::PrototypesLocation + configs[i]->Filename);
			this->_prototypes->Add(configs[i]->Name, finfo);
		}				
	}

	String^ EffectBankFactory::BankFactoryException::Filename::get()
	{
		return _filename;
	}

	void EffectBankFactory::BankFactoryException::Filename::set(String^ value)
	{
		_filename = value;
	}

	String^ EffectBankFactory::BankFactoryException::XML::get()
	{
		return _XML;
	}

	void EffectBankFactory::BankFactoryException::XML::set(String^ value)
	{
		_XML = value;
	}
}