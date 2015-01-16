namespace Tagplaner
{
    partial class RaumVerwaltenUserControl
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
            this.raumAnzeigen = new System.Windows.Forms.GroupBox();
            this.seminarOrtComboBox = new System.Windows.Forms.ComboBox();
            this.seminarortLabel = new System.Windows.Forms.Label();
            this.raeumeLabel = new System.Windows.Forms.Label();
            this.raeumeComboBox = new System.Windows.Forms.ComboBox();
            this.raumBearbeiten = new System.Windows.Forms.GroupBox();
            this.zuruecksetzenButton = new System.Windows.Forms.Button();
            this.loeschenButton = new System.Windows.Forms.Button();
            this.speichernButton = new System.Windows.Forms.Button();
            this.raumTextBox = new System.Windows.Forms.TextBox();
            this.raumLabel = new System.Windows.Forms.Label();
            this.raumAnzeigen.SuspendLayout();
            this.raumBearbeiten.SuspendLayout();
            this.SuspendLayout();
            // 
            // raumAnzeigen
            // 
            this.raumAnzeigen.Controls.Add(this.seminarOrtComboBox);
            this.raumAnzeigen.Controls.Add(this.seminarortLabel);
            this.raumAnzeigen.Controls.Add(this.raeumeLabel);
            this.raumAnzeigen.Controls.Add(this.raeumeComboBox);
            this.raumAnzeigen.Dock = System.Windows.Forms.DockStyle.Top;
            this.raumAnzeigen.Location = new System.Drawing.Point(0, 0);
            this.raumAnzeigen.Name = "raumAnzeigen";
            this.raumAnzeigen.Size = new System.Drawing.Size(640, 77);
            this.raumAnzeigen.TabIndex = 0;
            this.raumAnzeigen.TabStop = false;
            this.raumAnzeigen.Text = "Räume anzeigen";
            // 
            // seminarOrtComboBox
            // 
            this.seminarOrtComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.seminarOrtComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.seminarOrtComboBox.FormattingEnabled = true;
            this.seminarOrtComboBox.Location = new System.Drawing.Point(89, 30);
            this.seminarOrtComboBox.Name = "seminarOrtComboBox";
            this.seminarOrtComboBox.Size = new System.Drawing.Size(168, 21);
            this.seminarOrtComboBox.TabIndex = 3;
            this.seminarOrtComboBox.Text = "bitte Seminarort wählen!";
            this.seminarOrtComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // seminarortLabel
            // 
            this.seminarortLabel.AutoSize = true;
            this.seminarortLabel.Location = new System.Drawing.Point(19, 30);
            this.seminarortLabel.Name = "seminarortLabel";
            this.seminarortLabel.Size = new System.Drawing.Size(60, 13);
            this.seminarortLabel.TabIndex = 2;
            this.seminarortLabel.Text = "Seminarort:";
            // 
            // raeumeLabel
            // 
            this.raeumeLabel.AutoSize = true;
            this.raeumeLabel.Location = new System.Drawing.Point(329, 30);
            this.raeumeLabel.Name = "raeumeLabel";
            this.raeumeLabel.Size = new System.Drawing.Size(44, 13);
            this.raeumeLabel.TabIndex = 1;
            this.raeumeLabel.Text = "Räume:";
            // 
            // raeumeComboBox
            // 
            this.raeumeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.raeumeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.raeumeComboBox.FormattingEnabled = true;
            this.raeumeComboBox.Location = new System.Drawing.Point(391, 27);
            this.raeumeComboBox.Name = "raeumeComboBox";
            this.raeumeComboBox.Size = new System.Drawing.Size(177, 21);
            this.raeumeComboBox.TabIndex = 0;
            this.raeumeComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // raumBearbeiten
            // 
            this.raumBearbeiten.Controls.Add(this.zuruecksetzenButton);
            this.raumBearbeiten.Controls.Add(this.loeschenButton);
            this.raumBearbeiten.Controls.Add(this.speichernButton);
            this.raumBearbeiten.Controls.Add(this.raumTextBox);
            this.raumBearbeiten.Controls.Add(this.raumLabel);
            this.raumBearbeiten.Dock = System.Windows.Forms.DockStyle.Top;
            this.raumBearbeiten.Location = new System.Drawing.Point(0, 77);
            this.raumBearbeiten.Name = "raumBearbeiten";
            this.raumBearbeiten.Size = new System.Drawing.Size(640, 196);
            this.raumBearbeiten.TabIndex = 1;
            this.raumBearbeiten.TabStop = false;
            this.raumBearbeiten.Text = "Raum bearbeiten";
            this.raumBearbeiten.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // zuruecksetzenButton
            // 
            this.zuruecksetzenButton.Location = new System.Drawing.Point(169, 71);
            this.zuruecksetzenButton.Name = "zuruecksetzenButton";
            this.zuruecksetzenButton.Size = new System.Drawing.Size(88, 23);
            this.zuruecksetzenButton.TabIndex = 5;
            this.zuruecksetzenButton.Text = "Zurücksetzen";
            this.zuruecksetzenButton.UseVisualStyleBackColor = true;
            this.zuruecksetzenButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // loeschenButton
            // 
            this.loeschenButton.Location = new System.Drawing.Point(88, 71);
            this.loeschenButton.Name = "loeschenButton";
            this.loeschenButton.Size = new System.Drawing.Size(75, 23);
            this.loeschenButton.TabIndex = 4;
            this.loeschenButton.Text = "Löschen";
            this.loeschenButton.UseVisualStyleBackColor = true;
            this.loeschenButton.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // speichernButton
            // 
            this.speichernButton.Location = new System.Drawing.Point(7, 71);
            this.speichernButton.Name = "speichernButton";
            this.speichernButton.Size = new System.Drawing.Size(75, 23);
            this.speichernButton.TabIndex = 2;
            this.speichernButton.Text = "Speichern";
            this.speichernButton.UseVisualStyleBackColor = true;
            this.speichernButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // raumTextBox
            // 
            this.raumTextBox.Location = new System.Drawing.Point(89, 33);
            this.raumTextBox.Name = "raumTextBox";
            this.raumTextBox.Size = new System.Drawing.Size(168, 20);
            this.raumTextBox.TabIndex = 1;
            // 
            // raumLabel
            // 
            this.raumLabel.AutoSize = true;
            this.raumLabel.Location = new System.Drawing.Point(7, 36);
            this.raumLabel.Name = "raumLabel";
            this.raumLabel.Size = new System.Drawing.Size(75, 13);
            this.raumLabel.TabIndex = 0;
            this.raumLabel.Text = "Raumnummer:";
            // 
            // RaumVerwaltenUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.raumBearbeiten);
            this.Controls.Add(this.raumAnzeigen);
            this.Name = "RaumVerwaltenUserControl";
            this.Size = new System.Drawing.Size(640, 341);
            this.Load += new System.EventHandler(this.RaumVerwaltenUserControl_Load);
            this.raumAnzeigen.ResumeLayout(false);
            this.raumAnzeigen.PerformLayout();
            this.raumBearbeiten.ResumeLayout(false);
            this.raumBearbeiten.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox raumAnzeigen;
        private System.Windows.Forms.GroupBox raumBearbeiten;
        private System.Windows.Forms.Label raeumeLabel;
        private System.Windows.Forms.ComboBox raeumeComboBox;
        private System.Windows.Forms.TextBox raumTextBox;
        private System.Windows.Forms.Label raumLabel;
        private System.Windows.Forms.Button zuruecksetzenButton;
        private System.Windows.Forms.Button loeschenButton;
        private System.Windows.Forms.Button speichernButton;
        private System.Windows.Forms.ComboBox seminarOrtComboBox;
        private System.Windows.Forms.Label seminarortLabel;

    }
}
