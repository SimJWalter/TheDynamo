namespace DSProcessor.EffectBankDesigner
{
    partial class EffectBankDesignerUI
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
          this.mnuStrip = new System.Windows.Forms.MenuStrip();
          this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.effectBankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.addChainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.statusStrip = new System.Windows.Forms.StatusStrip();
          this.lblFilename = new System.Windows.Forms.ToolStripStatusLabel();
          this.pnlChainList = new System.Windows.Forms.Panel();
          this.mnuStrip.SuspendLayout();
          this.statusStrip.SuspendLayout();
          this.SuspendLayout();
          // 
          // mnuStrip
          // 
          this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.effectBankToolStripMenuItem});
          this.mnuStrip.Location = new System.Drawing.Point(0, 0);
          this.mnuStrip.Name = "mnuStrip";
          this.mnuStrip.Size = new System.Drawing.Size(480, 24);
          this.mnuStrip.TabIndex = 0;
          this.mnuStrip.Text = "menuStrip1";
          // 
          // fileToolStripMenuItem
          // 
          this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
          this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
          this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
          this.fileToolStripMenuItem.Text = "File";
          // 
          // newToolStripMenuItem
          // 
          this.newToolStripMenuItem.Name = "newToolStripMenuItem";
          this.newToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
          this.newToolStripMenuItem.Text = "New";
          this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
          // 
          // openToolStripMenuItem
          // 
          this.openToolStripMenuItem.Name = "openToolStripMenuItem";
          this.openToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
          this.openToolStripMenuItem.Text = "Open";
          this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
          // 
          // saveToolStripMenuItem
          // 
          this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
          this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
          this.saveToolStripMenuItem.Text = "Save";
          this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
          // 
          // saveAsToolStripMenuItem
          // 
          this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
          this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
          this.saveAsToolStripMenuItem.Text = "Save As..";
          this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
          // 
          // exitToolStripMenuItem
          // 
          this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
          this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
          this.exitToolStripMenuItem.Text = "Exit";
          this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
          // 
          // effectBankToolStripMenuItem
          // 
          this.effectBankToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addChainToolStripMenuItem});
          this.effectBankToolStripMenuItem.Name = "effectBankToolStripMenuItem";
          this.effectBankToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
          this.effectBankToolStripMenuItem.Text = "EffectChains";
          // 
          // addChainToolStripMenuItem
          // 
          this.addChainToolStripMenuItem.Name = "addChainToolStripMenuItem";
          this.addChainToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
          this.addChainToolStripMenuItem.Text = "Add Chain";
          this.addChainToolStripMenuItem.Click += new System.EventHandler(this.addChainToolStripMenuItem_Click);
          // 
          // statusStrip
          // 
          this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFilename});
          this.statusStrip.Location = new System.Drawing.Point(0, 422);
          this.statusStrip.Name = "statusStrip";
          this.statusStrip.Size = new System.Drawing.Size(480, 22);
          this.statusStrip.TabIndex = 1;
          this.statusStrip.Text = "statusStrip1";
          // 
          // lblFilename
          // 
          this.lblFilename.Name = "lblFilename";
          this.lblFilename.Size = new System.Drawing.Size(0, 17);
          // 
          // pnlChainList
          // 
          this.pnlChainList.AutoScroll = true;
          this.pnlChainList.Location = new System.Drawing.Point(0, 25);
          this.pnlChainList.Name = "pnlChainList";
          this.pnlChainList.Size = new System.Drawing.Size(480, 394);
          this.pnlChainList.TabIndex = 2;
          // 
          // EffectBankDesignerUI
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(480, 444);
          this.Controls.Add(this.pnlChainList);
          this.Controls.Add(this.statusStrip);
          this.Controls.Add(this.mnuStrip);
          this.MainMenuStrip = this.mnuStrip;
          this.Name = "EffectBankDesignerUI";
          this.mnuStrip.ResumeLayout(false);
          this.mnuStrip.PerformLayout();
          this.statusStrip.ResumeLayout(false);
          this.statusStrip.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectBankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addChainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblFilename;
        private System.Windows.Forms.Panel pnlChainList;
    }
}