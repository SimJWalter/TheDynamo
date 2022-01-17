using Designer.Manager.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSProcessor.Contract;
using DSProcessor.ReadyState;
using System.IO;
using Processor;
using Designer.Manager;

namespace DSProcessor.DSProcessor
{
  public class DSProcessorController : ControllerBase
  {
    private ProcessHarness _processor;
    private BankPrototypeManager _bankManager;

    private static DSProcessorController controller;

    private DSProcessorController()
      : base(new DSProcessorUI("DSProcessor: Live"))
    {
      _gui.Controller = this;
      _bankManager = new BankPrototypeManager();
    }

    public static DSProcessorController Instance
    {
      get
      {
        if (controller == null)
        {
          controller = new DSProcessorController();
        }
        return controller;
      }
    }

    public bool IsRunning
    {
      get 
      {
        return _processor.IsRunning;
      }
    }

    public override void Close()
    {
      _processor.Destroy();
      _processor = null;
      _bankManager = null;
      System.GC.Collect();
      this.DisplayUI(false);
      ReadyStateController.Instance.DisplayUI(true);
      DSProcessorController.Dispose();
    }

    public static void Dispose()
    {
      controller = null;
      System.GC.Collect();
    }

    internal override void UpdateDisplay()
    {
      if (!_processor.IsRunning)
      {
        ((DSProcessorUI)_gui).SetDriversList(_processor.GetDriverDetails());
        ((DSProcessorUI)_gui).SetBanksList(_bankManager.DescribeBanks().ToArray());
      }
    }

    public override ViewBase Open()
    {
      _processor = new ProcessHarness();
      UpdateDisplay();
      DisplayUI(true);
      return _gui;
    }


    internal void SelectBank(BankDetails bd)
    {
      try
      {
        Designer.Entity.EffectBank eb = _bankManager.GetBank(bd);
        _processor.LoadBank(eb);
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message, "Error..");
      }
    }

    internal void SelectDriver(BlueWave.Interop.Asio.InstalledDriver id)
    {
      try
      {
        if (id != null)
        {
          _processor.SelectDriver(id);
          ((DSProcessorUI)_gui).SetDriverDetails(_processor.GetCurrentDriverInfo());
        }
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message, "Error..");
      }      
    }

    public DriverInfo DriverInfo
    {
      get
      {
        DriverInfo ret = null;
        try
        { 
          ret = _processor.GetCurrentDriverInfo(); 
        }
        catch(Exception e) {}
        return ret;                
      }      
    }

    internal void StartProcessor()
    {
      try
      {
        _processor.Go();
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message, "Error..");
      }
    }

    internal void StopProcessor()
    {
      _processor.Stop();
    }

    internal void SwitchToNextChain()
    {
      _processor.SwitchToNext();
      ((DSProcessorUI)_gui).HighlightNextChainAsSelected();
    }
  }
}
