using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // 4 I010/140 Time of the Day
        double timeOfTheDay;

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

        //13 
        string targetaddress;

        //20 (19 real) I010/270 Target Size and Orientation
        double length; // m
        double orientation;
        double width; // m

        //21 (20 real)
        string[] systemstatus = new string[5];

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
        public CAT10(byte[] arraymessage)
        {
            int byteread = 3;
            int j = 0;
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

            for (int i = 0; i < UAP.Length; i++)
            {
                if (UAP[i] == 1)
                {
                    switch(i)
                    {
                        case 0: 
                            // 1 I010/010
                            sac = arraymessage[byteread + 1];
                            sic = arraymessage[byteread + 2];
                            break;

                        case 1:
                            // 2 I010/000
                            byte num = arraymessage[byteread + 3];
                            if (num == 1)
                            {
                                messageType = "Target Report";
                            }
                            else if(num == 2)
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
                            break;

                        case 2:
                            // 3 I010/020 Target Report Descriptor
                            int cont = 0;
                            bool fx = true;

                            while (fx)
                            {
                                bool[] octetArray = getOctet(arraymessage[byteread + 4]);                                

                                switch (cont)
                                {
                                    case 0:
                                        // TYP
                                        if (octetArray[0] == false && octetArray[1] == false && octetArray[2] == false)
                                        {
                                            typ = "SSR multilateration";
                                        }
                                        else if (octetArray[0] == false && octetArray[1] == false && octetArray[2] == true)
                                        {
                                            typ = "Mode S multilateration";
                                        }
                                        else if (octetArray[0] == false && octetArray[1] == true && octetArray[2] == false)
                                        {
                                            typ = "ADS-B";
                                        }
                                        else if (octetArray[0] == false && octetArray[1] == true && octetArray[2] == true)
                                        {
                                            typ = "PSR";
                                        }
                                        else if (octetArray[0] == true && octetArray[1] == false && octetArray[2] == false)
                                        {
                                            typ = "Magnetic Loop System";
                                        }
                                        else if (octetArray[0] == true && octetArray[1] == false && octetArray[2] == true)
                                        {
                                            typ = "HF multilateration";
                                        }
                                        else if (octetArray[0] == true && octetArray[1] == true && octetArray[2] == false)
                                        {
                                            typ = "Not defined";
                                        }
                                        else
                                        {
                                            typ = "Other types";
                                        }

                                        drc = octetArray[3] ? "Differential Correction (ADS-B)" : "No Differential Correction (ADS_B)";
                                        chn = octetArray[4] ? "Chain 2" : "Chain 1";
                                        gbs = octetArray[5] ? "Transponder Ground bit set" : "Transponder Ground bit not set";
                                        crt = octetArray[6] ? "Corrupted replies in multilateration" : "No Corrupted reply in multilateration";

                                        fx = octetArray[7];

                                        break;

                                    case 1:

                                        sim = octetArray[0] ? "Simulated target report" : "Actual target report";
                                        tst = octetArray[1] ? "Test Target" : "Default";
                                        rab = octetArray[2] ? "Report from field monitor (fixed transponder)" : "Report from target monitor";

                                        if (octetArray[3] == false && octetArray[4] == false)
                                        {
                                            this.lop = "Undetermined";
                                        }
                                        else if (octetArray[3] == false && octetArray[4] == true)
                                        {
                                            this.lop = "Loop start";
                                        }
                                        else if (octetArray[3] == true && octetArray[4] == false)
                                        {
                                            this.lop = "Loop finish";
                                        }
                                        else
                                        {
                                            this.lop = "Error";
                                        }

                                        if (octetArray[5] == false && octetArray[6] == false)
                                        {
                                            this.tot = "Undetermined";
                                        }
                                        else if (octetArray[5] == false && octetArray[6] == true)
                                        {
                                            this.tot = "Aircraft";
                                        }
                                        else if (octetArray[5] == true && octetArray[6] == false)
                                        {
                                            this.tot = "Ground vehicle";
                                        }
                                        else
                                        {
                                            this.tot = "Helicopter";
                                        }                                       

                                        fx = octetArray[7];
                                        break;

                                    case 2:

                                        spi = octetArray[0] ? "Special Position Identification" : "Absence of SPI";

                                        fx = octetArray[7];
                                        break;
                                }
                                cont++;
                            }

                            break;

                        case 3:
                            // 4 I010/140 Time of the Day
                            this.timeOfTheDay= getInt32FromBytes(0, arraymessage[byteread + 5], arraymessage[byteread + 6], arraymessage[byteread + 7]) / (double)128; // segons
                            break;

                        case 4:

                            break;

                        case 5:
                            // 6 I010/040 Measured Position in Polar Co-ordinates                            
                            rho = getInt32FromBytes(0, 0, arraymessage[byteread + 8], arraymessage[byteread + 9]);
                            theta = getInt32FromBytes(0, 0, arraymessage[byteread + 10], arraymessage[byteread + 11]) * 0.0055;
                            break;

                        case 6:
                            // 7 I010//042 Position in Cartesian Co-ordinates
                            bool[] octet = getOctet(arraymessage[byteread + 12]);
                            bool x2Complement = octet[0];
                            octet = getOctet(arraymessage[byteread + 14]);
                            bool y2complement = octet[0];

                            byte[] x1Array = new byte[1];
                            byte[] x2Array = new byte[1];

                            byte[] y1Array = new byte[1];
                            byte[] y2Array = new byte[1];

                            if (x2Complement)
                            {
                                onebyte[0] = arraymessage[byteread+12];
                                BitArray xbits1 = new BitArray(onebyte);
                                BitArray xbits1Complement = complement2(xbits1);
                                onebyte[0] = arraymessage[byteread+13];
                                BitArray xbits2 = new BitArray(onebyte);
                                BitArray xbits2Complement = complement2(xbits2);
                                
                                xbits1Complement.CopyTo(x1Array,0);                                
                                xbits2Complement.CopyTo(x2Array,0);

                                x = getInt32FromBytes(0, 0, x1Array[0], x2Array[0]);
                                x = x*(-1);
                            }
                            else
                            {
                                x1Array[0] = arraymessage[byteread+12];
                                x2Array[0] = arraymessage[byteread+13];
                                x = getInt32FromBytes(0, 0, x1Array[0], x2Array[0]);                                
                            }
                            if (y2complement)
                            {
                                onebyte[0] = arraymessage[byteread+14];
                                BitArray ybits1 = new BitArray(onebyte);
                                BitArray ybits1Complement = complement2(ybits1);
                                onebyte[0] = arraymessage[byteread+15];
                                BitArray ybits2 = new BitArray(onebyte);
                                BitArray ybits2Complement = complement2(ybits2);
                                
                                ybits1Complement.CopyTo(y1Array,0);                                
                                ybits2Complement.CopyTo(y2Array,0);
                                y = getInt32FromBytes(0, 0, y1Array[0], y2Array[0]);
                                y = y*(-1);
                            }
                            else
                            {
                                y1Array[0] = arraymessage[byteread+14];
                                y2Array[0] = arraymessage[byteread+15];
                                y = getInt32FromBytes(0, 0, y1Array[0], y2Array[0]);
                            }                            

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
                                    for (int k = 0; k < bytestogether1.Length; k++)
                                    {
                                        if (bytestogether1[k] == true) { 
                                            groundspeed_polar_coordinates = groundspeed_polar_coordinates + Math.Pow(2,k);
                                        }
                                    }
                                    groundspeed_polar_coordinates = groundspeed_polar_coordinates * 0.22;
                                }
                                else
                                {
                                    for (int k = 0; k < bytestogether1.Length; k++)
                                    {
                                        if (bytestogether1[k] == true) { 
                                            trackangle_polar_coordinates = trackangle_polar_coordinates + Math.Pow(2,k);
                                        }
                                    }
                                    trackangle_polar_coordinates = trackangle_polar_coordinates * (360/Math.Pow(2,16));
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
                                onebyte[0] = getBit(twobytes[1], 7);
                                if (onebyte[0] == 1)
                                {
                                    complement2(bytestogether1);
                                    complement2done = true;
                                }
                                if (j == 0)
                                {
                                    for (int k = 0; k < bytestogether1.Length; k++)
                                    {
                                        if (bytestogether1[k] == true) { 
                                            vx_cartesian_coordinates = vx_cartesian_coordinates + Math.Pow(2,k);
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
                                    for (int k = 0; k < bytestogether1.Length; k++)
                                    {
                                        if (bytestogether1[k] == true) { 
                                            vy_cartesian_coordinates = vy_cartesian_coordinates + Math.Pow(2,k);
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
                            for (int k = 0; k < bytestogether1.Length; k++)
                            {
                                if (bytestogether1[k] == true) 
                                { 
                                    tracknumber = tracknumber + Math.Pow(2,k);
                                }
                            }
                            byteread = byteread + 2;
                            break;
                        case 11:
                            // I010/170
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
                            }
                            byteread++;
                            if (eightbits[0] == 0)
                            {
                                trackstatus[0]= "CNF: Confirmed track";
                            }
                            else
                            {
                                trackstatus[0]= "CNF: Track in initialisation phase";
                            }
                            if (eightbits[1] == 0)
                            {
                                trackstatus[1]= "TRE: Default";
                            }
                            else
                            {
                                trackstatus[1]= "TRE: Last report for a track";
                            }
                            if (eightbits[2] == 0 && eightbits[3] == 0)
                            {
                                trackstatus[2]= "CST: No extraploation";
                            }
                            else if(eightbits[2] == 0 && eightbits[3] == 1)
                            {
                                trackstatus[2]= "CST: Predictable extrapolation due to sensor refresh period (see Note)";
                            }
                            else if(eightbits[2] == 1 && eightbits[3] == 0)
                            {
                                trackstatus[2]= "CST: Predictable extrapolation in masked area";
                            }
                            else if(eightbits[2] == 1 && eightbits[3] == 1)
                            {
                                trackstatus[2]= "CST: Extrapolation due to unpredictable absence of detection";
                            }
                            if (eightbits[4] == 0)
                            {
                                trackstatus[3]= "MAH: Default";
                            }
                            else 
                            {
                                trackstatus[3] = "MAH: Horizontal Manouvre";
                            }
                            if (eightbits[5] == 0) 
                            {
                                trackstatus[4] = "TCC: Tracking performed in 'Sensor Plane', i.e. neither start range correction nor projection was applied";
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
                                for(j = 0; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread], j);
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
                                    if(eightbits[0] == 0)
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
                                targetaddress = targetaddress + stringbits;
                                positionfourbits = 0;
                                fourbits[0] = 0;
                            }
                            break;
                        case 14:
                            // I010/245
                            break;
                        case 16:
                            // I010/250
                            break;
                        case 17:
                            // I010/300
                            break;
                        case 18:
                            // I010/090
                            break;
                        case 19:
                            // I010/091
                            break;
                        case 20:
                            // I010/270
                            for(j = 1; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
                                if(eightbits[7-j] == 1)
                                {
                                    length = length + Math.Pow(2,j-1);
                                }
                            }
                            eightbits[7] = getBit(arraymessage[byteread], 0); //bit utilizado para conocer si hay extent o no
                            byteread++;
                            if (eightbits[7] == 1)
                            {
                                for(j = 1; j < 8; j++) 
                                {
                                    eightbits[7-j] = getBit(arraymessage[byteread], j);
                                    if(eightbits[7-j] == 1)
                                    {
                                        orientation = orientation + Math.Pow(2,j-1);
                                    }
                                }
                                eightbits[7] = getBit(arraymessage[byteread], 0);
                                byteread++;
                                orientation = (orientation * 360) / 128;
                                if (eightbits[7] == 1)
                                {
                                    for(j = 1; j < 8; j++) 
                                    {
                                        eightbits[7-j] = getBit(arraymessage[byteread], j);
                                        if(eightbits[7-j] == 1)
                                        {
                                            width = width + Math.Pow(2,j-1);
                                        }
                                    }
                                    byteread++;
                                }
                            }
                            break;
                        case 21:
                            // I010/550
                            for(j = 0; j < 8; j++) 
                            {
                                eightbits[7-j] = getBit(arraymessage[byteread], j);
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
                            break;
                        case 24:
                            // I010/500
                            break;
                        case 25:
                            // I010/280
                            break;
                        case 26:
                            // I010/131
                            break;
                        case 27:
                            // I010/210
                            complement2done = false;
                            for (j=0; j < 2; j++)
                            {
                                onebyte[0] = arraymessage[byteread + j];
                                simplebyte = new BitArray(onebyte);
                                onebyte1[0] = getBit(onebyte[0], 7);
                                if (onebyte1[0] == 1)
                                {
                                    complement2(simplebyte);
                                    complement2done = true;
                                }
                                if (j == 0)
                                {
                                    for (int k = 0; k < simplebyte.Length; k++)
                                    {
                                        if (simplebyte[k] == true) { 
                                            Ax = Ax + Math.Pow(2,k);
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
                                    for (int k = 0; k < simplebyte.Length; k++)
                                    {
                                        if (simplebyte[k] == true) { 
                                            Ay = Ay + Math.Pow(2,k);
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

        public BitArray complement2(BitArray b)
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
    }
}
