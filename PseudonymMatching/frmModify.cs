using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PseudonymMatching.Properties;

namespace PseudonymMatching
{
    public partial class frmModify : Form
    {
        public frmModify()
        {
            InitializeComponent();
        }

        private void IncrementProgressBar(int inc)
        {
            // Increment Progress Bar as application progresses
            // Shows user that application has not crashed
            progressBar1.Increment(inc);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Set constants and dataset
            const string comma = ",";
            const string tablename = "Data";
            var dataset = new DataSet();
            // Use openFileDialog like before
            using (var openFileDialog1 = new OpenFileDialog
            {
                Filter = Resources.Form1_btnOpenPseudonymFile_Click_CSV___csv, // Again in resources file
                FilterIndex = 1
            })
            {
                // Check file opened is valid
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    IncrementProgressBar(1);
                    var filename = openFileDialog1.FileName;
                    if (filename == null) throw new ArgumentNullException("filename");
                    // Open file and read contents
                    var sr = new StreamReader(filename);
                    File.ReadAllText(openFileDialog1.FileName);
                    // Add table and columns to dataset
                    dataset.Tables.Add(tablename);
                    dataset.Tables[tablename].Columns.Add("Basename");
                    dataset.Tables[tablename].Columns.Add("Pseudonyms");
                    IncrementProgressBar(1);
                    var allData = sr.ReadToEnd();
                    var rows = allData.Split(Environment.NewLine.ToCharArray());
                    IncrementProgressBar(1);
                    // Loop through rows
                    for (var i = 0; i < rows.Length; i++)
                    {
                        IncrementProgressBar(1);
                        var r = rows[i].Replace("\"", ""); // Remove "
                        var items = r.Split(comma.ToCharArray()); // Split on CSV
                        dataset.Tables[tablename].Rows.Add(items); // Add to dataset table
                    }
                    IncrementProgressBar(1);
                    if (dataGridView1 != null)
                        if (dataset.Tables != null) dataGridView1.DataSource = dataset.Tables[0].DefaultView; // Populate datagrid
                    IncrementProgressBar(1);

                }
                else
                {
                    // Invalid file or non selected
                    MessageBox.Show(Resources.Form1_btnOpenCsv_Click_Something_went_wrong);
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Inst saveFileDialog
            var saveFileDialog1 = new SaveFileDialog {DefaultExt = ".csv", Filter = "CSV|.csv"};
            var exportData = string.Empty;
            // Check file name is valid
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fileName = saveFileDialog1.FileName;
            // Loop through datagridview rows
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                IncrementProgressBar(1);
                // Now loop through cells
                foreach (DataGridViewCell dc in dr.Cells)
                {
                    IncrementProgressBar(1);
                    // Check has value and assign
                    if (dc.Value != null)
                    {
                        exportData += dc.Value.ToString() + ","; // Split on comma. Previously used a constant for this
                    }
                }
                IncrementProgressBar(1);
                exportData += Environment.NewLine.ToString(CultureInfo.InvariantCulture); // Add some line breaks
                exportData = exportData.Substring(0, exportData.Length - 3) + Environment.NewLine.ToString(CultureInfo.InvariantCulture);
                // Inst StreamWriter
                var sw = new System.IO.StreamWriter(fileName);
                IncrementProgressBar(1);
                // Write the file
                sw.Write(exportData);
                // Close the file
                sw.Close();
                IncrementProgressBar(1);
                // Tell user is has saved so they are not waiting
                MessageBox.Show(Resources.frmModify_btnSave_Click_Saved);
            }
        }
    }
}
