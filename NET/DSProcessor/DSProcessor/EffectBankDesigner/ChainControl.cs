using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Designer.Manager.Helper;
using DSProcessor.Contract;

namespace DSProcessor.EffectBankDesigner
{
  public partial class ChainControl : UserControl
  {
    private EffectBankDesignerController _controller;

    public ChainControl()
    {
      InitializeComponent();
    }

    internal EffectBankDesignerController Controller
    {
      set 
      {
        _controller = value;
      }
    }

    private ChainDetails _bankMember;
    public ChainDetails BankMember
    {
      get { return _bankMember; }
      set
      {
        if (value != null)
        {
          _bankMember = value;
          lblName.Text = value.Name;
          lblOrder.Text = (value.Index + 1).ToString();
        }
      }
    }

    public string Order
    {
      get { return lblOrder.Text; }
    }
    
    public string ChainName
    {
      get { return lblName.Text; }
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      _controller.ShiftChainUp(_bankMember);
      //EffectBankDesignerController.Instance.ShiftChainUp(_bankMember);
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      _controller.ShiftChainDown(_bankMember);
      //EffectBankDesignerController.Instance.ShiftChainDown(_bankMember);
    }

    private void btnConfig_Click(object sender, EventArgs e)
    {
      _controller.ViewChainDetails(_bankMember);
      //EffectBankDesignerController.Instance.ViewChainDetails(_bankMember);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      _controller.RemoveFromBank(_bankMember);
      //EffectBankDesignerController.Instance.RemoveFromBank(_bankMember);
    }
  }
}
