namespace DSProcessor.EffectDesigner
{
    partial class ConfigControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblParamName = new System.Windows.Forms.Label();
            this.tbParamValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblParamName
            // 
            this.lblParamName.AutoSize = true;
            this.lblParamName.Location = new System.Drawing.Point(4, 6);
            this.lblParamName.MinimumSize = new System.Drawing.Size(250, 13);
            this.lblParamName.Name = "lblParamName";
            this.lblParamName.Size = new System.Drawing.Size(250, 13);
            this.lblParamName.TabIndex = 0;
            this.lblParamName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbParamValue
            // 
            this.tbParamValue.Location = new System.Drawing.Point(260, 3);
            this.tbParamValue.Name = "tbParamValue";
            this.tbParamValue.Size = new System.Drawing.Size(100, 20);
            this.tbParamValue.TabIndex = 1;
            // 
            // EffectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tbParamValue);
            this.Controls.Add(this.lblParamName);
            this.Name = "EffectControl";
            this.Size = new System.Drawing.Size(364, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblParamName;
        private System.Windows.Forms.TextBox tbParamValue;

    }
}
