namespace Tagplaner
{
    partial class SeminarVerwaltenUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.seminarAuswaehlen = new System.Windows.Forms.GroupBox();
            this.seminarLabel = new System.Windows.Forms.Label();
            this.seminarComboBox = new System.Windows.Forms.ComboBox();
            this.seminarVerwalten = new System.Windows.Forms.GroupBox();
            this.loeschenButton = new System.Windows.Forms.Button();
            this.zuruecksetzenButton = new System.Windows.Forms.Button();
            this.speichernButton = new System.Windows.Forms.Button();
            this.technikTextBox = new System.Windows.Forms.TextBox();
            this.untertitelTextBox = new System.Windows.Forms.TextBox();
            this.kuerzelTextBox = new System.Windows.Forms.TextBox();
            this.titelTextBox = new System.Windows.Forms.TextBox();
            this.technikLabel = new System.Windows.Forms.Label();
            this.kuerzelLabel = new System.Windows.Forms.Label();
            this.untertitelLabel = new System.Windows.Forms.Label();
            this.titelLabel = new System.Windows.Forms.Label();
            this.seminarAuswaehlen.SuspendLayout();
            this.seminarVerwalten.SuspendLayout();
            this.SuspendLayout();
            // 
            // seminarAuswaehlen
            // 
            this.seminarAuswaehlen.Controls.Add(this.seminarLabel);
            this.seminarAuswaehlen.Controls.Add(this.seminarComboBox);
            this.seminarAuswaehlen.Dock = System.Windows.Forms.DockStyle.Top;
            this.seminarAuswaehlen.Location = new System.Drawing.Point(0, 0);
            this.seminarAuswaehlen.Name = "seminarAuswaehlen";
            this.seminarAuswaehlen.Size = new System.Drawing.Size(513, 73);
            this.seminarAuswaehlen.TabIndex = 0;
            this.seminarAuswaehlen.TabStop = false;
            this.seminarAuswaehlen.Text = "Seminar auswählen";
            // 
            // seminarLabel
            // 
            this.seminarLabel.AutoSize = true;
            this.seminarLabel.Location = new System.Drawing.Point(3, 28);
            this.seminarLabel.Name = "seminarLabel";
            this.seminarLabel.Size = new System.Drawing.Size(48, 13);
            this.seminarLabel.TabIndex = 1;
            this.seminarLabel.Text = "Seminar:";
            // 
            // seminarComboBox
            // 
            this.seminarComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.seminarComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.seminarComboBox.FormattingEnabled = true;
            this.seminarComboBox.Location = new System.Drawing.Point(88, 28);
            this.seminarComboBox.Name = "seminarComboBox";
            this.seminarComboBox.Size = new System.Drawing.Size(404, 21);
            this.seminarComboBox.TabIndex = 0;
            this.seminarComboBox.SelectedIndexChanged += new System.EventHandler(this.SeminarComboBox_SelectedIndexChanged);
            this.seminarComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seminarComboBox_KeyPress);
            // 
            // seminarVerwalten
            // 
            this.seminarVerwalten.Controls.Add(this.loeschenButton);
            this.seminarVerwalten.Controls.Add(this.zuruecksetzenButton);
            this.seminarVerwalten.Controls.Add(this.speichernButton);
            this.seminarVerwalten.Controls.Add(this.technikTextBox);
            this.seminarVerwalten.Controls.Add(this.untertitelTextBox);
            this.seminarVerwalten.Controls.Add(this.kuerzelTextBox);
            this.seminarVerwalten.Controls.Add(this.titelTextBox);
            this.seminarVerwalten.Controls.Add(this.technikLabel);
            this.seminarVerwalten.Controls.Add(this.kuerzelLabel);
            this.seminarVerwalten.Controls.Add(this.untertitelLabel);
            this.seminarVerwalten.Controls.Add(this.titelLabel);
            this.seminarVerwalten.Dock = System.Windows.Forms.DockStyle.Top;
            this.seminarVerwalten.Location = new System.Drawing.Point(0, 73);
            this.seminarVerwalten.Name = "seminarVerwalten";
            this.seminarVerwalten.Size = new System.Drawing.Size(513, 215);
            this.seminarVerwalten.TabIndex = 1;
            this.seminarVerwalten.TabStop = false;
            this.seminarVerwalten.Text = "Seminar verwalten";
            // 
            // loeschenButton
            // 
            this.loeschenButton.Location = new System.Drawing.Point(332, 177);
            this.loeschenButton.Name = "loeschenButton";
            this.loeschenButton.Size = new System.Drawing.Size(75, 23);
            this.loeschenButton.TabIndex = 8;
            this.loeschenButton.Text = "Löschen";
            this.loeschenButton.UseVisualStyleBackColor = true;
            this.loeschenButton.Click += new System.EventHandler(this.LoeschenButton_Click);
            // 
            // zuruecksetzenButton
            // 
            this.zuruecksetzenButton.Location = new System.Drawing.Point(413, 177);
            this.zuruecksetzenButton.Name = "zuruecksetzenButton";
            this.zuruecksetzenButton.Size = new System.Drawing.Size(87, 23);
            this.zuruecksetzenButton.TabIndex = 9;
            this.zuruecksetzenButton.Text = "Zurücksetzen";
            this.zuruecksetzenButton.UseVisualStyleBackColor = true;
            this.zuruecksetzenButton.Click += new System.EventHandler(this.ZuruecksetzenButton_Click);
            // 
            // speichernButton
            // 
            this.speichernButton.Location = new System.Drawing.Point(251, 177);
            this.speichernButton.Name = "speichernButton";
            this.speichernButton.Size = new System.Drawing.Size(75, 23);
            this.speichernButton.TabIndex = 6;
            this.speichernButton.Text = "Speichern";
            this.speichernButton.UseVisualStyleBackColor = true;
            this.speichernButton.Click += new System.EventHandler(this.SpeichernButton_Click);
            // 
            // technikTextBox
            // 
            this.technikTextBox.Location = new System.Drawing.Point(87, 103);
            this.technikTextBox.Name = "technikTextBox";
            this.technikTextBox.Size = new System.Drawing.Size(405, 20);
            this.technikTextBox.TabIndex = 4;
            // 
            // untertitelTextBox
            // 
            this.untertitelTextBox.Location = new System.Drawing.Point(87, 51);
            this.untertitelTextBox.Name = "untertitelTextBox";
            this.untertitelTextBox.Size = new System.Drawing.Size(405, 20);
            this.untertitelTextBox.TabIndex = 2;
            // 
            // kuerzelTextBox
            // 
            this.kuerzelTextBox.Location = new System.Drawing.Point(87, 77);
            this.kuerzelTextBox.Name = "kuerzelTextBox";
            this.kuerzelTextBox.Size = new System.Drawing.Size(405, 20);
            this.kuerzelTextBox.TabIndex = 3;
            // 
            // titelTextBox
            // 
            this.titelTextBox.Location = new System.Drawing.Point(87, 25);
            this.titelTextBox.Name = "titelTextBox";
            this.titelTextBox.Size = new System.Drawing.Size(405, 20);
            this.titelTextBox.TabIndex = 1;
            this.titelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.titelTextBox_KeyPress);
            // 
            // technikLabel
            // 
            this.technikLabel.AutoSize = true;
            this.technikLabel.Location = new System.Drawing.Point(6, 103);
            this.technikLabel.Name = "technikLabel";
            this.technikLabel.Size = new System.Drawing.Size(49, 13);
            this.technikLabel.TabIndex = 3;
            this.technikLabel.Text = "Technik:";
            // 
            // kuerzelLabel
            // 
            this.kuerzelLabel.AutoSize = true;
            this.kuerzelLabel.Location = new System.Drawing.Point(6, 77);
            this.kuerzelLabel.Name = "kuerzelLabel";
            this.kuerzelLabel.Size = new System.Drawing.Size(39, 13);
            this.kuerzelLabel.TabIndex = 2;
            this.kuerzelLabel.Text = "Kürzel:";
            // 
            // untertitelLabel
            // 
            this.untertitelLabel.AutoSize = true;
            this.untertitelLabel.Location = new System.Drawing.Point(6, 51);
            this.untertitelLabel.Name = "untertitelLabel";
            this.untertitelLabel.Size = new System.Drawing.Size(52, 13);
            this.untertitelLabel.TabIndex = 1;
            this.untertitelLabel.Text = "Untertitel:";
            // 
            // titelLabel
            // 
            this.titelLabel.AutoSize = true;
            this.titelLabel.Location = new System.Drawing.Point(6, 25);
            this.titelLabel.Name = "titelLabel";
            this.titelLabel.Size = new System.Drawing.Size(30, 13);
            this.titelLabel.TabIndex = 0;
            this.titelLabel.Text = "Titel:";
            // 
            // SeminarVerwaltenUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.seminarVerwalten);
            this.Controls.Add(this.seminarAuswaehlen);
            this.Name = "SeminarVerwaltenUserControl";
            this.Size = new System.Drawing.Size(513, 365);
            this.seminarAuswaehlen.ResumeLayout(false);
            this.seminarAuswaehlen.PerformLayout();
            this.seminarVerwalten.ResumeLayout(false);
            this.seminarVerwalten.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox seminarAuswaehlen;
        private System.Windows.Forms.GroupBox seminarVerwalten;
        private System.Windows.Forms.Label seminarLabel;
        private System.Windows.Forms.ComboBox seminarComboBox;
        private System.Windows.Forms.TextBox technikTextBox;
        private System.Windows.Forms.TextBox untertitelTextBox;
        private System.Windows.Forms.TextBox kuerzelTextBox;
        private System.Windows.Forms.TextBox titelTextBox;
        private System.Windows.Forms.Label technikLabel;
        private System.Windows.Forms.Label kuerzelLabel;
        private System.Windows.Forms.Label untertitelLabel;
        private System.Windows.Forms.Label titelLabel;
        private System.Windows.Forms.Button speichernButton;
        private System.Windows.Forms.Button zuruecksetzenButton;
        private System.Windows.Forms.Button loeschenButton;
    }
}
