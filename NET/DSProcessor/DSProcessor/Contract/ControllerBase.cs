using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DSProcessor.Contract
{
  public abstract class ControllerBase
  {
    protected ViewBase _gui;

    protected ControllerBase(ViewBase gui)
    {
      _gui = gui;
      _gui.Visible = false;
      _gui.Enabled = false;
    }

    internal bool MainUIEnabled
    {
      get { return _gui.Enabled; }
      set { _gui.Enabled = value; }
    }

    public void DisplayUI(bool state)
    {
      while (_gui == null)
      {
        try
        {
          Thread.Sleep(10);
        }
        catch { }
      }
      if (state)
      {
        _gui.Enabled = true;
        _gui.BringToFront();
        _gui.Visible = true;
        _gui.Focus();
      }
      else
      {
        _gui.Visible = false;
        _gui.Enabled = false;
        _gui.SendToBack();
      }
    }

    /// <summary>
    /// this method is only included for the associated view class to be able to cleanly shut the controller when it's form is closed/disposed
    /// </summary>
    public abstract void Close();
    public abstract ViewBase Open();

    internal abstract void UpdateDisplay();

  }
}
