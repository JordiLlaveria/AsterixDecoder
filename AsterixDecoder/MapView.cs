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
        List<Flight> FlightsList = new List<Flight>();
        List<Flight> flightsMarkers = new List<Flight>();
        Flight flightSelected;

        int hores;
        int minuts;
        int segons;
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

        double LATLEBL = 41.298289294252534;
        double LONGLEBL = 2.0832589365462204;

        public MapView(List<Flight> flights)
        {
            InitializeComponent();
            FlightsList = flights;
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
            hores = 8;
            minuts = 0;
            segons = 0;
            flightSelected = null;
            timeMarkerSelected = new TimeSpan(hores, minuts, segons);

        }
        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(timer1s_Tick);
            timer.Interval = 1000; // in miliseconds
            firstTick = true;
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
            for (int i = 0; i < FlightsList.Count; i++)
            {
                bool contains = FlightsList[i].getTimes().Contains(time);
                if (contains == true)
                {
                    flightsMarkers.Add(FlightsList[i]);
                    string sensor = flightsMarkers[flightsMarkers.Count-1].getSensor();
                    j = flightsMarkers[flightsMarkers.Count-1].getTimes().IndexOf(time);
                    //FlightsList[i].removeTimes(j);
                    coordinates = flightsMarkers[flightsMarkers.Count-1].getCoordinates();
                    coord = coordinates[j];
                    //FlightsList[i].removeCoordinates(j);
                    //FlightsList[i].removeGroundSpeed(j);
                    //FlightsList[i].removeFL(j);
                    string trackNumberMarker = flightsMarkers[flightsMarkers.Count-1].getTrackNumber().ToString();
                    //bool markerfound = false;
                    /*
                    for (int k = 0; k < markers.Markers.Count; k++)
                    {
                        if (markers.Markers[k].Tag.ToString() == trackNumberMarker) 
                        {
                            markers.Markers[k].Position = new PointLatLng(coord.GetLatitude(), coord.GetLongitude());
                            markerfound = true;
                        }
                    }
                    if (markerfound == false)
                    {
                    */
                    if (sensor == "SMR" && checkBoxSMR.Checked == true)
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), GMarkerGoogleType.blue_small);
                        marker.Tag = trackNumberMarker;
                        markers.Markers.Add(marker);
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.ToolTipText = trackNumberMarker.ToString();
                    }
                    else if (sensor == "MLAT" && checkBoxMLAT.Checked == true)
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), GMarkerGoogleType.yellow_small);
                        marker.Tag = trackNumberMarker;
                        markers.Markers.Add(marker);
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.ToolTipText = trackNumberMarker.ToString();
                    }
                    else if (sensor == "ADSB" && checkBoxADSB.Checked == true)
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), GMarkerGoogleType.red_small);
                        marker.Tag = trackNumberMarker;
                        markers.Markers.Add(marker);
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        marker.ToolTipText = trackNumberMarker.ToString();
                    }
                }
            }
            gMapControl1.Overlays.Add(markers);
            i = 0;
            if (flightSelected != null && checkBoxSMR.Checked == true)
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
            buttonX1.BackColor = Color.Green;
            time = new TimeSpan(hores,minuts,segons);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            timer.Interval = 1000;
            buttonX1.BackColor = Color.Green;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.White;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            timer.Interval = 500;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.Green;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.White;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            timer.Interval = 200;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.Green;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.White;
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            timer.Interval = 100;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.Green;
            buttonX20.BackColor = Color.White;
        }

        private void buttonX20_Click(object sender, EventArgs e)
        {
            timer.Interval = 50;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
            buttonX20.BackColor = Color.Green;
        }

        private void gMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (timeMarkerSelected != time)
            {
                timeMarkerSelected = time;
                GC.Collect();
                dataMarker.Visible = true;
                dataTable.Rows.Clear();
                found = false;
                int i = 0;
                if (firstMarker == true)
                {
                    dataTable.Columns.Add("Target Identification");
                    dataTable.Columns.Add("Target Address");
                    dataTable.Columns.Add("Track Number");
                    dataTable.Columns.Add("Emitter Category");
                    dataTable.Columns.Add("Detection Mode");
                    dataTable.Columns.Add("Time");
                    dataTable.Columns.Add("Ground Speed");
                    dataTable.Columns.Add("Flight level");
                    firstMarker = false;
                }
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
            information[4] = flight.getSensor();
            information[5] = time.ToString();
            j = flight.getTimes().IndexOf(time);
            information[6] = flight.getGroundSpeed(j);
            information[7] = flight.getFL(j);
            for (int i = 0; i < information.Length; i++)
            {
                if (information[i] == null)
                    information[i] = "No Data";
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
                    j = flightsMarkers[i].getTimes().IndexOf(time);
                    coordinates = flightsMarkers[i].getCoordinates();
                    coordinatesKML = coordinates[j];
                    this.latKML = coordinatesKML.GetLatitude().ToString();
                    this.latKML = this.latKML.Replace(',', '.');
                    this.longKML = coordinatesKML.GetLongitude().ToString();
                    this.longKML = this.longKML.Replace(',', '.');
                    string flightname = flightsMarkers[i].getTrackNumber().ToString();
                    writer.WriteStartElement("Placemark");
                    writer.WriteStartElement("title");
                    if (flightsMarkers[i].getSensor() == "SMR")
                        writer.WriteCData(flightname);
                    else if (flightsMarkers[i].getSensor() == "MLAT")
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
                    writer.WriteElementString("href", "http://google.com/mapfiles/ms/micons/icon48.png");
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
    }
}
