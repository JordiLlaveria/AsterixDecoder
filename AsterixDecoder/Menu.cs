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
using ClassLibrary;

namespace AsterixDecoder
{
    public partial class Menu : Form
    {
        AsterixFile asterixFile;
        public Menu()
        {
            InitializeComponent();
        }

        private void OnClickFile(object sender, EventArgs e)
        {
            //FileStream fichero = new FileStream("201002-lebl-080001_smr.ast", FileMode.Open, FileAccess.Read);
            //Byte[] buffer = new byte[fichero.Length];
            //fichero.Read(buffer, 0, buffer.Length);

            //string path = @"C:\Users\joana\Documents\uni\5A\Projectes en Gestió del Trànsit Aeri\Project 1\Ficheros_asterix\201002-lebl-080001_mlat.ast";

            string path = @"C:\Users\Jordi\AsterixDecoder\AsterixDecoder\bin\Debug\201002-lebl-080001_mlat.ast";

            
            asterixFile = new AsterixFile(path);

        }
    }

}
