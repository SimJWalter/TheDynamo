using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace DSProcessor.Contract
{
  public /*abstract */ class ViewBase : Form
  {
    //included merely so the controller can be closed when the form is
    protected ControllerBase _controller;

    protected ViewBase(String title)
      : this()
    {
      this.CenterToScreen();
      this.Text = title;
      this.Icon = global::DSProcessor.Properties.Resources.Icon;
    }

    protected ViewBase()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Set this Controller property when this is the main View for that Controller, as such the Controller will be closed when this View closes
    /// </summary>
    public ControllerBase Controller
    {
      set
      {
        _controller = value;
      }
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      // 
      // ViewBase
      //             
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewBase_FormClosing);
      this.ResumeLayout(false);

    }

    public void DisplayError(String errorMessage, String title)
    {
      if (title == "" || title == null)
      {
        title = "Error";
      }
      MessageBox.Show(errorMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void ViewBase_FormClosing(object sender, FormClosingEventArgs e)
    {
      Enabled = false;
      Visible = false;
      if (_controller != null)
      {
        _controller.Close();
      }
    }

    public void CentreOnScreen()
    {
      int x = (int)((SystemInformation.PrimaryMonitorSize.Width / 2) - (this.Width / 2));
      int y = (int)((SystemInformation.PrimaryMonitorSize.Height / 2) - (this.Height / 2));
      this.Location = new System.Drawing.Point(x, y);
    }
  }
}
