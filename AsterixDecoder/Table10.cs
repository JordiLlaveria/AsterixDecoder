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
        DataTable dataTableNew;
        List<CAT10> CAT10list;
        public Table10(List<CAT10> list)
        {
            InitializeComponent();
            this.CAT10list = list;
        }

        private void Table10_Load(object sender, EventArgs e)
        {
            filterComboBox.Items.Add("Target Address");
            filterComboBox.Items.Add("Target Identification");
            filterComboBox.Items.Add("Track Number");
            filterComboBox.Items.Add("Mode 3A");

            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("Category");
            dataTable.Columns.Add("SAC");
            dataTable.Columns.Add("SIC");
            dataTable.Columns.Add("Message Type");
            dataTable.Columns.Add("Target Report Descriptor"); //5
            dataTable.Columns.Add("Time of Day");
            dataTable.Columns.Add("Position in WGS-84 Coordinates");
            dataTable.Columns.Add("Position in Polar Coordinates");
            dataTable.Columns.Add("Position in Cartesian Coordinates");
            dataTable.Columns.Add("Track Velocity in Polar Coordinates");
            dataTable.Columns.Add("Track Velocity in Cartesian Coordinates");
            dataTable.Columns.Add("Track Number");
            dataTable.Columns.Add("Track Status"); //13
            dataTable.Columns.Add("Mode 3/A Code");
            dataTable.Columns.Add("Target Address");
            dataTable.Columns.Add("Target Identification");
            dataTable.Columns.Add("Mode S MB Data");
            dataTable.Columns.Add("Vehicle Fleet Identification");
            dataTable.Columns.Add("Flight Level");
            dataTable.Columns.Add("Measured Height");
            dataTable.Columns.Add("Target Size and Orientation");
            dataTable.Columns.Add("System Status"); //22
            dataTable.Columns.Add("Preprogrammed Message"); //23
            dataTable.Columns.Add("Standard Deviation of Position");
            dataTable.Columns.Add("Presence");
            dataTable.Columns.Add("Amplitude of Primary Plor");
            dataTable.Columns.Add("Calculated Acceleration");
            
            loadTable();
            dataTableNew = dataTable.Copy();
            DataView dv = new DataView(dataTableNew);
            CAT10Grid.DataSource = dv;
            drawTable();
        }

        private void loadTable()
        {
            dataTable.Clear();
            for (int i = 0; i < CAT10list.Count; i++)
            {
                string[] rowInformation = CAT10list[i].getInformation(i);
                dataTable.Rows.Add(rowInformation);
            }
        }

        private void drawTable()
        {   
            CAT10Grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CAT10Grid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < CAT10Grid.ColumnCount; i++)
            {
                CAT10Grid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            CAT10Grid.Columns[0].Width = 200;
            CAT10Grid.Columns[1].Width = 65;
            CAT10Grid.Columns[2].Width = 35;
            CAT10Grid.Columns[3].Width = 35;
            CAT10Grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            CAT10Grid.RowHeadersVisible = false;
        }

        private void filterByTargetAddressButton_Click(object sender, EventArgs e)
        {
            int index = filterComboBox.SelectedIndex;

            if (index == 0)
            {
                if (filterByTargetAddressTextBox.Text != null)
                {
                    dataTableNew = dataTable.Copy();
                    DataView dataView = new DataView(dataTableNew);
                    dataView.RowFilter = "[Target Address] = '" + filterByTargetAddressTextBox.Text + "'";
                    CAT10Grid.DataSource = dataView;
                    drawTable();
                }
            }
            else if (index == 1)
            {
                if (filterByTargetAddressTextBox.Text != null)
                {
                    dataTableNew = dataTable.Copy();
                    DataView dataView = new DataView(dataTableNew);
                    dataView.RowFilter = "[Target Identification] = '" + filterByTargetAddressTextBox.Text + "'";
                    CAT10Grid.DataSource = dataView;
                    drawTable();
                }
            }
            else if (index == 2)
            {
                if (filterByTargetAddressTextBox.Text != null)
                {
                    dataTableNew = dataTable.Copy();
                    DataView dataView = new DataView(dataTableNew);
                    dataView.RowFilter = "[Track Number] = '" + filterByTargetAddressTextBox.Text + "'";
                    CAT10Grid.DataSource = dataView;
                    drawTable();
                }
            }
            else if (index == 3)
            {
                if (filterByTargetAddressTextBox.Text != null)
                {
                    dataTableNew = dataTable.Copy();
                    DataView dataView = new DataView(dataTableNew);
                    dataView.RowFilter = "[Mode 3/A Code] = '" + filterByTargetAddressTextBox.Text + "'";
                    CAT10Grid.DataSource = dataView;
                    drawTable();
                }
            }

        }

        private void resetFilterButton_Click(object sender, EventArgs e)
        {
            dataTableNew = dataTable.Copy();
            DataView dataView = new DataView(dataTableNew);
            CAT10Grid.DataSource = dataView;
            drawTable();
        }
              

        private void CAT10Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if(String.Equals(CAT10Grid.Rows[row].Cells[col].Value, "Click to expand"))
            {
                int num = Convert.ToInt32(CAT10Grid.Rows[row].Cells[0].Value);
                string[] val = CAT10list[num].getClickToExpandValues(col);
                if (val[0] != null)
                {
                    CAT10Grid.Rows[row].Cells[col].Value = val[0];
                    int i = 1;
                    while (i < val.Length)
                    {
                        if(val[i] != null)
                        {
                            CAT10Grid.Rows[row].Cells[col].Value = CAT10Grid.Rows[row].Cells[col].Value + "\n" + val[i];
                        }
                        i = i + 1;
                    }                    
                    CAT10Grid.AutoResizeRow(row);
                }
            }      
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            DataTable exportDT = new DataTable();

            exportDT.Columns.Add("Number");
            exportDT.Columns.Add("Category");
            exportDT.Columns.Add("SAC");
            exportDT.Columns.Add("SIC");
            exportDT.Columns.Add("Message Type");
            exportDT.Columns.Add("Target Report Descriptor"); //5
            exportDT.Columns.Add("Time of Day");
            exportDT.Columns.Add("Position in WGS-84 Coordinates");
            exportDT.Columns.Add("Position in Polar Coordinates");
            exportDT.Columns.Add("Position in Cartesian Coordinates");
            exportDT.Columns.Add("Track Velocity in Polar Coordinates");
            exportDT.Columns.Add("Track Velocity in Cartesian Coordinates");
            exportDT.Columns.Add("Track Number");
            exportDT.Columns.Add("Track Status"); //13
            exportDT.Columns.Add("Mode 3/A Code");
            exportDT.Columns.Add("Target Address");
            exportDT.Columns.Add("Target Identification");
            exportDT.Columns.Add("Mode S MB Data");
            exportDT.Columns.Add("Vehicle Fleet Identification");
            exportDT.Columns.Add("Flight Level");
            exportDT.Columns.Add("Measured Height");
            exportDT.Columns.Add("Target Size and Orientation");
            exportDT.Columns.Add("System Status"); //22
            exportDT.Columns.Add("Preprogrammed Message"); //23
            exportDT.Columns.Add("Standard Deviation of Position");
            exportDT.Columns.Add("Presence");
            exportDT.Columns.Add("Amplitude of Primary Plot");
            exportDT.Columns.Add("Calculated Acceleration");

            for (int i = 0; i < CAT10list.Count; i++)
            {
                string[] rowInformation = CAT10list[i].getInformation(i);
                exportDT.Rows.Add(rowInformation);
            }

            new ExportHelper().Export(exportDT);
            exportDT = null;
        }
    }
}
