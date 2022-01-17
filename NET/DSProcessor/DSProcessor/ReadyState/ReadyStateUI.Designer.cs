namespace DSProcessor.ReadyState
{
    partial class ReadyStateUI
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
          this.lblTitle = new System.Windows.Forms.Label();
          this.lblAuthor = new System.Windows.Forms.Label();
          this.btnEffectChain = new System.Windows.Forms.Button();
          this.btnEffectBank = new System.Windows.Forms.Button();
          this.btnLiveProcessor = new System.Windows.Forms.Button();
          this.SuspendLayout();
          // 
          // lblTitle
          // 
          this.lblTitle.AutoSize = true;
          this.lblTitle.Font = new System.Drawing.Font("probot", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lblTitle.Location = new System.Drawing.Point(70, 25);
          this.lblTitle.Name = "lblTitle";
          this.lblTitle.Size = new System.Drawing.Size(367, 30);
          this.lblTitle.TabIndex = 0;
          this.lblTitle.Text = "DS-Processor";
          // 
          // lblAuthor
          // 
          this.lblAuthor.AutoSize = true;
          this.lblAuthor.Font = new System.Drawing.Font("probot", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lblAuthor.Location = new System.Drawing.Point(150, 87);
          this.lblAuthor.Name = "lblAuthor";
          this.lblAuthor.Size = new System.Drawing.Size(183, 16);
          this.lblAuthor.TabIndex = 1;
          this.lblAuthor.Text = "John S. Walter";
          // 
          // btnEffectChain
          // 
          this.btnEffectChain.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnEffectChain.Location = new System.Drawing.Point(27, 147);
          this.btnEffectChain.Name = "btnEffectChain";
          this.btnEffectChain.Size = new System.Drawing.Size(183, 45);
          this.btnEffectChain.TabIndex = 2;
          this.btnEffectChain.Text = "Effect-Chain Designer";
          this.btnEffectChain.UseVisualStyleBackColor = true;
          this.btnEffectChain.Click += new System.EventHandler(this.btnEffectChain_Click);
          // 
          // btnEffectBank
          // 
          this.btnEffectBank.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnEffectBank.Location = new System.Drawing.Point(276, 147);
          this.btnEffectBank.Name = "btnEffectBank";
          this.btnEffectBank.Size = new System.Drawing.Size(183, 45);
          this.btnEffectBank.TabIndex = 3;
          this.btnEffectBank.Text = "Effect-Bank Designer";
          this.btnEffectBank.UseVisualStyleBackColor = true;
          this.btnEffectBank.Click += new System.EventHandler(this.btnEffectBank_Click);
          // 
          // btnLiveProcessor
          // 
          this.btnLiveProcessor.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btnLiveProcessor.Location = new System.Drawing.Point(153, 211);
          this.btnLiveProcessor.Name = "btnLiveProcessor";
          this.btnLiveProcessor.Size = new System.Drawing.Size(180, 35);
          this.btnLiveProcessor.TabIndex = 4;
          this.btnLiveProcessor.Text = "Run Live Processor";
          this.btnLiveProcessor.UseVisualStyleBackColor = true;
          this.btnLiveProcessor.Click += new System.EventHandler(this.btnLiveProcessor_Click);
          // 
          // ReadyStateUI
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(504, 266);
          this.Controls.Add(this.btnLiveProcessor);
          this.Controls.Add(this.btnEffectBank);
          this.Controls.Add(this.btnEffectChain);
          this.Controls.Add(this.lblAuthor);
          this.Controls.Add(this.lblTitle);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.Name = "ReadyStateUI";
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Button btnEffectChain;
        private System.Windows.Forms.Button btnEffectBank;
        private System.Windows.Forms.Button btnLiveProcessor;
    }
}