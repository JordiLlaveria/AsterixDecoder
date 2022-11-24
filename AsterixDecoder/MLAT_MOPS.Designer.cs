namespace AsterixDecoder
{
    partial class MLAT_MOPS
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
            this.buttonIDProb = new System.Windows.Forms.Button();
            this.buttonFalseDetect = new System.Windows.Forms.Button();
            this.labelID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonIDProb
            // 
            this.buttonIDProb.Location = new System.Drawing.Point(185, 51);
            this.buttonIDProb.Name = "buttonIDProb";
            this.buttonIDProb.Size = new System.Drawing.Size(386, 211);
            this.buttonIDProb.TabIndex = 0;
            this.buttonIDProb.Text = "Probability Identification";
            this.buttonIDProb.UseVisualStyleBackColor = true;
            this.buttonIDProb.Click += new System.EventHandler(this.buttonIDProb_Click);
            // 
            // buttonFalseDetect
            // 
            this.buttonFalseDetect.Location = new System.Drawing.Point(185, 280);
            this.buttonFalseDetect.Name = "buttonFalseDetect";
            this.buttonFalseDetect.Size = new System.Drawing.Size(386, 281);
            this.buttonFalseDetect.TabIndex = 1;
            this.buttonFalseDetect.Text = "Probability False Detection";
            this.buttonFalseDetect.UseVisualStyleBackColor = true;
            this.buttonFalseDetect.Click += new System.EventHandler(this.buttonFalseDetect_Click);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(643, 154);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(0, 16);
            this.labelID.TabIndex = 2;
            // 
            // MLAT_MOPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1446, 891);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.buttonFalseDetect);
            this.Controls.Add(this.buttonIDProb);
            this.Name = "MLAT_MOPS";
            this.Text = "MLAT";
            this.Load += new System.EventHandler(this.MLAT_MOPS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonIDProb;
        private System.Windows.Forms.Button buttonFalseDetect;
        private System.Windows.Forms.Label labelID;
    }
}