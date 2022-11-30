using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class MLAT
    {
        List<Coordinates> coordinates;
        List<string> listIdentifications;
        List<TimeSpan> timePositions;
        List<double> groundSpeeds;
        string tracknumber;
        string targetAddress;

        public MLAT(string tracknumber, string targetAddress, TimeSpan times, string identifications, double groundSpeed)
        {
            this.targetAddress = targetAddress;
            this.tracknumber = tracknumber;
            coordinates = new List<Coordinates>();
            listIdentifications = new List<string>();
            timePositions = new List<TimeSpan>();
            groundSpeeds = new List<double>();
            listIdentifications.Add(identifications);
            timePositions.Add(times);
            groundSpeeds.Add(groundSpeed);
        }

        public string getTrackNumber()
        {
            return tracknumber;
        }

        public string getTargetAddress()
        {
            return targetAddress;
        }

        public List<string> getTargetIdentifications()
        {
            return listIdentifications;
        }

        public string getFirstIdentification()
        {
            return listIdentifications[0];
        }

        public List<Coordinates> getCoordinates()
        {
            return coordinates;
        }
        public void setCoordinates(Coordinates coord)
        {
            coordinates.Add(coord);
        }

        public void setTimes(TimeSpan times)
        {
            timePositions.Add(times);
        }

        public void setTrackNumber(string track)
        {
            tracknumber = track;
        }

        public void setTargetAddress(string targetadd)
        {
            targetAddress = targetadd;
        }

        public void setIdentification(string identifications)
        {
            listIdentifications.Add(identifications);
        }

        public void setGroundSpeed(double groundspeed)
        {
            groundSpeeds.Add(groundspeed);
        }

        public TimeSpan getLastTime()
        {
            return timePositions[timePositions.Count - 1];
        }

        public TimeSpan getTime(int j)
        {
            return timePositions[j];
        }

        public double getGroundSpeed(int j)
        {
            return groundSpeeds[j];
        }

        public double getLongitude(int j)
        {
            return coordinates[j].GetLongitude();
        }

        public double getLatitude(int j)
        {
            return coordinates[j].GetLatitude();
        }
    }

}
