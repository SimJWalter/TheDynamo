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
  public partial class ChainSaveUI : ViewBase
  {
    private EffectDesignerController _cntlr;

    internal ChainSaveUI(EffectDesignerController controller, string title)
      : base(title)
    {
      _cntlr = controller;
      InitializeComponent();
    }
    
    private void ChainSaveUI_Load(object sender, EventArgs e)
    {
      _cntlr.MainUIEnabled = false;
      BringToFront();
      Focus();
    }

    private void ChainSaveUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      _cntlr.DisplayUI(true);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void lvChainView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      if (lvChainView.SelectedItems.Count > 0)
      {
        tbFileName.Text = lvChainView.SelectedItems[0].Text;
      }      
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

    private void btnSave_Click(object sender, EventArgs e)
    {
      string param1 = tbFileName.Text;
      ChainDetails param2 = null;
      if (lvChainView.SelectedItems.Count > 0)
      {
        param2 = ((ChainDetailsListViewItem)lvChainView.SelectedItems[0]).Details;
      }
      
      _cntlr.SaveChainAsSpecified(param1, param2, this);
    }
  }
}
