using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using PseudonymMatching.Properties;

namespace PseudonymMatching
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            ControlsState(false);
        }

        private void ControlsState(Boolean state)
        {
            btnOpenPseudonymFile.Enabled = state;
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
                    progressBar1.Increment(1);
                    var filename = openFileDialog1.FileName;
                    if (filename == null) throw new ArgumentNullException("filename");
                    var sr = new StreamReader(filename);
                    File.ReadAllText(openFileDialog1.FileName);
                    dataset.Tables.Add(tablename);
                    dataset.Tables[tablename].Columns.Add("Forename");
                    dataset.Tables[tablename].Columns.Add("Gender");
                    dataset.Tables[tablename].Columns.Add("Occurance");
                    progressBar1.Increment(1);
                    var allData = sr.ReadToEnd();
                    var rows = allData.Split("\r".ToCharArray());
                    progressBar1.Increment(1);
                    for (var i = 0; i < rows.Length; i++)
                    {
                        var r = rows[i].Replace("\"", "");
                        var items = r.Split(comma.ToCharArray());
                        dataset.Tables[tablename].Rows.Add(items);
                    }
                    progressBar1.Increment(1);
                    if (dataGridView1 != null)
                        if (dataset.Tables != null) dataGridView1.DataSource = dataset.Tables[0].DefaultView;
                    ControlsState(true);
                    progressBar1.Increment(1);

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
                    progressBar1.Increment(1);
                    var filename = openFileDialog2.FileName;
                    var sr = new StreamReader(filename);
                    File.ReadAllText(openFileDialog2.FileName);
                    dataset.Tables.Add(tablename);
                    dataset.Tables[tablename].Columns.Add("Basename");
                    dataset.Tables[tablename].Columns.Add("Pseudonyms");
                    progressBar1.Increment(1);
                    var allData = sr.ReadToEnd();
                    var rows = allData.Split("\r".ToCharArray());

                    for (var i = 0; i < rows.Length; i++)
                    {
                        var r = rows[i].Replace("\"", "");
                        var items = r.Split(comma.ToCharArray());
                        dataset.Tables[tablename].Rows.Add(items);
                    }
                    progressBar1.Increment(1);
                    //var searchValueArray = searchValue.Split(",".ToCharArray());

                    var dv = new DataView {Table = dataset.Tables[tablename]};

                    for (var i = 0; i < dv.Table.Rows.Count;i++)
                    {
                        //var baseName = dataset.Tables[tablename].Columns["Basename"];

                        var baseName = dv.Table.Rows[i][0].ToString().Replace("\n","");
                        var possibleMatches = dv.Table.Rows[i][1].ToString().Split("|".ToCharArray());

                        var rowsFound = new List<int>();

                        string pos = null;
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
                                    progressBar1.Increment(1);
                                }
                            }
                        }
                        foreach (var i1 in rowsFound)
                        {
                            MessageBox.Show(Resources.Form1_btnOpenPseudonymFile_Click_A_possible_pseudonym_has_been_discovered_in_the_original_data_on_row_ + i1.ToString()+Resources.Form1_btnOpenPseudonymFile_Click_);

                            // Magic time

                            const string finalTable = "Results";
                            var finalDataset = new DataSet();
                            finalDataset.Tables.Add(finalTable);
                            finalDataset.Tables[finalTable].Columns.Add("OriginalName");
                            finalDataset.Tables[finalTable].Columns.Add("Gender");
                            finalDataset.Tables[finalTable].Columns.Add("RowId");
                            finalDataset.Tables[finalTable].Columns.Add("BaseName");
                            finalDataset.Tables[finalTable].Columns.Add("Pseudonym");
                            progressBar1.Increment(1);
                            //var values = "";

                            if (dataGridView1 != null)
                            {
                                if (dataGridView1.Rows.Count > i1)
                                {
                                    var originalName = dataGridView1.Rows[i1].Cells[0].Value.ToString();
                                    var gender = dataGridView1.Rows[i1].Cells[1].Value.ToString();
                                    var rowId = i1.ToString(CultureInfo.InvariantCulture);
                                    var bN = baseName;
                                    var pseudonym = pos;

                                    var resultsArray = new string[5];
                                    resultsArray[0] = originalName.Replace("\n","");
                                    resultsArray[1] = gender.Replace("\n", "");
                                    resultsArray[2] = rowId.Replace("\n", "");
                                    resultsArray[3] = bN.Replace("\n", "");
                                    if (pseudonym != null) resultsArray[4] = pseudonym.Replace("\n", "");

                                    finalDataset.Tables[finalTable].Rows.Add(resultsArray);
                                    progressBar1.Increment(1);
                                }
                            }

                            if (dataGridView1 != null)
                                dataGridView1.DataSource = finalDataset.Tables[finalTable].DefaultView;
                            progressBar1.Increment(1);
                        }

                    }

                    MessageBox.Show(Resources.Form1_btnOpenPseudonymFile_Click_Completed);
                    progressBar1.Value = 0;

                }
                else
                {
                    MessageBox.Show(Resources.Form1_btnOpenCsv_Click_Something_went_wrong);
                }
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnModifyPseudonymFile_Click(object sender, EventArgs e)
        {
            var foo = new frmModify();
            foo.Show();
        }
    }
}