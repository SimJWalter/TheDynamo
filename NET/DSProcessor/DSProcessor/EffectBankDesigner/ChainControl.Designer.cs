namespace DSProcessor.EffectBankDesigner
{
    partial class ChainControl
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
          this.lblOrder = new System.Windows.Forms.Label();
          this.lblName = new System.Windows.Forms.Label();
          this.btnUp = new System.Windows.Forms.Button();
          this.btnDown = new System.Windows.Forms.Button();
          this.btnDetails = new System.Windows.Forms.Button();
          this.btnDelete = new System.Windows.Forms.Button();
          this.SuspendLayout();
          // 
          // lblOrder
          // 
          this.lblOrder.AutoSize = true;
          this.lblOrder.Location = new System.Drawing.Point(3, 0);
          this.lblOrder.MinimumSize = new System.Drawing.Size(30, 0);
          this.lblOrder.Name = "lblOrder";
          this.lblOrder.Size = new System.Drawing.Size(30, 13);
          this.lblOrder.TabIndex = 0;
          // 
          // lblName
          // 
          this.lblName.AutoSize = true;
          this.lblName.Location = new System.Drawing.Point(39, 0);
          this.lblName.MinimumSize = new System.Drawing.Size(180, 0);
          this.lblName.Name = "lblName";
          this.lblName.Size = new System.Drawing.Size(180, 13);
          this.lblName.TabIndex = 1;
          // 
          // btnUp
          // 
          this.btnUp.Location = new System.Drawing.Point(225, 0);
          this.btnUp.Name = "btnUp";
          this.btnUp.Size = new System.Drawing.Size(46, 25);
          this.btnUp.TabIndex = 2;
          this.btnUp.Text = "Up";
          this.btnUp.UseVisualStyleBackColor = true;
          this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
          // 
          // btnDown
          // 
          this.btnDown.Location = new System.Drawing.Point(277, 0);
          this.btnDown.Name = "btnDown";
          this.btnDown.Size = new System.Drawing.Size(46, 25);
          this.btnDown.TabIndex = 3;
          this.btnDown.Text = "Down";
          this.btnDown.UseVisualStyleBackColor = true;
          this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
          // 
          // btnDetails
          // 
          this.btnDetails.Location = new System.Drawing.Point(329, 0);
          this.btnDetails.Name = "btnDetails";
          this.btnDetails.Size = new System.Drawing.Size(67, 25);
          this.btnDetails.TabIndex = 4;
          this.btnDetails.Text = "Details";
          this.btnDetails.UseVisualStyleBackColor = true;
          this.btnDetails.Click += new System.EventHandler(this.btnConfig_Click);
          // 
          // btnDelete
          // 
          this.btnDelete.Image = global::DSProcessor.Properties.Resources.bin;
          this.btnDelete.Location = new System.Drawing.Point(411, 0);
          this.btnDelete.Name = "btnDelete";
          this.btnDelete.Size = new System.Drawing.Size(27, 25);
          this.btnDelete.TabIndex = 5;
          this.btnDelete.UseVisualStyleBackColor = true;
          this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
          // 
          // ChainControl
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.Controls.Add(this.btnDelete);
          this.Controls.Add(this.btnDetails);
          this.Controls.Add(this.btnDown);
          this.Controls.Add(this.btnUp);
          this.Controls.Add(this.lblName);
          this.Controls.Add(this.lblOrder);
          this.MaximumSize = new System.Drawing.Size(450, 32);
          this.MinimumSize = new System.Drawing.Size(450, 32);
          this.Name = "ChainControl";
          this.Size = new System.Drawing.Size(446, 28);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Button btnDelete;
    }
}
