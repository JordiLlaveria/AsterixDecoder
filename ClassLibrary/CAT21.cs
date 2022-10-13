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
        int latitude;
        int longitude;

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
        string targetAddress;

        // 12 Time of Message Reception for Position
        double tomrp;

        // 13 Time of Message Reception of Position-High Precision
        double tomrphp;

        // 14 Time of Message Reception for Velocity
        double tomrv;

        // 15 Time of Message Reception of Velocity-High Precision
        double tomrvhp;

        // 17 Geometric Height
        double geometricHeight;
        bool greaterThanGeometricHeight;

        // 18 Quality Indicators
        double nucr;
        double nucp;
        double nicbaro;
        double sil;
        double nacp;
        string silSupplement;
        double sda;
        double gva;
        double pic;

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

                                x = getInt32FromBytes(0, 0, x1Array[0], x2Array[0]);
                                x = x * (-1);
                            }
                            else
                            {
                                x1Array[0] = arraymessage[byteread];
                                x2Array[0] = arraymessage[byteread + 1];
                                x = getInt32FromBytes(0, 0, x1Array[0], x2Array[0]);
                            }
                            if (y2complement)
                            {
                                onebyte[0] = arraymessage[byteread + 2];
                                BitArray ybits1 = new BitArray(onebyte);
                                BitArray ybits1Complement = complement2(ybits1);
                                onebyte[0] = arraymessage[byteread + 3];
                                BitArray ybits2 = new BitArray(onebyte);
                                BitArray ybits2Complement = complement2(ybits2);

                                ybits1Complement.CopyTo(y1Array, 0);
                                ybits2Complement.CopyTo(y2Array, 0);
                                y = getInt32FromBytes(0, 0, y1Array[0], y2Array[0]);
                                y = y * (-1);
                            }
                            else
                            {
                                y1Array[0] = arraymessage[byteread + 2];
                                y2Array[0] = arraymessage[byteread + 3];
                                y = getInt32FromBytes(0, 0, y1Array[0], y2Array[0]);
                            }
                            byteread = byteread + 6;
                        break;

                        case 6: 
                            // I021/131
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
                            threebytes[2] = arraymessage[byteread];
                            threebytes[1] = arraymessage[byteread + 1];
                            threebytes[0] = arraymessage[byteread + 2];
                            bytestogether1 = new BitArray(threebytes);
                            bytestogether1 = Reverse(bytestogether1);
                            int position = 0;
                            int positionfourbits = 0;
                            string stringbits;
                            byte[] fourbits = new byte[1];
                            fourbits[0] = 0;
                            while (position < bytestogether1.Length)
                            {
                                while (positionfourbits < 4)
                                {
                                    if (bytestogether1[position] == true)
                                    {
                                        fourbits[0] = (byte)(fourbits[0] + Math.Pow(2, 3 - positionfourbits));
                                    }
                                    positionfourbits++;
                                    position++;
                                }
                                string decimalNumber = fourbits[0].ToString();
                                int number = int.Parse(decimalNumber);
                                stringbits = number.ToString("x");
                                targetAddress = targetAddress + stringbits;
                                positionfourbits = 0;
                                fourbits[0] = 0;
                            }
                            byteread = byteread + 3;
                        break;

                        case 12:
                            // I021/073
                            tomrp = getInt32FromBytes(0,arraymessage[byteread],arraymessage[byteread+1],arraymessage[byteread+2]) * Math.Pow(2,-7);
                            byteread = byteread + 3;
                        break;

                        case 13:
                            // I021/074
                            fourbytes[3] = arraymessage[byteread];
                            fourbytes[2] = arraymessage[byteread+1];
                            fourbytes[1] = arraymessage[byteread+2];
                            fourbytes[0] = arraymessage[byteread+3];
                            bytestogether1 = new BitArray(fourbytes);
                            bytestogether1 = Reverse(bytestogether1);
                            tomrphp = 0;
                            if (bytestogether1[0] == true && bytestogether1[1] == false)
                                tomrp = tomrp - 1;
                            else if (bytestogether1[0] == true && bytestogether1[1] == false)
                                tomrp = tomrp + 1;
                            for (int q = 2; q < bytestogether1.Length; q++)
                            {
                                if (bytestogether1[q] == true)
                                    tomrphp = tomrphp + Math.Pow(2,bytestogether1.Length - 3 - q);
                            }
                            tomrphp = tomrphp * Math.Pow(2,-30);
                            byteread = byteread + 4;
                        break;

                        case 14:
                            // I021/075
                            tomrv = getInt32FromBytes(0,arraymessage[byteread],arraymessage[byteread+1],arraymessage[byteread+2]) * Math.Pow(2,-7);
                            byteread = byteread + 3;
                        break;
                        
                        case 16:
                            // I021/076
                            fourbytes[3] = arraymessage[byteread];
                            fourbytes[2] = arraymessage[byteread+1];
                            fourbytes[1] = arraymessage[byteread+2];
                            fourbytes[0] = arraymessage[byteread+3];
                            bytestogether1 = new BitArray(fourbytes);
                            bytestogether1 = Reverse(bytestogether1);
                            tomrvhp = 0;
                            if (bytestogether1[0] == true && bytestogether1[1] == false)
                                tomrv = tomrv - 1;
                            else if (bytestogether1[0] == true && bytestogether1[1] == false)
                                tomrv = tomrv + 1;
                            for (int q = 2; q < bytestogether1.Length; q++)
                            {
                                if (bytestogether1[q] == true)
                                    tomrvhp = tomrvhp + Math.Pow(2,bytestogether1.Length - 3 - q);
                            }
                            tomrvhp = tomrvhp * Math.Pow(2,-30);
                            byteread = byteread + 4;
                        break;

                        case 17:
                            // I021/140
                            eightbits[0] = getBit(arraymessage[byteread+1],7);
                            if (eightbits[0] == 1) 
                            { 
                                twobytes[1] = arraymessage[byteread];
                                twobytes[0] = arraymessage[byteread+1];
                                bytestogether1 = new BitArray(twobytes);
                                bytestogether1 = Reverse(bytestogether1);
                                bytestogether1 = complement2(bytestogether1);
                                geometricHeight = 0;
                                for (int w = 0; w < bytestogether1.Length; w++)
                                {
                                    if (bytestogether1[w] == true)
                                        geometricHeight = geometricHeight + Math.Pow(2,bytestogether1.Length - 1 - w);
                                }
                                geometricHeight = geometricHeight * -6.25;
                            }
                            else
                                geometricHeight = getInt32FromBytes(0,0,arraymessage[byteread],arraymessage[byteread+1]) * 6.25;
                            if (geometricHeight == 204800)
                                greaterThanGeometricHeight = true;
                            else
                                greaterThanGeometricHeight = false;
                            byteread = byteread + 2;
                        break;

                        case 18:
                            // I021/090
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
                            }
                            nucr = eightbits[0] * Math.Pow(2,2) + eightbits[1] * Math.Pow(2,1) + eightbits[2] * Math.Pow(2,0);
                            nucr = eightbits[3] * Math.Pow(2,3) + eightbits[4] * Math.Pow(2,2) + eightbits[5] * Math.Pow(2,1) + eightbits[6] * Math.Pow(2,0);
                            byteread++;
                            if (eightbits[7] == 1)
                            {
                                for(j = 0; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread], j);
                                }
                                nicbaro = eightbits[0];
                                sil = eightbits[1] * Math.Pow(2,1) + eightbits[2] * Math.Pow(2,0);
                                nacp = eightbits[3] * Math.Pow(2,2) + eightbits[4] * Math.Pow(2,1) + eightbits[5] * Math.Pow(2,0);
                                byteread++;
                                if (eightbits[7] == 1)
                                {
                                    for(j = 0; j < 8; j++) 
                                    {
                                        eightbits[7-j] = getBit(arraymessage[byteread], j);
                                    }
                                    if (eightbits[2] == 1)
                                        silSupplement = "Measured per Flight-hour";
                                    else
                                        silSupplement = "Measured per sample";
                                    sda = eightbits[3] * Math.Pow(2,1) + eightbits[4] * Math.Pow(2,0);
                                    gva = eightbits[5] * Math.Pow(2,1) + eightbits[6] * Math.Pow(2,0);
                                    byteread++;
                                    if (eightbits[7] == 1)
                                    {
                                        for(j = 0; j < 8; j++) 
                                        {
                                            eightbits[7-j] = getBit(arraymessage[byteread], j);
                                        }
                                        pic = eightbits[0] * Math.Pow(2,3) + eightbits[1] * Math.Pow(2,2) + eightbits[2] * Math.Pow(2,1) + eightbits[3] * Math.Pow(2,0);
                                        byteread++;
                                    }
                            }
                            }
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
    }
}
