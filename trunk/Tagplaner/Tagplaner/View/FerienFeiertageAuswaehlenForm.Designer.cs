namespace Tagplaner.View
{
    partial class FerienFeiertageAuswaehlenForm
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
            this.button_HolidayCurrentYear = new System.Windows.Forms.Button();
            this.button_HolidayNextYear = new System.Windows.Forms.Button();
            this.button_VacationCurrentYear = new System.Windows.Forms.Button();
            this.button_VacationNextYear = new System.Windows.Forms.Button();
            this.textBox_HolidayCurrentYear = new System.Windows.Forms.TextBox();
            this.textBox_HolidayNextYear = new System.Windows.Forms.TextBox();
            this.textBox_VacationCurrentYear = new System.Windows.Forms.TextBox();
            this.textBox_VacationNextYear = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog4 = new System.Windows.Forms.OpenFileDialog();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Abbrechen = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_HolidayCurrentYear
            // 
            this.button_HolidayCurrentYear.Location = new System.Drawing.Point(365, 15);
            this.button_HolidayCurrentYear.Name = "button_HolidayCurrentYear";
            this.button_HolidayCurrentYear.Size = new System.Drawing.Size(147, 23);
            this.button_HolidayCurrentYear.TabIndex = 0;
            this.button_HolidayCurrentYear.Text = "Durchsuchen";
            this.button_HolidayCurrentYear.UseVisualStyleBackColor = true;
            this.button_HolidayCurrentYear.Click += new System.EventHandler(this.button_HolidayCurrentYear_Click);
            // 
            // button_HolidayNextYear
            // 
            this.button_HolidayNextYear.Location = new System.Drawing.Point(365, 44);
            this.button_HolidayNextYear.Name = "button_HolidayNextYear";
            this.button_HolidayNextYear.Size = new System.Drawing.Size(147, 23);
            this.button_HolidayNextYear.TabIndex = 1;
            this.button_HolidayNextYear.Text = "Durchsuchen";
            this.button_HolidayNextYear.UseVisualStyleBackColor = true;
            this.button_HolidayNextYear.Click += new System.EventHandler(this.button_HolidayNextYear_Click);
            // 
            // button_VacationCurrentYear
            // 
            this.button_VacationCurrentYear.Location = new System.Drawing.Point(365, 73);
            this.button_VacationCurrentYear.Name = "button_VacationCurrentYear";
            this.button_VacationCurrentYear.Size = new System.Drawing.Size(147, 23);
            this.button_VacationCurrentYear.TabIndex = 2;
            this.button_VacationCurrentYear.Text = "Durchsuchen";
            this.button_VacationCurrentYear.UseVisualStyleBackColor = true;
            this.button_VacationCurrentYear.Click += new System.EventHandler(this.button_VacationCurrentYear_Click);
            // 
            // button_VacationNextYear
            // 
            this.button_VacationNextYear.Location = new System.Drawing.Point(365, 102);
            this.button_VacationNextYear.Name = "button_VacationNextYear";
            this.button_VacationNextYear.Size = new System.Drawing.Size(147, 23);
            this.button_VacationNextYear.TabIndex = 3;
            this.button_VacationNextYear.Text = "Durchsuchen";
            this.button_VacationNextYear.UseVisualStyleBackColor = true;
            this.button_VacationNextYear.Click += new System.EventHandler(this.button_VacationNextYear_Click);
            // 
            // textBox_HolidayCurrentYear
            // 
            this.textBox_HolidayCurrentYear.Enabled = false;
            this.textBox_HolidayCurrentYear.Location = new System.Drawing.Point(37, 17);
            this.textBox_HolidayCurrentYear.Name = "textBox_HolidayCurrentYear";
            this.textBox_HolidayCurrentYear.Size = new System.Drawing.Size(321, 20);
            this.textBox_HolidayCurrentYear.TabIndex = 8;
            this.textBox_HolidayCurrentYear.Text = "Feiertagedatei des aktuellen Jahres auswählen";
            // 
            // textBox_HolidayNextYear
            // 
            this.textBox_HolidayNextYear.Enabled = false;
            this.textBox_HolidayNextYear.Location = new System.Drawing.Point(37, 46);
            this.textBox_HolidayNextYear.Name = "textBox_HolidayNextYear";
            this.textBox_HolidayNextYear.Size = new System.Drawing.Size(321, 20);
            this.textBox_HolidayNextYear.TabIndex = 9;
            this.textBox_HolidayNextYear.Text = "Feiertagedatei des nächsten Jahres auswählen";
            // 
            // textBox_VacationCurrentYear
            // 
            this.textBox_VacationCurrentYear.Enabled = false;
            this.textBox_VacationCurrentYear.Location = new System.Drawing.Point(37, 74);
            this.textBox_VacationCurrentYear.Name = "textBox_VacationCurrentYear";
            this.textBox_VacationCurrentYear.Size = new System.Drawing.Size(321, 20);
            this.textBox_VacationCurrentYear.TabIndex = 10;
            this.textBox_VacationCurrentYear.Text = "Feriendatei des aktuellen Jahres auswählen";
            // 
            // textBox_VacationNextYear
            // 
            this.textBox_VacationNextYear.Enabled = false;
            this.textBox_VacationNextYear.Location = new System.Drawing.Point(37, 104);
            this.textBox_VacationNextYear.Name = "textBox_VacationNextYear";
            this.textBox_VacationNextYear.Size = new System.Drawing.Size(321, 20);
            this.textBox_VacationNextYear.TabIndex = 11;
            this.textBox_VacationNextYear.Text = "Feriendatei des nächsten Jahres auswählen";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.ShowHelp = true;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.ShowHelp = true;
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog3";
            this.openFileDialog3.ShowHelp = true;
            // 
            // openFileDialog4
            // 
            this.openFileDialog4.FileName = "openFileDialog4";
            this.openFileDialog4.ShowHelp = true;
            // 
            // button_OK
            // 
            this.button_OK.Enabled = false;
            this.button_OK.Location = new System.Drawing.Point(364, 150);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(147, 23);
            this.button_OK.TabIndex = 12;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Abbrechen
            // 
            this.button_Abbrechen.Location = new System.Drawing.Point(211, 150);
            this.button_Abbrechen.Name = "button_Abbrechen";
            this.button_Abbrechen.Size = new System.Drawing.Size(147, 23);
            this.button_Abbrechen.TabIndex = 13;
            this.button_Abbrechen.Text = "Abbrechen";
            this.button_Abbrechen.UseVisualStyleBackColor = true;
            this.button_Abbrechen.Click += new System.EventHandler(this.button_Abbrechen_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 186);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(561, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "test";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // FerienFeiertageAuswaehlenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 208);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_Abbrechen);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_VacationNextYear);
            this.Controls.Add(this.textBox_VacationCurrentYear);
            this.Controls.Add(this.textBox_HolidayNextYear);
            this.Controls.Add(this.textBox_HolidayCurrentYear);
            this.Controls.Add(this.button_VacationNextYear);
            this.Controls.Add(this.button_VacationCurrentYear);
            this.Controls.Add(this.button_HolidayNextYear);
            this.Controls.Add(this.button_HolidayCurrentYear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FerienFeiertageAuswaehlenForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ferien- und Feiertagedateien auswählen";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_HolidayCurrentYear;
        private System.Windows.Forms.Button button_HolidayNextYear;
        private System.Windows.Forms.Button button_VacationCurrentYear;
        private System.Windows.Forms.Button button_VacationNextYear;
        private System.Windows.Forms.TextBox textBox_HolidayCurrentYear;
        private System.Windows.Forms.TextBox textBox_HolidayNextYear;
        private System.Windows.Forms.TextBox textBox_VacationCurrentYear;
        private System.Windows.Forms.TextBox textBox_VacationNextYear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.OpenFileDialog openFileDialog4;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Abbrechen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        }
}