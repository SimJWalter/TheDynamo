namespace DSProcessor.EffectBankDesigner
{
  partial class BankSaveUI
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
      this.lvBankView = new System.Windows.Forms.ListView();
      this.lblFilename = new System.Windows.Forms.Label();
      this.tbFileName = new System.Windows.Forms.TextBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lvChainView
      // 
      this.lvBankView.Location = new System.Drawing.Point(16, 16);
      this.lvBankView.MultiSelect = false;
      this.lvBankView.Name = "lvChainView";
      this.lvBankView.Size = new System.Drawing.Size(434, 244);
      this.lvBankView.TabIndex = 0;
      this.lvBankView.UseCompatibleStateImageBehavior = false;
      this.lvBankView.View = System.Windows.Forms.View.List;
      this.lvBankView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvBankView_ItemSelectionChanged);
      // 
      // lblFilename
      // 
      this.lblFilename.AutoSize = true;
      this.lblFilename.Location = new System.Drawing.Point(13, 272);
      this.lblFilename.Name = "lblFilename";
      this.lblFilename.Size = new System.Drawing.Size(41, 13);
      this.lblFilename.TabIndex = 1;
      this.lblFilename.Text = "Name: ";
      // 
      // tbFileName
      // 
      this.tbFileName.Location = new System.Drawing.Point(54, 269);
      this.tbFileName.Name = "tbFileName";
      this.tbFileName.Size = new System.Drawing.Size(171, 20);
      this.tbFileName.TabIndex = 2;
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(263, 292);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 3;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(375, 292);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // ChainSaveUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(466, 327);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.tbFileName);
      this.Controls.Add(this.lblFilename);
      this.Controls.Add(this.lvBankView);
      this.Name = "ChainSaveUI";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lvBankView;
    private System.Windows.Forms.Label lblFilename;
    private System.Windows.Forms.TextBox tbFileName;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;
  }
}