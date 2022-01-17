using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Designer.Manager.Helper;

namespace DSProcessor.EffectDesigner
{
  public partial class EffectControl : UserControl
  {
    public EffectControl()
    {
      InitializeComponent();
    }

    private EffectDesignerController _controller;
    internal EffectDesignerController Controller
    {
      set {
        _controller = value;
      }
    }

    private EffectDetails _chainMember;
    public EffectDetails ChainMember
    {
      get { return _chainMember; }
      set 
      {
        if (value != null)
        {
          _chainMember = value;
          lblOrder.Text = (_chainMember.Index + 1).ToString();
          lblName.Text = _chainMember.Name;
        }        
      }
    }

    public bool Muted
    {
      get { return cbMute.Checked; }
      set { cbMute.Checked = true; }
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      _controller.ShiftEffectUp(this.ChainMember); ;
      //EffectDesignerController.Instance.ShiftEffectUp(this.ChainMember);
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      _controller.ShiftEffectDown(this.ChainMember);
      //EffectDesignerController.Instance.ShiftEffectDown(this.ChainMember);
    }

    private void btnConfig_Click(object sender, EventArgs e)
    {
      _controller.OpenConfigForEdit(this.ChainMember);
      //EffectDesignerController.Instance.OpenConfigForEdit(this.ChainMember);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      _controller.RemoveFromChain(this.ChainMember);
      //EffectDesignerController.Instance.RemoveFromChain(this.ChainMember);
    }
  }
}
