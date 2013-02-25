namespace PseudonymMatching
{
    partial class Form1
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
            this.btnCheckPsudonym = new System.Windows.Forms.Button();
            this.btnOpenPseudonymFile = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenCsv
            // 
            this.btnOpenCsv.Location = new System.Drawing.Point(114, 455);
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
            // btnCheckPsudonym
            // 
            this.btnCheckPsudonym.Location = new System.Drawing.Point(609, 455);
            this.btnCheckPsudonym.Name = "btnCheckPsudonym";
            this.btnCheckPsudonym.Size = new System.Drawing.Size(148, 23);
            this.btnCheckPsudonym.TabIndex = 2;
            this.btnCheckPsudonym.Text = "Check Pseudonym";
            this.btnCheckPsudonym.UseVisualStyleBackColor = true;
            this.btnCheckPsudonym.Click += new System.EventHandler(this.btnCheckPsudonym_Click);
            // 
            // btnOpenPseudonymFile
            // 
            this.btnOpenPseudonymFile.Location = new System.Drawing.Point(369, 454);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 500);
            this.Controls.Add(this.btnOpenPseudonymFile);
            this.Controls.Add(this.btnCheckPsudonym);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOpenCsv);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpenCsv;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCheckPsudonym;
        private System.Windows.Forms.Button btnOpenPseudonymFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

