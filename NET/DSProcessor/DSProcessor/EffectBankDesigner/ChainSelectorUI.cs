using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DSProcessor.Contract;
using Designer.Manager.Helper;

namespace DSProcessor.EffectBankDesigner
{
  public partial class ChainSelectorUI : ViewBase
  {
    private EffectBankDesignerController _cntlr;

    internal ChainSelectorUI(EffectBankDesignerController controller, string title)
      : base(title)
    {
      _cntlr = controller;
      InitializeComponent();
    }

    private void ChainSelectorUI_Load(object sender, EventArgs e)
    {
      _cntlr.MainUIEnabled = false;
      lbSelectChain.Items.AddRange(_cntlr.CollectPrototypesList());
      lbSelectChain.DisplayMember = "Name";
    }

    private void btnSelect_Click(object sender, EventArgs e)
    {
      if (lbSelectChain.SelectedItem != null)
      {
        _cntlr.SelectChainToAdd((ChainDetails)lbSelectChain.SelectedItem);
        this.Close();        
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void ChainSelectorUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      _cntlr.DisplayUI(true);
    }
  }
}
