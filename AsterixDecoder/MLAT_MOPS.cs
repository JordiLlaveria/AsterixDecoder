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

namespace AsterixDecoder
{
    public partial class MLAT_MOPS : Form
    {
        DataTable dataTable = new DataTable();
        DataTable dataTable2;
        List<MLAT> MLATlist = new List<MLAT>();
        string targetAddress;
        string tracknumber;
        string sensor;
        List<double> FalseDetections;
        List<double> Identifications;
        List<string> differentTargetAddresses;
        double totalFalseIdentification = 0;
        double probabilityFalseIdentification;
        double totalDetection = 0;
        double probabilityFalseDetection;
        bool insideRange;
        bool falseDetectionSelected = true;
        bool firstDataGrid = true;

        //Estadísticas para el dataGridView
        int totalTargetIDAllFlights;
        int equalTargetIDAllFlights;
        int totalTargetID;
        int equalTargetID;

        double totalCoordinatesAllFlights;
        double equalCoordinatesAllFlights;

        int i;
        int j;
        public MLAT_MOPS()
        {
            InitializeComponent();
        }

        public MLAT_MOPS(List<CAT10> cat10List, string fileName)
        {
            InitializeComponent();
            if (fileName == "201002-lebl-080001_smr_mlat_adsb")
            {
                for (int i = 0; i < cat10List.Count; i++)
                {
                    if (cat10List[i].getSic() != 107)
                        cat10List.Remove(cat10List[i]);
                }
            }
            for (i = 0; i < cat10List.Count; i++)
            {
                CAT10 cat10Info = cat10List[i];
                sensor = cat10Info.getTypeSensor();
                tracknumber = cat10Info.getTrackNumber().ToString();
                targetAddress = cat10Info.getTargetAddress();
                MLAT mlatFlightFound = MLATlist.FirstOrDefault(MLAT => MLAT.getTargetAddress() == targetAddress && (cat10Info.getTime() - MLAT.getLastTime()).TotalMinutes < 10);
                if (mlatFlightFound != null)
                {
                    if (cat10Info.getTargetIdentification() != null)
                    {
                        double[] latLong = cat10Info.getLatitudeLongitudeWGS84(sensor);
                        Coordinates coordinates = new Coordinates(latLong[0], latLong[1]);
                        mlatFlightFound.setCoordinates(coordinates);
                        mlatFlightFound.setTimes(cat10Info.getTime());
                        mlatFlightFound.setIdentification(cat10Info.getTargetIdentification());
                        mlatFlightFound.setGroundSpeed(cat10Info.getGroundSpeed());
                    }
                }
                else
                {
                    if (cat10Info.getTargetIdentification() != null)
                    {
                        MLAT mlat = new MLAT(tracknumber, targetAddress, cat10Info.getTime(), cat10Info.getTargetIdentification(), cat10Info.getGroundSpeed());
                        double[] latLong = cat10Info.getLatitudeLongitudeWGS84(sensor);
                        Coordinates coordinates = new Coordinates(latLong[0], latLong[1]);
                        mlat.setCoordinates(coordinates);
                        MLATlist.Add(mlat);
                    }
                }
            }
            Console.WriteLine("Hola");
        }
        private void calculateProbIdentification()
        {
            totalTargetIDAllFlights = 0;
            equalTargetIDAllFlights = 0;
            probabilityFalseIdentification = 0;
            totalFalseIdentification = 0;
            differentTargetAddresses = new List<string>();
            string firstIdentification;
            List<string> targetids;
            Identifications = new List<double>();
            for(i = 0; i < MLATlist.Count; i++)
            {
                equalTargetID = 0;
                firstIdentification = MLATlist[i].getFirstIdentification();
                targetids = MLATlist[i].getTargetIdentifications();
                totalTargetID = MLATlist[i].getTargetIdentifications().Count;
                for (j = 0; j < MLATlist[i].getTargetIdentifications().Count; j++)
                {
                    if (targetids[j] == firstIdentification)
                        equalTargetID++;
                }
                string mlatFlightFound = differentTargetAddresses.FirstOrDefault(targetAddress => targetAddress == MLATlist[i].getTargetAddress());
                if (mlatFlightFound == null)
                    differentTargetAddresses.Add(MLATlist[i].getTargetAddress());
                Identifications.Add(((float)equalTargetID / totalTargetID) * 100);
                totalTargetIDAllFlights = totalTargetIDAllFlights + totalTargetID;
                equalTargetIDAllFlights = equalTargetIDAllFlights + equalTargetID;
            }
            for(i = 0; i < Identifications.Count; i++)
            {
                totalFalseIdentification = totalFalseIdentification + Identifications[i];
            }
            probabilityFalseIdentification = (float)totalFalseIdentification / (Identifications.Count);
            if (falseDetectionSelected == true || firstDataGrid == true)
            {
                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                dataTable.Columns.Add("Definition");
                dataTable.Columns.Add("Value");
                string[] information = new string[2];
                information[0] = "Total different target addresses";
                information[1] = differentTargetAddresses.Count.ToString();
                dataTable.Rows.Add(information);
                information[0] = "Amount of different target identifications";
                information[1] = Identifications.Count.ToString();
                dataTable.Rows.Add(information);
                information[0] = "Total equal target identifications detected";
                information[1] = equalTargetIDAllFlights.ToString();
                dataTable.Rows.Add(information);
                information[0] = "Total target identifications detected";
                information[1] = totalTargetIDAllFlights.ToString();
                dataTable.Rows.Add(information);
                information[0] = "Probability of correct identification";
                information[1] = probabilityFalseIdentification.ToString() + "%";
                dataTable.Rows.Add(information);
                information[0] = "Probability of false identification";
                double probabilityfalse = 100 - probabilityFalseIdentification;
                information[1] = probabilityfalse.ToString() + "%";
                dataTable.Rows.Add(information);
                dataTable2 = dataTable.Copy();
                DataView dv = new DataView(dataTable2);
                dataGridViewInfo.DataSource = dv;
                drawTable();
                dataGridViewInfo.ClearSelection();
                firstDataGrid = false;
                falseDetectionSelected = false;
            }
        }

