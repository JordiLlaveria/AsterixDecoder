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
        string[] targetReportDescriptor = new string[17];

        // 2 Track Number
        int trackNumber;

        // 3 Service Identification
        int serviceIdentification;

        // 4 Time of Applicability for Position
        double timeOfApplicabilityForPosition;

        // 5 Position in WGS-84 Co-ordinates
        double latitude;
        double longitude;

        // 6 High-Resolution Position inf WGS-84 Co-cordinates
        double highResLatitude;
        double  highResLongitude;

        // 8 Time of Applicability for Velocity
        int timeOfApplicabilityVelocity;

        // 9 Air Speed
        string airSpeedUnits;
        double airSpeed;

        // 10 True Airspeed
        string rangeTrueAirspeed;
        double trueAirspeed;

        // 11 Target Address
        int targetAddress;

        // 12 Time of Message Reception for Position
        int timeMessageReceptionPosition;


        // 24 Magnetic Heading
        double magneticHeading;

        // 25 Target Status
        string[] targetStatus = new string[4];

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
                            byteread++;
                            if (eightbits[0] == 0 && eightbits[1] == 0 && eightbits[2] == 0)
                                targetReportDescriptor[0] = "24-Bit ICAO address";
                            else if (eightbits[0] == 0 && eightbits[1] == 0 && eightbits[2] == 1)
                                targetReportDescriptor[0] = "Duplicate address";
                            else if (eightbits[0] == 0 && eightbits[1] == 1 && eightbits[2] == 0)
                                targetReportDescriptor[0] = "Surface vehicle address";
                            else if (eightbits[0] == 0 && eightbits[1] == 1 && eightbits[2] == 1)
                                targetReportDescriptor[0] = "Anonymous address";
                            else
                                targetReportDescriptor[0] = "Reserved for future use";
                            if (eightbits[3] == 0 && eightbits[4] == 0)
                                targetReportDescriptor[1] = "25 ft";
                            else if (eightbits[3] == 0 && eightbits[4] == 1)
                                targetReportDescriptor[1] = "100 ft";
                            else if (eightbits[3] == 1 && eightbits[4] == 0)
                                targetReportDescriptor[1] = "Altitude Reporting Capability: Unknown";
                            else
                                targetReportDescriptor[1] = "Altitude Reporting Capability: Invalid";
                            if (eightbits[5] == 0)
                                targetReportDescriptor[2] = "Range Check: Default";
                            else
                                targetReportDescriptor[2] = "Range Check passed, CPR Validation pending";
                            if (eightbits[6] == 0)
                                targetReportDescriptor[3] = "Report from target transponder";
                            else
                                targetReportDescriptor[3] = "Report from field monitor (fixed transponder)";
                            if (eightbits[7] == 1)
                            {
                                for(j = 0; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread], j);
                                }
                                byteread++;
                                if (eightbits[0] == 0)
                                    targetReportDescriptor[4] = "No differential correction (ADS-B)";
                                else
                                    targetReportDescriptor[4] = "Differential correction (ADS-B)";
                                if (eightbits[1] == 0)
                                    targetReportDescriptor[5] = "Ground Bit not set";
                                else
                                    targetReportDescriptor[5] = "Ground Bit set";
                                if (eightbits[2] == 0)
                                    targetReportDescriptor[6] = "Actual target report";
                                else
                                    targetReportDescriptor[6] = "Simulated target report";
                                if (eightbits[3] == 0)
                                    targetReportDescriptor[7] = "Test Target: Default";
                                else
                                    targetReportDescriptor[7] = "Test Target";
                                if (eightbits[4] == 0)
                                    targetReportDescriptor[8] = "Equipment capable to provide Selected Altitude";
                                else
                                    targetReportDescriptor[8] = "Equipment not capable to provide Selected Altitude";
                                if (eightbits[5] == 0 && eightbits[6] == 0)
                                    targetReportDescriptor[9] = "Confidence Level: Report valid";
                                else if (eightbits[5] == 0 && eightbits[6] == 1)
                                    targetReportDescriptor[9] = "Confidence Level: Report suspect";
                                else if (eightbits[5] == 1 && eightbits[6] == 0)
                                    targetReportDescriptor[9] = "Confidence Level: No information";
                                else
                                    targetReportDescriptor[9] = "Confidence Level: Reserved for future use";
                                if (eightbits[7] == 1)
                                {
                                    for(j = 0; j < 8; j++) 
                                    {
                                        eightbits[7-j] = getBit(arraymessage[byteread], j);
                                    }
                                    byteread++;
                                    if (eightbits[2] == 0)
                                        targetReportDescriptor[10] = "Independent Position check: Default";
                                    else
                                        targetReportDescriptor[10] = "Independent Position Check failed";
                                    if (eightbits[3] == 0)
                                        targetReportDescriptor[11] = "NOGO-bit not set";
                                    else
                                        targetReportDescriptor[11] = "NOGO-bit set";
                                    if (eightbits[4] == 0)
                                        targetReportDescriptor[12] = "CPR Validation correct";
                                    else
                                        targetReportDescriptor[12] = "CPR Validation failed";
                                    if (eightbits[5] == 0)
                                        targetReportDescriptor[13] = "LDPJ not detected";
                                    else
                                        targetReportDescriptor[13] = "LDPJ detected";
                                    if (eightbits[6] == 0)
                                        targetReportDescriptor[14] = "Range Check: Default";
                                    else
                                        targetReportDescriptor[14] = "Range Check failed";
                                }
                            }
                            
                        break;

                        case 2:
                            // I021/161 Track Number
                            trackNumber = getInt32FromBytes(0, 0, arraymessage[byteread], arraymessage[byteread + 1]);
                            byteread = byteread + 2;
                        break;

                        case 3:
                            // I021/015
                            serviceIdentification = getInt32FromBytes(0,0,0,arraymessage[byteread]);
                            byteread++;
                        break;

                        case 4: 
                            // I021/071
                            threebytes[2] = arraymessage[byteread];
                            threebytes[1] = arraymessage[byteread + 1];
                            threebytes[0] = arraymessage[byteread + 2];
                            bytestogether1 = new BitArray(threebytes);
                            bytestogether1 = Reverse(bytestogether1);
                            for(int k = 0; k < threebytes.Length; k++)
                            {
                                timeOfApplicabilityForPosition = 0;
                                if (bytestogether1[k] == true)
                                {
                                    timeOfApplicabilityForPosition = timeOfApplicabilityForPosition + Math.Pow(2, threebytes.Length - k);
                                }
                            }
                            timeOfApplicabilityForPosition = timeOfApplicabilityForPosition * Math.Pow(2,-7);
                            byteread = byteread + 3;
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
                            byteread = byteread + 6;
                        break;

                        case 6:
                            // I021/131
                            Console.Write(byteread);
                            fourbytes[3] = arraymessage[byteread];
                            fourbytes[2] = arraymessage[byteread+1];
                            fourbytes[1] = arraymessage[byteread+2];
                            fourbytes[0] = arraymessage[byteread+3];
                            byte complement2check = getBit(fourbytes[3], 7);
                            highResLatitude = 0;
                            if (complement2check == 1)
                            {
                                bytestogether1 = new BitArray(fourbytes);
                                bytestogether1 = Reverse(bytestogether1);
                                bytestogether1 = complement2(bytestogether1);
                                for(int k = 0; k < bytestogether1.Length; k++)
                                {
                                    if (bytestogether1[k] == true)
                                        highResLatitude = highResLatitude + Math.Pow(2,bytestogether1.Length - 1 - k);
                                }
                                highResLatitude =  highResLatitude * (180/(Math.Pow(2,30))) * -1;
                            }
                            else
                            {
                                highResLatitude = getInt32FromBytes(fourbytes[3], fourbytes[2], fourbytes[1], fourbytes[0]) * (180/(Math.Pow(2,30)));
                            }
                            fourbytes[3] = arraymessage[byteread+4];
                            fourbytes[2] = arraymessage[byteread+5];
                            fourbytes[1] = arraymessage[byteread+6];
                            fourbytes[0] = arraymessage[byteread+7];
                            complement2check = getBit(fourbytes[3], 7);
                            highResLongitude = 0;
                            if (complement2check == 1)
                            {
                                bytestogether1 = new BitArray(fourbytes);
                                bytestogether1 = Reverse(bytestogether1);
                                bytestogether1 = complement2(bytestogether1);
                                for(int k = 0; k < bytestogether1.Length; k++)
                                {
                                    if (bytestogether1[k] == true)
                                        highResLongitude = highResLongitude + Math.Pow(2,bytestogether1.Length - 1 - k);
                                }
                                highResLongitude = highResLongitude * (180/(Math.Pow(2,30))) * -1;
                            }
                            else
                            {
                                highResLongitude = getInt32FromBytes(fourbytes[3], fourbytes[2], fourbytes[1], fourbytes[0]) * (180/(Math.Pow(2,30)));
                            }
                            byteread = byteread + 8;
                        break;

                        case 8:
                            // I021/072
                            timeOfApplicabilityVelocity = getInt32FromBytes(0, arraymessage[byteread], arraymessage[byteread+1], arraymessage[byteread+2]);
                            byteread = byteread + 3;
                            
                        break;

                        case 9: 
                            // I021/150
                            twobytes[1] = arraymessage[byteread];
                            twobytes[0] = arraymessage[byteread+1];
                            eightbits[0] = getBit(twobytes[1],7);
                            bytestogether1 = new BitArray(twobytes);
                            bytestogether1 = Reverse(bytestogether1);
                            airSpeed = 0;
                            for (int m = 1; m<bytestogether1.Length; m++)
                            {
                                airSpeed = airSpeed + Math.Pow(2,bytestogether1.Length - 2 - m);
                            }
                            if (bytestogether1[0] == true)
                            { 
                                airSpeedUnits = "Mach";
                                airSpeed = airSpeed * 0.001;
                            }
                            else
                            { 
                                airSpeedUnits = "IAS";
                                airSpeed = airSpeed * Math.Pow(2,-14);
                            }
                            byteread = byteread + 2;
                        break;

                        case 10:
                            // I021/151
                            twobytes[1] = arraymessage[byteread];
                            twobytes[0] = arraymessage[byteread+1];
                            eightbits[0] = getBit(twobytes[1],7);
                            bytestogether1 = new BitArray(twobytes);
                            bytestogether1 = Reverse(bytestogether1);
                            trueAirspeed = 0;
                            for (int m = 1; m<bytestogether1.Length; m++)
                            {
                                trueAirspeed = trueAirspeed + Math.Pow(2,bytestogether1.Length - 2 - m);
                            }
                            if (bytestogether1[0] == true)
                                rangeTrueAirspeed = "Value exceeds defined range";
                            else
                                rangeTrueAirspeed =  "Value in defined range";
                            byteread = byteread + 2;
