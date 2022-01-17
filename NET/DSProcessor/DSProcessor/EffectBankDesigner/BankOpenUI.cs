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
  public partial class BankOpenUI : ViewBase
  {
    private EffectBankDesignerController _cntlr;

    internal BankOpenUI(EffectBankDesignerController controller, string title)
      : base(title)
    {
      _cntlr = controller;
      InitializeComponent();
    }
    
    private void BankOpenUI_Load(object sender, EventArgs e)
    {
      _cntlr.MainUIEnabled = false;
    }

    private void BankOpenUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      _cntlr.DisplayUI(true);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    internal void UpdateBankList(BankDetails[] bankDetails)
    {
      List<BankDetailsListViewItem> lviList = new List<BankDetailsListViewItem>();

      foreach (BankDetails details in bankDetails)
      {
        lviList.Add(new BankDetailsListViewItem(details));
      }      
      lvChainView.Items.AddRange(lviList.ToArray());
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (lvChainView.SelectedItems.Count > 0)
      {
        _cntlr.OpenSelectedBank(((BankDetailsListViewItem)lvChainView.SelectedItems[0]).Details);
        Close();
      }
    }
  }
}
