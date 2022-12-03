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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonIDProb = new System.Windows.Forms.Button();
            this.buttonFalseDetect = new System.Windows.Forms.Button();
            this.labelID = new System.Windows.Forms.Label();
            this.dataGridViewInfo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonIDProb
            // 
            this.buttonIDProb.BackColor = System.Drawing.Color.Transparent;
            this.buttonIDProb.FlatAppearance.BorderSize = 5;
            this.buttonIDProb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonIDProb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIDProb.ForeColor = System.Drawing.Color.AliceBlue;
            this.buttonIDProb.Location = new System.Drawing.Point(97, 42);
            this.buttonIDProb.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonIDProb.Name = "buttonIDProb";
            this.buttonIDProb.Size = new System.Drawing.Size(289, 172);
            this.buttonIDProb.TabIndex = 0;
            this.buttonIDProb.Text = "Probability Identification";
            this.buttonIDProb.UseVisualStyleBackColor = false;
            this.buttonIDProb.Click += new System.EventHandler(this.buttonIDProb_Click);
            // 
            // buttonFalseDetect
            // 
            this.buttonFalseDetect.BackColor = System.Drawing.Color.Transparent;
            this.buttonFalseDetect.FlatAppearance.BorderSize = 5;
            this.buttonFalseDetect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFalseDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFalseDetect.ForeColor = System.Drawing.Color.AliceBlue;
            this.buttonFalseDetect.Location = new System.Drawing.Point(95, 225);
            this.buttonFalseDetect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonFalseDetect.Name = "buttonFalseDetect";
            this.buttonFalseDetect.Size = new System.Drawing.Size(289, 228);
            this.buttonFalseDetect.TabIndex = 1;
            this.buttonFalseDetect.Text = "Probability False Detection";
            this.buttonFalseDetect.UseVisualStyleBackColor = false;
            this.buttonFalseDetect.Click += new System.EventHandler(this.buttonFalseDetect_Click);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(482, 125);
            this.labelID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(0, 13);
            this.labelID.TabIndex = 2;
            // 
            // dataGridViewInfo
            // 
            this.dataGridViewInfo.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Rubik", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(88)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(95)))), ((int)(((byte)(112)))));

            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(88)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(95)))), ((int)(((byte)(112)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewInfo.Location = new System.Drawing.Point(470, 77);
            this.dataGridViewInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewInfo.MultiSelect = false;
            this.dataGridViewInfo.Name = "dataGridViewInfo";
            this.dataGridViewInfo.ReadOnly = true;
            this.dataGridViewInfo.RowHeadersWidth = 51;
            this.dataGridViewInfo.RowTemplate.Height = 24;
            this.dataGridViewInfo.Size = new System.Drawing.Size(427, 329);
            this.dataGridViewInfo.TabIndex = 3;
            this.dataGridViewInfo.TabStop = false;
            // 
            // MLAT_MOPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BackgroundImage = global::AsterixDecoder.Properties.Resources.fondo2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1019, 536);
            this.Controls.Add(this.dataGridViewInfo);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.buttonFalseDetect);
            this.Controls.Add(this.buttonIDProb);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MLAT_MOPS";
            this.Text = "MLAT";
            this.Load += new System.EventHandler(this.MLAT_MOPS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonIDProb;
        private System.Windows.Forms.Button buttonFalseDetect;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.DataGridView dataGridViewInfo;
    }
}