namespace DSProcessor.EffectBankDesigner
{
  partial class ChainDetailsUI
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.tbDetails = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // tbDetails
      // 
      this.tbDetails.Location = new System.Drawing.Point(12, 12);
      this.tbDetails.Multiline = true;
      this.tbDetails.Name = "tbDetails";
      this.tbDetails.ReadOnly = true;
      this.tbDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tbDetails.ShortcutsEnabled = false;
      this.tbDetails.Size = new System.Drawing.Size(314, 332);
      this.tbDetails.TabIndex = 0;
      this.tbDetails.TabStop = false;
      // 
      // ChainDetailsUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(338, 356);
      this.Controls.Add(this.tbDetails);
      this.Name = "ChainDetailsUI";
      this.Load += new System.EventHandler(this.ChainDetailsUI_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChainDetailsUI_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbDetails;
  }
}