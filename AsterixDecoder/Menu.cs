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
            if (CAT10list.Count != 0)
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
            else
            {
                MessageBox.Show("The table can't be shown, as there's any information about CAT10 data items");
            }
        }

        private void CAT21TableButton_Click(object sender, EventArgs e)
        {
            if (CAT21list.Count != 0)
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
            else
            {
                MessageBox.Show("The table can't be shown, as there's any information about CAT21 data items");
            }
        }

        private void mapViewButton_Click(object sender, EventArgs e)
        {
            if(Flightslist.Count != 0)
            {
                MapView mapView = new MapView(Flightslist);
                mapView.TopLevel = false;
                mapView.FormBorderStyle = FormBorderStyle.None;
                mapView.Dock = DockStyle.Fill;
                panelMenu.Controls.Add(mapView);
                panelMenu.Tag = mapView;
                mapView.Show();
                mapView.BringToFront();
            }
            else
            {
                MessageBox.Show("There's any flight already decoded, so they can't be shown in the map");
            }
        }
    }
}
