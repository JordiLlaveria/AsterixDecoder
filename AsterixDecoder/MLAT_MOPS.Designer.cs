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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.buttonIDProb.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIDProb.ForeColor = System.Drawing.Color.AliceBlue;
            this.buttonIDProb.Location = new System.Drawing.Point(145, 64);
            this.buttonIDProb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonIDProb.Name = "buttonIDProb";
            this.buttonIDProb.Size = new System.Drawing.Size(434, 264);
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
            this.buttonFalseDetect.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFalseDetect.ForeColor = System.Drawing.Color.AliceBlue;
            this.buttonFalseDetect.Location = new System.Drawing.Point(143, 346);
            this.buttonFalseDetect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonFalseDetect.Name = "buttonFalseDetect";
            this.buttonFalseDetect.Size = new System.Drawing.Size(434, 351);
            this.buttonFalseDetect.TabIndex = 1;
            this.buttonFalseDetect.Text = "Probability False Detection";
            this.buttonFalseDetect.UseVisualStyleBackColor = false;
            this.buttonFalseDetect.Click += new System.EventHandler(this.buttonFalseDetect_Click);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(723, 192);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(0, 20);
            this.labelID.TabIndex = 2;
            // 
            // dataGridViewInfo
            // 
            this.dataGridViewInfo.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewInfo.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewInfo.Location = new System.Drawing.Point(705, 119);
            this.dataGridViewInfo.MultiSelect = false;
            this.dataGridViewInfo.Name = "dataGridViewInfo";
            this.dataGridViewInfo.ReadOnly = true;
            this.dataGridViewInfo.RowHeadersWidth = 51;
            this.dataGridViewInfo.RowTemplate.Height = 24;
            this.dataGridViewInfo.Size = new System.Drawing.Size(640, 506);
            this.dataGridViewInfo.TabIndex = 3;
            this.dataGridViewInfo.TabStop = false;
            // 
            // MLAT_MOPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BackgroundImage = global::AsterixDecoder.Properties.Resources.fondo2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1528, 824);
            this.Controls.Add(this.dataGridViewInfo);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.buttonFalseDetect);
            this.Controls.Add(this.buttonIDProb);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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