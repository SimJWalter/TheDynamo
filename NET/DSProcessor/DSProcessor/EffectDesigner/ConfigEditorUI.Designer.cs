namespace DSProcessor.EffectDesigner
{
    partial class ConfigEditorUI
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
            this.pnlConfigs = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlConfigs
            // 
            this.pnlConfigs.AutoScroll = true;
            this.pnlConfigs.Location = new System.Drawing.Point(0, 0);
            this.pnlConfigs.Name = "pnlConfigs";
            this.pnlConfigs.Size = new System.Drawing.Size(387, 320);
            this.pnlConfigs.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(91, 333);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnDiscard
            // 
            this.btnDiscard.Location = new System.Drawing.Point(232, 333);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(75, 23);
            this.btnDiscard.TabIndex = 2;
            this.btnDiscard.Text = "Discard";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // ConfigEditorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 368);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.pnlConfigs);
            this.MinimumSize = new System.Drawing.Size(364, 300);
            this.Name = "ConfigEditorUI";
            this.Text = "ConfigEditorUI";
            this.Load += new System.EventHandler(this.ConfigEditorUI_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigEditorUI_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlConfigs;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnDiscard;
    }
}