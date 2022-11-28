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
        List<CAT21> cat21List = new List<CAT21>(); 
        List<MLAT> MLATlist = new List<MLAT>();
        string targetAddress;
        string tracknumber;
        string sensor;
        List<double> totalFalseDetection;
        List<double> totalIdentification;
        double totalFalseIdentification = 0;
        double probabilityFalseIdentification;
        double totalDetection = 0;
        double probabilityFalseDetection;
        int totalTargetID;
        int equalTargetID;
        List<Coordinates> secondsLatLong;
        double maxDif = 1.618;
        double latdegrees;
        double latminutes;
        double latseconds;
        double londegrees;
        double lonminutes;
        double lonseconds;

        int i;
        int j;
        public MLAT_MOPS()
        {
            InitializeComponent();
        }

        public MLAT_MOPS(List<CAT21> cat21List, List<CAT10> cat10List, string fileName)
        {
            this.cat21List = cat21List;
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
                MLAT mlatFlightFound = MLATlist.FirstOrDefault(MLAT => MLAT.getTargetAddress() == targetAddress);
                if (mlatFlightFound != null)
                {
                    if (cat10Info.getTargetIdentification() != null)
                    {
                        double[] latLong = cat10Info.getLatitudeLongitudeWGS84(sensor);
                        Coordinates coordinates = new Coordinates(latLong[0], latLong[1]);
                        mlatFlightFound.setCoordinates(coordinates);
                        mlatFlightFound.setTimes(cat10Info.getTime());
                        mlatFlightFound.setIdentification(cat10Info.getTargetIdentification());
                    }
                }
                else
                {
                    if (cat10Info.getTargetIdentification() != null)
                    {
                        MLAT mlat = new MLAT(tracknumber, targetAddress);
                        double[] latLong = cat10Info.getLatitudeLongitudeWGS84(sensor);
                        Coordinates coordinates = new Coordinates(latLong[0], latLong[1]);
                        mlat.setCoordinates(coordinates);
                        mlat.setTimes(cat10Info.getTime());
                        mlat.setIdentification(cat10Info.getTargetIdentification());
                        MLATlist.Add(mlat);
                    }
                }
            }
            if (cat21List.Count != 0)
            {
                Coordinates coordADSB;
                for(i=0; i < cat21List.Count; i++)
                {
                    targetAddress = cat21List[i].getTargetAddress().ToString();
                    int k = 0;
                    bool found = false;
                    MLAT mlatFlightFound = MLATlist.FirstOrDefault(MLAT => MLAT.getTargetAddress() == targetAddress);
                    if (mlatFlightFound != null)
                    {
                        MLATlist.Remove(mlatFlightFound);
                        coordADSB = new Coordinates(cat21List[i].getLatitude(), cat21List[i].getLongitude());
                        mlatFlightFound.setCoordinatesADSB(coordADSB);
                        MLATlist.Add(mlatFlightFound);
                    }
                }
            }
            Console.WriteLine("Hola");
        }
        private void calculateProbIdentification()
        {
            probabilityFalseIdentification = 0;
            totalFalseIdentification = 0;
            string firstIdentification;
            List<string> targetids;
            totalIdentification = new List<double>();
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
                totalIdentification.Add((equalTargetID / totalTargetID) * 100);
            }
            for(i = 0; i < totalIdentification.Count; i++)
            {
                totalFalseIdentification = totalFalseIdentification + totalIdentification[i];
            }
            probabilityFalseIdentification = totalFalseIdentification / (totalIdentification.Count);
            labelID.Text = "Probability ended and the value is " + probabilityFalseIdentification.ToString();
        }

        //50m son 1.618''
        private void calculateFalseDetection()
        {
            probabilityFalseDetection = 0;
            totalDetection = 0;
            double totalCoordinates;
            double equalCoordinates;
            List<Coordinates> coord;
            totalFalseDetection = new List<double>();
            for(i=0; i < MLATlist.Count; i++)
            {
                int j;
                coord = obtainSecondsLongLat(MLATlist[i].getCoordinates());
                totalCoordinates = coord.Count-1;
                equalCoordinates = coord.Count - 1;
                for (j=0; j <coord.Count-1; j++)
                {
                    if (coord[j+1].GetLatitude() > (coord[j].GetLatitude() + maxDif) || coord[j + 1].GetLatitude() < (coord[j].GetLatitude() - maxDif) || coord[j + 1].GetLongitude() > (coord[j].GetLongitude() + maxDif) || coord[j + 1].GetLongitude() < (coord[j].GetLongitude() - maxDif))
                    {
                        equalCoordinates = equalCoordinates - 1;
                    }
                }
                totalFalseDetection.Add((equalCoordinates / totalCoordinates) * 100);
            }
            for (i = 0; i < totalFalseDetection.Count; i++)
            {
                totalDetection = totalDetection + totalFalseDetection[i];
            }
            probabilityFalseDetection = totalDetection / (totalFalseDetection.Count);
            labelID.Text = "Probability ended and the value is " + probabilityFalseDetection.ToString();
        }

        private void buttonIDProb_Click(object sender, EventArgs e)
        {
            probabilityFalseIdentification = 0;
            calculateProbIdentification();
        }

        private void buttonFalseDetect_Click(object sender, EventArgs e)
        {
            calculateFalseDetection();
        }

        private List<Coordinates> obtainSecondsLongLat(List<Coordinates> coord)
        {
            secondsLatLong = new List<Coordinates>();
            Coordinates coordSec;
            for (int k = 0; k < coord.Count; k++)
            {
                latdegrees = Math.Truncate(coord[k].GetLatitude() * 1) / 1;
                latminutes = (coord[k].GetLatitude() - (Math.Truncate(coord[k].GetLatitude() * 1) / 1)) * 60;
                latseconds = (latminutes - (Math.Truncate(latminutes * 1) / 1)) * 60;
                latminutes = (Math.Truncate(latminutes * 1) / 1);
                latseconds = (Math.Truncate(latseconds * 100) / 100);
                londegrees = Math.Truncate(coord[k].GetLongitude() * 1) / 1;
                lonminutes = (coord[k].GetLongitude() - (Math.Truncate(coord[k].GetLongitude() * 1) / 1)) * 60;
                lonseconds = (lonminutes - (Math.Truncate(lonminutes * 1) / 1)) * 60;
                lonminutes = (Math.Truncate(lonminutes * 1) / 1);
                lonseconds = (Math.Truncate(lonseconds * 100) / 100);
                coordSec = new Coordinates(latseconds, lonseconds);
                secondsLatLong.Add(coordSec);
            }
            return secondsLatLong;

        }
        private void MLAT_MOPS_Load(object sender, EventArgs e)
        {

        }
    }
}