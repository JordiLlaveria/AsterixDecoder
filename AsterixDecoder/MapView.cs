using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace AsterixDecoder
{
    public partial class MapView : Form
    {
        GMapMarker marker;
        GMapOverlay markers = new GMapOverlay("markers");
        DataTable dataTable = new DataTable();
        DataTable dataTable2;
        List<Flight> flightsList = new List<Flight>();
        List<Flight> flightsMarkers = new List<Flight>();
        Flight flightSelected;


        Bitmap blueDotBmp = (Bitmap)Image.FromFile(@"..\..\Resources\bluedot.png");
        Bitmap blueDotResized;
        Bitmap redDotBmp = (Bitmap)Image.FromFile(@"..\..\Resources\reddot.png");
        Bitmap redDotResized;
        Bitmap greenDotBmp = (Bitmap)Image.FromFile(@"..\..\Resources\greendot.png");
        Bitmap greenDotResized;
        Bitmap aircraftBmp = (Bitmap)Image.FromFile(@"..\..\Resources\aircraft.png");
        Bitmap aircraftResized;
        Bitmap groundBmp = (Bitmap)Image.FromFile(@"..\..\Resources\truck.png");
        Bitmap groundResized;

        int i = 0;
        int j = 0;
        TimeSpan time;
        TimeSpan secondAdded = TimeSpan.FromSeconds(1);
        bool firstTick;
        /*
        string stringLongitude;
        string stringLatitude;
        double latdegrees;
        double latminutes;
        double latseconds;
        double longdegrees;
        double longminutes;
        double longseconds;
        */
        bool found = false;
        bool findSelectedMarkerUpdate = false;
        bool firstMarker = true;
        string[] information;
        int hoursTextBox;
        int minutesTextBox;
        int secondsTextBox;
        TimeSpan timeMarkerSelected;
        string filename;
        List<Coordinates> coordinates;
        Coordinates coordinatesKML;
        Coordinates coord;
        string latKML;
        string longKML;
        bool filtered = false;
        Flight filterFlight;
        string trackNumberMarker;
        List<string> sensorList;

        double LATLEBL = 41.298289294252534;
        double LONGLEBL = 2.0832589365462204;

        public MapView(List<Flight> flights)
        {
            InitializeComponent();
            flightsList = flights;
        }

        private void gMapControl1_Load_1(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LATLEBL, LONGLEBL);
            gMapControl1.OnMarkerClick += new MarkerClick(gMapControl1_OnMarkerClick);
            gMapControl1.ShowCenter = false;
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 14;
            gMapControl1.AutoScroll = true;
            dataMarker.Visible = false;
            dataMarker.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataMarker.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataMarker.AllowUserToAddRows = false;
            dataMarker.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataMarker.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataMarker.RowHeadersVisible = false;
            time = new TimeSpan(8, 0, 0);
            flightSelected = new Flight(null, null, 0);
            blueDotResized = new Bitmap(blueDotBmp, new Size(blueDotBmp.Width / 120, blueDotBmp.Height / 120));
            redDotResized = new Bitmap(redDotBmp, new Size(redDotBmp.Width / 72, redDotBmp.Height / 72));
            greenDotResized = new Bitmap(greenDotBmp, new Size(greenDotBmp.Width / 72, greenDotBmp.Height / 72));
            aircraftResized = new Bitmap(aircraftBmp, new Size(aircraftBmp.Width / 78, aircraftBmp.Height / 78));
            groundResized = new Bitmap(groundBmp, new Size(groundBmp.Width / 108, groundBmp.Height / 108));
        }
        public void InitTimer()
        {
            if(timer == null)
            {
                timer = new Timer();
                timer.Tick += new EventHandler(timer1s_Tick);
                firstTick = true;
            }            
            timer.Interval = 1000; // in miliseconds            
            timer.Start();
        }

        private void timer1s_Tick(object sender, EventArgs e)
        {
            flightsMarkers.Clear();
            gMapControl1.Refresh();
            if (firstTick == false)
                time = time.Add(secondAdded);
            firstTick = false; 
            
            markers.Markers.Clear();           

            if(filtered == false)
            {
                for (int i = 0; i < flightsList.Count; i++)
                {
                    bool contains = flightsList[i].getTimes().Contains(time);
                    if (contains == true)
                    {
                        flightsMarkers.Add(flightsList[i]);
                        sensorList = flightsList[i].getSensors();
                        int j = flightsList[i].getTimes().IndexOf(time);
                        coordinates = flightsList[i].getCoordinates();
                        coord = coordinates[j];
                        string sensor = sensorList[j];
                        trackNumberMarker = flightsList[i].getTrackNumber().ToString();

                        if (sensor == "SMR" && checkBoxSMR.Checked == true)
                        {
                            marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), blueDotResized);
                            marker.Tag = trackNumberMarker.ToString();
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            marker.ToolTipText = trackNumberMarker.ToString();
                            markers.Markers.Add(marker);
                        }
                        else if (sensor == "MLAT" && checkBoxMLAT.Checked == true)
                        {
                            marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), greenDotResized);
                            marker.Tag = trackNumberMarker.ToString();
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            marker.ToolTipText = flightsList[i].getTargetAddress();
                            markers.Markers.Add(marker);
                        }
                        else if (sensor == "ADSB" && checkBoxADSB.Checked == true)
                        {

                            if (String.Equals(flightsList[i].getTargetAddress(), "4CA2A8"))
                            {
                                Console.Write("sip");
                            }

                            if (flightsList[i].getTypeVehicleNum() == 1 || flightsList[i].getTypeVehicleNum() == 2 || flightsList[i].getTypeVehicleNum() == 3 || flightsList[i].getTypeVehicleNum() == 4 || flightsList[i].getTypeVehicleNum() == 5 || flightsList[i].getTypeVehicleNum() == 6)
                            {
                                
                                marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), aircraftResized);
                                marker.Tag = trackNumberMarker.ToString();
                                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                                marker.ToolTipText = flightsList[i].getTargetAddress();
                                markers.Markers.Add(marker);
                            }
                            else if (flightsList[i].getTypeVehicleNum() == 20 || flightsList[i].getTypeVehicleNum() == 21)
                            {
                                marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), groundResized);
                                marker.Tag = trackNumberMarker.ToString();
                                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                                marker.ToolTipText = flightsList[i].getTargetAddress();
                                markers.Markers.Add(marker);
                            }
                            else
                            {
                                marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), redDotResized);
                                marker.Tag = trackNumberMarker.ToString();
                                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                                marker.ToolTipText = flightsList[i].getTargetAddress();
                                markers.Markers.Add(marker);
                            }
                        }
                    }
                }
            }
            else
            {
                bool contains = filterFlight.getTimes().Contains(time);
                if (contains == true)
                {
                    sensorList = filterFlight.getSensors();
                    int j = filterFlight.getTimes().IndexOf(time);
                    coordinates = filterFlight.getCoordinates();
                    coord = coordinates[j];
                    string sensor = sensorList[j];
                    trackNumberMarker = filterFlight.getTrackNumber().ToString();
                    if (sensor == "SMR" && checkBoxSMR.Checked == true)
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), blueDotResized);
                        marker.Tag = trackNumberMarker.ToString();
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.ToolTipText = trackNumberMarker.ToString();
                        markers.Markers.Add(marker);
                    }
                    else if (sensor == "MLAT" && checkBoxMLAT.Checked == true)
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), greenDotResized);
                        marker.Tag = trackNumberMarker.ToString();
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.ToolTipText = filterFlight.getTargetAddress();
                        markers.Markers.Add(marker);
                    }
                    else if (sensor == "ADSB" && checkBoxADSB.Checked == true)
                    {
                        if (filterFlight.getTypeVehicleNum() == 1 || filterFlight.getTypeVehicleNum() == 2 || filterFlight.getTypeVehicleNum() == 3 || filterFlight.getTypeVehicleNum() == 4 || filterFlight.getTypeVehicleNum() == 5 || filterFlight.getTypeVehicleNum() == 6)
                        {
                            marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), aircraftResized);
                            marker.Tag = trackNumberMarker.ToString();
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            marker.ToolTipText = filterFlight.getTargetAddress();
                            markers.Markers.Add(marker);
                        }
                        else if (filterFlight.getTypeVehicleNum() == 20 || filterFlight.getTypeVehicleNum() == 21)
                        {
                            marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), groundResized);
                            marker.Tag = trackNumberMarker.ToString();
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            marker.ToolTipText = filterFlight.getTargetAddress();
                            markers.Markers.Add(marker);
                        }
                        else
                        {
                            marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), redDotResized);
                            marker.Tag = trackNumberMarker.ToString();
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            marker.ToolTipText = filterFlight.getTargetAddress();
                            markers.Markers.Add(marker);
                        }
                    }
                }
            }            
            gMapControl1.Overlays.Add(markers);
            i = 0;
            if (filtered == true)
            {
                dataTable.Rows.Clear();
                ordenateInformation(filterFlight);
                dataTable2 = dataTable.Copy();
                DataView dv = new DataView(dataTable2);
                dataMarker.DataSource = dv;
                drawTable();
            }
            else if (flightSelected.getEmitterCategory() != null && checkBoxSMR.Checked == true)
            {
                while (i < flightsMarkers.Count && findSelectedMarkerUpdate == false)
                {
                    if (flightsMarkers[i].getTrackNumber().ToString() == flightSelected.getTrackNumber().ToString())
                    {
                        findSelectedMarkerUpdate = true;
                        dataTable.Rows.Clear();
                        ordenateInformation(flightSelected);
                        dataTable2 = dataTable.Copy();
                        DataView dv = new DataView(dataTable2);
                        dataMarker.DataSource = dv;
                        drawTable();
                    }
                    i++;
                }
                findSelectedMarkerUpdate = false;
            }
            labelTime.Text = time.ToString();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            InitTimer();
            buttonX1.BackColor = Color.LightBlue;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.White;        
            timer.Interval = 1000;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            timer.Interval = 1000;
            buttonX1.BackColor = Color.LightBlue;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.White;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            timer.Interval = 500;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.LightBlue;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.White;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            timer.Interval = 200;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.LightBlue;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.White;
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            timer.Interval = 100;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.LightBlue;
            buttonX20.BackColor = Color.White;
        }

        private void buttonX20_Click(object sender, EventArgs e)
        {
            timer.Interval = 50;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.LightBlue;
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (flightSelected.getEmitterCategory() == null && timeMarkerSelected != time)
            {
                timeMarkerSelected = time;
                i = 0;
                dataMarker.Visible = true;
                dataTable.Columns.Add("Target Identification");
                dataTable.Columns.Add("Target Address");
                dataTable.Columns.Add("Track Number");
                dataTable.Columns.Add("Emitter Category");
                dataTable.Columns.Add("Detection Mode");
                dataTable.Columns.Add("Time");
                dataTable.Columns.Add("Ground Speed");
                dataTable.Columns.Add("Flight level");
                firstMarker = false;
                while (i < flightsMarkers.Count && found == false)
                {
                    if (flightsMarkers[i].getTrackNumber().ToString() == item.Tag.ToString())
                    {
                        found = true;
                        ordenateInformation(flightsMarkers[i]);
                        flightSelected = flightsMarkers[i];
                    }
                    i++;
                }
                dataTable2 = dataTable.Copy();
                DataView dv = new DataView(dataTable2);
                dataMarker.DataSource = dv;
                drawTable();
            }
            if (item.Tag.ToString() != flightSelected.getTrackNumber().ToString() && timeMarkerSelected != time)
            {
                timeMarkerSelected = time;
                GC.Collect();
                dataTable.Rows.Clear();
                found = false;
                i = 0;
                while (i < flightsMarkers.Count && found == false)
                {
                    if (flightsMarkers[i].getTrackNumber().ToString() == item.Tag.ToString())
                    {
                        found = true;
                        ordenateInformation(flightsMarkers[i]);
                        flightSelected = flightsMarkers[i];
                    }
                    i++;
                }
                dataTable2 = dataTable.Copy();
                DataView dv = new DataView(dataTable2);
                dataMarker.DataSource = dv;
                drawTable();
            }
        }

        private void ordenateInformation(Flight flight)
        {
            information = new string[8];
            information[0] = flight.getTargetIdentification();
            information[1] = flight.getTargetAddress();
            information[2] = flight.getTrackNumber().ToString();
            information[3] = flight.getEmitterCategory();
            StringBuilder sb = new StringBuilder();
            List<string> list = flight.getSensors();
            sb.Append(list[0]);
            bool sensChanged = false;
            for (int i = 1; i<list.Count() && sensChanged == false; i++)
            {
                if (String.Equals(list[i], sb.ToString()) == false)
                {
                    sensChanged = true;
                    sb.Append(" and ");
                    sb.Append(list[i]);
                }
            }
            
            information[4] = sb.ToString();
            information[5] = time.ToString();
            j = flight.getTimes().IndexOf(time);
            if (j >= 0)
            {
                information[6] = flight.getGroundSpeed(j);
                information[7] = flight.getFL(j);
            }
            dataTable.Rows.Add(information);
        }
        private void drawTable()
        {
            foreach (DataGridViewRow row in dataMarker.Rows)
            {
                row.Height = (dataMarker.ClientRectangle.Height - dataMarker.ColumnHeadersHeight) / dataMarker.Rows.Count;
            }
        }

        private void buttonSelectTime_Click(object sender, EventArgs e)
        {
            hoursTextBox = Convert.ToInt32(textBoxHour.Text);
            minutesTextBox = Convert.ToInt32(textBoxMinutes.Text);
            secondsTextBox = Convert.ToInt32(textBoxSeconds.Text);
            time = new TimeSpan(hoursTextBox, minutesTextBox, secondsTextBox);
        }

        private void buttonExportKML_Click(object sender, EventArgs e)
        {
            timer.Stop();
            Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "Kml files (*.kml)|*.kml";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    filename = saveFileDialog.FileName;
                    myStream.Close();
                }

                XmlTextWriter writer = new
                XmlTextWriter((filename), Encoding.UTF8);

                writer.WriteStartDocument();
                writer.WriteStartElement("kml");
                writer.WriteAttributeString("xmlns", "http://earth.google.com/kml/2.2");
                writer.WriteStartElement("Folder");
                writer.WriteStartElement("description");
                writer.WriteCData("All Flights");
                writer.WriteEndElement();
                writer.WriteElementString("name", "Asterix Decoder");
                writer.WriteStartElement("Folder");

                int len = flightsMarkers.Count();
                for (int i = 0; i < len; i++)
                {
                    coordinates = flightsMarkers[i].getCoordinates();
                    coordinatesKML = coordinates[j];
                    this.latKML = coordinatesKML.GetLatitude().ToString();
                    this.latKML = this.latKML.Replace(',', '.');
                    this.longKML = coordinatesKML.GetLongitude().ToString();
                    this.longKML = this.longKML.Replace(',', '.');
                    string flightname;
                    if (flightsMarkers[i].getTargetAddress()!=null)
                        flightname = flightsMarkers[i].getTargetAddress().ToString();
                    else
                        flightname = flightsMarkers[i].getTrackNumber().ToString();
                    writer.WriteStartElement("Placemark");
                    writer.WriteStartElement("title");
                    if (flightsMarkers[i].getSensors()[7] == "SMR")
                        writer.WriteCData(flightname);
                    else if (flightsMarkers[i].getSensors()[7] == "MLAT")
                        writer.WriteCData(flightsMarkers[i].getTargetAddress());
                    else
                        writer.WriteCData(flightsMarkers[i].getTargetIdentification());
                    writer.WriteEndElement();
                    writer.WriteStartElement("description");
                    writer.WriteCData(flightname);
                    writer.WriteEndElement();

                    writer.WriteElementString("name", flightname);
                    writer.WriteStartElement("Style");
                    writer.WriteStartElement("IconStyle");
                    writer.WriteStartElement("Icon");
                    writer.WriteElementString("href", "http://maps.google.com/mapfiles/kml/pal2/icon56.png");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteStartElement("LookAt");
                    writer.WriteElementString("longitude", this.longKML);
                    writer.WriteElementString("latitude", this.latKML);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Point");
                    writer.WriteElementString("altitudeMode", "relativeToGround");
                    writer.WriteElementString("coordinates", this.longKML + "," + this.latKML + ",50");
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndDocument();
                writer.Close();
                try
                {
                    string message = "You have saved your KML file, ¿Do you want to open it in Google Earth?";
                    string caption = "Open KML";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(this.filename);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    return;
                }
            }
            else
            {
                const string message = "The file was not saved.";
                const string caption = "Warning";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Warning);
            }
        }

        private void seeAllButton_Click(object sender, EventArgs e)
        {
            filterTextBox.Clear();
            filtered = false;
            filterFlight = null;
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            i = 0;
            while (i < flightsMarkers.Count() && filtered == false)
            {
                if (String.Equals(flightsMarkers[i].getTargetAddress(), filterTextBox.Text))
                {
                    filtered = true;
                    filterFlight = flightsMarkers[i];
                }
                i++;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            filtered = false;
            filterTextBox.Clear();
            time = new TimeSpan(8, 0, 1);
            firstTick = true;
            buttonX1.BackColor = Color.LightBlue;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.White;
            InitTimer();
        }

        private void trajectoryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(trajectoryCheckBox.Checked == true && filtered == true)
            {
                timer.Stop();
                firstTick = true;
                buttonX1.BackColor = Color.White;
                buttonX2.BackColor = Color.White;
                buttonX5.BackColor = Color.White;
                buttonX10.BackColor = Color.White;
                buttonX20.BackColor = Color.White;

                markers.Clear();
                coordinates = filterFlight.getCoordinates();
                sensorList = filterFlight.getSensors();
                int c = 0;
                while(c<coordinates.Count)
                {
                    coord = coordinates[c];
                    string sensor = sensorList[c];
                    trackNumberMarker = filterFlight.getTrackNumber().ToString();
                    if (sensor == "SMR")
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), blueDotResized);
                        marker.Tag = trackNumberMarker.ToString();
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.ToolTipText = trackNumberMarker.ToString();
                        markers.Markers.Add(marker);
                    }
                    else if (sensor == "MLAT")
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), greenDotResized);
                        marker.Tag = flightsList[i].getTargetAddress();
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.ToolTipText = flightsList[i].getTargetAddress();
                        markers.Markers.Add(marker);
                    }
                    else if (sensor == "ADSB")
                    {
                        if (filterFlight.getTypeVehicleNum() == 1 || filterFlight.getTypeVehicleNum() == 2 || filterFlight.getTypeVehicleNum() == 3 || filterFlight.getTypeVehicleNum() == 4 || filterFlight.getTypeVehicleNum() == 5 || filterFlight.getTypeVehicleNum() == 6)
                        {
                            marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), aircraftResized);
                            marker.Tag = flightsList[i].getTargetAddress();
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            marker.ToolTipText = flightsList[i].getTargetAddress();
                            markers.Markers.Add(marker);
                        }
                        else if (filterFlight.getTypeVehicleNum() == 20 || filterFlight.getTypeVehicleNum() == 21)
                        {
                            marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), groundResized);
                            marker.Tag = flightsList[i].getTargetAddress();
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            marker.ToolTipText = flightsList[i].getTargetAddress();
                            markers.Markers.Add(marker);
                        }
                        else
                        {
                            marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), redDotResized);
                            marker.Tag = flightsList[i].getTargetAddress();
                            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            marker.ToolTipText = flightsList[i].getTargetAddress();
                            markers.Markers.Add(marker);
                        }
                    }
                    c = c + 1;
                }
                gMapControl1.Overlays.Add(markers);

            }
            else if(trajectoryCheckBox.Checked == false)
            {
                InitTimer();
                buttonX1.BackColor = Color.LightBlue;
                buttonX2.BackColor = Color.White;
                buttonX5.BackColor = Color.White;
                buttonX10.BackColor = Color.White;
                buttonX20.BackColor = Color.White;
                timer.Interval = 1000;

            }
        }
    }
}
