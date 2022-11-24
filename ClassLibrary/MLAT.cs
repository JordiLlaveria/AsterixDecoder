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
        List<Coordinates> coordinatesADSB;
        List<string> listIdentifications;
        List<TimeSpan> timePositions;
        string tracknumber;
        string targetAddress;

        public MLAT(string tracknumber, string targetAddress)
        {
            this.targetAddress = targetAddress;
            this.tracknumber = tracknumber;
            coordinates = new List<Coordinates>();
            coordinatesADSB = new List<Coordinates>();
            listIdentifications = new List<string>();
            timePositions = new List<TimeSpan>();
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

        public List<Coordinates> getCoordinatesADSB()
        {
            return coordinatesADSB;
        }

        public void setCoordinatesADSB(Coordinates coordADSB)
        {
            coordinatesADSB.Add(coordADSB);
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

        public void setIdentification(string identifications)
        {
            listIdentifications.Add(identifications);
        }
    }

}
