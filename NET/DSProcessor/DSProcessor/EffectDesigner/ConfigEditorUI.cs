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
  public partial class ConfigEditorUI : ViewBase
  {
    private EffectDesignerController _cntlr;
    private EffectDetails _chainMember;

    internal ConfigEditorUI(EffectDesignerController controller, string title)
      : base(title)
    {
      _cntlr = controller;
      InitializeComponent();
    }

    private void ConfigEditorUI_Load(object sender, EventArgs e)
    {
      _cntlr.MainUIEnabled = false;
    }

    private void ConfigEditorUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      _cntlr.DisplayUI(true);
    }

    internal void DisplayConfigControls(ConfigControl[] controls)
    {
      if (controls != null && controls.Length > 0)
      {
        pnlConfigs.Controls.Clear();
        pnlConfigs.Controls.AddRange(controls);
        Invalidate();
      }
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      List<ConfigControl> cParams = new List<ConfigControl>();
      foreach (Control c in pnlConfigs.Controls)
      {
        if (typeof(ConfigControl).IsInstanceOfType(c))
        {
          cParams.Add((ConfigControl)c);
        }
      }

      _cntlr.MakeConfigUpdate(cParams.ToArray(), _chainMember);
      this.Close();
    }

    private void btnDiscard_Click(object sender, EventArgs e)
    {
      this.Close();
      _cntlr.DisplayUI(true);
    }

    public EffectDetails ChainMember
    {
      set
      {
        _chainMember = value;
      }
    }
  }
}
