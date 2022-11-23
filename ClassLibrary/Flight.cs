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
        int typeVehicleNum;
        double trackNumber;
        string targetAddress;
        string typeVehicle;
        List<double> flightLevel;
        List<double> groundSpeed;
        string targetIdentification;

        public Flight(string sensor, string emitterCategory, double trackNumber)
        {
            this.sensor = sensor;
            this.emitterCategory = emitterCategory;
            this.trackNumber = trackNumber;
            this.groundSpeed = new List<double>();
            this.coordinates = new List<Coordinates>();
            this.times = new List<TimeSpan>();
            this.flightLevel = new List<double>();
        }

        public double getTrackNumber()
        {
            return this.trackNumber;
        }

        public void setFlightLevel(double FL)
        {
            this.flightLevel.Add(FL);
        }

        public void setGroundSpeed(double gSpeed)
        {
            this.groundSpeed.Add(gSpeed);
        }

        public void setCoordinates(Coordinates coordinates)
        {
            this.coordinates.Add(coordinates);
        }

        public void setTime(TimeSpan time)
        {
            this.times.Add(time);
        }

        public void setTargetIdentification(string id)
        {
            this.targetIdentification = id;
        }

        public void setTargetAddress(string address)
        {
            this.targetAddress = address;
        }

        public void setTypeVehicle(string vehicle)
        {
            this.typeVehicle = vehicle;
        }

        public void setTypeVehicleNum(int vehicleNum)
        {
            this.typeVehicleNum = vehicleNum;
        }

        public int getTypeVehicleNum()
        {
            return this.typeVehicleNum;
        }

        public string getTargetAddress()
        {
            return targetAddress;
        }
        public List<TimeSpan> getTimes()
        {
            return times;
        }

        public List<Coordinates> getCoordinates()
        {
            return coordinates;
        }

        public string getSensor()
        {
            return sensor;
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
