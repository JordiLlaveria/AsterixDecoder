using MultiCAT6.Utils;
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
    public class CAT10
    {
        byte[] UAP = new byte[32];
        byte[] flightinformation = new byte[300];

        // 1 I010/010 Data Source Identifier
        byte sac;
        byte sic;
        // 2 I010/000 Message Type
        string messageType;

        // 3 I010/020 Target Report Descriptor
        string typ;
        string drc;
        string chn;
        string gbs;
        string crt;
        string sim;
        string tst;
        string rab;
        string lop;
        string tot;
        string spi;
        string[] targetReportDescriptor = new string[10];

        // 4 I010/140 Time of the Day
        double timeOfTheDay;
        int hores;
        int minuts;
        int segons;
        double ms;

        // 6 I010/040 Measured Position in Polar Co-ordinates
        double rho;
        double theta;

        // 7 I010//042 Position in Cartesian Co-ordinates
        double x;
        double y;

        // 8 I010/200 Calculated Track Velocity in Polar Coordinates
        double groundspeed_polar_coordinates; // kt
        double trackangle_polar_coordinates; // deg

        // 9 I010/202 Calculated Track Velocity in Cartesian Coordinates
        double vx_cartesian_coordinates; // m/s
        double vy_cartesian_coordinates; // m/s

        //10 I010/161 Track Number
        double tracknumber;

        //11 I010/170 Track Status
        string[] trackstatus = new string[10];

        //12
        string[] mode3Acode = new string[3];
        string code3A;

        //13 
        string targetaddress;

        //14 I010/245 Target Identification
        string sti;
        string targetIdentification;

        //17 (16 real) Vehicle fleet identification
        string vfi;

        //18 (17 real) I010/090 Flight Level in Binary Representation
        string v;
        string g;
        int FL;

        //19 (18 real)
        double height;

        //20 (19 real) I010/270 Target Size and Orientation
        double length; // m
        double orientation;
        double width; // m

        //21 (20 real)
        string[] systemstatus = new string[5];

        //22 (21 real)
        string[] pre_programmed_message = new string[2];

        //24 (23 real)
        double x_standard_deviation;
        double y_standard_deviation;
        double covariance;

        //26 (24 real) I010/131 Amplitude of primary plot
        byte amplitudeOfPrimaryPlot;

        //27 (25 real) I010/210 Calculated Acceleration
        double Ax; // m/s^2
        double Ay; // m/s^2

        BitArray bytestogether1 = new BitArray(0);
        BitArray simplebyte = new BitArray(0);
        byte[] onebyte = new byte[1];
        byte[] onebyte1 = new byte[1];
        byte[] twobytes = new byte[2];
        byte[] threebytes = new byte[3];
        byte[] fourbytes = new byte[4];
        byte[] eightbits = new byte[8];
        bool[] octet;
        public CAT10(byte[] arraymessage)
        {
            int j = 0;
            int byteread = 3;
            this.flightinformation = arraymessage;
            for (int i = 0; i < 8; i++)
            {
                byte byteobtained = getBit(arraymessage[byteread], 7 - i);
                UAP[j] = byteobtained;
                j++;
            }

            while (UAP[j - 1] == 1)
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
                    switch (i)
                    {
                        case 0:
                            // 1 I010/010
                            sac = arraymessage[byteread];
                            sic = arraymessage[byteread + 1];
                            byteread = byteread + 2;
                            break;

                        case 1:
                            // 2 I010/000
                            byte num = arraymessage[byteread];
                            if (num == 1)
                            {
                                messageType = "Target Report";
                            }
                            else if (num == 2)
                            {
                                messageType = "Start of Update Cycle";
                            }
                            else if (num == 3)
                            {
                                messageType = "Periodic Status Message";
                            }
                            else if (num == 4)
                            {
                                messageType = "Event-Triggered Status Message";
                            }
                            else
                            {
                                messageType = "Error";
                            }

                            byteread = byteread + 1;
                            break;

                        case 2:
                            // 3 I010/020 Target Report Descriptor
                            int cont = 0;
                            bool fx = true;

                            while (fx)
                            {
                                bool[] octetArray = getOctet(arraymessage[byteread]);

                                switch (cont)
                                {
                                    case 0:
                                        // TYP
                                        if (octetArray[0] == false && octetArray[1] == false && octetArray[2] == false)
                                        {
                                            targetReportDescriptor[0] = "SSR multilateration";
                                        }
                                        else if (octetArray[0] == false && octetArray[1] == false && octetArray[2] == true)
                                        {
                                            targetReportDescriptor[0] = "Mode S multilateration";
                                        }
                                        else if (octetArray[0] == false && octetArray[1] == true && octetArray[2] == false)
                                        {
                                            targetReportDescriptor[0] = "ADS-B";
                                        }
                                        else if (octetArray[0] == false && octetArray[1] == true && octetArray[2] == true)
                                        {
                                            targetReportDescriptor[0] = "PSR";
                                        }
                                        else if (octetArray[0] == true && octetArray[1] == false && octetArray[2] == false)
                                        {
                                            targetReportDescriptor[0] = "Magnetic Loop System";
                                        }
                                        else if (octetArray[0] == true && octetArray[1] == false && octetArray[2] == true)
                                        {
                                            targetReportDescriptor[0] = "HF multilateration";
                                        }
                                        else if (octetArray[0] == true && octetArray[1] == true && octetArray[2] == false)
                                        {
                                            targetReportDescriptor[0] = "TYP Not defined";
                                        }
                                        else
                                        {
                                            targetReportDescriptor[0] = "Other types";
                                        }

                                        targetReportDescriptor[1] = octetArray[3] ? "Differential Correction (ADS-B)" : "No Differential Correction (ADS_B)";
                                        targetReportDescriptor[2] = octetArray[4] ? "Chain 2" : "Chain 1";
                                        targetReportDescriptor[3] = octetArray[5] ? "Transponder Ground bit set" : "Transponder Ground bit not set";
                                        targetReportDescriptor[4] = octetArray[6] ? "Corrupted replies in multilateration" : "No Corrupted reply in multilateration";

                                        fx = octetArray[7];

                                        break;

                                    case 1:

                                        targetReportDescriptor[5] = octetArray[0] ? "Simulated target report" : "Actual target report";
                                        targetReportDescriptor[6] = octetArray[1] ? "Test Target" : "Default";
                                        targetReportDescriptor[7] = octetArray[2] ? "Report from field monitor (fixed transponder)" : "Report from target monitor";

                                        if (octetArray[3] == false && octetArray[4] == false)
                                        {
                                            targetReportDescriptor[8] = "Undetermined";
                                        }
                                        else if (octetArray[3] == false && octetArray[4] == true)
                                        {
                                            targetReportDescriptor[8] = "Loop start";
                                        }
                                        else if (octetArray[3] == true && octetArray[4] == false)
                                        {
                                            targetReportDescriptor[8] = "Loop finish";
                                        }
                                        else
                                        {
                                            targetReportDescriptor[8] = "Error";
                                        }

                                        if (octetArray[5] == false && octetArray[6] == false)
                                        {
                                            targetReportDescriptor[9] = "Undetermined";
                                        }
                                        else if (octetArray[5] == false && octetArray[6] == true)
                                        {
                                            targetReportDescriptor[9] = "Aircraft";
                                        }
                                        else if (octetArray[5] == true && octetArray[6] == false)
                                        {
                                            targetReportDescriptor[9] = "Ground vehicle";
                                        }
                                        else
                                        {
                                            targetReportDescriptor[9] = "Helicopter";
                                        }

                                        fx = octetArray[7];
                                        break;

                                    case 2:

                                        targetReportDescriptor[10] = octetArray[0] ? "Special Position Identification" : "Absence of SPI";

                                        fx = octetArray[7];
                                        break;
                                }
                                byteread = byteread + 1;
                                cont++;
                            }

                            break;

                        case 3:
                            // 4 I010/140 Time of the Day
                            this.timeOfTheDay = getInt32FromBytes(0, arraymessage[byteread], arraymessage[byteread + 1], arraymessage[byteread + 2]) / (double)128; // segons
                            double segonsprov = timeOfTheDay;
                            double minutsprov = segonsprov / 60;
                            double horesprov = minutsprov / 60;
                            double horesfinals = Math.Truncate(horesprov * 1) / 1;
                            minutsprov = (horesprov - horesfinals) * 60;
                            hores = Convert.ToInt32(horesfinals);
                            double minutsfinalsprov;
                            if (minutsprov > 0)
                            {
                                minutsfinalsprov = Math.Truncate(minutsprov * 1) / 1;
                                segonsprov = (minutsprov - minutsfinalsprov) * 60;
                            }
                            else
                            {
                                segonsprov = minutsprov * 60;
                                minutsfinalsprov = 0;
                            }
                            minuts = Convert.ToInt32(minutsfinalsprov);
                            double segonsfinalsprov;
                            if (segonsprov > 0)
                            {
                                segonsfinalsprov = Math.Truncate(segonsprov * 1) / 1;
                                ms = (segonsprov - segonsfinalsprov) * 1000;
                            }
                            else
                            {
                                ms = minutsprov * 1000;
                                segonsfinalsprov = 0;
                            }
                            ms = Math.Truncate(ms * 1) / 1;
                            segons = Convert.ToInt32(segonsfinalsprov);
                            byteread = byteread + 3;
                            break;

                        case 4:
                            //WGS-Co-ordinates
                            //
                            //
                            //
                            break;

                        case 5:
                            // 6 I010/040 Measured Position in Polar Co-ordinates                            
                            rho = getInt32FromBytes(0, 0, arraymessage[byteread], arraymessage[byteread + 1]);
                            theta = getInt32FromBytes(0, 0, arraymessage[byteread + 2], arraymessage[byteread + 3]) * 0.0055;
                            theta = Math.Truncate(theta * 100) / 100;
                            byteread = byteread + 4;
                            break;

                        case 6:
                            // 7 I010//042 Position in Cartesian Co-ordinates
                            octet = getOctet(arraymessage[byteread]);
                            bool x2Complement = octet[0];
                            octet = getOctet(arraymessage[byteread + 2]);
                            bool y2complement = octet[0];

                            byte[] x1Array = new byte[1];
                            byte[] x2Array = new byte[1];

                            byte[] y1Array = new byte[1];
                            byte[] y2Array = new byte[1];

                            if (x2Complement)
                            {
                                onebyte[0] = arraymessage[byteread];
                                BitArray xbits1 = new BitArray(onebyte);
                                BitArray xbits1Complement = complement2xy(xbits1);
                                onebyte[0] = arraymessage[byteread + 1];
                                BitArray xbits2 = new BitArray(onebyte);
                                BitArray xbits2Complement = complement2xy(xbits2);

                                xbits1Complement.CopyTo(x1Array, 0);
                                xbits2Complement.CopyTo(x2Array, 0);

                                x = getInt32FromBytes(0, 0, x1Array[0], x2Array[0]);
                                x = x * (-1) - 1;
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
                                BitArray ybits1Complement = complement2xy(ybits1);
                                onebyte[0] = arraymessage[byteread + 3];
                                BitArray ybits2 = new BitArray(onebyte);
                                BitArray ybits2Complement = complement2xy(ybits2);

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
                            byteread = byteread + 4;
                            break;

                        case 8:
                            // I010/200
                            for (j = 0; j < 2; j++)
                            {
                                twobytes[1] = arraymessage[byteread + j * 2];
                                twobytes[0] = arraymessage[byteread + j * 2 + 1];
                                bytestogether1 = new BitArray(twobytes);
                                if (j == 0)
                                {
                                    for (int p = 0; p < bytestogether1.Length; p++)
                                    {
                                        if (bytestogether1[p] == true) {
                                            groundspeed_polar_coordinates = groundspeed_polar_coordinates + Math.Pow(2, p);
                                        }
                                    }
                                    groundspeed_polar_coordinates = groundspeed_polar_coordinates * Math.Pow(2, -14) * 1852;
                                    groundspeed_polar_coordinates = Math.Truncate(groundspeed_polar_coordinates * 100) / 100;
                                }
                                else
                                {
                                    for (int p = 0; p < bytestogether1.Length; p++)
                                    {
                                        if (bytestogether1[p] == true) {
                                            trackangle_polar_coordinates = trackangle_polar_coordinates + Math.Pow(2, p);
                                        }
                                    }
                                    trackangle_polar_coordinates = trackangle_polar_coordinates * (360 / Math.Pow(2, 16));
                                    trackangle_polar_coordinates = Math.Truncate(trackangle_polar_coordinates * 100) / 100;
                                }
                            }
                            byteread = byteread + 4;
                            break;
                        case 9:
                            // I010/202
                            //byteread = 26;
                            bool complement2done = false;
                            for (j = 0; j < 2; j++)
                            {
                                twobytes[1] = arraymessage[byteread + j * 2];
                                twobytes[0] = arraymessage[byteread + j * 2 + 1];
                                bytestogether1 = new BitArray(twobytes);
                                bytestogether1 = Reverse(bytestogether1);
                                onebyte[0] = getBit(twobytes[1], 7);
                                if (onebyte[0] == 1)
                                {
                                    complement2(bytestogether1);
                                    complement2done = true;
                                }
                                if (j == 0)
                                {
                                    for (int s = 0; s < bytestogether1.Length; s++)
                                    {
                                        if (bytestogether1[s] == true) {
                                            vx_cartesian_coordinates = vx_cartesian_coordinates + Math.Pow(2, bytestogether1.Length - 1 - s);
                                        }
                                    }
                                    if (complement2done == true)
                                    {
                                        vx_cartesian_coordinates = vx_cartesian_coordinates * -0.25;
                                    }
                                    else
                                    {
                                        vx_cartesian_coordinates = vx_cartesian_coordinates * 0.25;
                                    }
                                }
                                else
                                {
                                    for (int q = 0; q < bytestogether1.Length; q++)
                                    {
                                        if (bytestogether1[q] == true) {
                                            vy_cartesian_coordinates = vy_cartesian_coordinates + Math.Pow(2, bytestogether1.Length - 1 - q);
                                        }
                                    }
                                    if (complement2done == true)
                                    {
                                        vy_cartesian_coordinates = vy_cartesian_coordinates * -0.25;
                                    }
                                    else
                                    {
                                        vy_cartesian_coordinates = vy_cartesian_coordinates * 0.25;
                                    }
                                }
                            }
                            byteread = byteread + 4;
                            break;
                        case 10:
                            // I010/161
                            twobytes[1] = arraymessage[byteread];
                            twobytes[0] = arraymessage[byteread + 1];
                            bytestogether1 = new BitArray(twobytes);
                            for (int t = 0; t < bytestogether1.Length; t++)
                            {
                                if (bytestogether1[t] == true)
                                {
                                    tracknumber = tracknumber + Math.Pow(2, t);
                                }
                            }
                            byteread = byteread + 2;
                            break;
                        case 11:
                            // I010/170
                            for (j = 0; j < 8; j++)
                            {
                                eightbits[7 - j] = getBit(arraymessage[byteread], j);
                            }
                            byteread++;
                            if (eightbits[0] == 0)
                            {
                                trackstatus[0] = "CNF: Confirmed track";
                            }
                            else
                            {
                                trackstatus[0] = "CNF: Track in initialisation phase";
                            }
                            if (eightbits[1] == 0)
                            {
                                trackstatus[1] = "TRE: Default";
                            }
                            else
                            {
                                trackstatus[1] = "TRE: Last report for a track";
                            }
                            if (eightbits[2] == 0 && eightbits[3] == 0)
                            {
                                trackstatus[2] = "CST: No extraploation";
                            }
                            else if (eightbits[2] == 0 && eightbits[3] == 1)
                            {
                                trackstatus[2] = "CST: Predictable extrapolation due to sensor refresh period (see Note)";
                            }
                            else if (eightbits[2] == 1 && eightbits[3] == 0)
                            {
                                trackstatus[2] = "CST: Predictable extrapolation in masked area";
                            }
                            else if (eightbits[2] == 1 && eightbits[3] == 1)
                            {
                                trackstatus[2] = "CST: Extrapolation due to unpredictable absence of detection";
                            }
                            if (eightbits[4] == 0)
                            {
                                trackstatus[3] = "MAH: Default";
                            }
                            else
                            {
                                trackstatus[3] = "MAH: Horizontal Manouvre";
                            }
                            if (eightbits[5] == 0)
                            {
                                trackstatus[4] = "TCC: Tracking performed in 'Sensor Plane' i.e. neither start range correction nor projection was applied";
                            }
                            else
                            {
                                trackstatus[4] = "TCC: Slant range correction and suitable projection technique are used to track in a 2D.reference plane, tangential to the earth model at the Sensor Site co-cordinates";
                            }
                            if (eightbits[6] == 0)
                            {
                                trackstatus[5] = "STH: Measured position";
                            }
                            else
                            {
                                trackstatus[5] = "STH: Smoothed position";
                            }
                            if (eightbits[7] == 1)
                            {
                                for (j = 0; j < 8; j++)
                                {
                                    eightbits[7 - j] = getBit(arraymessage[byteread], j);
                                }
                                byteread++;
                                if (eightbits[0] == 0 && eightbits[1] == 0)
                                {
                                    trackstatus[6] = "TOM: Unknown type of movement";
                                }
                                else if (eightbits[0] == 0 && eightbits[1] == 1)
                                {
                                    trackstatus[6] = "TOM: Taking-off";
                                }
                                else if (eightbits[0] == 1 && eightbits[1] == 0)
                                {
                                    trackstatus[6] = "TOM: Landing";
                                }
                                else if (eightbits[0] == 1 && eightbits[1] == 1)
                                {
                                    trackstatus[6] = "TOM: Other types of movement";
                                }
                                if (eightbits[2] == 0 && eightbits[3] == 0 && eightbits[4] == 0)
                                {
                                    trackstatus[7] = "DOU: No doubt";
                                }
                                else if (eightbits[2] == 0 && eightbits[3] == 0 && eightbits[4] == 1)
                                {
                                    trackstatus[7] = "DOU: Doubtful correlation (undetermined reason)";
                                }
                                else if (eightbits[2] == 0 && eightbits[3] == 1 && eightbits[4] == 0)
                                {
                                    trackstatus[7] = "DOU: Doubtful correlation in clutter";
                                }
                                else if (eightbits[2] == 0 && eightbits[3] == 1 && eightbits[4] == 1)
                                {
                                    trackstatus[7] = "DOU: Loss of accuracy";
                                }
                                else if (eightbits[2] == 1 && eightbits[3] == 0 && eightbits[4] == 0)
                                {
                                    trackstatus[7] = "DOU: Loss of accuracy in clutter";
                                }
                                else if (eightbits[2] == 1 && eightbits[3] == 0 && eightbits[4] == 1)
                                {
                                    trackstatus[7] = "DOU: Unstable track";
                                }
                                else if (eightbits[2] == 1 && eightbits[3] == 1 && eightbits[4] == 0)
                                {
                                    trackstatus[7] = "DOU: Previously coasted";
                                }
                                if (eightbits[5] == 0 && eightbits[6] == 0)
                                {
                                    trackstatus[8] = "MSR: Merge or split indication undetermined";
                                }
                                else if (eightbits[5] == 0 && eightbits[6] == 1)
                                {
                                    trackstatus[8] = "MSR: Track merged by assocation to plot";
                                }
                                else if (eightbits[5] == 1 && eightbits[6] == 0)
                                {
                                    trackstatus[8] = "MSR: Track merged by non-association to plot";
                                }
                                else if (eightbits[5] == 1 && eightbits[6] == 1)
                                {
                                    trackstatus[8] = "MSR: Split track";
                                }
                                if (eightbits[7] == 1)
                                {
                                    eightbits[0] = getBit(arraymessage[byteread], 7);
                                    byteread++;
                                    if (eightbits[0] == 0)
                                    {
                                        trackstatus[9] = "Default";
                                    }
                                    else
                                    {
                                        trackstatus[9] = "Ghost track";
                                    }
                                }
                            }
                            break;

                        case 12:
                            // I010/060
                            twobytes[1] = arraymessage[byteread];
                            twobytes[0] = arraymessage[byteread + 1];
                            bytestogether1 = new BitArray(twobytes);
                            bytestogether1 = Reverse(bytestogether1);
                            code3A = "";
                            string letter3Acode = "";
                            if (bytestogether1[0] == false)
                            {
                                mode3Acode[0] = "Code Validated";
                            }
                            else
                            {
                                mode3Acode[0] = "Code not validated";
                            }
                            if (bytestogether1[1] == false)
                            {
                                mode3Acode[1] = "Default";
                            }
                            else
                            {
                                mode3Acode[1] = "Garbled Mode";
                            }
                            if (bytestogether1[2] == false)
                            {
                                mode3Acode[2] = "Mode-3/A code derived from the reply of the transponder";
                            }
                            else
                            {
                                mode3Acode[2] = "Mode-3/A code not extracted during the last scan";
                            }
                            int k = 4;
                            BitArray threebytesarray = new BitArray(3);
                            while (k < bytestogether1.Length)
                            {
                                threebytesarray[0] = bytestogether1[k];
                                threebytesarray[1] = bytestogether1[k + 1];
                                threebytesarray[2] = bytestogether1[k + 2];
                                letter3Acode = GetNumber3Bits(threebytesarray);
                                code3A = code3A + letter3Acode;
                                k = k + 3;
                            }
                            byteread = byteread + 2;
                            break;
                        case 13:
                            // I010/220
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
                                targetaddress = targetaddress + stringbits.ToUpper();
                                positionfourbits = 0;
                                fourbits[0] = 0;
                            }
                            byteread = byteread + 3;
                            break;
                        case 14:
                            // I010/245 Target Identification
                            octet = getOctet(arraymessage[byteread]);

                            if (octet[0] == false && octet[1] == false)
                            {
                                sti = "Callsign or registration downlinked from transponder";
                            }
                            else if (octet[0] == false && octet[1] == true)
                            {
                                sti = "Callsign not downlinked from transponder";
                            }
                            else if (octet[0] == true && octet[1] == false)
                            {
                                sti = "Registration not downlinked from transponder";
                            }

                            bool[] totalCharactersBits = new bool[48];
                            getOctet(arraymessage[byteread + 1]).CopyTo(totalCharactersBits, 0);
                            getOctet(arraymessage[byteread + 2]).CopyTo(totalCharactersBits, 8);
                            getOctet(arraymessage[byteread + 3]).CopyTo(totalCharactersBits, 16);
                            getOctet(arraymessage[byteread + 4]).CopyTo(totalCharactersBits, 24);
                            getOctet(arraymessage[byteread + 5]).CopyTo(totalCharactersBits, 32);
                            getOctet(arraymessage[byteread + 6]).CopyTo(totalCharactersBits, 40);

                            targetIdentification = "";

                            int numchar = 0;
                            while (numchar < 48)
                            {
                                bool[] char1 = new bool[6];
                                Array.Copy(totalCharactersBits, numchar, char1, 0, 6);
                                string character = "";

                                if (char1.SequenceEqual(new bool[] { false, false, false, false, false, true }))
                                {
                                    character = "A";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, false, false, true, false }))
                                {
                                    character = "B";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, false, false, true, true }))
                                {
                                    character = "C";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, false, true, false, false }))
                                {
                                    character = "D";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, false, true, false, true }))
                                {
                                    character = "E";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, false, true, true, false }))
                                {
                                    character = "F";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, false, true, true, true }))
                                {
                                    character = "G";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, true, false, false, false }))
                                {
                                    character = "H";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, true, false, false, true }))
                                {
                                    character = "I";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, true, false, true, false }))
                                {
                                    character = "J";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, true, false, true, true }))
                                {
                                    character = "K";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, true, true, false, false }))
                                {
                                    character = "L";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, true, true, false, true }))
                                {
                                    character = "M";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, true, true, true, false }))
                                {
                                    character = "N";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, false, true, true, true, true }))
                                {
                                    character = "O";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, false, false, false, false }))
                                {
                                    character = "P";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, false, false, false, true }))
                                {
                                    character = "Q";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, false, false, true, false }))
                                {
                                    character = "R";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, false, false, true, true }))
                                {
                                    character = "S";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, false, true, false, false }))
                                {
                                    character = "T";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, false, true, false, true }))
                                {
                                    character = "U";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, false, true, true, false }))
                                {
                                    character = "V";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, false, true, true, true }))
                                {
                                    character = "W";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, true, false, false, false }))
                                {
                                    character = "X";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, true, false, false, true }))
                                {
                                    character = "Y";
                                }
                                else if (char1.SequenceEqual(new bool[] { false, true, true, false, true, false }))
                                {
                                    character = "Z";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, false, false, false, false, false }))
                                {
                                    character = " ";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, false, false, false, false }))
                                {
                                    character = "0";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, false, false, false, true }))
                                {
                                    character = "1";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, false, false, true, false }))
                                {
                                    character = "2";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, false, false, true, true }))
                                {
                                    character = "3";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, false, true, false, false }))
                                {
                                    character = "4";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, false, true, false, true }))
                                {
                                    character = "5";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, false, true, true, false }))
                                {
                                    character = "6";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, false, true, true, true }))
                                {
                                    character = "7";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, true, false, false, false }))
                                {
                                    character = "8";
                                }
                                else if (char1.SequenceEqual(new bool[] { true, true, true, false, false, true }))
                                {
                                    character = "9";
                                }

                                targetIdentification = targetIdentification + character;

                                numchar = numchar + 6;
                            }

                            byteread = byteread + 7;
                            break;
                        case 16:
                            // I010/250
                            break;
                        case 17:
                            // I010/300
                            if (arraymessage[byteread] == 0)
                            {
                                this.vfi = "Unknown";
                            }
                            else if (arraymessage[byteread] == 1)
                            {
                                this.vfi = "ATC equipment maintenance";
                            }
                            else if (arraymessage[byteread] == 2)
                            {
                                this.vfi = "Airport maintenance";
                            }
                            else if (arraymessage[byteread] == 3)
                            {
                                this.vfi = "Fire";
                            }
                            else if (arraymessage[byteread] == 4)
                            {
                                this.vfi = "Bird scarer";
                            }
                            else if (arraymessage[byteread] == 5)
                            {
                                this.vfi = "Snow plough";
                            }
                            else if (arraymessage[byteread] == 6)
                            {
                                this.vfi = "Runway sweeper";
                            }
                            else if (arraymessage[byteread] == 7)
                            {
                                this.vfi = "Emergency";
                            }
                            else if (arraymessage[byteread] == 8)
                            {
                                this.vfi = "Police";
                            }
                            else if (arraymessage[byteread] == 9)
                            {
                                this.vfi = "Bus";
                            }
                            else if (arraymessage[byteread] == 10)
                            {
                                this.vfi = "Tug (push/tow)";
                            }
                            else if (arraymessage[byteread] == 11)
                            {
                                this.vfi = "Grass cutter";
                            }
                            else if (arraymessage[byteread] == 12)
                            {
                                this.vfi = "Fuel";
                            }
                            else if (arraymessage[byteread] == 13)
                            {
                                this.vfi = "Baggage";
                            }
                            else if (arraymessage[byteread] == 14)
                            {
                                this.vfi = "Catering";
                            }
                            else if (arraymessage[byteread] == 15)
                            {
                                this.vfi = "Aircraft maintenance";
                            }
                            else
                            {
                                this.vfi = "Flyco (follow me)";
                            }
                            byteread++;
                            break;
                        case 18:
                            // 17 I010/090 Flight Level in Binary Representation
                            octet = getOctet(arraymessage[byteread]);
                            this.v = octet[0] ? "Code not validated" : "Code validated";
                            this.g = octet[1] ? "Garbled code" : "Default";

                            BitArray flbits = new BitArray(new bool[] { octet[7], octet[6], octet[5], octet[4], octet[3], octet[2], false, false });
                            byte[] fl1 = new byte[1];
                            flbits.CopyTo(fl1, 0);

                            this.FL = getInt32FromBytes(0, 0, fl1[0], arraymessage[byteread + 1]) / 4;

                            byteread = byteread + 2;
                            break;

                        case 19:
                            // I010/091
                            twobytes[1] = arraymessage[byteread];
                            twobytes[0] = arraymessage[byteread + 1];
                            bytestogether1 = new BitArray(twobytes);
                            bytestogether1 = Reverse(bytestogether1);
                            if (bytestogether1[0] == true)
                            {
                                bytestogether1 = complement2(bytestogether1);
                            }
                            height = 0;
                            for (j = 1; j < bytestogether1.Length; j++)
                            {
                                height = height + Math.Pow(2, 14 - j);
                            }
                            height = height * 6.25;
                            byteread = byteread + 2;
                            break;
                        case 20:
                            // I010/270
                            for (j = 1; j < 8; j++)
                            {
                                eightbits[7 - j] = getBit(arraymessage[byteread], j);
                                if (eightbits[7 - j] == 1)
                                {
                                    length = length + Math.Pow(2, j - 1);
                                }
                            }
                            eightbits[7] = getBit(arraymessage[byteread], 0); //bit utilizado para conocer si hay extent o no
                            byteread++;
                            if (eightbits[7] == 1)
                            {
                                for (j = 1; j < 8; j++)
                                {
                                    eightbits[7 - j] = getBit(arraymessage[byteread], j);
                                    if (eightbits[7 - j] == 1)
                                    {
                                        orientation = orientation + Math.Pow(2, j - 1);
                                    }
                                }
                                eightbits[7] = getBit(arraymessage[byteread], 0);
                                byteread++;
                                orientation = (orientation * 360) / 128;
                                if (eightbits[7] == 1)
                                {
                                    for (j = 1; j < 8; j++)
                                    {
                                        eightbits[7 - j] = getBit(arraymessage[byteread], j);
                                        if (eightbits[7 - j] == 1)
                                        {
                                            width = width + Math.Pow(2, j - 1);
                                        }
                                    }
                                    byteread++;
                                }
                            }
                            break;
                        case 21:
                            // I010/550
                            for (j = 0; j < 8; j++)
                            {
                                eightbits[7 - j] = getBit(arraymessage[byteread], j);
                            }
                            if (eightbits[0] == 0 && eightbits[1] == 0)
                            {
                                systemstatus[0] = "Operational Release Status of the System(NOGO): Operational";
                            }
                            else if (eightbits[0] == 0 && eightbits[1] == 1)
                            {
                                systemstatus[0] = "Operational Release Status of the System(NOGO): Degraded";
                            }
                            else if (eightbits[0] == 1 && eightbits[1] == 0)
                            {
                                systemstatus[0] = "Operational Release Status of the System(NOGO): NOGO";
                            }
                            if (eightbits[2] == 0)
                            {
                                systemstatus[1] = "Overload indicator: No overload";
                            }
                            else
                            {
                                systemstatus[1] = "Overload indicator: Overload";
                            }
                            if (eightbits[3] == 0)
                            {
                                systemstatus[2] = "Time Source Validity: valid";
                            }
                            else
                            {
                                systemstatus[2] = "Time Source Validity: invalid";
                            }
                            if (eightbits[4] == 0)
                            {
                                systemstatus[3] = "DIV: Normal Operation";
                            }
                            else
                            {
                                systemstatus[3] = "DIV: Diversity degraded";
                            }
                            if (eightbits[5] == 0)
                            {
                                systemstatus[4] = "TTF: Test Target Operative";
                            }
                            else
                            {
                                systemstatus[4] = "TTF: Test Target Failure";
                            }
                            byteread++;
                            break;
                        case 22:
                            // I010/310
                            for (j = 0; j < 8; j++)
                            {
                                eightbits[7 - j] = getBit(arraymessage[byteread], j);
                            }
                            if (eightbits[0] == 1)
                            {
                                pre_programmed_message[0] = "Default";
                            }
                            else
                            {
                                pre_programmed_message[0] = "In Trouble";
                            }
                            if (eightbits[5] == 0 && eightbits[6] == 0 && eightbits[7] == 1)
                            {
                                pre_programmed_message[1] = "Towing aircraft";
                            }
                            else if (eightbits[5] == 0 && eightbits[6] == 1 && eightbits[7] == 0)
                            {
                                pre_programmed_message[1] = "“Follow me” operation";
                            }
                            else if (eightbits[5] == 0 && eightbits[6] == 1 && eightbits[7] == 1)
                            {
                                pre_programmed_message[1] = "Runway check";
                            }
                            else if (eightbits[5] == 1 && eightbits[6] == 0 && eightbits[7] == 0)
                            {
                                pre_programmed_message[1] = "Emergency operation (fire, medical…)";
                            }
                            else if (eightbits[5] == 1 && eightbits[6] == 0 && eightbits[7] == 1)
                            {
                                pre_programmed_message[1] = "Work in progress (maintenance, birds scarer,sweepers…)";
                            }
                            byteread++;
                            break;
                        case 24:
                            // I010/500
                            x_standard_deviation = arraymessage[byteread] * 0.25;
                            y_standard_deviation = arraymessage[byteread + 1] * 0.25;
                            covariance = 0;
                            twobytes[1] = arraymessage[byteread + 2];
                            twobytes[0] = arraymessage[byteread + 4];
                            bytestogether1 = new BitArray(twobytes);
                            bytestogether1 = Reverse(bytestogether1);
                            bytestogether1 = complement2(bytestogether1);
                            for (int r = 0; r < bytestogether1.Length; r++)
                            {
                                if (bytestogether1[r] == true)
                                {
                                    covariance = covariance + Math.Pow(2, 15 - r);
                                }
                            }
                            covariance = covariance * 0.25;
                            byteread = byteread + 4;
                            break;
                        case 25:
                            // I010/280
                            byteread = byteread + 2;
                            break;
                        case 26:
                            // I010/131
                            amplitudeOfPrimaryPlot = arraymessage[byteread];
                            byteread = byteread + 1;
                            break;
                        case 27:
                            // I010/210
                            complement2done = false;
                            for (j = 0; j < 2; j++)
                            {
                                onebyte[0] = arraymessage[byteread + j];
                                simplebyte = new BitArray(onebyte);
                                simplebyte = Reverse(simplebyte);
                                onebyte1[0] = getBit(onebyte[0], 7);
                                if (onebyte1[0] == 1)
                                {
                                    complement2(simplebyte);
                                    complement2done = true;
                                }
                                if (j == 0)
                                {
                                    for (k = 0; k < simplebyte.Length; k++)
                                    {
                                        if (simplebyte[k] == true) {
                                            Ax = Ax + Math.Pow(2, simplebyte.Length - 1 - k);
                                        }
                                    }
                                    if (complement2done == true)
                                    {
                                        Ax = Ax * -0.25;
                                    }
                                    else
                                    {
                                        Ax = Ax * 0.25;
                                    }
                                }
                                else
                                {
                                    for (k = 0; k < simplebyte.Length; k++)
                                    {
                                        if (simplebyte[k] == true) {
                                            Ay = Ay + Math.Pow(2, simplebyte.Length - 1 - k);
                                        }
                                    }
                                    if (complement2done == true)
                                    {
                                        Ay = Ay * -0.25;
                                    }
                                    else
                                    {
                                        Ay = Ay * 0.25;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        public string[] getInformation(int j)
        {
            string[] values = new string[28];
            values[0] = j.ToString();
            values[1] = "10";
            for (int k = 2; k < values.Length; k++)
            {
                values[k] = "No data";
            }
            for (int i = 0; i < UAP.Length; i++)
            {
                if (UAP[i] == 1)
                {
                    switch (i)
                    {
                        case 0:
                            values[2] = sac.ToString();
                            values[3] = sic.ToString();
                            break;
                        case 1:
                            values[4] = messageType;
                            break;
                        case 2:
                            values[5] = "Click to expand";
                            break;
                        case 3:
                            if (hores > 9)
                                values[6] = hores.ToString();
                            else
                                values[6] = "0" + hores.ToString();
                            if (minuts > 9)
                                values[6] = values[6] + ":" + minuts.ToString();
                            else
                                values[6] = values[6] + ":0" + minuts.ToString();
                            if (segons > 9)
                                values[6] = values[6] + ":" + segons.ToString();
                            else
                                values[6] = values[6] + ":0" + segons.ToString();
                            values[6] = values[6] + ":" + ms.ToString();
                            break;
                        case 4:
                            //WGS
                            break;
                        case 5:
                            values[8] = "ρ: " + rho.ToString() + "m θ: " + theta.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "º";
                            break;
                        case 6:
                            values[9] = "X: " + x.ToString() + " Y: " + y.ToString();
                            break;
                        case 8:
                            values[10] = "GS: " + groundspeed_polar_coordinates.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + "m/s TA: " + trackangle_polar_coordinates.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + "º";
                            break;
                        case 9:
                            values[11] = "Vx: " + vx_cartesian_coordinates.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + "m/s Vy: " + vy_cartesian_coordinates.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + "m/s";
                            break;
                        case 10:
                            values[12] = tracknumber.ToString();
                            break;
                        case 11:
                            values[13] = "Click to expand";
                            break;
                        case 12:
                            values[14] = code3A.ToString();
                            break;
                        case 13:
                            values[15] = targetaddress;
                            break;
                        case 14:
                            values[16] = targetIdentification;
                            break;
                        case 15:
                            break;
                        case 16:
                            //MODE S MB
                            break;
                        case 17:
                            values[18] = vfi;
                            break;
                        case 18:
                            values[19] = FL.ToString();
                            break;
                        case 19:
                            values[20] = height.ToString();
                            break;
                        case 20:
                            values[21] = "Length: " + length.ToString() + "m Orientation: " + orientation.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " Width: " + width.ToString() + "m";
                            break;
                        case 21:
                            values[22] = "Click to expand";
                            break;
                        case 22:
                            values[23] = "Click to expand";
                            break;
                        case 23:
                            break;
                        case 24:
                            values[24] = "σx = " + x_standard_deviation.ToString() + " σy = " + y_standard_deviation.ToString() + " σxy = " + covariance.ToString();
                            break;
                        case 26:
                            values[26] = amplitudeOfPrimaryPlot.ToString();
                            break;
                        case 27:
                            values[27] = "Ax: " + Ax.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "m/s^2 Ay: " + Ay.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + "m/s^2";
                            break;
                    }
                }
            }
            return values;
        }

        public double[] getLatitudeLongitudeWGS84(string sensor)
        {
            double[] radar = new double[2];
            if (sensor == "SMR")
            {
                radar[0] = 41.29561944444444;
                radar[1] = 2.09511388888888;
            }
            else
            {
                radar[0] = 41.297063888888886;
                radar[1] = 2.0784472222222226;
            }
            GeoUtils geoUtils = new GeoUtils();
            Coordinates radarCoordinates = new Coordinates(radar[0], radar[1]);
            CoordinatesWGS84 radarWGS84 = new CoordinatesWGS84(radarCoordinates.GetLatitude() * (Math.PI / 180.0), radarCoordinates.GetLongitude() * (Math.PI / 180.0));
            CoordinatesXYZ objectCartesian = new CoordinatesXYZ(x, y, 0);
            CoordinatesXYZ objectGeocentric = geoUtils.change_radar_cartesian2geocentric(radarWGS84, objectCartesian);
            CoordinatesWGS84 objectWGS84 = geoUtils.change_geocentric2geodesic(objectGeocentric);
            double[] wgs84coordinates = { objectWGS84.Lat * (180.0 / Math.PI), objectWGS84.Lon * (180.0 / Math.PI) };
            return wgs84coordinates;
        }

        public string getTypeSensor()
        {
            if (sic == 7)
                return "SMR";
            else
                return "MLAT";
        }

        public string getTargetAddress()
        {
            return this.targetaddress;
        }

        public string getTargetIdentification()
        {
            return this.targetIdentification;
        }

        public double getTrackNumber()
        {
            return this.tracknumber;
        }

        public double getFlightLevel()
        {
            return this.height;
        }

        public TimeSpan getTime()
        {
            TimeSpan timeSpan = new TimeSpan(hores, minuts, segons);
            return timeSpan;
        }

        public double getGroundSpeed()
        {
            return this.groundspeed_polar_coordinates;
        }

        public string getMessageType()
        {
            return this.messageType;
        }

        public string getTypeVehicle()
        {
            return targetReportDescriptor[9];
        }

        public string[] getClickToExpandValues(int col)
        {
            string[] val = new string[15];
            switch (col)
            {
                case (5):
                    // Target Report Descriptor
                    val = targetReportDescriptor;
                    break;

                case (13):
                    // Target Status
                    val = trackstatus;
                    break;
                case (22):
                    // System Status
                    val = systemstatus;
                    break;
                case (23):
                    val = pre_programmed_message;
                    break;
            }
            return val;
        }

        public byte getBit(byte b, int bitNumber)
        {
            int valueint = (b >> bitNumber) & 0x01;
            byte b2 = Convert.ToByte(valueint);
            return (b2);
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

        public string GetNumber3Bits(BitArray bytes)
        {
            double total = 0;
            if (bytes[0] == true)
            {
                total = total + Math.Pow(2, 2);
            }
            if(bytes[1] == true)
            {
                total = total + Math.Pow(2, 1);
            }
            if (bytes[2] == true)
            {
                total = total + Math.Pow(2, 0);
            }
            return total.ToString();
        }

        int getInt32FromBytes(byte first, byte second, byte third, byte fourth)
        {
            byte[] bytesArray = { first , second, third, fourth };
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

        public BitArray complement2xy(BitArray b)
        {
            //Complemento a 1
            for(int i = 0; i < b.Length; i++)
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
            if(b[0] == true)
            {
                b[0] = false;
                mellevoununo = true;
            }
            while (mellevoununo==true)
            {
                if (b[j] == false)
                {
                    b[j]=true;
                    mellevoununo=false;
                }
                else
                {
                    b[j]=false;
                }
                j++;
            }
            return b;
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
                b[b.Length-1] = true;
            }
            while (mellevoununo == true && j > 0)
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
