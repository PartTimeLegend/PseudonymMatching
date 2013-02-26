namespace PseudonymMatching
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenCsv = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnOpenPseudonymFile = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnModifyPseudonymFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenCsv
            // 
            this.btnOpenCsv.Location = new System.Drawing.Point(112, 448);
            this.btnOpenCsv.Name = "btnOpenCsv";
            this.btnOpenCsv.Size = new System.Drawing.Size(98, 23);
            this.btnOpenCsv.TabIndex = 0;
            this.btnOpenCsv.Text = "Open CSV File";
            this.btnOpenCsv.UseVisualStyleBackColor = true;
            this.btnOpenCsv.Click += new System.EventHandler(this.btnOpenCsv_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(893, 424);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnOpenPseudonymFile
            // 
            this.btnOpenPseudonymFile.Location = new System.Drawing.Point(356, 448);
            this.btnOpenPseudonymFile.Name = "btnOpenPseudonymFile";
            this.btnOpenPseudonymFile.Size = new System.Drawing.Size(166, 23);
            this.btnOpenPseudonymFile.TabIndex = 3;
            this.btnOpenPseudonymFile.Text = "Open Pseodonym File";
            this.btnOpenPseudonymFile.UseVisualStyleBackColor = true;
            this.btnOpenPseudonymFile.Click += new System.EventHandler(this.btnOpenPseudonymFile_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 477);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(938, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // btnModifyPseudonymFile
            // 
            this.btnModifyPseudonymFile.Location = new System.Drawing.Point(634, 448);
            this.btnModifyPseudonymFile.Name = "btnModifyPseudonymFile";
            this.btnModifyPseudonymFile.Size = new System.Drawing.Size(158, 23);
            this.btnModifyPseudonymFile.TabIndex = 5;
            this.btnModifyPseudonymFile.Text = "Modify Pseudonym File";
            this.btnModifyPseudonymFile.UseVisualStyleBackColor = true;
            this.btnModifyPseudonymFile.Click += new System.EventHandler(this.btnModifyPseudonymFile_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 500);
            this.Controls.Add(this.btnModifyPseudonymFile);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnOpenPseudonymFile);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOpenCsv);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pseudonym Matching";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpenCsv;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOpenPseudonymFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnModifyPseudonymFile;
    }
}

