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
    public partial class Table21 : Form
    {
        DataTable dataTable = new DataTable();
        DataTable dataTableNew;
        List<CAT21> CAT21list;
        public Table21(List<CAT21> list)
        {
            InitializeComponent();
            this.CAT21list = list;
        }

        private void Table21_Load(object sender, EventArgs e)
        {
            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("Category");
            dataTable.Columns.Add("SAC");
            dataTable.Columns.Add("SIC");
            dataTable.Columns.Add("Target Report Descriptor");
            dataTable.Columns.Add("Track Number");
            dataTable.Columns.Add("Service Identification");
            dataTable.Columns.Add("Time of Applicability Position");
            dataTable.Columns.Add("Position in WGS-84 Coordinates");
            dataTable.Columns.Add("Position in WGS-84 Coordinates High Resolution");
            dataTable.Columns.Add("Time of Applicability Velocity");
            dataTable.Columns.Add("Air Speed");
            dataTable.Columns.Add("True Airspeed");
            dataTable.Columns.Add("Target Address");
            dataTable.Columns.Add("Time of Message Reception Position");
            dataTable.Columns.Add("Time of Message Reception Position High Res");
            dataTable.Columns.Add("Time of Message Reception Velocity");
            dataTable.Columns.Add("Time of Message Reception Velocity High Res");
            dataTable.Columns.Add("Geometric Height");
            dataTable.Columns.Add("Quality Indicators");
            dataTable.Columns.Add("MOPS Version");
            dataTable.Columns.Add("Mode 3A Code");
            dataTable.Columns.Add("Roll Angle");
            dataTable.Columns.Add("Flight Level");
            dataTable.Columns.Add("Magnetic Heading");
            dataTable.Columns.Add("Target Status");
            dataTable.Columns.Add("Barometric Vertical Rate");
            dataTable.Columns.Add("Geometric Vertical Rate");
            dataTable.Columns.Add("Airborne Ground Vector");
            dataTable.Columns.Add("Track Angle Rate");
            dataTable.Columns.Add("Time of Report Transmission");
            dataTable.Columns.Add("Target Identification");
            dataTable.Columns.Add("Emitter Category");
            dataTable.Columns.Add("Met Information");
            dataTable.Columns.Add("Selected Altitude");
            dataTable.Columns.Add("Final State Selected Altitude");
            dataTable.Columns.Add("Trajectory Intent");
            dataTable.Columns.Add("Service Management");
            dataTable.Columns.Add("Aircraft Operational Status");
            dataTable.Columns.Add("Surface Capabilities and Characteristics");
            dataTable.Columns.Add("Message Amplitude");
            dataTable.Columns.Add("Mode S MB Data");
            dataTable.Columns.Add("ACAS Resolution");
            dataTable.Columns.Add("Receiver ID");
            dataTable.Columns.Add("Data Ages");

            loadTable();
            dataTableNew = dataTable.Copy();
            DataView dataView = new DataView(dataTableNew);
            CAT21Grid.DataSource = dataView;
            drawTable();
        }

        public void loadTable()
        {
            dataTable.Clear();
            for (int i = 0; i < CAT21list.Count; i++)
            {
                string[] rowInformation = CAT21list[i].getInformation(i);
                dataTable.Rows.Add(rowInformation);
            }
        }
        private void drawTable()
        {            
            CAT21Grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CAT21Grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CAT21Grid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT21Grid.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT21Grid.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT21Grid.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT21Grid.Columns[21].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT21Grid.Columns[27].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CAT21Grid.Columns[0].Width = 50;
            CAT21Grid.Columns[1].Width = 65;
            CAT21Grid.Columns[2].Width = 35;
            CAT21Grid.Columns[3].Width = 35;
            CAT21Grid.RowHeadersVisible = false;
        }

        private void filterByTargetAddressButton_Click(object sender, EventArgs e)
        {
            if (filterByTargetAddressTextBox.Text != null)
            {
                dataTableNew = dataTable.Copy();
                DataView dataView = new DataView(dataTableNew);
                dataView.RowFilter = "[Target Address] = '" + filterByTargetAddressTextBox.Text + "'";
                CAT21Grid.DataSource = dataView;
                drawTable();
            }
        }

        private void resetFilterButton_Click(object sender, EventArgs e)
        {
            dataTableNew = dataTable.Copy();
            DataView dataView = new DataView(dataTableNew);
            CAT21Grid.DataSource = dataView;
            drawTable();
        }

        private void CAT21Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (String.Equals(CAT21Grid.Rows[row].Cells[col].Value, "Click to expand"))
            {
                int num = Convert.ToInt32(CAT21Grid.Rows[row].Cells[0].Value);
                string[] val = CAT21list[num].getClickToExpandValues(col);
                if (val[0] != null)
                {
                    CAT21Grid.Rows[row].Cells[col].Value = val[0];
                    int i = 1;
                    while (i < val.Length)
                    {
                        if (val[i] != null)
                        {
                            CAT21Grid.Rows[row].Cells[col].Value = CAT21Grid.Rows[row].Cells[col].Value + "\n" + val[i];
                        }
                        i = i + 1;
                    }
                    CAT21Grid.AutoResizeRow(row);
                }
            }
        }
    }
}
