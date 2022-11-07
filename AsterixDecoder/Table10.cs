using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace AsterixDecoder
{
    public partial class Table10 : Form
    {
        DataTable dataTable = new DataTable();
        List<CAT10> CAT10list;
        public Table10(List<CAT10> list)
        {
            InitializeComponent();
            this.CAT10list = list;
        }

        private void Table10_Load(object sender, EventArgs e)
        {
            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("Category");
            dataTable.Columns.Add("SAC");
            dataTable.Columns.Add("SIC");
            dataTable.Columns.Add("Message Type");
            dataTable.Columns.Add("Target Report Descriptor");
            dataTable.Columns.Add("Time of Day");
            dataTable.Columns.Add("Position in WGS-84 Coordinates");
            dataTable.Columns.Add("Position in Polar Coordinates");
            dataTable.Columns.Add("Position in Cartesian Coordinates");
            dataTable.Columns.Add("Track Velocity in Polar Coordinates");
            dataTable.Columns.Add("Track Velocity in Cartesian Coordinates");
            dataTable.Columns.Add("Track Number");
            dataTable.Columns.Add("Track Status");
            dataTable.Columns.Add("Mode 3/A Code");
            dataTable.Columns.Add("Target Address");
            dataTable.Columns.Add("Target Identification");
            dataTable.Columns.Add("Mode S MB Data");
            dataTable.Columns.Add("Vehicle Fleet Identification");
            dataTable.Columns.Add("Flight Level");
            dataTable.Columns.Add("Measured Height");
            dataTable.Columns.Add("Target Size and Orientation");
            dataTable.Columns.Add("System Status");
            dataTable.Columns.Add("Preprogrammed Message");
            dataTable.Columns.Add("Standard Deviation of Position");
            dataTable.Columns.Add("Presence");
            dataTable.Columns.Add("Amplitude of Primary Plor");
            dataTable.Columns.Add("Calculated Acceleration");

            for (int i = 0; i < CAT10list.Count; i++)
            {
                string[] rowInformation = CAT10list[i].getInformation(i);
                dataTable.Rows.Add(rowInformation);
            }
            CAT10Grid.DataSource = dataTable;
            loadTable();
        }

        private void loadTable()
        {   
            CAT10Grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CAT10Grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CAT10Grid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT10Grid.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT10Grid.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT10Grid.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT10Grid.Columns[21].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT10Grid.Columns[27].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT10Grid.Columns[0].Width = 50;
            CAT10Grid.Columns[1].Width = 65;
            CAT10Grid.Columns[2].Width = 35;
            CAT10Grid.Columns[3].Width = 35;
            CAT10Grid.RowHeadersVisible = false;
        }

        private void filterByTargetAddressButton_Click(object sender, EventArgs e)
        {
            
            if (filterByTargetAddressTextBox.Text != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.RowFilter = "[Target Address] = '" + filterByTargetAddressTextBox.Text + "'";
                CAT10Grid.DataSource = dataView;
                loadTable();
            }

        }

        private void resetFilterButton_Click(object sender, EventArgs e)
        {
            DataView dataView = new DataView(dataTable);
            CAT10Grid.DataSource = dataView;
            loadTable();
        }
              

        private void CAT10Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            int num = Convert.ToInt32(CAT10Grid.Rows[row].Cells[0].Value);
            string[] val = CAT10list[num].getClickToExpandValues(col);
            if (val[0] != null)
            {
                CAT10Grid.Rows[row].Cells[col].Value = val[0] + "\n" + val[1];
            }
            
        }
    }
}
