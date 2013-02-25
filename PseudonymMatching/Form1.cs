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
                    File.ReadAllText(openFileDialog1.FileName);
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
            //var searchValue = "A,A J";
            //var searchValueArray = searchValue.Split(",".ToCharArray());


            //foreach (var s in searchValueArray)
            //{
            //    var rowsFound = new List<int>();
            //    for (var i = 0; i < dataGridView1.Rows.Count; i++)
            //    {
            //        var row = dataGridView1.Rows[i];
            //        var value = row.Cells[0].Value;
            //        if (value != null && value.ToString().Replace("\n", "") == s)
            //        {
            //            i = row.Index;
            //            //MessageBox.Show(i.ToString());
            //            rowsFound.Add(i);

            //        }

            //    }
            //    for (var index = 0; index < rowsFound.Count; index++)
            //    {
            //        var i1 = rowsFound[index];
            //        MessageBox.Show(rowsFound[index].ToString());
            //    }
            //}
            //MessageBox.Show("Completed");
        }

        private void btnOpenPseudonymFile_Click(object sender, EventArgs e)
        {
            var comma = ",";
            var tablename = "Pseudonyms";
            var dataset = new DataSet();
            using (var openFileDialog2 = new OpenFileDialog
                {
                    Filter = "CSV|*.csv",
                    FilterIndex = 1
                })
            {
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    var filename = openFileDialog2.FileName;
                    var sr = new StreamReader(filename);
                    File.ReadAllText(openFileDialog2.FileName);
                    dataset.Tables.Add(tablename);
                    dataset.Tables[tablename].Columns.Add("Basename");
                    dataset.Tables[tablename].Columns.Add("Pseudonyms");

                    var allData = sr.ReadToEnd();
                    var rows = allData.Split("\r".ToCharArray());

                    for (var i = 0; i < rows.Length; i++)
                    {
                        var r = rows[i].Replace("\"", "");
                        var items = r.Split(comma.ToCharArray());
                        dataset.Tables[tablename].Rows.Add(items);
                    }
                    //var searchValueArray = searchValue.Split(",".ToCharArray());


                    foreach (var rowD in dataset.Tables[tablename].Rows)
                    {
                        var baseName = dataset.Tables[tablename].Columns["Basename"];
                        var possibleMatches =
                            dataset.Tables[tablename].Columns["Pseudonyms"].ToString().Split("|".ToCharArray());

                        var rowsFound = new List<int>();

                        foreach (var pos in possibleMatches)
                        {
                            for (var i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                var row = dataGridView1.Rows[i];
                                var value = row.Cells[0].Value;
                                if (value != null && value.ToString().Replace("\n", "") == pos.ToString())
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
                        }
                    }
                    MessageBox.Show("Completed");

                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }

        }
    }
}