using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            const string comma = ",";
            const string tablename = "Data";
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
                    dataset.Tables[tablename].Columns.Add("Basename");
                    dataset.Tables[tablename].Columns.Add("Pseudonyms");
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
                    progressBar1.Increment(1);

                }
                else
                {
                    MessageBox.Show(Resources.Form1_btnOpenCsv_Click_Something_went_wrong);
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = ".csv";
            saveFileDialog1.Filter = "CSV|.csv";
            var exportData = "";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            var fileName = saveFileDialog1.FileName;
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                progressBar1.Increment(1);
                foreach (DataGridViewCell dc in dr.Cells)
                {
                    progressBar1.Increment(1);
                    if (dc.Value != null)
                    {
                        exportData += dc.Value.ToString() + ",";
                    }
                }
                progressBar1.Increment(1);
                exportData += Environment.NewLine.ToString();
                exportData = exportData.Substring(0, exportData.Length - 3) + Environment.NewLine.ToString();
                var sw = new System.IO.StreamWriter(fileName);
                progressBar1.Increment(1);
                sw.Write(exportData);
                sw.Close();
                progressBar1.Value = 0;
                MessageBox.Show(Resources.frmModify_btnSave_Click_Saved);
            }
        }
    }
}
