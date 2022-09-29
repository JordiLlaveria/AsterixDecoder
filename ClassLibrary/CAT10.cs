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

        //8
        double groundspeed_polar_coordinates; // kt
        double trackangle_polar_coordinates; // deg
        //9
        double vx_cartesian_coordinates; // m/s
        double vy_cartesian_coordinates; // m/s
        //10
        double tracknumber;
        //11
        string[] trackstatus = new string[10];
        //20 (19 real)
        double length; // m
        double orientation;
        double width; // m
        //27 (25 real)
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
                            break;
                        case 1:
                            break;
                        case 8:
                            // I010/200
                            byteread = 22;
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
                                trackstatus[0]= "Confirmed track";
                            }
                            else
                            {
                                trackstatus[0]= "Track in initialisation phase";
                            }
                            if (eightbits[1] == 0)
                            {
                                trackstatus[1]= "Default";
                            }
                            else
                            {
                                trackstatus[1]= "Last report for a track";
                            }
                            if (eightbits[2] == 0 && eightbits[3] == 0)
                            {
                                trackstatus[2]= "No extraploation";
                            }
                            else if(eightbits[2] == 0 && eightbits[3] == 1)
                            {
                                trackstatus[2]= "Predictable extrapolation due to sensor refresh period (see Note)";
                            }
                            else if(eightbits[2] == 1 && eightbits[3] == 0)
                            {
                                trackstatus[2]= "Predictable extrapolation in masked area";
                            }
                            else if(eightbits[2] == 1 && eightbits[3] == 1)
                            {
                                trackstatus[2]= "Extrapolation due to unpredictable absence of detection";
                            }
                            if (eightbits[4] == 0)
                            {
                                trackstatus[3]= "Default";
                            }
                            else 
                            {
                                trackstatus[3] = "Horizontal Manouvre";
                            }
                            if (eightbits[5] == 0) 
                            {
                                trackstatus[4] = "Tracking performed in 'Sensor Plane', i.e. neither start range correction nor projection was applied";
                            }
                            else 
                            {
                                trackstatus[4] = "Slant range correction and suitable projection technique are used to track in a 2D.reference plane, tangential to the earth model at the Sensor Site co-cordinates";
                            }
                            if (eightbits[6] == 0) 
                            {
                                trackstatus[5] = "Measured position";
                            }
                            else 
                            {
                                trackstatus[5] = "Smoothed position";
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
                                    trackstatus[6] = "Unknown type of movement";
                                }
                                else if (eightbits[0] == 0 && eightbits[1] == 1)
                                {
                                    trackstatus[6] = "Taking-off";
                                }
                                else if (eightbits[0] == 1 && eightbits[1] == 0)
                                {
                                    trackstatus[6] = "Landing";
                                }
                                else if (eightbits[0] == 1 && eightbits[1] == 1)
                                {
                                    trackstatus[6] = "Other types of movement";
                                }
                                if (eightbits[2] == 0 && eightbits[3] == 0 && eightbits[4] == 0)
                                {
                                    trackstatus[7] = "No doubt";
                                }
                                else if (eightbits[2] == 0 && eightbits[3] == 0 && eightbits[4] == 1)
                                {
                                    trackstatus[7] = "Doubtful correlation (undetermined reason)";
                                }
                                else if (eightbits[2] == 0 && eightbits[3] == 1 && eightbits[4] == 0)
                                {
                                    trackstatus[7] = "Doubtful correlation in clutter";
                                }
                                else if (eightbits[2] == 0 && eightbits[3] == 1 && eightbits[4] == 1)
                                {
                                    trackstatus[7] = "Loss of accuracy";
                                }
                                else if (eightbits[2] == 1 && eightbits[3] == 0 && eightbits[4] == 0)
                                {
                                    trackstatus[7] = "Loss of accuracy in clutter";
                                }
                                else if (eightbits[2] == 1 && eightbits[3] == 0 && eightbits[4] == 1)
                                {
                                    trackstatus[7] = "Unstable track";
                                }
                                else if (eightbits[2] == 1 && eightbits[3] == 1 && eightbits[4] == 0)
                                {
                                    trackstatus[7] = "Previously coasted";
                                }
                                if (eightbits[5] == 0 && eightbits[6] == 0)
                                {
                                    trackstatus[8] = "Merge or split indication undetermined";
                                }
                                else if (eightbits[5] == 0 && eightbits[6] == 1)
                                {
                                    trackstatus[8] = "Track merged by assocation to plot";
                                }
                                else if (eightbits[5] == 1 && eightbits[6] == 0)
                                {
                                    trackstatus[8] = "Track merged by non-association to plot";
                                }
                                else if (eightbits[5] == 1 && eightbits[6] == 1)
                                {
                                    trackstatus[8] = "Split track";
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

        byte getBit(byte b, int bitNumber)
        {
            int valueint = (b >> bitNumber) & 0x01;
            byte b2 = Convert.ToByte(valueint);
            return (b2);
        }

        BitArray complement2(BitArray b)
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
           /*
            bool complement2done = false;
            int j = 0;
            while (complement2done == false && j < b.Length)
            {
                if (b[j] == true)
                {
                    b[j]=false;
                }
                else
                {
                    if (j == 0)
                    {
                        b[j]=true;
                    }
                    complement2done = true;
                }
                j++;
            }
            */
            //bool complement2done = false;
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
