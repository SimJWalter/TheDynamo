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
  public partial class ChainOpenUI : ViewBase
  {
    private EffectDesignerController _cntlr;

    internal ChainOpenUI(EffectDesignerController controller, string title)
      : base(title)
    {
      _cntlr = controller;
      InitializeComponent();
    }
    
    private void ChainOpenUI_Load(object sender, EventArgs e)
    {
      _cntlr.MainUIEnabled = false;
    }

    private void ChainOpenUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      _cntlr.DisplayUI(true);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    internal void UpdateChainList(ChainDetails[] chainDetails)
    {
      List<ChainDetailsListViewItem> lviList = new List<ChainDetailsListViewItem>();

      foreach (ChainDetails details in chainDetails)
      {
        lviList.Add(new ChainDetailsListViewItem(details));
      }      
      lvChainView.Items.AddRange(lviList.ToArray());
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (lvChainView.SelectedItems.Count > 0)
      {
        _cntlr.OpenSelectedChain(((ChainDetailsListViewItem)lvChainView.SelectedItems[0]).Details);
        Close();
      }
    }
  }
}
