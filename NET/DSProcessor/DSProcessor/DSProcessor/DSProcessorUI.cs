using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DSProcessor.Contract;

using BlueWave.Interop.Asio;
using Designer.Manager.Helper;
using Processor;

namespace DSProcessor.DSProcessor
{
  public partial class DSProcessorUI : ViewBase
  {
    public DSProcessorUI()
    {
      InitializeComponent();
    }

    public DSProcessorUI(String title)
      : base(title)
    {
      InitializeComponent();
      lbCurrentBank.DisplayMember = "Name";
      cbBankSelect.Enabled = false;
      cbBankSelect.SelectedItem = null;
      cbBankSelect.Invalidate();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      _controller.Close();
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
      if (!DSProcessorController.Instance.IsRunning)
      {
        cbBankSelect.Enabled = false;
        cbDriverSelect.Enabled = false;
        bool error = false;
        if (cbDriverSelect.SelectedItem == null)
        {
          DisplayError("You must select a driver from the list,\nIf you do not have any installed you may use ASIO4ALL\nwith regular sound hardware.", "no driver selected");
          error = true;
        }
        if (cbBankSelect.SelectedItem == null)
        {
          DisplayError("You must select a bank to use for processing the audio stream", "No bank selected");
          error = true;
        }
        if (error)
        {
          cbBankSelect.Enabled = true;
          cbDriverSelect.Enabled = true;
        }
        else
        {
          lbCurrentBank.SelectedIndex = 0;
          DSProcessorController.Instance.StartProcessor();
        }
      }
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
      if (((DSProcessorController)_controller).IsRunning)
      {
        ((DSProcessorController)_controller).StopProcessor();
        //DSProcessorController.Instance.StopProcessor();
        cbBankSelect.Enabled = true;
        cbDriverSelect.Enabled = true;
      }
    }

    private void btnSwitch_Click(object sender, EventArgs e)
    {
      ((DSProcessorController)_controller).SwitchToNextChain();
    }

    internal void SetDriversList(InstalledDriver[] drivers)
    {
      cbDriverSelect.Items.Clear();
      cbDriverSelect.Items.AddRange(drivers);
      this.tbDriverInfo.Text = "";
    }

    internal void SetBanksList(BankDetails[] banks)
    {
      cbBankSelect.Items.Clear();
      cbBankSelect.Items.AddRange(banks);
    }

    internal void SetDriverDetails(DriverInfo dInfo)
    { 
      string[] desc = new string[9];
      desc[0] = "Driver Name: " + dInfo.Name;
      desc[1] = "Driver Version: " + dInfo.Version;
      desc[2] = "Maximum buffer size: " + dInfo.BufferMaxSize;
      desc[3] = "Minimum buffer size: " + dInfo.BufferMinSize;
      desc[4] = "Current buffer size: " + dInfo.BufferPreferredSize;
      desc[5] = "# of Input Channels: " + dInfo.InputChannelCount;
      desc[6] = "# of Output Channels: " + dInfo.OutputChannelCount;
      desc[7] = "Sample-Rate: " + dInfo.SampleRate;
      desc[8] = "Granularity: " + dInfo.Granularity;
      tbDriverInfo.Lines = desc;
    }

    private void cbDriverSelect_SelectionChangeCommitted(object sender, EventArgs e)
    {
      ComboBox cb = sender as ComboBox;
      InstalledDriver id = (InstalledDriver)cb.SelectedItem;
      if (id != null)
      {
        ((DSProcessorController)_controller).SelectDriver(id);
        //DSProcessorController.Instance.SelectDriver(id);
      }
      cbBankSelect.Enabled = true;
    }

    private void cbBankSelect_SelectionChangeCommitted(object sender, EventArgs e)
    {
      ComboBox cb = sender as ComboBox;
      BankDetails bd = (BankDetails)cb.SelectedItem;
      if (bd != null)
      {
        ((DSProcessorController)_controller).SelectBank(bd);
        //DSProcessorController.Instance.SelectBank(bd);
        lbCurrentBank.Items.Clear();
        lbCurrentBank.Items.AddRange(bd.Chains.ToArray());
      }
    }

    internal void HighlightNextChainAsSelected()
    {
      int curInd = lbCurrentBank.SelectedIndex;
      if (curInd < lbCurrentBank.Items.Count - 1)
      {
        lbCurrentBank.SelectedIndex++;
      }
      else
      {
        lbCurrentBank.SelectedIndex = 0;
      }
    }
  }
}
