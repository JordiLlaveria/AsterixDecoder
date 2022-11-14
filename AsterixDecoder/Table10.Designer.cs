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
            this.filterByTargetAddressButton = new System.Windows.Forms.Button();
            this.filterByTargetAddressTextBox = new System.Windows.Forms.TextBox();
            this.resetFilterButton = new System.Windows.Forms.Button();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.filterLabel = new System.Windows.Forms.Label();
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
            this.CAT10Grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CAT10Grid_CellClick);
            // 
            // filterByTargetAddressButton
            // 
            this.filterByTargetAddressButton.Font = new System.Drawing.Font("Microsoft JhengHei", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressButton.Location = new System.Drawing.Point(467, 37);
            this.filterByTargetAddressButton.Name = "filterByTargetAddressButton";
            this.filterByTargetAddressButton.Size = new System.Drawing.Size(80, 28);
            this.filterByTargetAddressButton.TabIndex = 3;
            this.filterByTargetAddressButton.Text = "Filter";
            this.filterByTargetAddressButton.UseVisualStyleBackColor = true;
            this.filterByTargetAddressButton.Click += new System.EventHandler(this.filterByTargetAddressButton_Click);
            // 
            // filterByTargetAddressTextBox
            // 
            this.filterByTargetAddressTextBox.Location = new System.Drawing.Point(320, 37);
            this.filterByTargetAddressTextBox.Name = "filterByTargetAddressTextBox";
            this.filterByTargetAddressTextBox.Size = new System.Drawing.Size(141, 26);
            this.filterByTargetAddressTextBox.TabIndex = 2;
            // 
            // resetFilterButton
            // 
            this.resetFilterButton.Font = new System.Drawing.Font("Microsoft JhengHei", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetFilterButton.Location = new System.Drawing.Point(553, 35);
            this.resetFilterButton.Name = "resetFilterButton";
            this.resetFilterButton.Size = new System.Drawing.Size(76, 28);
            this.resetFilterButton.TabIndex = 4;
            this.resetFilterButton.Text = "Reset";
            this.resetFilterButton.UseVisualStyleBackColor = true;
            this.resetFilterButton.Click += new System.EventHandler(this.resetFilterButton_Click);
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(193, 35);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(121, 28);
            this.filterComboBox.TabIndex = 5;
            // 
            // filterLabel
            // 
            this.filterLabel.AutoSize = true;
            this.filterLabel.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterLabel.Location = new System.Drawing.Point(93, 38);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(91, 25);
            this.filterLabel.TabIndex = 6;
            this.filterLabel.Text = "Filter by:";
            // 
            // Table10
            // 
            this.ClientSize = new System.Drawing.Size(1123, 676);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.resetFilterButton);
            this.Controls.Add(this.filterByTargetAddressButton);
            this.Controls.Add(this.filterByTargetAddressTextBox);
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
        private System.Windows.Forms.Button filterByTargetAddressButton;
        private System.Windows.Forms.TextBox filterByTargetAddressTextBox;
        private System.Windows.Forms.Button resetFilterButton;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.Label filterLabel;
    }
}