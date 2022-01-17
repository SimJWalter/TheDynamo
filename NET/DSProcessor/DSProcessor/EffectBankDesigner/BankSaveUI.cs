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
  public partial class BankSaveUI : ViewBase
  {
    private EffectBankDesignerController _cntlr;

    internal BankSaveUI(EffectBankDesignerController controller, string title)
      : base(title)
    {
      _cntlr = controller;
      InitializeComponent();
    }
    
    private void BankSaveUI_Load(object sender, EventArgs e)
    {
      _cntlr.MainUIEnabled = false;
      BringToFront();
      Focus();
    }

    private void BankSaveUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      _cntlr.DisplayUI(true);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void lvBankView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      if (lvBankView.SelectedItems.Count > 0)
      {
        tbFileName.Text = lvBankView.SelectedItems[0].Text;
      }      
    }

    internal void UpdateBankList(BankDetails[] bankDetails)
    {
      List<BankDetailsListViewItem> lviList = new List<BankDetailsListViewItem>();

      foreach (BankDetails details in bankDetails)
      {
        lviList.Add(new BankDetailsListViewItem(details));
      }      
      lvBankView.Items.AddRange(lviList.ToArray());
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      string param1 = tbFileName.Text;
      BankDetails param2 = null;
      if (lvBankView.SelectedItems.Count > 0)
      {
        param2 = ((BankDetailsListViewItem)lvBankView.SelectedItems[0]).Details;
      }
      
      _cntlr.SaveBankAsSpecified(param1, param2, this);
    }
  }
}
