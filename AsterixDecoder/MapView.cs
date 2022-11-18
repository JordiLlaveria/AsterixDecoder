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
        GMarkerGoogle marker;
        GMapOverlay markers = new GMapOverlay("markers");
        DataTable dt;
        List<Flight> FlightsList = new List<Flight>();

        int hores;
        int minuts;
        int segons;
        TimeSpan time;
        bool firstTick;
        int interval = 1;

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
            gMapControl1.ShowCenter = false;
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 14;
            gMapControl1.AutoScroll = true;
            hores = 8;
            minuts = 0;
            segons = 0;

        }
        public void InitTimer()
        {
            timer1s = new Timer();
            timer1s.Tick += new EventHandler(timer1s_Tick);
            timer1s.Interval = 1000; // in miliseconds
            firstTick = true;
            timer1s.Start();
        }

        private void timer1s_Tick(object sender, EventArgs e)
        {
            gMapControl1.Refresh();
            if (firstTick == false)
                transformTimeToSeconds(interval);
            firstTick = false;
            markers.Markers.Clear();
            for (int i = 0; i < FlightsList.Count; i++)
            {
                bool contains = FlightsList[i].getTimes().Contains(time);
                if (contains == true)
                {
                    string sensor = FlightsList[i].getSensor();
                    int j = FlightsList[i].getTimes().IndexOf(time);
                    List<Coordinates> coordinates = FlightsList[i].getCoordinates();
                    Coordinates coord = coordinates[j];
                    string trackNumberMarker = FlightsList[i].getTrackNumber().ToString();
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
                    GMapMarker marker;
                    if (sensor == "SMR")
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), GMarkerGoogleType.blue_small);
                    }
                    else if (sensor == "MLAT")
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), GMarkerGoogleType.yellow_small);
                    }
                    else
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), GMarkerGoogleType.red_small);
                    }
                    marker.Tag = trackNumberMarker;
                    markers.Markers.Add(marker);
                }
            }
            gMapControl1.Overlays.Add(markers);
            labelTime.Text = time.ToString();
        }

        private void transformTimeToSeconds(int interval)
        {
            segons = segons + interval;
            if (segons > 59)
            {
                minuts = minuts + 1;
                segons = segons - 60;
            }
            if (minuts > 59)
            {
                hores = hores + 1;
                minuts = minuts - 60;
            }
            time = new TimeSpan(hores, minuts, segons);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            InitTimer();
            buttonX1.BackColor = Color.Green;
            time = new TimeSpan(hores,minuts,segons);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            interval = 1;
            buttonX1.BackColor = Color.Green;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            interval = 2;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.Green;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.White;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            interval = 5;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.Green;
            buttonX10.BackColor = Color.White;
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            interval = 10;
            buttonX1.BackColor = Color.White;
            buttonX2.BackColor = Color.White;
            buttonX5.BackColor = Color.White;
            buttonX10.BackColor = Color.Green;
        }
    }
}