        private void calculateFalseDetection()
        {
            totalCoordinatesAllFlights = 0;
            equalCoordinatesAllFlights = 0;
            probabilityFalseDetection = 0;
            totalDetection = 0;
            differentTargetAddresses = new List<string>();
            double differenceTimes;
            double distanceMadeBetweenDetection;
            double totalCoordinatesDetected;
            double equalCoordinatesDetected;
            FalseDetections = new List<double>();
            bool positionInsideRange;
            for(i=0; i < MLATlist.Count; i++)
            {
                equalCoordinatesDetected = 0;
                totalCoordinatesDetected = MLATlist[i].getCoordinates().Count;
                totalCoordinatesAllFlights = totalCoordinatesAllFlights + totalCoordinatesDetected;
                for(j = 0; j < MLATlist[i].getCoordinates().Count - 1; j++)
                {
                    differenceTimes = (MLATlist[i].getTime(j + 1) - MLATlist[i].getTime(j)).TotalSeconds;
                    distanceMadeBetweenDetection = differenceTimes * MLATlist[i].getGroundSpeed(j);
                    positionInsideRange = GetDistance(distanceMadeBetweenDetection, MLATlist[i].getLatitude(j), MLATlist[i].getLongitude(j), MLATlist[i].getLatitude(j + 1), MLATlist[i].getLongitude(j + 1));
                    if (positionInsideRange)
                    {
                        equalCoordinatesDetected++;
                        equalCoordinatesAllFlights++;
                    }
                }
                string mlatFlightFound = differentTargetAddresses.FirstOrDefault(targetAddress => targetAddress == MLATlist[i].getTargetAddress());
                if (mlatFlightFound == null)
                    differentTargetAddresses.Add(MLATlist[i].getTargetAddress());
                FalseDetections.Add(((float)equalCoordinatesDetected / totalCoordinatesDetected) * 100);
            }
            for (i = 0; i < FalseDetections.Count; i++)
            {
                totalDetection = totalDetection + FalseDetections[i];
            }
            probabilityFalseDetection = (float)totalDetection / (FalseDetections.Count);
            if (falseDetectionSelected == false || firstDataGrid == true)
            {
                dataTable.Rows.Clear();
                dataTable.Columns.Clear();
                dataTable.Columns.Add("Definition");
                dataTable.Columns.Add("Value");
                string[] information = new string[2];
                information[0] = "Total different target addresses";
                information[1] = differentTargetAddresses.Count.ToString();
                dataTable.Rows.Add(information);
                information[0] = "Amount of different target identifications";
                information[1] = MLATlist.Count.ToString();
                dataTable.Rows.Add(information);
                information[0] = "Amount of coordinates below 50 metres from expected";
                information[1] = equalCoordinatesAllFlights.ToString();
                dataTable.Rows.Add(information);
                information[0] = "Total amount of coordinates analysed";
                information[1] = totalCoordinatesAllFlights.ToString();
                dataTable.Rows.Add(information);
                information[0] = "Probability of correct detection";
                information[1] = probabilityFalseDetection.ToString() + " %";
                dataTable.Rows.Add(information);
                information[0] = "Probability of false detection";
                double probabilityfalse = 100 - probabilityFalseDetection;
                information[1] = probabilityfalse.ToString() + " %";
                dataTable.Rows.Add(information);
                dataTable2 = dataTable.Copy();
                DataView dv = new DataView(dataTable2);
                dataGridViewInfo.DataSource = dv;
                drawTable();
                dataGridViewInfo.ClearSelection();
                firstDataGrid = false;
                falseDetectionSelected = true;
            }
        }

        private void buttonIDProb_Click(object sender, EventArgs e)
        {
            dataGridViewInfo.Visible = true;
            probabilityFalseIdentification = 0;
            calculateProbIdentification();
        }

        private void buttonFalseDetect_Click(object sender, EventArgs e)
        {
            dataGridViewInfo.Visible = true;
            probabilityFalseDetection = 0;
            calculateFalseDetection();
        }

        private bool GetDistance(double mruDistance, double lat1, double lon1, double lat2, double lon2)
        {
            var R = 0.05 + mruDistance * Math.Pow(10,-3); // Radius in km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            if (d < R)
                insideRange = true;
            else
                insideRange = false;
            return insideRange;
        }

        private double ToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }

        private void drawTable()
        {
            foreach (DataGridViewRow row in dataGridViewInfo.Rows)
            {
                row.Height = (dataGridViewInfo.ClientRectangle.Height - dataGridViewInfo.ColumnHeadersHeight) / dataGridViewInfo.Rows.Count;
            }
        }

        private void MLAT_MOPS_Load(object sender, EventArgs e)
        {
            dataGridViewInfo.Visible = false;
            dataGridViewInfo.ClearSelection();
            dataGridViewInfo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewInfo.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewInfo.AllowUserToAddRows = false;
            dataGridViewInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewInfo.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewInfo.RowHeadersVisible = false;
        }
    }
}