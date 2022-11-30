﻿namespace AsterixDecoder
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
            this.buttonIDProb.Location = new System.Drawing.Point(129, 82);
            this.buttonIDProb.Name = "buttonIDProb";
            this.buttonIDProb.Size = new System.Drawing.Size(386, 211);
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
            this.buttonFalseDetect.Location = new System.Drawing.Point(127, 308);
            this.buttonFalseDetect.Name = "buttonFalseDetect";
            this.buttonFalseDetect.Size = new System.Drawing.Size(386, 281);
            this.buttonFalseDetect.TabIndex = 1;
            this.buttonFalseDetect.Text = "Probability False Detection";
            this.buttonFalseDetect.UseVisualStyleBackColor = false;
            this.buttonFalseDetect.Click += new System.EventHandler(this.buttonFalseDetect_Click);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(643, 185);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(0, 16);
            this.labelID.TabIndex = 2;
            // 
            // dataGridViewInfo
            // 
            this.dataGridViewInfo.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewInfo.Location = new System.Drawing.Point(627, 82);
            this.dataGridViewInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewInfo.MultiSelect = false;
            this.dataGridViewInfo.Name = "dataGridViewInfo";
            this.dataGridViewInfo.ReadOnly = true;
            this.dataGridViewInfo.RowHeadersWidth = 51;
            this.dataGridViewInfo.RowTemplate.Height = 24;
            this.dataGridViewInfo.Size = new System.Drawing.Size(569, 507);
            this.dataGridViewInfo.TabIndex = 3;
            this.dataGridViewInfo.TabStop = false;
            // 
            // MLAT_MOPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BackgroundImage = global::AsterixDecoder.Properties.Resources.fondo2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1358, 659);
            this.Controls.Add(this.dataGridViewInfo);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.buttonFalseDetect);
            this.Controls.Add(this.buttonIDProb);
            this.DoubleBuffered = true;
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