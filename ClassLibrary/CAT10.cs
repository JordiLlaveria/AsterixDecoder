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

        public CAT10()
        {
            D010 = new byte[2];
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
    }
}
