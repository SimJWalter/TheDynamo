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
  public partial class EffectBankDesignerUI : ViewBase
  {
    public EffectBankDesignerUI()
    {
      InitializeComponent();
    }

    public EffectBankDesignerUI(String title)
      : base(title)
    {
      InitializeComponent();
    }

    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectBankDesignerController.Instance.CreateNewBank();
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectBankDesignerController.Instance.OpenSavedBank();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectBankDesignerController.Instance.SaveCurrentBank();
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectBankDesignerController.Instance.SaveCurrentBankAsNew();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      bool goAhead = true;
      if (EffectBankDesignerController.Instance.UnsavedChanges)
      {
        DialogResult result = MessageBox.Show("Current chain has unsaved changes, continue?", "Confirm discard", MessageBoxButtons.OKCancel);
        goAhead = (result == DialogResult.OK);
      }
      if (goAhead)
      {
        this.Close();
      }      
    }

    private void addChainToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectBankDesignerController.Instance.AddSavedChain();
    }

    internal void SetChainList(ChainControl[] chainControls)
    {
      pnlChainList.Controls.Clear();
      for (int i = 0; i < chainControls.Length; i++)
      {
        chainControls[i].Location = new Point(0, i * chainControls[i].Height);
      }
      pnlChainList.Controls.AddRange(chainControls);
    }

  }
}
