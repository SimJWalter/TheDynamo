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

namespace DSProcessor.EffectDesigner
{
  public partial class EffectSelectorUI : ViewBase
  {
    private EffectDesignerController _cntlr;
    internal EffectSelectorUI(EffectDesignerController controller, string title)
      : base(title)
    {
      _cntlr = controller;
      InitializeComponent();
    }

    private void EffectSelectorUI_Load(object sender, EventArgs e)
    {
      _cntlr.MainUIEnabled = false;
      lbSelectEffect.Items.AddRange(_cntlr.CollectPrototypesList());
      lbSelectEffect.DisplayMember = "Name";
    }

    private void btnSelect_Click(object sender, EventArgs e)
    {
      if (lbSelectEffect.SelectedItem != null)
      {
        this.Close();
        _cntlr.SelectEffectToAdd((EffectDetails)lbSelectEffect.SelectedItem);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void EffectSelectorUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      _cntlr.DisplayUI(true);
    }
  }
}
