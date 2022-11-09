using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Flight
    {
        List<Coordinates> coordinates;
        List<TimeSpan> times;
        string sensor;
        string emitterCategory;
        int trackNumber;
        string targetAddress;
        string[] MOPSVersion = new string[3];
        List<double> flightLevel;
        List<double> groundSpeed;
        string targetIdentification;

        public Flight(string sensor, string emitterCategory, int trackNumber)
        {
            this.sensor = sensor;
            this.emitterCategory = emitterCategory;
            this.trackNumber = trackNumber;
            this.groundSpeed = new List<double>();
            this.coordinates = new List<Coordinates>();
            this.times = new List<TimeSpan>();
            this.flightLevel = new List<double>();
        }

        public void setTargetAddress(string address)
        {
            this.targetAddress = address;
        }

        public string getTargetAddress()
        {
            return this.targetAddress;
        }
    }

    public class Coordinates
    {
        double latitude;
        double longitude;
        public Coordinates(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
        public double GetLatitude()
        {
            return this.latitude;
        }
        public double GetLongitude()
        {
            return this.longitude;
        }
    }
}
