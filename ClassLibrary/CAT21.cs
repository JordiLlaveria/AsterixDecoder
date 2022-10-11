using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CAT21
    {
        byte[] UAP = new byte[56];
        byte[] flightinformation = new byte[300];

        BitArray bytestogether1 = new BitArray(0);
        BitArray simplebyte = new BitArray(0);
        byte[] onebyte = new byte[1];
        byte[] onebyte1 = new byte[1];
        byte[] twobytes = new byte[2];
        byte[] threebytes = new byte[3];
        byte[] fourbytes = new byte[4];
        byte[] eightbits = new byte[8];
        bool[] octet;
        
        // 0 Data Source identification
        byte sac;
        byte sic;

        // 1 Target Report Descriptor
        string[] targetreportdescriptor = new string[17];


        public CAT21(byte[] arraymessage)
        {
            int j = 0;
            int byteread = 3;
            this.flightinformation = arraymessage;
            for (int i = 0; i < 8; i++)
            {
                byte byteobtained = getBit(arraymessage[byteread], 7-i);
                UAP[j] = byteobtained;
                j++;
            }
            
            while (UAP[j-1] == 1)
            {
                byteread++;
                for (int i = 0; i < 8; i++)
                {
                    byte byteobtained = getBit(arraymessage[byteread], 7 - i);
                    UAP[j] = byteobtained;
                    j++;
                }
            }
            byteread++;

            for (int i = 0; i < UAP.Length; i++)
            {
                if (UAP[i] == 1)
                {
                    switch(i)
                    {
                        case 0: 
                            // I021/010
                            sac = arraymessage[byteread];
                            sic = arraymessage[byteread + 1];
                            byteread = byteread + 2;
                            break;

                        case 1: 
                            // I021/040
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
                            }
                            if (eightbits[0] == 0 && eightbits[1] == 0 && eightbits[2] == 0)
                                targetreportdescriptor[0] = "24-Bit ICAO address";
                            else if (eightbits[0] == 0 && eightbits[1] == 0 && eightbits[2] == 1)
                                targetreportdescriptor[0] = "Duplicate address";
                            else if (eightbits[0] == 0 && eightbits[1] == 1 && eightbits[2] == 0)
                                targetreportdescriptor[0] = "Surface vehicle address";
                            else if (eightbits[0] == 0 && eightbits[1] == 1 && eightbits[2] == 1)
                                targetreportdescriptor[0] = "Anonymous address";
                            else
                                targetreportdescriptor[0] = "Reserved for future use";
                            if (eightbits[3] == 0 && eightbits[4] == 0)
                                targetreportdescriptor[1] = "25 ft";
                            else if (eightbits[3] == 0 && eightbits[4] == 1)
                                targetreportdescriptor[1] = "100 ft";
                            else if (eightbits[3] == 1 && eightbits[4] == 0)
                                targetreportdescriptor[1] = "Unknown";
                            else
                                targetreportdescriptor[1] = "Invalid";

                        break;

                        case 2: 
                            // I021/161
                        break;

                        case 3: 
                            // I021/015
                        break;

                        case 4: 
                            // I021/071
                        break;

                        case 5: 
                            // I021/130
                        break;

                        case 6: 
                            // I021/131
                        break;

                        case 8:
                            // I021/072
                        break;

                        case 9: 
                            // I021/150
                        break;

                        case 10:
                            // I021/151
                        break;

                        case 11: 
                            // I021/080
                        break;

                        case 12:
                            // I021/073
                        break;

                        case 13:
                            // I021/074
                        break;

                        case 14:
                            // I021/075
                        break;
                        
                        case 16:
                            // I021/076
                        break;

                        case 17:
                            // I021/140
                        break;

                        case 18:
                            // I021/090
                        break;
                        
                        case 19:
                            // I021/210
                        break;

                        case 20:
                            // I021/070
                        break;

                        case 21:
                            // I021/230
                        break;

                        case 22:
                            // I021/145
                        break;

                        case 24:
                            // I021/152
                        break;

                        case 25:
                            // I021/200
                        break;

                        case 26:
                            // I021/155
                        break;

                        case 27:
                            // I021/157
                        break;

                        case 28:
                            // I021/160
                        break;

                        case 29:
                            // I021/165
                        break;

                        case 30:
                            // I021/077
                        break;

                        case 32: 
                            // I021/170
                        break;

                        case 33:
                            // I021/020
                        break;

                        case 34:
                            // I021/220
                        break;

                        case 35:
                            // I021/146
                        break;

                        case 36:
                            // I021/148
                        break;

                        case 37:
                            // I021/110
                        break;

                        case 38:
                            // I021/016
                        break;

                        case 40:
                            // I021/008
                        break;

                        case 41:
                            // I021/271
                        break;

                        case 42:
                            // I021/132
                        break;

                        case 43:
                            // I021/250
                        break;

                        case 44:
                            // I021/260
                        break;

                        case 45:
                            // I021/400
                        break;

                        case 46:
                            // I021/295
                        break;
                    }
                }
            }
        }

        public byte getBit(byte b, int bitNumber)
        {
            int valueint = (b >> bitNumber) & 0x01;
            byte b2 = Convert.ToByte(valueint);
            return (b2);
        }
    }
}
