namespace AsterixDecoder
{
    partial class Table10
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
            this.CAT10Grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.CAT10Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // CAT10Grid
            // 
            this.CAT10Grid.AllowUserToAddRows = false;
            this.CAT10Grid.AllowUserToDeleteRows = false;
            this.CAT10Grid.BackgroundColor = System.Drawing.Color.White;
            this.CAT10Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CAT10Grid.Location = new System.Drawing.Point(43, 47);
            this.CAT10Grid.Name = "CAT10Grid";
            this.CAT10Grid.ReadOnly = true;
            this.CAT10Grid.RowHeadersWidth = 51;
            this.CAT10Grid.RowTemplate.Height = 24;
            this.CAT10Grid.Size = new System.Drawing.Size(950, 500);
            this.CAT10Grid.TabIndex = 0;
            // 
            // Table10
            // 
            this.ClientSize = new System.Drawing.Size(1123, 676);
            this.Controls.Add(this.CAT10Grid);
            this.Name = "Table10";
            this.Load += new System.EventHandler(this.Table10_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CAT10Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CAT10DataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridView CAT10Grid;
    }
}