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
        List<Flight> Flightslist = new List<Flight>();
        AsterixFile asterixFile;
        string file;

        public Menu()
        {
            InitializeComponent();
        }

        private void OnClickFile(object sender, EventArgs e)
        {
            Stream stream = null;

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
            Flightslist = asterixFile.getFlights();

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

        private void CAT21TableButton_Click(object sender, EventArgs e)
        {
            Table21 table21 = new Table21(CAT21list);
            table21.TopLevel = false;
            table21.FormBorderStyle = FormBorderStyle.None;
            table21.Dock = DockStyle.Fill;
            panelMenu.Controls.Add(table21);
            panelMenu.Tag = table21;
            table21.Show();
            table21.BringToFront();
        }

        private void mapViewButton_Click(object sender, EventArgs e)
        {
            MapView mapView = new MapView(Flightslist);
            mapView.TopLevel = false;
            mapView.FormBorderStyle = FormBorderStyle.None;
            mapView.Dock = DockStyle.Fill;
            panelMenu.Controls.Add(mapView);
            panelMenu.Tag = mapView;
            mapView.Show();
            mapView.BringToFront();
            Console.WriteLine("Hola");
        }
    }
}
