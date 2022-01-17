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
  public partial class ConfigControl : UserControl
  {
    public ConfigControl()
    {
      InitializeComponent();
    }

    public string ParamName
    {
      get
      { return this.lblParamName.Text; }
      set
      { this.lblParamName.Text = value; }
    }

    public string ParamValue
    {
      get
      { return this.tbParamValue.Text; }
      set
      { this.tbParamValue.Text = value; }
    }
  }
}
