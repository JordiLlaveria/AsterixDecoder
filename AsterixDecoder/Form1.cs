using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using ClassLibrary;

namespace AsterixDecoder
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void OnClickFile(object sender, EventArgs e)
        {
            FileStream fichero = new FileStream("201002-lebl-080001_smr.ast", FileMode.Open, FileAccess.Read);

            Byte[] buffer = new byte[fichero.Length];

            fichero.Read(buffer, 0, buffer.Length);

            fichero.Close();

            byte CAT = buffer[0];
            int length = buffer[1] + buffer[2];

            List<bool> fspecList = new List<bool>();
            int i = 3;
            byte[] buffer2 = new byte[1];
            buffer2[0] = buffer[i];
            BitArray bits = new BitArray(buffer2);
            bool[] bitsArray = new bool[8];
            bits.CopyTo(bitsArray,0);
            Array.Reverse(bitsArray);
            fspecList.AddRange(bitsArray);
            int fspecLength = 0;
            while (bits[0] == true)
            {
                i++;                
                buffer2[0] = buffer[i];
                bits = new BitArray(buffer2);  
                bits.CopyTo(bitsArray,0);
                Array.Reverse(bitsArray);
                fspecList.AddRange(bitsArray);
                fspecLength++;
            }
                       

            int frn = 0;
            int pos = fspecLength + 3;
            if(CAT == 10)
            {
                CAT10 data = new CAT10();

                while (frn < fspecList.Count)
                {
                    if (fspecList[frn])
                    {
                        switch (frn)
                        {
                            case 0: // 1 I010/010 Data Source identifier

                                byte sac = buffer[pos+1];
                                byte sic = buffer[pos+2];
                                data.setD010(sac,sic);                                
                                break;

                            case 1: // 2 I010/000 Message Type

                                byte messageType = buffer[pos + 3];
                                data.setD000(messageType);
                                Console.Write("Hello World");
                                break;

                            case 2: // 3 I010/020 Target Report Descriptor
                                
                                int cont = 0;
                                bool fx = true;
                                List<byte> targetReportDescriptorList = new List<byte>();
                                while (fx)
                                {
                                    byte[] octet = new byte[1];
                                    octet[0] = buffer[pos + 4];
                                    BitArray octetBits = new BitArray(octet);
                                    bool[] octetArray = new bool[8];
                                    octetBits.CopyTo(octetArray, 0);
                                    Array.Reverse(octetArray);

                                    switch (cont)
                                    {
                                        case 0:

                                            BitArray typBits = new BitArray(new bool[] { octetArray[2], octetArray[1], octetArray[0], false, false, false, false, false  });
                                            byte[] typ = new byte[1];
                                            typBits.CopyTo(typ, 0);
                                            targetReportDescriptorList.Add(typ[0]);

                                            int a = 3;
                                            while (a<7)
                                            { 
                                                byte res = octetArray[a] ? (byte) 1 : (byte) 0;
                                                targetReportDescriptorList.Add(res);                                                
                                                a++;
                                            }

                                            fx = octetArray[7];

                                            break;

                                        case 1:

                                            targetReportDescriptorList.Add(octetArray[0] ? (byte)1 : (byte)0);
                                            targetReportDescriptorList.Add(octetArray[1] ? (byte)1 : (byte)0);
                                            targetReportDescriptorList.Add(octetArray[2] ? (byte)1 : (byte)0);

                                            BitArray lopBits = new BitArray(new bool[] { octetArray[4], octetArray[3], false, false, false, false, false, false });
                                            byte[] lop = new byte[1];
                                            lopBits.CopyTo(lop, 0);
                                            targetReportDescriptorList.Add(lop[0]);

                                            BitArray totBits = new BitArray(new bool[] { octetArray[6], octetArray[5], false, false, false, false, false, false });
                                            byte[] tot = new byte[1];
                                            totBits.CopyTo(tot, 0);
                                            targetReportDescriptorList.Add(tot[0]);

                                            fx = octetArray[7];
                                            break;

                                        case 2:

                                            targetReportDescriptorList.Add(octetArray[0] ? (byte)1 : (byte)0);

                                            fx = octetArray[7];
                                            break;
                                    }
                                    cont++;
                                }

                                byte[] targetReportDescriptor = targetReportDescriptorList.ToArray();
                                data.setD020(targetReportDescriptor);
                                break;

                            case 3: // 4 I010/140 Time of the Day

                                byte[] timeOfTheDayArray = { 0, buffer[pos+5], buffer[pos+6], buffer[pos+7] };
                                if (BitConverter.IsLittleEndian) {
                                    Array.Reverse(timeOfTheDayArray);
                                }

                                double timeOfTheDay = BitConverter.ToInt32(timeOfTheDayArray, 0) / (double) 128; // segons
                                data.setD140(timeOfTheDay);
                                break;

                            case 4:

                                break;

                            case 5: // 6 I010/040 Measured Position in Polar Co-ordinates

                                byte[] rhoArray = { 0, 0, buffer[pos + 8], buffer[pos + 9] };
                                byte[] thetaArray = { 0, 0, buffer[pos + 10], buffer[pos + 11] };
                                if (BitConverter.IsLittleEndian)
                                {
                                    Array.Reverse(rhoArray);
                                    Array.Reverse(thetaArray);
                                }

                                double rho = BitConverter.ToInt32(rhoArray, 0);
                                double theta = BitConverter.ToInt32(thetaArray, 0) * 0.0055;
                                data.setD040(rho, theta);
                                break;

                            case 7: // 7 I010//042 Position in Cartesian Co-ordinates

                                byte[] xArray = { 0, 0, buffer[pos + 12], buffer[pos + 13] };
                                byte[] yArray = { 0, 0, buffer[pos + 14], buffer[pos + 15] };
                                if (BitConverter.IsLittleEndian)
                                {
                                    Array.Reverse(xArray);
                                    Array.Reverse(yArray);
                                }

                                double x = BitConverter.ToInt32(xArray, 0);
                                double y = BitConverter.ToInt32(yArray, 0);

                                data.setD042(x, y);

                                break;

                        }
                    }
                    frn++;
                }
            }
            
            

            Console.Write("Hello World");

        }
    }

}
