using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DSProcessor.Contract;

namespace DSProcessor.EffectBankDesigner
{
  public partial class ChainDetailsUI : ViewBase
  {
    private EffectBankDesignerController _cntlr;

    internal ChainDetailsUI(EffectBankDesignerController controller, string title) : base(title)
    {
      _cntlr = controller;
      InitializeComponent();
    }

    internal void ShowString(string p)
    {
      tbDetails.Text = p;
    }

    private void ChainDetailsUI_Load(object sender, EventArgs e)
    {
      _cntlr.MainUIEnabled = false;
    }

    private void ChainDetailsUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      _cntlr.DisplayUI(true);
    }
  }
}
