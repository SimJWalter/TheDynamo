using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DSProcessor.Contract;

namespace DSProcessor.ReadyState
{
  public partial class ReadyStateUI : ViewBase
  {
    public ReadyStateUI()
    {
      InitializeComponent();
    }

    public ReadyStateUI(String title)
      : base(title)
    {
      InitializeComponent();
    }

    private void btnEffectChain_Click(object sender, EventArgs e)
    {
      ReadyStateController.Instance.OpenEffectDesigner();
    }

    private void btnEffectBank_Click(object sender, EventArgs e)
    {
      ReadyStateController.Instance.OpenEffectBankBuilder();
    }

    private void btnLiveProcessor_Click(object sender, EventArgs e)
    {
      ReadyStateController.Instance.OpenLiveProcessor();
    }


  }
}
