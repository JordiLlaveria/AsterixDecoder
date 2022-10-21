using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ClassLibrary;

namespace AsterixDecoder
{
    public partial class Menu : Form
    {
        List<CAT10> CAT10list = new List<CAT10>();
        List<CAT21> CAT21list = new List<CAT21>();
        AsterixFile asterixFile;
        string file;

        public Menu()
        {
            InitializeComponent();
        }

        private void OnClickFile(object sender, EventArgs e)
        {
            Stream stream = null;
            //FileStream fichero = new FileStream("201002-lebl-080001_smr.ast", FileMode.Open, FileAccess.Read);
            //Byte[] buffer = new byte[fichero.Length];
            //fichero.Read(buffer, 0, buffer.Length);

            //string path = @"C:\Users\joana\Documents\uni\5A\Projectes en Gestió del Trànsit Aeri\Project 1\Ficheros_asterix\201002-lebl-080001_smr_mlat_adsb.ast";
            //string path = @"C:\Users\joana\Documents\uni\5A\Projectes en Gestió del Trànsit Aeri\Project 1\Ficheros_asterix\201002-lebl-080001_smr_mlat_adsb.ast";


            //string path = @"C:\Users\Jordi\AsterixDecoder\AsterixDecoder\bin\Debug\201002-lebl-080001_mlat.ast";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = openFileDialog.OpenFile()) != null)
                {
                    this.file = openFileDialog.FileName;
                    file = Path.GetFileName(file);
                }
            }
            asterixFile = new AsterixFile(file);
            FileReadLabel.Text = "File " + file + " read";
            CAT10list = asterixFile.getCAT10List();
            CAT21list = asterixFile.getCAT21List();

        }

        private void CAT10TableButton_Click(object sender, EventArgs e)
        { 
            Table10 table10 = new Table10(CAT10list);
            table10.TopLevel = false;
            table10.FormBorderStyle = FormBorderStyle.None;
            table10.Dock = DockStyle.Fill;
            panelMenu.Controls.Add(table10);
            panelMenu.Tag = table10;
            table10.Show();
            table10.BringToFront();
        }
    }

}
