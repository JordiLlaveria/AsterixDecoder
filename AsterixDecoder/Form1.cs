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

            //for (int i = 0; i & lt; buffer.Length; i++)
            //{
            //    Console.WriteLine(buffer[i]);
            //}

            fichero.Close();
        }
    }

}
