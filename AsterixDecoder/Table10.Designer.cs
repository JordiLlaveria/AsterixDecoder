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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CAT10Grid = new System.Windows.Forms.DataGridView();
            this.filterByTargetAddressButton = new System.Windows.Forms.Button();
            this.filterByTargetAddressTextBox = new System.Windows.Forms.TextBox();
            this.resetFilterButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.filterLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CAT10Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // CAT10Grid
            // 
            this.CAT10Grid.AllowUserToAddRows = false;
            this.CAT10Grid.AllowUserToDeleteRows = false;
            this.CAT10Grid.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.CAT10Grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CAT10Grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CAT10Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = "No data";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CAT10Grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.CAT10Grid.EnableHeadersVisualStyles = false;
            this.CAT10Grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CAT10Grid.Location = new System.Drawing.Point(12, 71);
            this.CAT10Grid.Name = "CAT10Grid";
            this.CAT10Grid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CAT10Grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.CAT10Grid.RowHeadersWidth = 51;
            this.CAT10Grid.RowTemplate.Height = 24;
            this.CAT10Grid.Size = new System.Drawing.Size(1012, 500);
            this.CAT10Grid.TabIndex = 0;
            this.CAT10Grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CAT10Grid_CellClick);
            // 
            // filterByTargetAddressButton
            // 
            this.filterByTargetAddressButton.BackColor = System.Drawing.Color.Transparent;
            this.filterByTargetAddressButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterByTargetAddressButton.Font = new System.Drawing.Font("Rubik", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.filterByTargetAddressButton.Location = new System.Drawing.Point(467, 22);
            this.filterByTargetAddressButton.Name = "filterByTargetAddressButton";
            this.filterByTargetAddressButton.Size = new System.Drawing.Size(80, 28);
            this.filterByTargetAddressButton.TabIndex = 3;
            this.filterByTargetAddressButton.Text = "Filter";
            this.filterByTargetAddressButton.UseVisualStyleBackColor = false;
            this.filterByTargetAddressButton.Click += new System.EventHandler(this.filterByTargetAddressButton_Click);
            // 
            // filterByTargetAddressTextBox
            // 
            this.filterByTargetAddressTextBox.Font = new System.Drawing.Font("Rubik", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByTargetAddressTextBox.Location = new System.Drawing.Point(320, 22);
            this.filterByTargetAddressTextBox.Name = "filterByTargetAddressTextBox";
            this.filterByTargetAddressTextBox.Size = new System.Drawing.Size(141, 20);
            this.filterByTargetAddressTextBox.TabIndex = 2;
            // 
            // resetFilterButton
            // 
            this.resetFilterButton.BackColor = System.Drawing.Color.Transparent;
            this.resetFilterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetFilterButton.Font = new System.Drawing.Font("Rubik", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetFilterButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.resetFilterButton.Location = new System.Drawing.Point(553, 22);
            this.resetFilterButton.Name = "resetFilterButton";
            this.resetFilterButton.Size = new System.Drawing.Size(76, 28);
            this.resetFilterButton.TabIndex = 4;
            this.resetFilterButton.Text = "Reset";
            this.resetFilterButton.UseVisualStyleBackColor = false;
            this.resetFilterButton.Click += new System.EventHandler(this.resetFilterButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.Color.Transparent;
            this.exportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportButton.Font = new System.Drawing.Font("Rubik", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportButton.ForeColor = System.Drawing.Color.AliceBlue;
            this.exportButton.Location = new System.Drawing.Point(918, 22);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 28);
            this.exportButton.TabIndex = 5;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.Font = new System.Drawing.Font("Rubik", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(193, 22);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(121, 21);
            this.filterComboBox.TabIndex = 5;
            // 
            // filterLabel
            // 
            this.filterLabel.AutoSize = true;
            this.filterLabel.BackColor = System.Drawing.Color.Transparent;
            this.filterLabel.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterLabel.ForeColor = System.Drawing.Color.AliceBlue;
            this.filterLabel.Location = new System.Drawing.Point(93, 22);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(74, 19);
            this.filterLabel.TabIndex = 6;
            this.filterLabel.Text = "Filter by:";
            // 
            // Table10
            // 
            this.BackgroundImage = global::AsterixDecoder.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1528, 824);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.resetFilterButton);
            this.Controls.Add(this.filterByTargetAddressButton);
            this.Controls.Add(this.filterByTargetAddressTextBox);
            this.Controls.Add(this.CAT10Grid);
            this.DoubleBuffered = true;
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
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.Label filterLabel;
    }
}