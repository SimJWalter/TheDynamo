using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Designer.Manager.Helper;

namespace DSProcessor.EffectDesigner
{
  internal class ChainDetailsListViewItem : ListViewItem
  {
    ChainDetails _details;

    internal ChainDetailsListViewItem(ChainDetails details)
    {
      _details = details;
      Name = details.Name;
      Text = details.Name;
    }

    public ChainDetails Details
    {
      get { return _details; }
    }
  }
}
