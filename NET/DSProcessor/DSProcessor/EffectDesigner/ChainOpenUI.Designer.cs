namespace DSProcessor.EffectDesigner
{
  partial class ChainOpenUI
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
      this.lvChainView = new System.Windows.Forms.ListView();
      this.btnOpen = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lvChainView
      // 
      this.lvChainView.Location = new System.Drawing.Point(16, 16);
      this.lvChainView.MultiSelect = false;
      this.lvChainView.Name = "lvChainView";
      this.lvChainView.Size = new System.Drawing.Size(434, 244);
      this.lvChainView.TabIndex = 0;
      this.lvChainView.UseCompatibleStateImageBehavior = false;
      this.lvChainView.View = System.Windows.Forms.View.List;
      // 
      // btnOpen
      // 
      this.btnOpen.Location = new System.Drawing.Point(99, 280);
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(75, 23);
      this.btnOpen.TabIndex = 3;
      this.btnOpen.Text = "Open";
      this.btnOpen.UseVisualStyleBackColor = true;
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(289, 280);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // ChainOpenUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(466, 327);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOpen);
      this.Controls.Add(this.lvChainView);
      this.Name = "ChainOpenUI";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView lvChainView;
    private System.Windows.Forms.Button btnOpen;
    private System.Windows.Forms.Button btnCancel;
  }
}