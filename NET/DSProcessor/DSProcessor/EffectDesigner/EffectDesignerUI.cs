using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DSProcessor.Contract;

namespace DSProcessor.EffectDesigner
{
  public partial class EffectDesignerUI : ViewBase
  {
    public EffectDesignerUI()
    {
      InitializeComponent();
    }

    public EffectDesignerUI(String title)
      : base(title)
    {
      InitializeComponent();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      bool goAhead = true;
      if (EffectDesignerController.Instance.UnsavedChanges)
      {
        DialogResult result = MessageBox.Show("Current chain has unsaved changes, continue?", "Confirm discard", MessageBoxButtons.OKCancel);
        goAhead = (result == DialogResult.OK);
      }
      if (goAhead)
      {
        this.Close();  
      }     
    }

    private void newToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectDesignerController.Instance.CreateNewChain();
    }

    internal void SetEffectList(EffectControl[] effectControls)
    {
      pnlEffectList.Controls.Clear();
      for (int i = 0; i < effectControls.Length; i++)
      {
        effectControls[i].Location = new Point(0, i * effectControls[i].Height);
      }
      pnlEffectList.Controls.AddRange(effectControls);
    }

    internal Control.ControlCollection GetEffectList()
    {
      return pnlEffectList.Controls;
    }

    private void addEffectToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectDesignerController.Instance.AddEffectToChain();
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectDesignerController.Instance.SaveCurrentChain();
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectDesignerController.Instance.OpenSavedChain();
    }

    private void testRunToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectDesignerController.Instance.TestRunCurrentChain();
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EffectDesignerController.Instance.SaveCurrentChainAsNew();
    }
  }
}
