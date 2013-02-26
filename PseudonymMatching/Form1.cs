using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
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
            const string comma = ",";
            const string tablename = "Persons";
            var dataset = new DataSet();
            using (var openFileDialog1 = new OpenFileDialog
                {
                    Filter = Resources.Form1_btnOpenPseudonymFile_Click_CSV___csv,
                    FilterIndex = 1
                })
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var filename = openFileDialog1.FileName;
                    if (filename == null) throw new ArgumentNullException("filename");
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
                    MessageBox.Show(Resources.Form1_btnOpenCsv_Click_Something_went_wrong);
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
            const string comma = ",";
            const string tablename = "Pseudonyms";
            var dataset = new DataSet();
            using (var openFileDialog2 = new OpenFileDialog
                {
                    Filter = Resources.Form1_btnOpenPseudonymFile_Click_CSV___csv,
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

                    var dv = new DataView {Table = dataset.Tables[tablename]};

                    for (var i = 0; i < dv.Table.Rows.Count;i++)
                    {
                        //var baseName = dataset.Tables[tablename].Columns["Basename"];

                        var baseName = dv.Table.Rows[i][0].ToString().Replace("\n","");
                        var possibleMatches = dv.Table.Rows[i][1].ToString().Split("|".ToCharArray());

                        var rowsFound = new List<int>();

                        string pos;
                        for (var index1 = 0; index1 < possibleMatches.Length; index1++)
                        {
                            pos = possibleMatches[index1];
                            for (var l = 0; l < dataGridView1.Rows.Count; l++)
                            {
                                var row = dataGridView1.Rows[l];
                                var value = row.Cells[0].Value;
                                if (value != null &&
                                    (value.ToString().Replace("\n", "") == pos.ToUpper()))
                                {
                                    i = row.Index;
                                    //MessageBox.Show(i.ToString());
                                    rowsFound.Add(i);
                                }
                            }
                        }
                        foreach (var i1 in rowsFound)
                        {
                            MessageBox.Show(i1.ToString());

                            // Magic time

                            const string finalTable = "Results";
                            var finalDataset = new DataSet();
                            finalDataset.Tables.Add(finalTable);
                            finalDataset.Tables[finalTable].Columns.Add("OriginalName");
                            finalDataset.Tables[finalTable].Columns.Add("Gender");
                            finalDataset.Tables[finalTable].Columns.Add("RowId");
                            finalDataset.Tables[finalTable].Columns.Add("BaseName");
                            finalDataset.Tables[finalTable].Columns.Add("Pseudonym");

                            var values = "";

                            //var originalName = ;
                            var gender = "";
                            var rowId = i1 + 1;
                            //baseName
                            pos = "";


                            dataset.Tables[tablename].Rows.Add(values);
                        }
                    }
                    MessageBox.Show(Resources.Form1_btnOpenPseudonymFile_Click_Completed);

                }
                else
                {
                    MessageBox.Show(Resources.Form1_btnOpenCsv_Click_Something_went_wrong);
                }
            }

        }
    }
}