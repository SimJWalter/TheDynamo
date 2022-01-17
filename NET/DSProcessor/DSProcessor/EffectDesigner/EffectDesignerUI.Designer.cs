namespace DSProcessor.EffectDesigner
{
    partial class EffectDesignerUI
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
          this.effectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.addEffectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.testRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.statusStrip = new System.Windows.Forms.StatusStrip();
          this.lblFilename = new System.Windows.Forms.ToolStripStatusLabel();
          this.pnlEffectList = new System.Windows.Forms.Panel();
          this.mnuStrip.SuspendLayout();
          this.statusStrip.SuspendLayout();
          this.SuspendLayout();
          // 
          // mnuStrip
          // 
          this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.effectToolStripMenuItem});
          this.mnuStrip.Location = new System.Drawing.Point(0, 0);
          this.mnuStrip.Name = "mnuStrip";
          this.mnuStrip.Size = new System.Drawing.Size(454, 24);
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
          // effectToolStripMenuItem
          // 
          this.effectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEffectToolStripMenuItem,
            this.testRunToolStripMenuItem});
          this.effectToolStripMenuItem.Name = "effectToolStripMenuItem";
          this.effectToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
          this.effectToolStripMenuItem.Text = "Effect";
          // 
          // addEffectToolStripMenuItem
          // 
          this.addEffectToolStripMenuItem.Name = "addEffectToolStripMenuItem";
          this.addEffectToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
          this.addEffectToolStripMenuItem.Text = "Add effect module";
          this.addEffectToolStripMenuItem.Click += new System.EventHandler(this.addEffectToolStripMenuItem_Click);
          // 
          // testRunToolStripMenuItem
          // 
          this.testRunToolStripMenuItem.Name = "testRunToolStripMenuItem";
          this.testRunToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
          this.testRunToolStripMenuItem.Text = "Test Run";
          this.testRunToolStripMenuItem.Click += new System.EventHandler(this.testRunToolStripMenuItem_Click);
          // 
          // statusStrip
          // 
          this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFilename});
          this.statusStrip.Location = new System.Drawing.Point(0, 546);
          this.statusStrip.Name = "statusStrip";
          this.statusStrip.Size = new System.Drawing.Size(454, 22);
          this.statusStrip.TabIndex = 1;
          // 
          // lblFilename
          // 
          this.lblFilename.Name = "lblFilename";
          this.lblFilename.Size = new System.Drawing.Size(0, 17);
          // 
          // pnlEffectList
          // 
          this.pnlEffectList.AutoScroll = true;
          this.pnlEffectList.Location = new System.Drawing.Point(0, 27);
          this.pnlEffectList.Name = "pnlEffectList";
          this.pnlEffectList.Size = new System.Drawing.Size(454, 516);
          this.pnlEffectList.TabIndex = 2;
          // 
          // EffectDesignerUI
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoScroll = true;
          this.ClientSize = new System.Drawing.Size(454, 568);
          this.Controls.Add(this.pnlEffectList);
          this.Controls.Add(this.statusStrip);
          this.Controls.Add(this.mnuStrip);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.MainMenuStrip = this.mnuStrip;
          this.MaximumSize = new System.Drawing.Size(460, 600);
          this.MinimumSize = new System.Drawing.Size(460, 600);
          this.Name = "EffectDesignerUI";
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
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEffectToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblFilename;
        private System.Windows.Forms.ToolStripMenuItem testRunToolStripMenuItem;
        private System.Windows.Forms.Panel pnlEffectList;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    }
}