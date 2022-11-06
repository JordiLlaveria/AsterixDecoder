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
            this.filterByTargetAddressLabel = new System.Windows.Forms.Label();
            this.filterByTargetAddressButton = new System.Windows.Forms.Button();
            this.filterByTargetAddressTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.CAT10Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // CAT10Grid
            // 
            this.CAT10Grid.AllowUserToAddRows = false;
            this.CAT10Grid.AllowUserToDeleteRows = false;
            this.CAT10Grid.BackgroundColor = System.Drawing.Color.White;
            this.CAT10Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CAT10Grid.Location = new System.Drawing.Point(43, 89);
            this.CAT10Grid.Name = "CAT10Grid";
            this.CAT10Grid.ReadOnly = true;
            this.CAT10Grid.RowHeadersWidth = 51;
            this.CAT10Grid.RowTemplate.Height = 24;
            this.CAT10Grid.Size = new System.Drawing.Size(950, 458);
            this.CAT10Grid.TabIndex = 0;
            // 
            // filterByTargetAddressLabel
            // 
            this.filterByTargetAddressLabel.AutoSize = true;
            this.filterByTargetAddressLabel.Location = new System.Drawing.Point(39, 41);
            this.filterByTargetAddressLabel.Name = "filterByTargetAddressLabel";
            this.filterByTargetAddressLabel.Size = new System.Drawing.Size(122, 20);
            this.filterByTargetAddressLabel.TabIndex = 1;
            this.filterByTargetAddressLabel.Text = "Target Address:";
            // 
            // filterByTargetAddressButton
            // 
            this.filterByTargetAddressButton.Location = new System.Drawing.Point(350, 40);
            this.filterByTargetAddressButton.Name = "filterByTargetAddressButton";
            this.filterByTargetAddressButton.Size = new System.Drawing.Size(81, 28);
            this.filterByTargetAddressButton.TabIndex = 3;
            this.filterByTargetAddressButton.Text = "filter";
            this.filterByTargetAddressButton.UseVisualStyleBackColor = true;
            this.filterByTargetAddressButton.Click += new System.EventHandler(this.filterByTargetAddressButton_Click);
            // 
            // filterByTargetAddressTextBox
            // 
            this.filterByTargetAddressTextBox.Location = new System.Drawing.Point(203, 41);
            this.filterByTargetAddressTextBox.Name = "filterByTargetAddressTextBox";
            this.filterByTargetAddressTextBox.Size = new System.Drawing.Size(141, 26);
            this.filterByTargetAddressTextBox.TabIndex = 2;
            // 
            // Table10
            // 
            this.ClientSize = new System.Drawing.Size(1123, 676);
            this.Controls.Add(this.filterByTargetAddressButton);
            this.Controls.Add(this.filterByTargetAddressTextBox);
            this.Controls.Add(this.filterByTargetAddressLabel);
            this.Controls.Add(this.CAT10Grid);
            this.Name = "Table10";
            this.Load += new System.EventHandler(this.Table10_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CAT10Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CAT10DataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridView CAT10Grid;
        private System.Windows.Forms.Label filterByTargetAddressLabel;
        private System.Windows.Forms.Button filterByTargetAddressButton;
        private System.Windows.Forms.TextBox filterByTargetAddressTextBox;
    }
}