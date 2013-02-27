using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            // Enable / Disable Controls
            btnOpenPseudonymFile.Enabled = state;
        }
        private void IncrementProgressBar(int inc)
        {
            // Increment Progress Bar as application progresses
            // Shows user that application has not crashed
            progressBar1.Increment(inc);
        }

        #region Open CSV file and populate datagridview
        private void btnOpenCsv_Click(object sender, EventArgs e)
        {
            // Set constants for use
            const string comma = ",";
            const string tablename = "Persons";
            // Store CSV to a dataset
            var dataset = new DataSet();
            // Start using the openFileDialog1
            using (var openFileDialog1 = new OpenFileDialog
                {
                    // Set filter to only allow CSV files
                    Filter = Resources.Form1_btnOpenPseudonymFile_Click_CSV___csv, // Text strings moved to resource file. This helps with cross language support.
                    FilterIndex = 1
                })
            {
                // Check that a valid file is opened
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    IncrementProgressBar(1);
                    var filename = openFileDialog1.FileName;
                    if (filename == null) throw new ArgumentNullException("filename"); // Just in case no file has got through.
                    // Use StreamReader to open file
                    var sr = new StreamReader(filename);
                    File.ReadAllText(openFileDialog1.FileName); // Read whole file
                    dataset.Tables.Add(tablename); // Add table to dataset
                    // Column names
                    dataset.Tables[tablename].Columns.Add("Forename"); 
                    dataset.Tables[tablename].Columns.Add("Gender");
                    dataset.Tables[tablename].Columns.Add("Occurance");
                    progressBar1.Increment(1);
                    var allData = sr.ReadToEnd(); // Read all of file
                    var rows = allData.Split(Environment.NewLine.ToCharArray()); // Split rows on line break
                    IncrementProgressBar(1);
                    // Loop through rows
                    for (var i = 0; i < rows.Length; i++)
                    {
                        var r = rows[i].Replace("\"", ""); // Remove " from any cells
                        var items = r.Split(comma.ToCharArray()); // Split row on comma to an array
                        dataset.Tables[tablename].Rows.Add(items); // Insert row into datatable
                        IncrementProgressBar(1);
                    }
                    IncrementProgressBar(1);
                    // Sanity checks
                    if (dataGridView1 != null)
                        if (dataset.Tables != null) dataGridView1.DataSource = dataset.Tables[0].DefaultView; // Set datasource of datagridview
                    ControlsState(true); // Enable controls
                    IncrementProgressBar(1);

                }
                else
                {
                    // No file selected
                    MessageBox.Show(Resources.Form1_btnOpenCsv_Click_Something_went_wrong);
                }
            }
        }
        #endregion
        
        private void btnCheckPsudonym_Click(object sender, EventArgs e)
        {

            #region Old Code - Commented Out - Left to show progression of design
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
            #endregion
        #region Open Pseudonym File
        private void btnOpenPseudonymFile_Click(object sender, EventArgs e)
        {
            // Set constants
            const string comma = ",";
            const string tablename = "Pseudonyms";
            var dataset = new DataSet(); // Another dataset
            // Use openFileDialog - Could have reused first, but kept seperate for future adaptation
            using (var openFileDialog2 = new OpenFileDialog
                {
                    Filter = Resources.Form1_btnOpenPseudonymFile_Click_CSV___csv, // Text again moved to resource file
                    FilterIndex = 1
                })
            {
                // Check valid file is opened
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    IncrementProgressBar(1);
                    var filename = openFileDialog2.FileName;
                    // Read in file
                    var sr = new StreamReader(filename); 
                    File.ReadAllText(openFileDialog2.FileName);
                    // Create datatable and columns
                    dataset.Tables.Add(tablename);
                    dataset.Tables[tablename].Columns.Add("Basename");
                    dataset.Tables[tablename].Columns.Add("Pseudonyms");
                    IncrementProgressBar(1);
                    var allData = sr.ReadToEnd();
                    var rows = allData.Split(Environment.NewLine.ToCharArray()); // Split rows on line break
                    // Loop through all rows
                    for (var i = 0; i < rows.Length; i++)
                    {
                        var r = rows[i].Replace("\"", ""); // Remove " from lines
                        var items = r.Split(comma.ToCharArray()); // Split row on commas to an array
                        dataset.Tables[tablename].Rows.Add(items); // Insert into datatable
                        IncrementProgressBar(1);
                    }
                    IncrementProgressBar(1);
                    //var searchValueArray = searchValue.Split(",".ToCharArray());

                    // Inst new DataView to do comparisons with
                    var dv = new DataView {Table = dataset.Tables[tablename]};

                    // Loop through all rows in dataview
                    for (var i = 0; i < dv.Table.Rows.Count;i++)
                    {
                        //var baseName = dataset.Tables[tablename].Columns["Basename"];
                        
                        var baseName = dv.Table.Rows[i][0].ToString().Replace("\n",""); // Remove "
                        var possibleMatches = dv.Table.Rows[i][1].ToString().Split("|".ToCharArray()); // Split match options on |

                        // Create list of rows found
                        var rowsFound = new List<int>();
                        // Cannot implicitly type a null - I need a string in outer scope
                        string pos = null;
                        IncrementProgressBar(1);
                        // Loop through possible matches
                        for (var index1 = 0; index1 < possibleMatches.Length; index1++)
                        {
                            IncrementProgressBar(1);
                            pos = possibleMatches[index1];
                            // Loop through datagridview rows
                            for (var l = 0; l < dataGridView1.Rows.Count; l++)
                            {
                                IncrementProgressBar(1);
                                var row = dataGridView1.Rows[l]; // Set row
                                var value = row.Cells[0].Value; // Get value of First Name cell
                                if (value != null &&
                                    (value.ToString().Replace(Environment.NewLine, "") == pos.ToUpper())) // Treat all as uppercase
                                {
                                    i = row.Index;
                                    //MessageBox.Show(i.ToString());
                                    Debug.Print(i.ToString());
                                    rowsFound.Add(i); // Add to list
                                    IncrementProgressBar(1);
                                }
                            }
                        }
                        // Loop through rows found list
                        foreach (var i1 in rowsFound)
                        {
                            // Display message to user to see found duplicate
                            MessageBox.Show(Resources.Form1_btnOpenPseudonymFile_Click_A_possible_pseudonym_has_been_discovered_in_the_original_data_on_row_ + i1.ToString()+Resources.Form1_btnOpenPseudonymFile_Click_);
                            IncrementProgressBar(1);
                            // Matching Code
                            // Table constants, dataset and columns
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
                            // Check datagridview is not null
                            if (dataGridView1 != null)
                            {
                                IncrementProgressBar(1);
                                // Check there are enough rows
                                if (dataGridView1.Rows.Count > i1)
                                {
                                    IncrementProgressBar(1);
                                    // Match values to sources
                                    var originalName = dataGridView1.Rows[i1].Cells[0].Value.ToString();
                                    var gender = dataGridView1.Rows[i1].Cells[1].Value.ToString();
                                    var rowId = i1.ToString(CultureInfo.InvariantCulture);
                                    var bN = baseName;
                                    var pseudonym = pos;

                                    var resultsArray = new string[5];
                                    resultsArray[0] = originalName.Replace(Environment.NewLine,"");
                                    resultsArray[1] = gender.Replace(Environment.NewLine, "");
                                    resultsArray[2] = rowId.Replace(Environment.NewLine, "");
                                    resultsArray[3] = bN.Replace(Environment.NewLine, "");
                                    if (pseudonym != null) resultsArray[4] = pseudonym.Replace(Environment.NewLine, "");
                                    // Add to dataset
                                    finalDataset.Tables[finalTable].Rows.Add(resultsArray);
                                    IncrementProgressBar(1);
                                }
                            }
                            // Final sanity check
                            if (dataGridView1 != null)
                                dataGridView1.DataSource = finalDataset.Tables[finalTable].DefaultView; // Populate datagrid with values
                            IncrementProgressBar(1);
                        }

                    }
                    // Inform user the process has completed
                    MessageBox.Show(Resources.Form1_btnOpenPseudonymFile_Click_Completed);
                    IncrementProgressBar(1);

                }
                else
                {
                    // Invalid or no file
                    MessageBox.Show(Resources.Form1_btnOpenCsv_Click_Something_went_wrong);
                }
            }
        #endregion
        }
        #region Form Init
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Avoid using form load
        }
        #endregion
        #region Launch Modify Pseudonym File
        private void btnModifyPseudonymFile_Click(object sender, EventArgs e)
        {
            // Instanciate form and display
            var frm = new frmModify();
            frm.Show();
        }
        #endregion
    }
}