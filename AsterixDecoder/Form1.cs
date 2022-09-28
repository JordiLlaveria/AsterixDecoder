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
                        }
                    }
                    frn++;
                }
            }
            
            

            Console.Write("Hello World");

        }
    }

}
