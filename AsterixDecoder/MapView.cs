using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        int hores;
        int minuts;
        int segons;
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
        bool firstMarker = true;
        string[] information;
        int hoursTextBox;
        int minutesTextBox;
        int secondsTextBox;

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
                    List<Coordinates> coordinates = flightsMarkers[flightsMarkers.Count-1].getCoordinates();
                    Coordinates coord = coordinates[j];
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
                        //marker.ToolTipMode = MarkerTooltipMode.Always;
                        //marker.ToolTipText = trackNumberMarker.ToString();
                    }
                    else if (sensor == "MLAT" && checkBoxMLAT.Checked == true)
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), GMarkerGoogleType.yellow_small);
                        marker.Tag = trackNumberMarker;
                        markers.Markers.Add(marker);
                        //marker.ToolTipText = trackNumberMarker;
                    }
                    else if (sensor == "ADSB" && checkBoxADSB.Checked == true)
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), GMarkerGoogleType.red_small);
                        marker.Tag = trackNumberMarker;
                        markers.Markers.Add(marker);
                        //marker.ToolTipText = trackNumberMarker;
                    }
                }
            }
            gMapControl1.Overlays.Add(markers);
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
                }
                i++;
            }
            dataTable2 = dataTable.Copy();
            DataView dv = new DataView(dataTable2);
            dataMarker.DataSource = dv;
            drawTable();
        }

        private void ordenateInformation(Flight flight)
        {
            information = new string[6];
            information[0] = flight.getTargetIdentification() != null ? flight.getTargetIdentification() : information[0];
            information[1] = flight.getTargetAddress() != null ? flight.getTargetAddress() : information[1];
            information[2] = flight.getTrackNumber().ToString();
            information[3] = flight.getEmitterCategory();
            information[4] = flight.getSensor();
            information[5] = time.ToString();
            j = flight.getTimes().IndexOf(time);
            information[6] = flight.getGroundSpeed(j) != null ? flight.getGroundSpeed(j) : information[6];
            information[7] = flight.getFL(j) != null ? flight.getFL(j) : information[7];
            for (int i = 0; i < information.Length; i++)
            {
                if (information[i] == null)
                    information[i] = "No data";
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
    }
}
