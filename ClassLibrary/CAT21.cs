using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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
        string[] targetReportDescriptor = new string[15];

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
        string[] qualityIndicators = new string[12];

        // 24 Magnetic Heading
        double magneticHeading;

        // 25 Target Status
        string[] targetStatus = new string[4];

        // 26 Barometric Vertical Rate
        string rangeExceeded;
        double barometricVerticalRate;

        // 35 Selected Altitude
        string[] selectedAltitudeInfo = new string[2];
        double selectedAltitude;

        // 36 Final Selected Altitude
        string[] finalSelectedAltitudeInfo = new string[3];
        double finalSelectedAltitude;

        // 37 Trajectory Intent
        string[] trajectoryIntent = new string[9];
        double trajectoryIntentAltitude;
        double trajectoryIntentLatitude;
        double trajectoryIntentLongitude;
        double trajectoryIntentTimeOverPoint;
        double trajectoryIntentTCPTurnRadius;

        // 38 Service Management
        double serviceManagement;

        // 40 Aircraft Operational Status
        string[] aircraftOperationalStatus = new string[7];

        // 41 Surface Capabilities and Chatacteristics
        string[] surfaceCapabilities = new string[5];
        bool lengthAircraftLowerThan;
        bool widthAircraftLowerThan;
        double lengthAircraft;
        double widthAircraft;

        // 42 Message Amplitude
        double messageAmplitude;

        // 44 ACAS Resolution Advisory Report
        string[] acasResolutionReport = new string[9];
        double[] acasResolutionReportValues = new double[9];

        // 45 Receiver ID
        double receiverID;

        // 46 Data Ages;
        string[] dataAges = new string[23];
        double[] dataAgesValue = new double[23];

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
                                targetReportDescriptor[0] = "Address Type: 24-Bit ICAO address";
                            else if (eightbits[0] == 0 && eightbits[1] == 0 && eightbits[2] == 1)
                                targetReportDescriptor[0] = "Address Type: Duplicate address";
                            else if (eightbits[0] == 0 && eightbits[1] == 1 && eightbits[2] == 0)
                                targetReportDescriptor[0] = "Address Type: Surface vehicle address";
                            else if (eightbits[0] == 0 && eightbits[1] == 1 && eightbits[2] == 1)
                                targetReportDescriptor[0] = "Address Type: Anonymous address";
                            else
                                targetReportDescriptor[0] = "Address Type: Reserved for future use";
                            if (eightbits[3] == 0 && eightbits[4] == 0)
                                targetReportDescriptor[1] = "Altitude Reporting Capability: 25 ft";
                            else if (eightbits[3] == 0 && eightbits[4] == 1)
                                targetReportDescriptor[1] = "Altitude Reporting Capability: 100 ft";
                            else if (eightbits[3] == 1 && eightbits[4] == 0)
                                targetReportDescriptor[1] = "Altitude Reporting Capability: Unknown";
                            else
                                targetReportDescriptor[1] = "Altitude Reporting Capability: Invalid";
                            if (eightbits[5] == 0)
                                targetReportDescriptor[2] = "Range Check: Default";
                            else
                                targetReportDescriptor[2] = "Range Check: Range Check passed, CPR Validation pending";
                            if (eightbits[6] == 0)
                                targetReportDescriptor[3] = "Report Type: Report from target transponder";
                            else
                                targetReportDescriptor[3] = "Report Type: Report from field monitor (fixed transponder)";
                            if (eightbits[7] == 1)
                            {
                                for(j = 0; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread], j);
                                }
                                byteread++;
                                if (eightbits[0] == 0)
                                    targetReportDescriptor[4] = "Differential Correction: No differential correction (ADS-B)";
                                else
                                    targetReportDescriptor[4] = "Differential Correction: Differential correction (ADS-B)";
                                if (eightbits[1] == 0)
                                    targetReportDescriptor[5] = "Ground Bit Setting: Ground Bit not set";
                                else
                                    targetReportDescriptor[5] = "Ground Bit Setting: Ground Bit set";
                                if (eightbits[2] == 0)
                                    targetReportDescriptor[6] = "Simulated Target: Actual target report";
                                else
                                    targetReportDescriptor[6] = "Simulated Target: Simulated target report";
                                if (eightbits[3] == 0)
                                    targetReportDescriptor[7] = "Test Target: Default";
                                else
                                    targetReportDescriptor[7] = "Test Target: Test Target";
                                if (eightbits[4] == 0)
                                    targetReportDescriptor[8] = "Selected Altitude Available: Equipment capable to provide Selected Altitude";
                                else
                                    targetReportDescriptor[8] = "Selected Altitude Available: Equipment not capable to provide Selected Altitude";
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
                                        targetReportDescriptor[10] = "Independent Position Check: Default";
                                    else
                                        targetReportDescriptor[10] = "Independent Position Check failed";
                                    if (eightbits[3] == 0)
                                        targetReportDescriptor[11] = "No-go Bit Status: NOGO-bit not set";
                                    else
                                        targetReportDescriptor[11] = "No-go Bit Status: NOGO-bit set";
                                    if (eightbits[4] == 0)
                                        targetReportDescriptor[12] = "Compact Position Recording: CPR Validation correct";
                                    else
                                        targetReportDescriptor[12] = "Compact Position Recording: CPR Validation failed";
                                    if (eightbits[5] == 0)
                                        targetReportDescriptor[13] = "Local Decoding Position Jump: LDPJ not detected";
                                    else
                                        targetReportDescriptor[13] = "Local Decoding Position Jump: LDPJ detected";
                                    if (eightbits[6] == 0)
                                        targetReportDescriptor[14] = "Range Check: Default";
                                    else
                                        targetReportDescriptor[14] = "Range Check: Range Check failed";
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
                            // I021/130 NO FUNCIONA, cambiar el 2 complement

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
                                threebytes[2] = arraymessage[byteread];
                                threebytes[1] = arraymessage[byteread + 1];
                                threebytes[0] = arraymessage[byteread + 2];
                                BitArray latbits = new BitArray(onebyte);
                                BitArray latbitsComplement = complement2(Reverse(latbits));                              

                                latitude = convertToInt32(Reverse(latbitsComplement)) * 180 / (Math.Pow(2, 23));
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
                                threebytes[2] = arraymessage[byteread + 3];
                                threebytes[1] = arraymessage[byteread + 4];
                                threebytes[0] = arraymessage[byteread + 5];
                                BitArray longbits = new BitArray(threebytes);
                                BitArray longbitsComplement = complement2(Reverse(longbits));

                                longitude =  convertToInt32(Reverse(longbitsComplement))* 180 / (Math.Pow(2, 23));
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
                                rangeTrueAirspeed = "Value in defined range";
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
                            tomrp = getInt32FromBytes(0, arraymessage[byteread], arraymessage[byteread + 1], arraymessage[byteread + 2]) * Math.Pow(2, -7);
                            byteread = byteread + 3;
                            break;

                        case 13:
                            // I021/074
                            fourbytes[3] = arraymessage[byteread];
                            fourbytes[2] = arraymessage[byteread + 1];
                            fourbytes[1] = arraymessage[byteread + 2];
                            fourbytes[0] = arraymessage[byteread + 3];
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
                                    tomrphp = tomrphp + Math.Pow(2, bytestogether1.Length - 3 - q);
                            }
                            tomrphp = tomrphp * Math.Pow(2, -30);
                            byteread = byteread + 4;
                            break;

                        case 14:
                            // I021/075
                            tomrv = getInt32FromBytes(0, arraymessage[byteread], arraymessage[byteread + 1], arraymessage[byteread + 2]) * Math.Pow(2, -7);
                            byteread = byteread + 3;
                            break;

                        case 16:
                            // I021/076
                            fourbytes[3] = arraymessage[byteread];
                            fourbytes[2] = arraymessage[byteread + 1];
                            fourbytes[1] = arraymessage[byteread + 2];
                            fourbytes[0] = arraymessage[byteread + 3];
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
                                    tomrvhp = tomrvhp + Math.Pow(2, bytestogether1.Length - 3 - q);
                            }
                            tomrvhp = tomrvhp * Math.Pow(2, -30);
                            byteread = byteread + 4;
                            break;

                        case 17:
                            // I021/140
                            eightbits[0] = getBit(arraymessage[byteread + 1], 7);
                            if (eightbits[0] == 1)
                            {
                                twobytes[1] = arraymessage[byteread];
                                twobytes[0] = arraymessage[byteread + 1];
                                bytestogether1 = new BitArray(twobytes);
                                bytestogether1 = Reverse(bytestogether1);
                                bytestogether1 = complement2(bytestogether1);
                                geometricHeight = 0;
                                for (int w = 0; w < bytestogether1.Length; w++)
                                {
                                    if (bytestogether1[w] == true)
                                        geometricHeight = geometricHeight + Math.Pow(2, bytestogether1.Length - 1 - w);
                                }
                                geometricHeight = geometricHeight * -6.25;
                            }
                            else
                                geometricHeight = getInt32FromBytes(0, 0, arraymessage[byteread], arraymessage[byteread + 1]) * 6.25;
                            if (geometricHeight == 204800)
                                greaterThanGeometricHeight = true;
                            else
                                greaterThanGeometricHeight = false;
                            byteread = byteread + 2;
                            break;

                        case 18:
                            Console.Write(byteread);
                            // I021/090
                            double value;
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7 - j] = getBit(arraymessage[byteread], j);
                            }
                            value = eightbits[0] * Math.Pow(2,2) + eightbits[1] * Math.Pow(2,1) + eightbits[2] * Math.Pow(2,0);
                            qualityIndicators[0] = "NUCr or NACv: " + value.ToString();
                            value = eightbits[3] * Math.Pow(2,3) + eightbits[4] * Math.Pow(2,2) + eightbits[5] * Math.Pow(2,1) + eightbits[6] * Math.Pow(2,0);
                            qualityIndicators[1] = "NUCp or NIC: " + value.ToString();
                            byteread++;
                            if (eightbits[7] == 1)
                            {
                                for (j = 0; j < 8; j++)
                                {
                                    eightbits[7 - j] = getBit(arraymessage[byteread], j);
                                }
                                qualityIndicators[2] = "Navigation Integrity Category for Barometric Altitude: " + eightbits[0].ToString();
                                value = eightbits[1] * Math.Pow(2,1) + eightbits[2] * Math.Pow(2,0);
                                qualityIndicators[3] = "Surveillance of Source Integrity Level: " + value.ToString();
                                value = eightbits[3] * Math.Pow(2,3) + eightbits[4] * Math.Pow(2,2) + eightbits[5] * Math.Pow(2,1) + eightbits[6] * Math.Pow(2,0);
                                qualityIndicators[4] = "Navigation Accuracy Category for Position: " + value.ToString();
                                byteread++;
                                if (eightbits[7] == 1)
                                {
                                    for (j = 0; j < 8; j++)
                                    {
                                        eightbits[7 - j] = getBit(arraymessage[byteread], j);
                                    }
                                    if (eightbits[2] == 0)
                                        qualityIndicators[5] = "SIL-Supplement: Measured per Flight-hour";
                                    else
                                        qualityIndicators[5] = "SIL-Supplement: Measured per sample";
                                    value = eightbits[3] * Math.Pow(2,1) + eightbits[4] * Math.Pow(2,0);
                                    qualityIndicators[6] = "Horizontal Position System Design Assurance Level: " + value.ToString();
                                    value = eightbits[5] * Math.Pow(2,1) + eightbits[6] * Math.Pow(2,0);
                                    qualityIndicators[7] = "Geometric Altitude Accuracy: " + value.ToString();
                                    byteread++;
                                    if (eightbits[7] == 1)
                                    {
                                        for (j = 0; j < 8; j++)
                                        {
                                            eightbits[7 - j] = getBit(arraymessage[byteread], j);
                                        }
                                        value = eightbits[0] * Math.Pow(2,3) + eightbits[1] * Math.Pow(2,2) + eightbits[2] * Math.Pow(2,1) + eightbits[3] * Math.Pow(2,0);
                                        if (value == 15)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: not defined";
                                            qualityIndicators[9] = "NUCp (+suppl.): not defined";
                                            qualityIndicators[10] = "NIC (+suppl.): not defined";
                                            qualityIndicators[11] = "NIC (+suppl.'s): not defined";
                                        }
                                        else if (value == 14)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 0.004 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 9";
                                            qualityIndicators[10] = "NIC (+suppl.): 11";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 11";
                                        }
                                        else if (value == 13)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 0.013 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 8";
                                            qualityIndicators[10] = "NIC (+suppl.): 10";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 10";
                                        }
                                        else if (value == 12)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 0.04 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): -";
                                            qualityIndicators[10] = "NIC (+suppl.): 9";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 9";
                                        }
                                        else if (value == 11)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 0.1 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 7";
                                            qualityIndicators[10] = "NIC (+suppl.): 8";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 8";
                                        }
                                        else if (value == 10)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 0.2 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 6";
                                            qualityIndicators[10] = "NIC (+suppl.): 7";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 7";
                                        }
                                        else if (value == 9)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 0.3 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): -";
                                            qualityIndicators[10] = "NIC (+suppl.): -";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 6 (+ 0/1)";
                                        }
                                        else if (value == 8)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 0.5 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 5";
                                            qualityIndicators[10] = "NIC (+suppl.): 6 (+ 0)";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 6 (+ 0/0)";
                                        }
                                        else if (value == 7)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 0.6 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): -";
                                            qualityIndicators[10] = "NIC (+suppl.): 6 (+ 1)";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 6 (+ 1/1)";
                                        }
                                        else if (value == 6)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 1.0 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 4";
                                            qualityIndicators[10] = "NIC (+suppl.): 5";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 5";
                                        }
                                        else if (value == 5)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 2.0 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 3";
                                            qualityIndicators[10] = "NIC (+suppl.): 4";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 4";
                                        }
                                        else if (value == 4)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 4.0 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): -";
                                            qualityIndicators[10] = "NIC (+suppl.): 3";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 3";
                                        }
                                        else if (value == 3)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 8.0 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): -";
                                            qualityIndicators[10] = "NIC (+suppl.): 2";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 2";
                                        }
                                        else if (value == 2)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 10.0 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 2";
                                            qualityIndicators[10] = "NIC (+suppl.): -";
                                            qualityIndicators[11] = "NIC (+suppl.'s): -";
                                        }
                                        else if (value == 1)
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: < 20.0 NM";
                                            qualityIndicators[9] = "NUCp (+suppl.): 1";
                                            qualityIndicators[10] = "NIC (+suppl.): 1";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 1";
                                        }
                                        else
                                        {
                                            qualityIndicators[8] = "Integrity Containment Bound: No integrity (or > 20.0 NM)";
                                            qualityIndicators[9] = "NUCp (+suppl.): 0";
                                            qualityIndicators[10] = "NIC (+suppl.): 0";
                                            qualityIndicators[11] = "NIC (+suppl.'s): 0";
                                        }
                                    }
                                }
                            }
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

                            bool[] octetBar = getOctet(arraymessage[byteread]);
                            rangeExceeded = octetBar[0] ? "Value exceeds defined range" : "Value in defined range";

                            bool[] octet2bar = getOctet(arraymessage[byteread + 1]);
                            bool[] z = new bool[15];
                            bool[] octet1Bar = new bool[] { octetBar[7], octetBar[6], octetBar[5], octetBar[4], octetBar[3], octetBar[2], octetBar[1] };
                            octet1Bar.CopyTo(z, 0);
                            octet2bar.CopyTo(z, 7);
                            BitArray firstBits = new BitArray(z);
                            
                            if (octetBar[1])
                            {
                                BitArray firstBitsComp = complement2(firstBits);
                                bool[] barometric = new bool[16];
                                barometric[0] = false;
                                firstBitsComp.CopyTo(barometric, 1);
                                BitArray barometricBit = new BitArray(barometric);
                                barometricVerticalRate = convertToInt32(Reverse(barometricBit))*(-1)* 6.25;

                            }
                            else
                            {
                                barometricVerticalRate = convertToInt32(Reverse(firstBits))* 6.25;
                            }
                            byteread = byteread + 2;
                            

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
                            //byteread = 69;
                            eightbits[0] = getBit(arraymessage[byteread], 7);
                            if (eightbits[0] == 0)
                                selectedAltitudeInfo[0] = "Source Availability: No source information provided";
                            else
                                selectedAltitudeInfo[0] = "Source Availability: Source Information provided";
                            eightbits[1] = getBit(arraymessage[byteread], 6);
                            eightbits[2] = getBit(arraymessage[byteread], 5);
                            if (eightbits[1] == 0 && eightbits[2] == 0)
                                selectedAltitudeInfo[1] = "Source: Unknown";
                            else if (eightbits[1] == 0 && eightbits[2] == 1)
                                selectedAltitudeInfo[1] = "Source: Aircraft Altitude (Holding Altitude)";
                            else if (eightbits[1] == 1 && eightbits[2] == 0)
                                selectedAltitudeInfo[1] = "MCP/FCU Selected Altitude";
                            else
                                selectedAltitudeInfo[1] = "FMS Selected Altitude";
                            twobytes[1] = arraymessage[byteread];
                            twobytes[0] = arraymessage[byteread+1];
                            bytestogether1 = new BitArray(twobytes);
                            bytestogether1 = Reverse(bytestogether1);
                            eightbits[3] = getBit(arraymessage[byteread + 1], 7);
                            if (eightbits[3] == 1)
                            {
                                bytestogether1 = complement2(bytestogether1);
                                selectedAltitude = 0;
                                for (int w = 3; w < bytestogether1.Length - 3; w++)
                                {
                                    if (bytestogether1[w] == true)
                                        selectedAltitude = selectedAltitude + Math.Pow(2,15 - w);
                                }
                                selectedAltitude = selectedAltitude * -25;
                            }
                            else
                                selectedAltitude = 0;
                                for (int w = 3; w < bytestogether1.Length - 3; w++)
                                {
                                    if (bytestogether1[w] == true)
                                        selectedAltitude = selectedAltitude + Math.Pow(2,15 - w);
                                }
                                selectedAltitude = selectedAltitude * 25;
                            byteread = byteread + 2;
                        break;

                        case 36:
                            // I021/148
                            eightbits[0] = getBit(arraymessage[byteread],0);
                            eightbits[1] = getBit(arraymessage[byteread+1],1);
                            eightbits[2] = getBit(arraymessage[byteread+2],2);
                            if (eightbits[0] == 0)
                                finalSelectedAltitudeInfo[0] = "Manage Vertical Mode: Not active of unknown";
                            else
                                finalSelectedAltitudeInfo[0] = "Manage Vertical Mode: Active";
                            if (eightbits[1] == 0)
                                finalSelectedAltitudeInfo[1] = "Altitude Hold Mode: Not active of unknown";
                            else
                                finalSelectedAltitudeInfo[1] = "Altitude Hold Mode: Active";
                            if (eightbits[2] == 0)
                                finalSelectedAltitudeInfo[2] = "Approach Mode: Not active of unknown";
                            else
                                finalSelectedAltitudeInfo[2] = "Approach Mode: Active";
                            eightbits[3] = getBit(arraymessage[byteread + 1], 7);
                            if (eightbits[3] == 1)
                            {
                                twobytes[1] = arraymessage[byteread];
                                twobytes[0] = arraymessage[byteread+1];
                                bytestogether1 = new BitArray(twobytes);
                                bytestogether1 = Reverse(bytestogether1);
                                bytestogether1 = complement2(bytestogether1);
                                finalSelectedAltitude = 0;
                                for (int w = 3; w < bytestogether1.Length; w++)
                                {
                                    if (bytestogether1[w] == true)
                                        finalSelectedAltitude = finalSelectedAltitude + Math.Pow(2,15 - w);
                                }
                                finalSelectedAltitude = finalSelectedAltitude * -25;
                            }
                            else
                                for (int w = 3; w < bytestogether1.Length; w++)
                                {
                                    if (bytestogether1[w] == true)
                                        finalSelectedAltitude = finalSelectedAltitude + Math.Pow(2,15 - w);
                                }
                                finalSelectedAltitude = finalSelectedAltitude * 25;
                            byteread = byteread + 2;
                        break;

                        case 37:
                            // I021/110
                            for(j = 6; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
                            }
                            byte firstsubfield = eightbits[0];
                            byte secondsubfield = eightbits[1];
                            byteread++;
                            if(firstsubfield == 1)
                            {
                                //First subfield
                                for(j = 6; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread], j);
                                }
                                if (eightbits[0] == 0)
                                    trajectoryIntent[0] = "NAV: Trajectory Intent Data is available for this aircraft";
                                else
                                    trajectoryIntent[0] = "NAV: Trajectory Intent Data is not available for this aircraft";
                                if (eightbits[1] == 0)
                                    trajectoryIntent[1] = "NVB: Trajectory Intent Data is valid";
                                else
                                    trajectoryIntent[1] = "NVB: Trajectory Intent Data is not valid";
                                byteread++;
                            }
                            if (secondsubfield == 1)
                            { 
                                //Second subfield
                                for(j = 6; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread], j);
                                }
                                double valueint = eightbits[0] * Math.Pow(2,7) + eightbits[1] * Math.Pow(2,6) + eightbits[2] * Math.Pow(2,5) + eightbits[3] * Math.Pow(2,4) + eightbits[4] * Math.Pow(2,3) + eightbits[5] * Math.Pow(2,2) + eightbits[6] * Math.Pow(2,1) + eightbits[7] * Math.Pow(2,0);
                                trajectoryIntent[2] = "Repetitive Factor: " + valueint.ToString();
                                for(j = 6; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread+1], j);
                                }
                                if (eightbits[0] == 0)
                                    trajectoryIntent[3] = "TCA: TCP number available";
                                else
                                    trajectoryIntent[3] = "TCA: TCP number not available";
                                if (eightbits[1] == 0)
                                    trajectoryIntent[4] = "NC: TCP compliance";
                                else
                                    trajectoryIntent[4] = "NC: TCP non-compliance";
                                double valuedouble = eightbits[2] * Math.Pow(2,5) + eightbits[3] * Math.Pow(2,4) + eightbits[4] * Math.Pow(2,3) + eightbits[5] * Math.Pow(2,2) + eightbits[6] * Math.Pow(2,1) + eightbits[7] * Math.Pow(2,0);
                                trajectoryIntent[5] = "Trajectory Change Point number: " + valuedouble.ToString();
                                valuedouble = 0;
                                twobytes[1] = arraymessage[byteread+2];
                                twobytes[0] = arraymessage[byteread+3];
                                bytestogether1 = new BitArray(twobytes);
                                bytestogether1 = Reverse(bytestogether1);
                                if (bytestogether1[bytestogether1.Length-1] == true)
                                    bytestogether1 = complement2(bytestogether1);
                                for (j=0; j < bytestogether1.Length; j++)
                                {
                                    if (bytestogether1[j] == true)
                                        valuedouble = valuedouble + Math.Pow(2,bytestogether1.Length - 1 - j);
                                }
                                if (bytestogether1[bytestogether1.Length-1] == true)
                                    trajectoryIntentAltitude = valuedouble * -1;
                                else
                                    trajectoryIntentAltitude = valuedouble;
                                valuedouble = 0;
                                threebytes[2] = arraymessage[byteread+4];
                                threebytes[1] = arraymessage[byteread+5];
                                threebytes[0] = arraymessage[byteread+6];
                                bytestogether1 = new BitArray(twobytes);
                                bytestogether1 = Reverse(bytestogether1);
                                if (bytestogether1[bytestogether1.Length-1] == true)
                                    bytestogether1 = complement2(bytestogether1);
                                for (j=0; j < bytestogether1.Length; j++)
                                {
                                    if (bytestogether1[j] == true)
                                        valuedouble = valuedouble + Math.Pow(2,bytestogether1.Length - 1 - j);
                                }
                                if (bytestogether1[bytestogether1.Length-1] == true)
                                    trajectoryIntentLongitude = valuedouble * -1;
                                else
                                    trajectoryIntentLongitude = valuedouble;
                                valuedouble = 0;
                                threebytes[2] = arraymessage[byteread+7];
                                threebytes[1] = arraymessage[byteread+8];
                                threebytes[0] = arraymessage[byteread+9];
                                bytestogether1 = new BitArray(twobytes);
                                bytestogether1 = Reverse(bytestogether1);
                                if (bytestogether1[bytestogether1.Length-1] == true)
                                    bytestogether1 = complement2(bytestogether1);
                                for (j=0; j < bytestogether1.Length; j++)
                                {
                                    if (bytestogether1[j] == true)
                                        valuedouble = valuedouble + Math.Pow(2,bytestogether1.Length - 1 - j);
                                }
                                if (bytestogether1[bytestogether1.Length-1] == true)
                                    trajectoryIntentLatitude = valuedouble * -1;
                                else
                                    trajectoryIntentLatitude = valuedouble;
                                for(j = 0; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread+10], j);
                                }
                                double pointType = eightbits[0] * Math.Pow(2,3) + eightbits[1] * Math.Pow(2,2) + eightbits[2] * Math.Pow(2,1) + eightbits[3] * Math.Pow(2,0);
                                if (pointType == 0)
                                    trajectoryIntent[5] = "Point Type: Unknown";
                                else if (pointType == 1)
                                    trajectoryIntent[5] = "Point Type: Fly by waypoint (LT)";
                                else if (pointType == 2)
                                    trajectoryIntent[5] = "Point Type: Fly over waypoint (LT)";
                                else if (pointType == 3)
                                    trajectoryIntent[5] = "Point Type: Hold pattern (LT)";
                                else if (pointType == 4)
                                    trajectoryIntent[5] = "Point Type: Procedure hold (LT)";
                                else if (pointType == 5)
                                    trajectoryIntent[5] = "Point Type: Procedure turn (LT)";
                                else if (pointType == 6)
                                    trajectoryIntent[5] = "Point Type: RF leg (LT)";
                                else if (pointType == 7)
                                    trajectoryIntent[5] = "Point Type: Top of climb (LT)";
                                else if (pointType == 8)
                                    trajectoryIntent[5] = "Point Type: Top of descent (LT)";
                                else if (pointType == 9)
                                    trajectoryIntent[5] = "Point Type: Start of level (LT)";
                                else if (pointType == 10)
                                    trajectoryIntent[5] = "Point Type: Cross-over altitude (LT)";
                                else
                                    trajectoryIntent[5] = "Point Type: Transition altitude (LT)";
                                if (eightbits[4] == 0 && eightbits[5] == 0)
                                    trajectoryIntent[6] = "TD: N/A";
                                else if (eightbits[4] == 0 && eightbits[5] == 1)
                                    trajectoryIntent[6] = "TD: Turn right";
                                else if (eightbits[4] == 1 && eightbits[5] == 0)
                                    trajectoryIntent[6] = "TD: Turn left";
                                else
                                    trajectoryIntent[6] = "TD: No turn";
                                if (eightbits[6] == 0)
                                    trajectoryIntent[7] = "Turn Radius Availability: TTR no available";
                                else
                                    trajectoryIntent[7] = "Turn Radius Availability: TTR available";
                                if (eightbits[7] == 0)
                                    trajectoryIntent[8] = "TOA: TOV available";
                                else
                                    trajectoryIntent[8] = "TOA: TOV not available";
                                trajectoryIntentTimeOverPoint = 0;
                                int tov = 0;
                                while (tov < 3)
                                {
                                    for(j = 0; j < 8; j++) 
                                    {
                                        eightbits[7-j] = getBit(arraymessage[byteread + 11 + tov], j);
                                        trajectoryIntentTimeOverPoint = trajectoryIntentTimeOverPoint + eightbits[7-j] * Math.Pow(2,j);
                                    }
                                    tov++;
                                }
                                trajectoryIntentTCPTurnRadius = 0;
                                int ttr = 0;
                                while (ttr < 2)
                                {
                                    for(j = 0; j < 8; j++) 
                                    {
                                        eightbits[7-j] = getBit(arraymessage[byteread + 14 + ttr], j);
                                        trajectoryIntentTCPTurnRadius = trajectoryIntentTCPTurnRadius + eightbits[7-j] * Math.Pow(2,j);
                                    }
                                    ttr++;
                                }
                                trajectoryIntentTCPTurnRadius = trajectoryIntentTCPTurnRadius * 0.01;
                                byteread = byteread + 16;
                            }
                        break;

                        case 38:
                            // I021/016
                            serviceManagement = getInt32FromBytes(0,0,0,arraymessage[byteread]) * 0.5;
                            byteread++;
                        break;

                        case 40:
                            // I021/008
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
                            }
                            if (eightbits[0] == 0)
                                aircraftOperationalStatus[0] = "TCAS II or ACAS RA not active";
                            else
                                aircraftOperationalStatus[0] = "TCAS RA active";
                            if (eightbits[1] == 0 && eightbits[2] == 0)
                                aircraftOperationalStatus[1] = "Target Trajectory Change Report Capability: No capability for Trajectory Change Reports";
                            else if (eightbits[1] == 0 && eightbits[2] == 1)
                                aircraftOperationalStatus[1] = "Target Trajectory Change Report Capability: Support for TC+0 reports only";
                            else if (eightbits[1] == 1 && eightbits[2] == 0)
                                aircraftOperationalStatus[1] = "Target Trajectory Change Report Capability: Support for multiple TC reports";
                            else
                                aircraftOperationalStatus[1] = "Target Trajectory Change Report Capability: Reserved";
                            if (eightbits[3] == 0)
                                aircraftOperationalStatus[2] = "Target State Report Capability: No capability to support Target State Reports";
                            else
                                aircraftOperationalStatus[2] = "Target State Report Capability: Capable of supporting target State Reports";
                            if (eightbits[4] == 0)
                                aircraftOperationalStatus[3] = "Air-Referenced Velocity Report Capability: No capability to generate ARV-reports";
                            else
                                aircraftOperationalStatus[3] = "Air-Referenced Velocity Report Capability: Capable of generate ARV-reports";
                            if (eightbits[5] == 0)
                                aircraftOperationalStatus[4] = "CDTI not operational";
                            else
                                aircraftOperationalStatus[4] = "CDTI operational";
                            if (eightbits[6] == 0)
                                aircraftOperationalStatus[5] = "TCAS operational";
                            else
                                aircraftOperationalStatus[5] = "TCAS not operational";
                            if (eightbits[7] == 0)
                                aircraftOperationalStatus[6] = "Antenna Diversity";
                            else
                                aircraftOperationalStatus[6] = "Single Antenna only";
                            byteread++;
                        break;

                        case 41:
                            // I021/271
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
                            }
                            if (eightbits[2] == 0)
                                surfaceCapabilities[0] = "Position Offset Applied: Position transmitted is not the ADS-B position reference point";
                            else
                                surfaceCapabilities[0] = "Position Offset Applied: Position transmitted is the ADS-B position reference point";
                            if (eightbits[3] == 0)
                                surfaceCapabilities[1] = "Cockpit Display of Traffic Information Surface: CDTI not operational";
                            else
                                surfaceCapabilities[1] = "Cockpit Display of Traffic Information Surface: CDTI operational";
                            if (eightbits[4] == 0)
                                surfaceCapabilities[2] = "Class B2 transmit power less than 70 Watts: ≥ 70 Watts";
                            else
                                surfaceCapabilities[2] = "Class B2 transmit power less than 70 Watts: < 70 Watts";
                            if (eightbits[5] == 0)
                                surfaceCapabilities[3] = "Receiving ATC Services: Aircraft not receiving ATC-services";
                            else
                                surfaceCapabilities[3] = "Receiving ATC Services: Aircraft receiving ATC services";
                            if (eightbits[6] == 0)
                                surfaceCapabilities[4] = "Setting of 'IDENT'-switch: IDENT switch not active";
                            else
                                surfaceCapabilities[4] = "Setting of 'IDENT'-switch: IDENT switch active";
                            byteread++;
                            if (eightbits[7] == 1)
                            {
                                for(j = 3; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread], j);
                                }
                                value = 0;
                                value = eightbits[4] * Math.Pow(2,3) + eightbits[5] * Math.Pow(2,2) + eightbits[6] * Math.Pow(2,1) + eightbits[7] * Math.Pow(2,0);
                                if (value == 0)
                                {
                                    lengthAircraft = 15;
                                    widthAircraft = 11.5;
                                }
                                else if (value == 1)
                                {
                                    lengthAircraft = 15;
                                    widthAircraft = 23;
                                }
                                else if (value == 2)
                                {
                                    lengthAircraft = 25;
                                    widthAircraft = 28.5;
                                }
                                else if (value == 3)
                                {
                                    lengthAircraft = 25;
                                    widthAircraft = 34;
                                }
                                else if (value == 4)
                                {
                                    lengthAircraft = 35;
                                    widthAircraft = 33;
                                }
                                else if (value == 5)
                                {
                                    lengthAircraft = 35;
                                    widthAircraft = 38;
                                }
                                else if (value == 6)
                                {
                                    lengthAircraft = 45;
                                    widthAircraft = 39.5;
                                }
                                else if (value == 7)
                                {
                                    lengthAircraft = 45;
                                    widthAircraft = 45;
                                }
                                else if (value == 8)
                                {
                                    lengthAircraft = 55;
                                    widthAircraft = 45;
                                }
                                else if (value == 9)
                                {
                                    lengthAircraft = 55;
                                    widthAircraft = 52;
                                }
                                else if (value == 10)
                                {
                                    lengthAircraft = 65;
                                    widthAircraft = 59.5;
                                }
                                else if (value == 11)
                                {
                                    lengthAircraft = 65;
                                    widthAircraft = 67;
                                }
                                else if (value == 12)
                                {
                                    lengthAircraft = 75;
                                    widthAircraft = 72.5;
                                }
                                else if (value == 13)
                                {
                                    lengthAircraft = 75;
                                    widthAircraft = 80;
                                }
                                else if (value == 14)
                                {
                                    lengthAircraft = 85;
                                    widthAircraft = 80;
                                }
                                else
                                {
                                    lengthAircraft = 85;
                                    lengthAircraftLowerThan = false;
                                    widthAircraftLowerThan = false;
                                    widthAircraft = 0;
                                }
                                byteread++;
                            }
                        break;

                        case 42:
                            // I021/132
                            eightbits[0] = getBit(arraymessage[byteread], 7);
                            if (eightbits[0] == 1)
                            {
                                onebyte[0] = arraymessage[byteread];
                                bytestogether1 = new BitArray(onebyte);
                                bytestogether1 = Reverse(bytestogether1);
                                bytestogether1 = complement2(bytestogether1);
                                messageAmplitude = 0;
                                for (j = 0; j < bytestogether1.Length; j++)
                                    if (bytestogether1[j] == true)
                                        messageAmplitude = messageAmplitude - Math.Pow(2,bytestogether1.Length - 1 - j);
                            }
                            else
                                messageAmplitude = getInt32FromBytes(0,0,0,arraymessage[byteread]);
                            byteread++;

                        break;

                        case 43:
                            // I021/250
                            // Mode S MB Data
                        break;

                        case 44:
                            // I021/260
                            /*
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
                            }
                            double value = eightbits[0] * Math.Pow(2,4) + eightbits[1] * Math.Pow(2,3) + eightbits[2] * Math.Pow(2,2) + eightbits[3] * Math.Pow(2,1) + eightbits[4] * Math.Pow(2,0);
                            acasResolutionReport[0] = "Message Type: " + value.ToString();
                            acasResolutionReportValues[0] = value;
                            value = eightbits[5] * Math.Pow(2,2) + eightbits[6] * Math.Pow(2,1) + eightbits[7] * Math.Pow(2,0);
                            acasResolutionReport[1] = "Message Sub-type: " + value.ToString();
                            acasResolutionReportValues[1] = value;
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread+1], j);
                            }
                            value = eightbits[0] * Math.Pow(2,13) + eightbits[1] * Math.Pow(2,12) + eightbits[2] * Math.Pow(2,11) + eightbits[3] * Math.Pow(2,10) + eightbits[4] * Math.Pow(2,9) + eightbits[5] * Math.Pow(2,8) + eightbits[6] * Math.Pow(2,7) + eightbits[7] * Math.Pow(2,6);
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread+2], j);
                            }
                            value = value + eightbits[0] * Math.Pow(2,5) + eightbits[1] * Math.Pow(2,4) + eightbits[2] * Math.Pow(2,3) + eightbits[3] * Math.Pow(2,2) + eightbits[4] * Math.Pow(2,1) + eightbits[5] * Math.Pow(2,0);
                            acasResolutionReport[2] = "Active Resolution Advisories: "
                            */

                        break;

                        case 45:
                            // I021/400
                            receiverID = getInt32FromBytes(0,0,0,arraymessage[byteread]);
                            byteread++;
                        break;

                        case 46:
                            // I021/295
                            byte[] dataAgesLength = new byte[32];
                            for(j = 0; j < 8; j++) 
                            {
                                dataAgesLength[7-j] = getBit(arraymessage[byteread], j);
                            }
                            byteread++;
                            if (dataAgesLength[7] == 1)
                            {
                                for(j = 0; j < 8; j++) 
                                {
                                    dataAgesLength[15-j] = getBit(arraymessage[byteread], j);
                                }
                                byteread++;
                                if (dataAgesLength[15] == 1)
                                {
                                    for(j = 0; j < 8; j++) 
                                    {
                                        dataAgesLength[23-j] = getBit(arraymessage[byteread], j);
                                    }
                                    byteread++;
                                    if (dataAgesLength[23] == 1)
                                    {
                                        for(j = 0; j < 8; j++) 
                                        {
                                            dataAgesLength[31-j] = getBit(arraymessage[byteread], j);
                                        }
                                        byteread++;
                                    }
                                }
                            }
                            string data;
                            if (dataAgesLength[0] == 1)
                            {
                                data = "Age of the latest received information transmitted in item IO21/008: ";
                                getDataAge(arraymessage,byteread,0,data);
                                byteread++;
                            }
                            if (dataAgesLength[1] == 1)
                            {
                                data = "Age of the last update of the Target Report Descriptor: ";
                                getDataAge(arraymessage,byteread,1,data);
                                byteread++;
                            }
                            if (dataAgesLength[2] == 1)
                            {
                                data = "Age of the last update of Mode 3/A Code: ";
                                getDataAge(arraymessage,byteread,2,data);
                                byteread++;
                            }
                            if (dataAgesLength[3] == 1)
                            {
                                data = "Age of the latest information received to update the Quality Indicators: ";
                                getDataAge(arraymessage,byteread,3,data);
                                byteread++;
                            }
                            if (dataAgesLength[4] == 1)
                            {
                                data = "Age of the last update of the Trajectory Intent information: ";
                                getDataAge(arraymessage,byteread,4,data);
                                byteread++;
                            }
                            if (dataAgesLength[5] == 1)
                            {
                                data = "Age of the latest measurement of the message amplitude: ";
                                getDataAge(arraymessage,byteread,5,data);
                                byteread++;
                            }
                            if (dataAgesLength[6] == 1)
                            {
                                data = "Age of the information contained in item 021/140: ";
                                getDataAge(arraymessage,byteread,6,data);
                                byteread++;
                            }
                            if (dataAgesLength[8] == 1)
                            {
                                data = "Age of the Flight Level information: ";
                                getDataAge(arraymessage,byteread,7,data);
                                byteread++;
                            }
                            if (dataAgesLength[9] == 1)
                            {
                                data = "Age of the Intermediate State Selected Altitude: ";
                                getDataAge(arraymessage,byteread,8,data);
                                byteread++;
                            }
                            if (dataAgesLength[10] == 1)
                            {
                                data = "Age of the Final State Selected Altitude: ";
                                getDataAge(arraymessage,byteread,9,data);
                                byteread++;
                            }
                            if (dataAgesLength[11] == 1)
                            {
                                data = "Age of the Air Speed: ";
                                getDataAge(arraymessage,byteread,10,data);
                                byteread++;
                            }
                            if (dataAgesLength[12] == 1)
                            {
                                data = "Age of the value for the True Air Speed: ";
                                getDataAge(arraymessage,byteread,11,data);
                                byteread++;
                            }
                            if (dataAgesLength[13] == 1)
                            {
                                data = "Age of the value for the Magnetic Heading: ";
                                getDataAge(arraymessage,byteread,12,data);
                                byteread++;
                            }
                            if (dataAgesLength[14] == 1)
                            {
                                data = "Age of the Barometric Vertical Rate: ";
                                getDataAge(arraymessage,byteread,13,data);
                                byteread++;
                            }
                            if (dataAgesLength[16] == 1)
                            {
                                data = "Age of the Geometric Vertical Rate: ";
                                getDataAge(arraymessage,byteread,14,data);
                                byteread++;
                            }
                            if (dataAgesLength[17] == 1)
                            {
                                data = "Age of the Ground Vector: ";
                                getDataAge(arraymessage,byteread,15,data);
                                byteread++;
                            }
                            if (dataAgesLength[18] == 1)
                            {
                                data = "Age of the Track Angle Rate: ";
                                getDataAge(arraymessage,byteread,16,data);
                                byteread++;
                            }
                            if (dataAgesLength[19] == 1)
                            {
                                data = "Age of the Target Identification: ";
                                getDataAge(arraymessage,byteread,17,data);
                                byteread++;
                            }
                            if (dataAgesLength[20] == 1)
                            {
                                data = "Age of the Target Status: ";
                                getDataAge(arraymessage,byteread,18,data);
                                byteread++;
                            }
                            if (dataAgesLength[21] == 1)
                            {
                                data = "Age of the Meteorological data: ";
                                getDataAge(arraymessage,byteread,19,data);
                                byteread++;
                            }
                            if (dataAgesLength[22] == 1)
                            {
                                data = "Age of the Roll Angle: ";
                                getDataAge(arraymessage,byteread,20,data);
                                byteread++;
                            }
                            if (dataAgesLength[24] == 1)
                            {
                                data = "Age of the latest update of an Active ACAS Resolution Advisory: ";
                                getDataAge(arraymessage,byteread,21,data);
                                byteread++;
                            }
                            if (dataAgesLength[25] == 1)
                            {
                                data = "Age of the latest information received on the surface capabilities and characteristics of the respective target: ";
                                getDataAge(arraymessage,byteread,22,data);
                                byteread++;
                            }
                        break;
                    }
                }
            }
        }

        public void getDataAge(byte[] arraymessage, int byteToRead, int position, string information)
        {
            onebyte[0] = arraymessage[byteToRead];
            double info = getInt32FromBytes(0,0,0,onebyte[0]) * 0.1;
            dataAges[position] = information + info.ToString() + " s";
            dataAgesValue[position] = info;
        }

        public int getInt32FromBytes(byte first, byte second, byte third, byte fourth)
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
            else
            {
                b[b.Length - 1] = true;
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

        int convertToInt32(BitArray bitArray)
        {
            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

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
