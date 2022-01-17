namespace DSProcessor.DSProcessor
{
    partial class DSProcessorUI
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
          this.btnStart = new System.Windows.Forms.Button();
          this.btnStop = new System.Windows.Forms.Button();
          this.lblDriverSelect = new System.Windows.Forms.Label();
          this.lblTitle = new System.Windows.Forms.Label();
          this.lblBankSelect = new System.Windows.Forms.Label();
          this.lbCurrentBank = new System.Windows.Forms.ListBox();
          this.lblCurrentEffect = new System.Windows.Forms.Label();
          this.btnSwitch = new System.Windows.Forms.Button();
          this.btnExit = new System.Windows.Forms.Button();
          this.tbDriverInfo = new System.Windows.Forms.TextBox();
          this.cbDriverSelect = new System.Windows.Forms.ComboBox();
          this.cbBankSelect = new System.Windows.Forms.ComboBox();
          this.SuspendLayout();
          // 
          // btnStart
          // 
          this.btnStart.Location = new System.Drawing.Point(311, 113);
          this.btnStart.Name = "btnStart";
          this.btnStart.Size = new System.Drawing.Size(75, 23);
          this.btnStart.TabIndex = 0;
          this.btnStart.Text = "Start";
          this.btnStart.UseVisualStyleBackColor = true;
          this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
          // 
          // btnStop
          // 
          this.btnStop.Location = new System.Drawing.Point(311, 152);
          this.btnStop.Name = "btnStop";
          this.btnStop.Size = new System.Drawing.Size(75, 23);
          this.btnStop.TabIndex = 1;
          this.btnStop.Text = "Stop";
          this.btnStop.UseVisualStyleBackColor = true;
          this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
          // 
          // lblDriverSelect
          // 
          this.lblDriverSelect.AutoSize = true;
          this.lblDriverSelect.Location = new System.Drawing.Point(12, 95);
          this.lblDriverSelect.Name = "lblDriverSelect";
          this.lblDriverSelect.Size = new System.Drawing.Size(75, 13);
          this.lblDriverSelect.TabIndex = 3;
          this.lblDriverSelect.Text = "Select driver:..";
          // 
          // lblTitle
          // 
          this.lblTitle.AutoSize = true;
          this.lblTitle.Font = new System.Drawing.Font("probot", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lblTitle.Location = new System.Drawing.Point(112, 26);
          this.lblTitle.Name = "lblTitle";
          this.lblTitle.Size = new System.Drawing.Size(367, 30);
          this.lblTitle.TabIndex = 4;
          this.lblTitle.Text = "DS-Processor";
          // 
          // lblBankSelect
          // 
          this.lblBankSelect.AutoSize = true;
          this.lblBankSelect.Location = new System.Drawing.Point(166, 95);
          this.lblBankSelect.Name = "lblBankSelect";
          this.lblBankSelect.Size = new System.Drawing.Size(73, 13);
          this.lblBankSelect.TabIndex = 6;
          this.lblBankSelect.Text = "Select bank:..";
          // 
          // lbCurrentBank
          // 
          this.lbCurrentBank.CausesValidation = false;
          this.lbCurrentBank.FormattingEnabled = true;
          this.lbCurrentBank.Location = new System.Drawing.Point(415, 113);
          this.lbCurrentBank.Name = "lbCurrentBank";
          this.lbCurrentBank.Size = new System.Drawing.Size(120, 238);
          this.lbCurrentBank.TabIndex = 7;
          // 
          // lblCurrentEffect
          // 
          this.lblCurrentEffect.AutoSize = true;
          this.lblCurrentEffect.Location = new System.Drawing.Point(419, 94);
          this.lblCurrentEffect.Name = "lblCurrentEffect";
          this.lblCurrentEffect.Size = new System.Drawing.Size(109, 13);
          this.lblCurrentEffect.TabIndex = 8;
          this.lblCurrentEffect.Text = "Current effect chain:..";
          // 
          // btnSwitch
          // 
          this.btnSwitch.Location = new System.Drawing.Point(453, 365);
          this.btnSwitch.Name = "btnSwitch";
          this.btnSwitch.Size = new System.Drawing.Size(75, 23);
          this.btnSwitch.TabIndex = 9;
          this.btnSwitch.Text = "Switch";
          this.btnSwitch.UseVisualStyleBackColor = true;
          this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
          // 
          // btnExit
          // 
          this.btnExit.Location = new System.Drawing.Point(12, 365);
          this.btnExit.Name = "btnExit";
          this.btnExit.Size = new System.Drawing.Size(61, 23);
          this.btnExit.TabIndex = 10;
          this.btnExit.Text = "Exit";
          this.btnExit.UseVisualStyleBackColor = true;
          this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
          // 
          // tbDriverInfo
          // 
          this.tbDriverInfo.Location = new System.Drawing.Point(15, 185);
          this.tbDriverInfo.Multiline = true;
          this.tbDriverInfo.Name = "tbDriverInfo";
          this.tbDriverInfo.ReadOnly = true;
          this.tbDriverInfo.Size = new System.Drawing.Size(269, 166);
          this.tbDriverInfo.TabIndex = 11;
          // 
          // cbDriverSelect
          // 
          this.cbDriverSelect.FormattingEnabled = true;
          this.cbDriverSelect.Location = new System.Drawing.Point(12, 115);
          this.cbDriverSelect.Name = "cbDriverSelect";
          this.cbDriverSelect.Size = new System.Drawing.Size(121, 21);
          this.cbDriverSelect.TabIndex = 16;
          this.cbDriverSelect.SelectionChangeCommitted += new System.EventHandler(this.cbDriverSelect_SelectionChangeCommitted);
          // 
          // cbBankSelect
          // 
          this.cbBankSelect.FormattingEnabled = true;
          this.cbBankSelect.Location = new System.Drawing.Point(163, 115);
          this.cbBankSelect.Name = "cbBankSelect";
          this.cbBankSelect.Size = new System.Drawing.Size(121, 21);
          this.cbBankSelect.TabIndex = 17;
          this.cbBankSelect.SelectionChangeCommitted += new System.EventHandler(this.cbBankSelect_SelectionChangeCommitted);
          // 
          // DSProcessorUI
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(583, 400);
          this.Controls.Add(this.cbBankSelect);
          this.Controls.Add(this.cbDriverSelect);
          this.Controls.Add(this.tbDriverInfo);
          this.Controls.Add(this.btnExit);
          this.Controls.Add(this.btnSwitch);
          this.Controls.Add(this.lblCurrentEffect);
          this.Controls.Add(this.lbCurrentBank);
          this.Controls.Add(this.lblBankSelect);
          this.Controls.Add(this.lblTitle);
          this.Controls.Add(this.lblDriverSelect);
          this.Controls.Add(this.btnStop);
          this.Controls.Add(this.btnStart);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
          this.Name = "DSProcessorUI";
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblDriverSelect;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBankSelect;
        private System.Windows.Forms.ListBox lbCurrentBank;
        private System.Windows.Forms.Label lblCurrentEffect;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbDriverInfo;
        private System.Windows.Forms.ComboBox cbDriverSelect;
        private System.Windows.Forms.ComboBox cbBankSelect;
    }
}