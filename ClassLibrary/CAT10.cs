using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CAT10
    {
        byte[] D010; // Data Source Identifier
        byte D000; // Message type
        byte[] D020; // Target Report Descriptor
        double D140; // Time of the day
        double[] D040; // Measured Position in Polar Co-ordinates
        double[] D042; //Position in Cartesian Co-ordinates 
        

        public CAT10()
        {
            D010 = new byte[2];
            D040 = new double[2];
            D042 = new double[2];
        }

        public void setD010(byte sac, byte sic)
        {
            D010[0] = sac;
            D010[1] = sic;
        }

        public byte[] getD010()
        {
            return D010;
        }

        public void setD000(byte messageType)
        {
            D000 = messageType;
        }

        public byte getD000()
        {
            return D000;
        }

        public void setD020(byte[] targetReportDescriptor)
        {
            D020 = targetReportDescriptor;
        }

        public byte[] getD020()
        {
            return D020;
        }

        public void setD140(double timeOfTheDay)
        {
            D140 = timeOfTheDay;
        }

        public double getD140()
        {
            return D140;
        }

        public void setD040(double rho, double theta)
        {
            D040[0] = rho;
            D040[1] = theta;
        }

        public double[] getD040()
        {
            return D040;
        }

        public void setD042(double x, double y)
        {
            D042[0] = x;
            D042[1] = y;
        }

        public double[] getD042()
        {
            return D042;
        }
    }
}
