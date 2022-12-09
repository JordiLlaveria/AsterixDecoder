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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CAT21Grid = new System.Windows.Forms.DataGridView();
            this.filterByTargetAddressLabel = new System.Windows.Forms.Label();
            this.filterByTargetAddressTextBox = new System.Windows.Forms.TextBox();
            this.filterByTargetAddressButton = new System.Windows.Forms.Button();
            this.resetFilterButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.CAT21Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // CAT21Grid
            // 
            this.CAT21Grid.AllowUserToAddRows = false;
            this.CAT21Grid.AllowUserToDeleteRows = false;
            this.CAT21Grid.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.CAT21Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.NullValue = "No data";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CAT21Grid.DefaultCellStyle = dataGridViewCellStyle1;
            this.CAT21Grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CAT21Grid.Location = new System.Drawing.Point(-1, 91);
            this.CAT21Grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CAT21Grid.Name = "CAT21Grid";
            this.CAT21Grid.ReadOnly = true;
            this.CAT21Grid.RowHeadersWidth = 51;
            this.CAT21Grid.RowTemplate.Height = 24;
            this.CAT21Grid.Size = new System.Drawing.Size(1368, 628);
            this.CAT21Grid.TabIndex = 1;
            this.CAT21Grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CAT21Grid_CellClick);
            // 
            // filterByTargetAddressLabel
            // 
            this.filterByTargetAddressLabel.AutoSize = true;
            this.filterByTargetAddressLabel.BackColor = System.Drawing.Color.Transparent;
            this.filterByTargetAddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressLabel.ForeColor = System.Drawing.Color.AliceBlue;
            this.filterByTargetAddressLabel.Location = new System.Drawing.Point(56, 39);
            this.filterByTargetAddressLabel.Name = "filterByTargetAddressLabel";
            this.filterByTargetAddressLabel.Size = new System.Drawing.Size(86, 25);
            this.filterByTargetAddressLabel.TabIndex = 2;
            this.filterByTargetAddressLabel.Text = "Filter by:";
            // 
            // filterByTargetAddressTextBox
            // 
            this.filterByTargetAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressTextBox.Location = new System.Drawing.Point(298, 41);
            this.filterByTargetAddressTextBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.filterByTargetAddressTextBox.Name = "filterByTargetAddressTextBox";
            this.filterByTargetAddressTextBox.Size = new System.Drawing.Size(125, 23);
            this.filterByTargetAddressTextBox.TabIndex = 3;
            // 
            // filterByTargetAddressButton
            // 
            this.filterByTargetAddressButton.BackColor = System.Drawing.Color.Transparent;
            this.filterByTargetAddressButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterByTargetAddressButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.filterByTargetAddressButton.Location = new System.Drawing.Point(438, 34);
            this.filterByTargetAddressButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.filterByTargetAddressButton.Name = "filterByTargetAddressButton";
            this.filterByTargetAddressButton.Size = new System.Drawing.Size(81, 35);
            this.filterByTargetAddressButton.TabIndex = 4;
            this.filterByTargetAddressButton.Text = "Filter";
            this.filterByTargetAddressButton.UseVisualStyleBackColor = false;
            this.filterByTargetAddressButton.Click += new System.EventHandler(this.filterByTargetAddressButton_Click);
            // 
            // resetFilterButton
            // 
            this.resetFilterButton.BackColor = System.Drawing.Color.Transparent;
            this.resetFilterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetFilterButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.resetFilterButton.Location = new System.Drawing.Point(538, 34);
            this.resetFilterButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.resetFilterButton.Name = "resetFilterButton";
            this.resetFilterButton.Size = new System.Drawing.Size(89, 35);
            this.resetFilterButton.TabIndex = 5;
            this.resetFilterButton.Text = "Reset";
            this.resetFilterButton.UseVisualStyleBackColor = false;
            this.resetFilterButton.Click += new System.EventHandler(this.resetFilterButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.Color.Transparent;
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.exportButton.Location = new System.Drawing.Point(1258, 39);
            this.exportButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(87, 42);
            this.exportButton.TabIndex = 6;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(173, 40);
            this.filterComboBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(108, 24);
            this.filterComboBox.TabIndex = 6;
            // 
            // Table21
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AsterixDecoder.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1711, 777);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.resetFilterButton);
            this.Controls.Add(this.filterByTargetAddressButton);
            this.Controls.Add(this.filterByTargetAddressTextBox);
            this.Controls.Add(this.filterByTargetAddressLabel);
            this.Controls.Add(this.CAT21Grid);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
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
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.ComboBox filterComboBox;
    }
}