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
        ArrayList CATBothlist = new ArrayList();
        List<Flight> Flightlist = new List<Flight>();
        public AsterixFile(string path)
        {
            byte[] messages = File.ReadAllBytes(path);

            //Vamos a intentar separar la información
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
                    CATBothlist.Add(cat10);
                }
                else if(arraymessage[0] == 21)
                {
                    CAT21 cat21 = new CAT21(arraymessage);
                    CAT21list.Add(cat21);
                    CATBothlist.Add(cat21);
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
                    int sensor = cat10Info.getTypeSensor();
                    string targetAddress = cat10Info.getTargetAddress();
                    Flight flightFound = Flightlist.FirstOrDefault(flight => flight.getTargetAddress() == targetAddress);
                    if (flightFound != null)
                    {

                    }
                    else
                    {

                    }
                }
            }
        }

        public List<CAT10> getCAT10List()
        {
            return CAT10list;
        }

        public List<CAT21> getCAT21List()
        {
            return CAT21list;
        }

    }
}
