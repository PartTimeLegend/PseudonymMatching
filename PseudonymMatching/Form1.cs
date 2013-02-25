using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PseudonymMatching.Properties;

namespace PseudonymMatching
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ControlsState(false);
        }

        private void ControlsState(Boolean state)
        {
            txtPseudonym.Enabled = state;
            btnCheckPsudonym.Enabled = state;
        }

        private void btnOpenCsv_Click(object sender, EventArgs e)
        {
            var comma = ",";
            var tablename = "Persons";
            var dataset = new DataSet();
            using (var openFileDialog1 = new OpenFileDialog
                {
                    Filter = "CSV|*.csv",
                    FilterIndex = 1
                })
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var filename = openFileDialog1.FileName;
                    var sr = new StreamReader(filename);
                    var csv = File.ReadAllText(openFileDialog1.FileName);
                    dataset.Tables.Add(tablename);
                    dataset.Tables[tablename].Columns.Add("Forename");
                    dataset.Tables[tablename].Columns.Add("Gender");
                    dataset.Tables[tablename].Columns.Add("Occurance");

                    var allData = sr.ReadToEnd();
                    var rows = allData.Split("\r".ToCharArray());

                    for (var i = 0; i < rows.Length; i++)
                    {
                        var r = rows[i].Replace("\"", "");
                        var items = r.Split(comma.ToCharArray());
                        dataset.Tables[tablename].Rows.Add(items);
                    }
                    if (dataGridView1 != null)
                        if (dataset.Tables != null) dataGridView1.DataSource = dataset.Tables[0].DefaultView;
                    ControlsState(true);

                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }

        private void btnCheckPsudonym_Click(object sender, EventArgs e)
        {
                // Sometimes ideas need to be rethought
                ////if (pn == "Richard")
                //{
                //    var possibilities = "KHAOLA";

                //    var posarray = possibilities.Split(",".ToCharArray());
                //    //}


                //    foreach (var name in posarray)
                //    {
                //        if (dataGridView1.Rows == null) continue;
                //        var matchedRows = dataGridView1.Rows
                //                                       .Cast<DataGridViewRow>()
                //                                       .Select(r =>
                //                                           {
                //                                               if (r.Cells != null)
                //                                               {
                //                                                   var value = r.Cells["Forename"].Value;
                //                                                   if (value == null)
                //                                                       throw new ArgumentNullException("value");
                //                                                   return value ?? null;
                //                                               }
                //                                           })
                //                                       .Where(v => v != null && v.ToString().Replace("\"","") == name)
                //                                       .ToList();


                //        var singleOrDefault = matchedRows.SingleOrDefault();
                //        if (singleOrDefault != null)
                //        {
                //            var s = singleOrDefault.ToString();
                //            Debug.Print(s);

                            
                //        }
                //        MessageBox.Show("Completed");
                //    }

                var searchValue = "A";
                //var rowIndex = -1;
                var rowsFound = new List<int>();
                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var row = dataGridView1.Rows[i];
                    var value = row.Cells[0].Value;
                    if (value != null && value.ToString().Replace("\n","") == searchValue)
                    {
                        i = row.Index;
                        //MessageBox.Show(i.ToString());
                        rowsFound.Add(i);
                        
                    }

                }
                for (var index = 0; index < rowsFound.Count; index++)
                {
                    var i1 = rowsFound[index];
                    MessageBox.Show(rowsFound[index].ToString());
                }
            MessageBox.Show("Completed");
        }
      }
}