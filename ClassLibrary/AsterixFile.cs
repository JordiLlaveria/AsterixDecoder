using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ClassLibrary
{
    public class AsterixFile
    {
        List<CAT10> CAT10list = new List<CAT10>();
        List<CAT21> CAT21list = new List<CAT21>();
        List<Flight> Flightslist = new List<Flight>();
        public AsterixFile(string path)
        {
            byte[] messages = File.ReadAllBytes(path);
            int i = 0;
            int lenmessage;
            while (i < messages.Length)
            {
                lenmessage = messages[i + 1] + messages[i + 2];
                byte[] arraymessage = new byte[lenmessage];
                for (int j = 0; j < lenmessage; j++)
                {
                    arraymessage[j] = messages[i];
                    i++;
                }
                if (arraymessage[0] == 10)
                {
                    CAT10 cat10 = new CAT10(arraymessage);
                    CAT10list.Add(cat10);
                }
                else if(arraymessage[0] == 21)
                {
                    CAT21 cat21 = new CAT21(arraymessage);
                    CAT21list.Add(cat21);
                }
            }
            this.obtainFlights();
        }

        public void obtainFlights()
        {
            if (CAT10list.Count != 0)
            {
                for (int i = 0; i < CAT10list.Count; i++)
                {
                    CAT10 cat10Info = CAT10list[i];
                    string sensor = cat10Info.getTypeSensor();
                    double tracknumber = cat10Info.getTrackNumber();
                    Flight flightFound = Flightslist.FirstOrDefault(flight => flight.getTrackNumber() == tracknumber);
                    if (flightFound != null)
                    {
                        if (cat10Info.getMessageType() == "Target Report") 
                        {
                            double[] latLong = cat10Info.getLatitudeLongitudeWGS84(sensor);
                            Coordinates coordinates = new Coordinates(latLong[0], latLong[1]);
                            flightFound.setCoordinates(coordinates);
                            flightFound.setFlightLevel(cat10Info.getFlightLevel());
                            flightFound.setTime(cat10Info.getTime());
                            flightFound.setGroundSpeed(cat10Info.getGroundSpeed());
                        }
                    }
                    else
                    {
                        Flight flight = new Flight(sensor, "10", tracknumber);
                        double[] latLong = cat10Info.getLatitudeLongitudeWGS84(sensor);
                        Coordinates coordinates = new Coordinates(latLong[0], latLong[1]);
                        string targetaddress = cat10Info.getTargetAddress();
                        if (targetaddress != null)
                            flight.setTargetAddress(targetaddress);
                        flight.setCoordinates(coordinates);
                        flight.setFlightLevel(cat10Info.getFlightLevel());
                        flight.setTime(cat10Info.getTime());
                        flight.setGroundSpeed(cat10Info.getGroundSpeed());
                        Flightslist.Add(flight);
                    }
                }
            }
            if (CAT21list.Count != 0)
            {
                for (int i = 0; i < CAT21list.Count; i++)
                {
                    CAT21 cat21Info = CAT21list[i];
                    string sensor = "ADSB";
                    double tracknumber = cat21Info.getTrackNumber();
                    Flight flightFound = Flightslist.FirstOrDefault(flight => flight.getTrackNumber() == tracknumber);
                    if (flightFound != null)
                    {
                        double latitude = cat21Info.getLatitude();
                        double longitude = cat21Info.getLongitude();
                        Coordinates coordinates = new Coordinates(latitude, longitude);
                        flightFound.setCoordinates(coordinates);
                        flightFound.setFlightLevel(cat21Info.getFlightLevel());
                        flightFound.setTime(cat21Info.getTime());
                        flightFound.setGroundSpeed(cat21Info.getGroundSpeed());
                    }
                    else
                    {
                        Flight flight = new Flight(sensor, "21", tracknumber);
                        flight.setTargetIdentification(cat21Info.getTargetIdentification());
                        double latitude = cat21Info.getLatitude();
                        double longitude = cat21Info.getLongitude();
                        Coordinates coordinates = new Coordinates(latitude, longitude);
                        flight.setCoordinates(coordinates);
                        flight.setFlightLevel(cat21Info.getFlightLevel());
                        flight.setTime(cat21Info.getTime());
                        flight.setGroundSpeed(cat21Info.getGroundSpeed());
                        flight.setTypeVehicle(cat21Info.getTypeVehicle());
                        flight.setTargetAddress(cat21Info.getTargetAddress());
                        Flightslist.Add(flight);
                    }
                }
            }
            Console.WriteLine("Hola");
        }

        public List<CAT10> getCAT10List()
        {
            return CAT10list;
        }

        public List<CAT21> getCAT21List()
        {
            return CAT21list;
        }

        public List<Flight> getFlights()
        {
            return Flightslist;
        }

    }
}
