using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CAT10
    {
        byte[] UAP = new byte[32];
        byte[] flightinformation = new byte[300];
        public CAT10(byte[] arraymessage)
        {
            int cont = 3;
            int j = 0;
            this.flightinformation = arraymessage;
            for (int i = 0; i < 8; i++)
            {
                byte byteobtained = getBit(arraymessage[cont], 7-i);
                UAP[j] = byteobtained;
                j++;
            }
            while (UAP[j-1] == 1)
            {
                cont++;
                for (int i = 0; i < 8; i++)
                {
                    byte byteobtained = getBit(arraymessage[cont], 7 - i);
                    UAP[j] = byteobtained;
                    j++;
                }
            }

            for (int i = 0; i < UAP.Length; i++)
            {
                if (UAP[i] == 1)
                {
                    switch(i)
                    {

                    }
                }
            }
        }

        byte getBit(byte b, int bitNumber)
        {
            int valueint = (b >> bitNumber) & 0x01;
            byte b2 = Convert.ToByte(valueint);
            return (b2);
        }
    }
}
