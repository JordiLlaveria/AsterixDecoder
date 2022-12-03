namespace AsterixDecoder
{
    partial class MapView
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
            this.components = new System.ComponentModel.Container();
            this.dataMarker = new System.Windows.Forms.DataGridView();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buttonX1 = new System.Windows.Forms.Button();
            this.buttonX2 = new System.Windows.Forms.Button();
            this.buttonX5 = new System.Windows.Forms.Button();
            this.buttonX10 = new System.Windows.Forms.Button();
            this.buttonX20 = new System.Windows.Forms.Button();
            this.checkBoxSMR = new System.Windows.Forms.CheckBox();
            this.checkBoxMLAT = new System.Windows.Forms.CheckBox();
            this.checkBoxADSB = new System.Windows.Forms.CheckBox();
            this.textBoxHour = new System.Windows.Forms.TextBox();
            this.textBoxMinutes = new System.Windows.Forms.TextBox();
            this.textBoxSeconds = new System.Windows.Forms.TextBox();
            this.buttonSelectTime = new System.Windows.Forms.Button();
            this.buttonExportKML = new System.Windows.Forms.Button();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.filterButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.seeAllButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.trajectoryCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataMarker)).BeginInit();
            this.SuspendLayout();
            // 
            // dataMarker
            // 
            this.dataMarker.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dataMarker.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataMarker.Location = new System.Drawing.Point(9, 3);
            this.dataMarker.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.dataMarker.Name = "dataMarker";
            this.dataMarker.ReadOnly = true;
            this.dataMarker.RowHeadersWidth = 51;
            this.dataMarker.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataMarker.RowTemplate.Height = 24;
            this.dataMarker.Size = new System.Drawing.Size(1016, 58);
            this.dataMarker.TabIndex = 1;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(9, 81);
            this.gMapControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(767, 512);
            this.gMapControl1.TabIndex = 2;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.Load += new System.EventHandler(this.gMapControl1_Load_1);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Font = new System.Drawing.Font("Rubik", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.Location = new System.Drawing.Point(803, 150);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(96, 52);
            this.buttonPlay.TabIndex = 3;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("Rubik", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.Black;
            this.labelTime.Location = new System.Drawing.Point(901, 158);
            this.labelTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(144, 35);
            this.labelTime.TabIndex = 4;
            this.labelTime.Text = "08:00:00";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer1s_Tick);
            // 
            // buttonX1
            // 
            this.buttonX1.BackColor = System.Drawing.Color.White;
            this.buttonX1.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(804, 90);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(43, 33);
            this.buttonX1.TabIndex = 5;
            this.buttonX1.Text = "x1";
            this.buttonX1.UseVisualStyleBackColor = false;
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.BackColor = System.Drawing.Color.White;
            this.buttonX2.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX2.Location = new System.Drawing.Point(847, 90);
            this.buttonX2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(37, 33);
            this.buttonX2.TabIndex = 6;
            this.buttonX2.Text = "x2";
            this.buttonX2.UseVisualStyleBackColor = false;
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX5
            // 
            this.buttonX5.BackColor = System.Drawing.Color.White;
            this.buttonX5.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX5.Location = new System.Drawing.Point(883, 90);
            this.buttonX5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(36, 33);
            this.buttonX5.TabIndex = 7;
            this.buttonX5.Text = "x5";
            this.buttonX5.UseVisualStyleBackColor = false;
            this.buttonX5.Click += new System.EventHandler(this.buttonX5_Click);
            // 
            // buttonX10
            // 
            this.buttonX10.BackColor = System.Drawing.Color.White;
            this.buttonX10.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX10.Location = new System.Drawing.Point(919, 90);
            this.buttonX10.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonX10.Name = "buttonX10";
            this.buttonX10.Size = new System.Drawing.Size(50, 33);
            this.buttonX10.TabIndex = 8;
            this.buttonX10.Text = "x10";
            this.buttonX10.UseVisualStyleBackColor = false;
            this.buttonX10.Click += new System.EventHandler(this.buttonX10_Click);
            // 
            // buttonX20
            // 
            this.buttonX20.BackColor = System.Drawing.Color.White;
            this.buttonX20.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX20.Location = new System.Drawing.Point(968, 90);
            this.buttonX20.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonX20.Name = "buttonX20";
            this.buttonX20.Size = new System.Drawing.Size(52, 33);
            this.buttonX20.TabIndex = 9;
            this.buttonX20.Text = "x20";
            this.buttonX20.UseVisualStyleBackColor = false;
            this.buttonX20.Click += new System.EventHandler(this.buttonX20_Click);
            // 
            // checkBoxSMR
            // 
            this.checkBoxSMR.AutoSize = true;
            this.checkBoxSMR.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSMR.Checked = true;
            this.checkBoxSMR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSMR.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSMR.Location = new System.Drawing.Point(804, 362);
            this.checkBoxSMR.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.checkBoxSMR.Name = "checkBoxSMR";
            this.checkBoxSMR.Size = new System.Drawing.Size(81, 32);
            this.checkBoxSMR.TabIndex = 10;
            this.checkBoxSMR.Text = "SMR";
            this.checkBoxSMR.UseVisualStyleBackColor = false;
            // 
            // checkBoxMLAT
            // 
            this.checkBoxMLAT.AutoSize = true;
            this.checkBoxMLAT.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxMLAT.Checked = true;
            this.checkBoxMLAT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMLAT.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMLAT.Location = new System.Drawing.Point(804, 393);
            this.checkBoxMLAT.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.checkBoxMLAT.Name = "checkBoxMLAT";
            this.checkBoxMLAT.Size = new System.Drawing.Size(94, 32);
            this.checkBoxMLAT.TabIndex = 11;
            this.checkBoxMLAT.Text = "MLAT";
            this.checkBoxMLAT.UseVisualStyleBackColor = false;
            // 
            // checkBoxADSB
            // 
            this.checkBoxADSB.AutoSize = true;
            this.checkBoxADSB.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxADSB.Checked = true;
            this.checkBoxADSB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxADSB.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxADSB.Location = new System.Drawing.Point(804, 424);
            this.checkBoxADSB.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.checkBoxADSB.Name = "checkBoxADSB";
            this.checkBoxADSB.Size = new System.Drawing.Size(95, 32);
            this.checkBoxADSB.TabIndex = 12;
            this.checkBoxADSB.Text = "ADSB";
            this.checkBoxADSB.UseVisualStyleBackColor = false;
            // 
            // textBoxHour
            // 
            this.textBoxHour.BackColor = System.Drawing.Color.AliceBlue;
            this.textBoxHour.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxHour.Font = new System.Drawing.Font("Rubik", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxHour.Location = new System.Drawing.Point(903, 258);
            this.textBoxHour.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxHour.Name = "textBoxHour";
            this.textBoxHour.Size = new System.Drawing.Size(39, 35);
            this.textBoxHour.TabIndex = 13;
            this.textBoxHour.Text = "08";
            // 
            // textBoxMinutes
            // 
            this.textBoxMinutes.BackColor = System.Drawing.Color.AliceBlue;
            this.textBoxMinutes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMinutes.Font = new System.Drawing.Font("Rubik", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMinutes.Location = new System.Drawing.Point(945, 258);
            this.textBoxMinutes.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxMinutes.Name = "textBoxMinutes";
            this.textBoxMinutes.Size = new System.Drawing.Size(37, 35);
            this.textBoxMinutes.TabIndex = 14;
            this.textBoxMinutes.Text = "00";
            // 
            // textBoxSeconds
            // 
            this.textBoxSeconds.BackColor = System.Drawing.Color.AliceBlue;
            this.textBoxSeconds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSeconds.Font = new System.Drawing.Font("Rubik", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSeconds.Location = new System.Drawing.Point(983, 258);
            this.textBoxSeconds.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.textBoxSeconds.Name = "textBoxSeconds";
            this.textBoxSeconds.Size = new System.Drawing.Size(37, 35);
            this.textBoxSeconds.TabIndex = 15;
            this.textBoxSeconds.Text = "00";
            // 
            // buttonSelectTime
            // 
            this.buttonSelectTime.Font = new System.Drawing.Font("Rubik", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectTime.Location = new System.Drawing.Point(803, 259);
            this.buttonSelectTime.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonSelectTime.Name = "buttonSelectTime";
            this.buttonSelectTime.Size = new System.Drawing.Size(89, 32);
            this.buttonSelectTime.TabIndex = 17;
            this.buttonSelectTime.Text = "Select Time";
            this.buttonSelectTime.UseVisualStyleBackColor = true;
            this.buttonSelectTime.Click += new System.EventHandler(this.buttonSelectTime_Click);
            // 
            // buttonExportKML
            // 
            this.buttonExportKML.Font = new System.Drawing.Font("Rubik", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExportKML.Location = new System.Drawing.Point(803, 304);
            this.buttonExportKML.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.buttonExportKML.Name = "buttonExportKML";
            this.buttonExportKML.Size = new System.Drawing.Size(202, 38);
            this.buttonExportKML.TabIndex = 18;
            this.buttonExportKML.Text = "Export KML";
            this.buttonExportKML.UseVisualStyleBackColor = true;
            this.buttonExportKML.Click += new System.EventHandler(this.buttonExportKML_Click);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterTextBox.Location = new System.Drawing.Point(803, 489);
            this.filterTextBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(127, 26);
            this.filterTextBox.TabIndex = 10;
            // 
            // filterButton
            // 
            this.filterButton.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterButton.Location = new System.Drawing.Point(933, 486);
            this.filterButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(71, 26);
            this.filterButton.TabIndex = 11;
            this.filterButton.Text = "Find";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.AliceBlue;
            this.label1.Location = new System.Drawing.Point(800, 461);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Filter by Target Address:";
            // 
            // seeAllButton
            // 
            this.seeAllButton.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seeAllButton.Location = new System.Drawing.Point(804, 558);
            this.seeAllButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.seeAllButton.Name = "seeAllButton";
            this.seeAllButton.Size = new System.Drawing.Size(201, 26);
            this.seeAllButton.TabIndex = 13;
            this.seeAllButton.Text = "See All";
            this.seeAllButton.UseVisualStyleBackColor = true;
            this.seeAllButton.Click += new System.EventHandler(this.seeAllButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Font = new System.Drawing.Font("Rubik", 9.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopButton.Location = new System.Drawing.Point(907, 216);
            this.stopButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(97, 32);
            this.stopButton.TabIndex = 14;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // restartButton
            // 
            this.restartButton.Font = new System.Drawing.Font("Rubik", 9.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartButton.Location = new System.Drawing.Point(803, 216);
            this.restartButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(95, 32);
            this.restartButton.TabIndex = 15;
            this.restartButton.Text = "RESTART";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // trajectoryCheckBox
            // 
            this.trajectoryCheckBox.AutoSize = true;
            this.trajectoryCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.trajectoryCheckBox.Font = new System.Drawing.Font("Rubik", 9.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trajectoryCheckBox.ForeColor = System.Drawing.Color.AliceBlue;
            this.trajectoryCheckBox.Location = new System.Drawing.Point(803, 521);
            this.trajectoryCheckBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.trajectoryCheckBox.Name = "trajectoryCheckBox";
            this.trajectoryCheckBox.Size = new System.Drawing.Size(123, 21);
            this.trajectoryCheckBox.TabIndex = 19;
            this.trajectoryCheckBox.Text = "See Trajectory";
            this.trajectoryCheckBox.UseVisualStyleBackColor = false;
            this.trajectoryCheckBox.CheckedChanged += new System.EventHandler(this.trajectoryCheckBox_CheckedChanged);
            // 
            // MapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::AsterixDecoder.Properties.Resources.fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1100, 618);
            this.Controls.Add(this.trajectoryCheckBox);
            this.Controls.Add(this.buttonExportKML);
            this.Controls.Add(this.buttonSelectTime);
            this.Controls.Add(this.textBoxMinutes);
            this.Controls.Add(this.textBoxSeconds);
            this.Controls.Add(this.textBoxHour);
            this.Controls.Add(this.checkBoxADSB);
            this.Controls.Add(this.checkBoxMLAT);
            this.Controls.Add(this.checkBoxSMR);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.seeAllButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(this.buttonX20);
            this.Controls.Add(this.buttonX10);
            this.Controls.Add(this.buttonX5);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.dataMarker);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MapView";
            this.Text = "MapView";
            this.TransparencyKey = System.Drawing.Color.White;
            ((System.ComponentModel.ISupportInitialize)(this.dataMarker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataMarker;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonX1;
        private System.Windows.Forms.Button buttonX2;
        private System.Windows.Forms.Button buttonX5;
        private System.Windows.Forms.Button buttonX10;
        private System.Windows.Forms.Button buttonX20;
        private System.Windows.Forms.CheckBox checkBoxSMR;
        private System.Windows.Forms.CheckBox checkBoxMLAT;
        private System.Windows.Forms.CheckBox checkBoxADSB;
        private System.Windows.Forms.TextBox textBoxHour;
        private System.Windows.Forms.TextBox textBoxMinutes;
        private System.Windows.Forms.TextBox textBoxSeconds;
        private System.Windows.Forms.Button buttonSelectTime;
        private System.Windows.Forms.Button buttonExportKML;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button seeAllButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.CheckBox trajectoryCheckBox;
    }
}