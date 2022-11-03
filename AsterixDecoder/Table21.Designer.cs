namespace AsterixDecoder
{
    partial class Table21
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
            this.CAT21Grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.CAT21Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // CAT21Grid
            // 
            this.CAT21Grid.AllowUserToAddRows = false;
            this.CAT21Grid.AllowUserToDeleteRows = false;
            this.CAT21Grid.BackgroundColor = System.Drawing.Color.White;
            this.CAT21Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CAT21Grid.Location = new System.Drawing.Point(57, 82);
            this.CAT21Grid.Name = "CAT21Grid";
            this.CAT21Grid.ReadOnly = true;
            this.CAT21Grid.RowHeadersWidth = 51;
            this.CAT21Grid.RowTemplate.Height = 24;
            this.CAT21Grid.Size = new System.Drawing.Size(1200, 650);
            this.CAT21Grid.TabIndex = 1;
            // 
            // Table21
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 1030);
            this.Controls.Add(this.CAT21Grid);
            this.Name = "Table21";
            this.Text = "Table21";
            this.Load += new System.EventHandler(this.Table21_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CAT21Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CAT21Grid;
    }
}