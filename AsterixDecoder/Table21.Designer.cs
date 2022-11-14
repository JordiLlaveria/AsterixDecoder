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
            this.filterByTargetAddressLabel = new System.Windows.Forms.Label();
            this.filterByTargetAddressTextBox = new System.Windows.Forms.TextBox();
            this.filterByTargetAddressButton = new System.Windows.Forms.Button();
            this.resetFilterButton = new System.Windows.Forms.Button();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.CAT21Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // CAT21Grid
            // 
            this.CAT21Grid.AllowUserToAddRows = false;
            this.CAT21Grid.AllowUserToDeleteRows = false;
            this.CAT21Grid.BackgroundColor = System.Drawing.Color.White;
            this.CAT21Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CAT21Grid.Location = new System.Drawing.Point(64, 102);
            this.CAT21Grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CAT21Grid.Name = "CAT21Grid";
            this.CAT21Grid.ReadOnly = true;
            this.CAT21Grid.RowHeadersWidth = 51;
            this.CAT21Grid.RowTemplate.Height = 24;
            this.CAT21Grid.Size = new System.Drawing.Size(1350, 812);
            this.CAT21Grid.TabIndex = 1;
            this.CAT21Grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CAT21Grid_CellClick);
            // 
            // filterByTargetAddressLabel
            // 
            this.filterByTargetAddressLabel.AutoSize = true;
            this.filterByTargetAddressLabel.Font = new System.Drawing.Font("Microsoft JhengHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressLabel.Location = new System.Drawing.Point(79, 51);
            this.filterByTargetAddressLabel.Name = "filterByTargetAddressLabel";
            this.filterByTargetAddressLabel.Size = new System.Drawing.Size(91, 25);
            this.filterByTargetAddressLabel.TabIndex = 2;
            this.filterByTargetAddressLabel.Text = "Filter by:";
            // 
            // filterByTargetAddressTextBox
            // 
            this.filterByTargetAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressTextBox.Location = new System.Drawing.Point(303, 51);
            this.filterByTargetAddressTextBox.Name = "filterByTargetAddressTextBox";
            this.filterByTargetAddressTextBox.Size = new System.Drawing.Size(122, 28);
            this.filterByTargetAddressTextBox.TabIndex = 3;
            // 
            // filterByTargetAddressButton
            // 
            this.filterByTargetAddressButton.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressButton.Location = new System.Drawing.Point(431, 51);
            this.filterByTargetAddressButton.Name = "filterByTargetAddressButton";
            this.filterByTargetAddressButton.Size = new System.Drawing.Size(87, 28);
            this.filterByTargetAddressButton.TabIndex = 4;
            this.filterByTargetAddressButton.Text = "Filter";
            this.filterByTargetAddressButton.UseVisualStyleBackColor = true;
            this.filterByTargetAddressButton.Click += new System.EventHandler(this.filterByTargetAddressButton_Click);
            // 
            // resetFilterButton
            // 
            this.resetFilterButton.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetFilterButton.Location = new System.Drawing.Point(524, 51);
            this.resetFilterButton.Name = "resetFilterButton";
            this.resetFilterButton.Size = new System.Drawing.Size(83, 28);
            this.resetFilterButton.TabIndex = 5;
            this.resetFilterButton.Text = "Reset";
            this.resetFilterButton.UseVisualStyleBackColor = true;
            this.resetFilterButton.Click += new System.EventHandler(this.resetFilterButton_Click);
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(176, 51);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(121, 28);
            this.filterComboBox.TabIndex = 6;
            // 
            // Table21
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1557, 1050);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.resetFilterButton);
            this.Controls.Add(this.filterByTargetAddressButton);
            this.Controls.Add(this.filterByTargetAddressTextBox);
            this.Controls.Add(this.filterByTargetAddressLabel);
            this.Controls.Add(this.CAT21Grid);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Table21";
            this.Text = "Table21";
            this.Load += new System.EventHandler(this.Table21_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CAT21Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CAT21Grid;
        private System.Windows.Forms.Label filterByTargetAddressLabel;
        private System.Windows.Forms.TextBox filterByTargetAddressTextBox;
        private System.Windows.Forms.Button filterByTargetAddressButton;
        private System.Windows.Forms.Button resetFilterButton;
        private System.Windows.Forms.ComboBox filterComboBox;
    }
}