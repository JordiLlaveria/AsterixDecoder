namespace AsterixDecoder
{
    partial class Menu
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
            this.InputFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FileReadLabel = new System.Windows.Forms.Label();
            this.CAT10TableButton = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.CAT21Button = new System.Windows.Forms.Button();
            this.mapViewButton = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputFile
            // 
            this.InputFile.Location = new System.Drawing.Point(512, 349);
            this.InputFile.Name = "InputFile";
            this.InputFile.Size = new System.Drawing.Size(109, 47);
            this.InputFile.TabIndex = 0;
            this.InputFile.Text = "Input file";
            this.InputFile.UseVisualStyleBackColor = true;
            this.InputFile.Click += new System.EventHandler(this.OnClickFile);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 1;
            // 
            // FileReadLabel
            // 
            this.FileReadLabel.AutoSize = true;
            this.FileReadLabel.Location = new System.Drawing.Point(464, 418);
            this.FileReadLabel.Name = "FileReadLabel";
            this.FileReadLabel.Size = new System.Drawing.Size(138, 16);
            this.FileReadLabel.TabIndex = 2;
            this.FileReadLabel.Text = "Waiting to read the file";
            // 
            // CAT10TableButton
            // 
            this.CAT10TableButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CAT10TableButton.ForeColor = System.Drawing.Color.Black;
            this.CAT10TableButton.Location = new System.Drawing.Point(35, 115);
            this.CAT10TableButton.Name = "CAT10TableButton";
            this.CAT10TableButton.Size = new System.Drawing.Size(128, 55);
            this.CAT10TableButton.TabIndex = 3;
            this.CAT10TableButton.Text = "CAT10 Table";
            this.CAT10TableButton.UseVisualStyleBackColor = false;
            this.CAT10TableButton.Click += new System.EventHandler(this.CAT10TableButton_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.FileReadLabel);
            this.panelMenu.Controls.Add(this.label1);
            this.panelMenu.Controls.Add(this.InputFile);
            this.panelMenu.Location = new System.Drawing.Point(186, 27);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1525, 850);
            this.panelMenu.TabIndex = 4;
            // 
            // CAT21Button
            // 
            this.CAT21Button.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CAT21Button.Location = new System.Drawing.Point(35, 192);
            this.CAT21Button.Name = "CAT21Button";
            this.CAT21Button.Size = new System.Drawing.Size(128, 55);
            this.CAT21Button.TabIndex = 5;
            this.CAT21Button.Text = "CAT21 Table";
            this.CAT21Button.UseVisualStyleBackColor = false;
            this.CAT21Button.Click += new System.EventHandler(this.CAT21TableButton_Click);
            // 
            // mapViewButton
            // 
            this.mapViewButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mapViewButton.Location = new System.Drawing.Point(35, 267);
            this.mapViewButton.Name = "mapViewButton";
            this.mapViewButton.Size = new System.Drawing.Size(128, 55);
            this.mapViewButton.TabIndex = 6;
            this.mapViewButton.Text = "Map View";
            this.mapViewButton.UseVisualStyleBackColor = false;
            this.mapViewButton.Click += new System.EventHandler(this.mapViewButton_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 1055);
            this.Controls.Add(this.mapViewButton);
            this.Controls.Add(this.CAT21Button);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.CAT10TableButton);
            this.Name = "Menu";
            this.Text = "Form1";
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InputFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label FileReadLabel;
        private System.Windows.Forms.Button CAT10TableButton;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button CAT21Button;
        private System.Windows.Forms.Button mapViewButton;
    }
}

