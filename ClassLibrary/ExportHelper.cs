using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary
{
    public class ExportHelper
    {
        public bool Export(DataTable dataTable)
        {
            bool exported = false;

            if (dataTable.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {

                        List<string> lines = new List<string>();

                        bool firstDone = false;
                        StringBuilder headerLine = new StringBuilder();

                        foreach (DataColumn column in dataTable.Columns)
                        {
                            if (!firstDone)
                            {
                                headerLine.Append(column.ColumnName);
                                firstDone = true;
                            }
                            else
                            {
                                headerLine.Append("," + column.ColumnName);
                            }
                        }

                        lines.Add(headerLine.ToString());

                        foreach (DataRow row in dataTable.Rows)
                        {
                            StringBuilder dataLine = new StringBuilder();
                            firstDone = false;

                            string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                            dataLine.Append(string.Join(",", fields));

                            //foreach (DataGridViewCell cell in row.Cells)
                            //{
                            //    if (!firstDone)
                            //    {
                            //        dataLine.Append(cell.Value.ToString());
                            //        firstDone = true;
                            //    }
                            //    else
                            //    {
                            //        dataLine.Append("," + cell.Value.ToString());
                            //    }
                            //}
                            lines.Add(dataLine.ToString());
                        }

                        File.WriteAllLines(sfd.FileName, lines, Encoding.UTF8);
                        MessageBox.Show("Data Exported Successfully !!!", "Info");
                    }
                }
            }
            

            return exported;
        }
    }
}