;
                        break;

                        case 11: 
                            // I021/080
                            targetAddress = getInt32FromBytes(0, arraymessage[byteread], arraymessage[byteread+1], arraymessage[byteread+3]);
                            byteread = byteread + 3;
                        break;

                        case 12:
                            // I021/073
                            byteread = byteread + 3;
                        break;

                        case 13:
                            // I021/074
                            byteread = byteread + 4;
                        break;

                        case 14:
                            // I021/075
                            byteread = byteread + 3;
                        break;
                        
                        case 16:
                            // I021/076
                            byteread = byteread + 4;
                        break;

                        case 17:
                            // I021/140
                            byteread = byteread + 2;
                        break;

                        case 18:
                            // I021/090
                            byteread = byteread + 4;
                        break;
                        
                        case 19:
                            // I021/210
                            byteread = byteread + 1;
                        break;

                        case 20:
                            // I021/070
                            byteread = byteread + 2;
                        break;

                        case 21:
                            // I021/230
                            byteread = byteread + 2;
                        break;

                        case 22:
                            // I021/145
                            byteread = byteread + 2;
                        break;

                        case 24:
                            // I021/152 Magnetic Heading
                            magneticHeading = getInt32FromBytes(0, 0, arraymessage[byteread], arraymessage[byteread + 1])*360/(Math.Pow(2,16));
                            byteread = byteread + 2;                            
                        break;

                        case 25:
                            // I021/200 Target Status
                            bool[] octetStatus = getOctet(arraymessage[byteread]);
                            targetStatus[0] = octetStatus[0] ? "Intent change flag raised" : "No intent change active";
                            targetStatus[1] = octetStatus[1] ? "LNAV Mode not engaged" : "LNAV Mode engaged";
                            BitArray priorityStatusBits = new BitArray(new bool[] { octetStatus[5], octetStatus[4], octetStatus[3], false, false, false,false, false });
                            byte priorityStatus = convertToByte(priorityStatusBits);

                            if (priorityStatus == 0)
                            {
                                targetStatus[2] = "No emergency / not reported";
                            }
                            else if(priorityStatus == 1)
                            {
                                targetStatus[2] = "General emergency";
                            }
                            else if (priorityStatus == 2)
                            {
                                targetStatus[2] = "Lifeguard / medical emergency";
                            }
                            else if (priorityStatus == 3)
                            {
                                targetStatus[2] = "Minimum fuel";
                            }
                            else if (priorityStatus == 4)
                            {
                                targetStatus[2] = "No communications";
                            }
                            else if (priorityStatus == 5)
                            {
                                targetStatus[2] = "Unlawful interference";
                            }
                            else if (priorityStatus == 6)
                            {
                                targetStatus[2] = "Downed Aircraft";
                            }

                            BitArray surveillanceStatusBits = new BitArray(new bool[] { octetStatus[7], octetStatus[6], false, false, false, false, false, false });
                            byte surveillanceStatus = convertToByte(surveillanceStatusBits);

                            if(surveillanceStatus == 0)
                            {
                                targetStatus[3] = "No condition reported";
                            }
                            else if (surveillanceStatus == 1)
                            {
                                targetStatus[3] = "Permanent Alert (Emergency condition)";
                            }
                            else if (surveillanceStatus == 2)
                            {
                                targetStatus[3] = "Temporary Alert (change in Mode 3/A\r\nCode other than emergency)";
                            }
                            else if (surveillanceStatus == 3)
                            {
                                targetStatus[3] = "SPI set";
                            }

                            byteread++;
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

        public BitArray Reverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
            return array;
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
            int j = b.Length-2;
            if (b[b.Length-1] == true)
            {
                b[b.Length-1] = false;
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
                j = j-1;
            }
            return b;
        }

        byte convertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
    }
}
