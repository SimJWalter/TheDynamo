using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DSProcessor.Contract;
using DSProcessor.EffectDesigner;
using DSProcessor.EffectBankDesigner;
using DSProcessor.DSProcessor;
using System.Threading;

namespace DSProcessor.ReadyState
{
  class ReadyStateController : ControllerBase
  {
    private static ReadyStateController controller;

    private ReadyStateController()
      : base(new ReadyStateUI("DSProcessor: Main Menu"))
    {
      _gui.Controller = this;
    }

    internal static ReadyStateController Instance
    {
      get
      {
        if (controller == null)
        {
          controller = new ReadyStateController();
        }
        return controller;
      }
    }

    internal void OpenEffectDesigner()
    {
      try
      {
        DisplayUI(false);
        EffectDesignerController.Instance.Open();
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message + "\n\n" + e.StackTrace, "Cannot open application..");
        DisplayUI(true);
      }
    }

    internal void OpenEffectBankBuilder()
    {
      try
      {
        DisplayUI(false);
        EffectBankDesignerController.Instance.Open();
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message + "\n\n" + e.StackTrace, "Cannot open application..");
        DisplayUI(true);
      }
    }

    internal void OpenLiveProcessor()
    {
      try
      {
        DisplayUI(false);
        DSProcessorController.Instance.Open();
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message + "\n\n" + e.StackTrace, "Cannot open application..");
        DisplayUI(true);
      }
    }

    public override void Close()
    {
      try
      {
        Application.DoEvents();
        Application.ExitThread();
        Application.Exit();
      }
      catch (Exception e)
      {
        _gui.DisplayError(e.Message + "\n\n" + e.StackTrace, "Cannot open application");
      }
    }

    internal override void UpdateDisplay() { /*//not reqd */ }

    public override ViewBase Open()
    {
      DisplayUI(true);
      return _gui;
    }
  }

}
