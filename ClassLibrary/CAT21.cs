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

        // 2 Track Number
        int trackNumber;

        // 5 Position in WGS-84 Co-ordinates
        double latitude;
        double longitude;

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
                            // I021/161 Track Number
                            byteread = byteread + 3; //Treure
                            trackNumber = getInt32FromBytes(0, 0, arraymessage[byteread], arraymessage[byteread + 1]);
                            byteread = byteread + 2;
                        break;

                        case 3:
                            // I021/015
                            byteread = byteread + 1;
                        break;

                        case 4: 
                            // I021/071

                        break;

                        case 5:
                            // I021/130
                            bool[] octetlat = getOctet(arraymessage[byteread]);
                            bool[] octetlong = getOctet(arraymessage[byteread + 3]);

                            bool latComp = octetlat[0];
                            bool longComp = octetlong[0];

                            byte[] lat1Array = new byte[1];
                            byte[] lat2Array = new byte[1];
                            byte[] lat3Array = new byte[1];

                            byte[] long1Array = new byte[1];
                            byte[] long2Array = new byte[1];
                            byte[] long3Array = new byte[1];

                            if (latComp)
                            {
                                onebyte[0] = arraymessage[byteread];
                                BitArray latbits1 = new BitArray(onebyte);
                                BitArray latbits1Complement = complement2(latbits1);
                                onebyte[0] = arraymessage[byteread + 1];
                                BitArray latbits2 = new BitArray(onebyte);
                                BitArray latbits2Complement = complement2(latbits2);
                                onebyte[0] = arraymessage[byteread + 2];
                                BitArray latbits3 = new BitArray(onebyte);
                                BitArray latbits3Complement = complement2(latbits3);

                                latbits1Complement.CopyTo(lat1Array, 0);
                                latbits2Complement.CopyTo(lat2Array, 0);
                                latbits3Complement.CopyTo(lat3Array, 0);

                                latitude = getInt32FromBytes(0, lat1Array[0], lat2Array[0], lat3Array[0]) * 180 / (Math.Pow(2, 23));
                                latitude = latitude * (-1);
                            }
                            else
                            {
                                lat1Array[0] = arraymessage[byteread];
                                lat2Array[0] = arraymessage[byteread + 1];
                                lat3Array[0] = arraymessage[byteread + 2];
                                latitude = getInt32FromBytes(0, lat1Array[0], lat2Array[0], lat3Array[0])*180/(Math.Pow(2,23));
                            }
                            if (longComp)
                            {
                                onebyte[0] = arraymessage[byteread + 3];
                                BitArray longbits1 = new BitArray(onebyte);
                                BitArray longbits1Complement = complement2(longbits1);
                                onebyte[0] = arraymessage[byteread + 4];
                                BitArray longbits2 = new BitArray(onebyte);
                                BitArray longbits2Complement = complement2(longbits2);
                                onebyte[0] = arraymessage[byteread + 5];
                                BitArray longbits3 = new BitArray(onebyte);
                                BitArray longbits3Complement = complement2(longbits3);

                                longbits1Complement.CopyTo(long1Array, 0);
                                longbits2Complement.CopyTo(long2Array, 0);
                                longbits3Complement.CopyTo(long3Array, 0);
                                longitude = getInt32FromBytes(0, long1Array[0], long2Array[0], long3Array[0]) * 180 / (Math.Pow(2, 23));
                                longitude = longitude * (-1);
                            }
                            else
                            {
                                long1Array[0] = arraymessage[byteread + 3];
                                long2Array[0] = arraymessage[byteread + 4];
                                long3Array[0] = arraymessage[byteread + 5];
                                longitude = getInt32FromBytes(0, long1Array[0], long2Array[0], long3Array[0]) * 180 / (Math.Pow(2, 23));
                            }

                            byteread = byteread + 8;
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

        int getInt32FromBytes(byte first, byte second, byte third, byte fourth)
        {
            byte[] bytesArray = { first, second, third, fourth };
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytesArray);
            }
            return BitConverter.ToInt32(bytesArray, 0);
        }

        public bool[] getOctet(byte Byte)
        {
            byte[] octet = new byte[1];
            octet[0] = Byte;
            BitArray octetBits = new BitArray(octet);
            bool[] octetArray = new bool[8];
            octetBits.CopyTo(octetArray, 0);
            Array.Reverse(octetArray);
            return octetArray;
        }

        public byte getBit(byte b, int bitNumber)
        {
            int valueint = (b >> bitNumber) & 0x01;
            byte b2 = Convert.ToByte(valueint);
            return (b2);
        }

        public BitArray complement2(BitArray b)
        {
            //Complemento a 1
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] == true)
                {
                    b[i] = false;
                }
                else
                {
                    b[i] = true;
                }
            }
            bool mellevoununo = false;
            int j = 0;
            if (b[0] == true)
            {
                b[0] = false;
                mellevoununo = true;
            }
            while (mellevoununo == true)
            {
                if (b[j] == false)
                {
                    b[j] = true;
                    mellevoununo = false;
                }
                else
                {
                    b[j] = false;
                }
                j++;
            }
            return b;
        }
    }
}
