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


        Bitmap blueDotBmp = (Bitmap)Image.FromFile(@"..\..\Resources\bluedot.png");
        Bitmap blueDotResized;
        Bitmap redDotBmp = (Bitmap)Image.FromFile(@"..\..\Resources\reddot.png");
        Bitmap redDotResized;
        Bitmap greenDotBmp = (Bitmap)Image.FromFile(@"..\..\Resources\greendot.png");
        Bitmap greenDotResized;

        int hores;
        int minuts;
        int segons;
        TimeSpan time;
        bool firstTick;

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
            blueDotResized = new Bitmap(blueDotBmp, new Size(blueDotBmp.Width / 100, blueDotBmp.Height / 100));
            redDotResized = new Bitmap(redDotBmp, new Size(redDotBmp.Width / 60, redDotBmp.Height / 60));
            greenDotResized = new Bitmap(greenDotBmp, new Size(greenDotBmp.Width / 60, greenDotBmp.Height / 60));

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
            gMapControl1.Refresh();
            if (firstTick == false)
                transformTimeToSeconds();
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
                        
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), blueDotResized);
                        //marker.ToolTipMode = MarkerTooltipMode.Always;
                        //marker.ToolTipText = trackNumberMarker.ToString();
                    }
                    else if (sensor == "MLAT")
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), greenDotResized);
                        marker.ToolTipText = trackNumberMarker;
                    }
                    else
                    {
                        marker = new GMarkerGoogle(new PointLatLng(coord.GetLatitude(), coord.GetLongitude()), redDotResized);
                        marker.ToolTipText = trackNumberMarker;
                    }
                    marker.Tag = trackNumberMarker;
                    markers.Markers.Add(marker);
                }
            }
            gMapControl1.Overlays.Add(markers);
            labelTime.Text = time.ToString();
        }

        private void transformTimeToSeconds()
        {
            segons = segons + 1;
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

    }
}
