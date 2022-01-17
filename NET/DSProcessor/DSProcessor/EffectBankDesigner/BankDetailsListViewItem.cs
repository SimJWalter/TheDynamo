using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Designer.Manager.Helper;

namespace DSProcessor.EffectBankDesigner
{
  internal class BankDetailsListViewItem : ListViewItem
  {
    BankDetails _details;

    internal BankDetailsListViewItem(BankDetails details)
    {
      _details = details;
      Name = details.Name;
      Text = details.Name;
    }

    public BankDetails Details
    {
      get { return _details; }
    }
  }
}
