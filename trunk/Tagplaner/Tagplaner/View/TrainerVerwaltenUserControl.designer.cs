namespace Tagplaner
{
    partial class TrainerVerwaltenUserControl
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
            this.trainerAnzeigen = new System.Windows.Forms.GroupBox();
            this.trainerComboBox = new System.Windows.Forms.ComboBox();
            this.trainerLabel = new System.Windows.Forms.Label();
            this.trainerVerwalten = new System.Windows.Forms.GroupBox();
            this.externRadioButton = new System.Windows.Forms.RadioButton();
            this.internRadioButton = new System.Windows.Forms.RadioButton();
            this.zuerucksetzenButton = new System.Windows.Forms.Button();
            this.loeschenButton = new System.Windows.Forms.Button();
            this.speichernButton = new System.Windows.Forms.Button();
            this.kuerzelLabel = new System.Windows.Forms.Label();
            this.vornameLabel = new System.Windows.Forms.Label();
            this.kuerzelTextBox = new System.Windows.Forms.TextBox();
            this.vornameTextBox = new System.Windows.Forms.TextBox();
            this.nachnameTextBox = new System.Windows.Forms.TextBox();
            this.nachnameLabel = new System.Windows.Forms.Label();
            this.trainerAnzeigen.SuspendLayout();
            this.trainerVerwalten.SuspendLayout();
            this.SuspendLayout();
            // 
            // trainerAnzeigen
            // 
            this.trainerAnzeigen.Controls.Add(this.trainerComboBox);
            this.trainerAnzeigen.Controls.Add(this.trainerLabel);
            this.trainerAnzeigen.Dock = System.Windows.Forms.DockStyle.Top;
            this.trainerAnzeigen.Location = new System.Drawing.Point(0, 0);
            this.trainerAnzeigen.Name = "trainerAnzeigen";
            this.trainerAnzeigen.Size = new System.Drawing.Size(481, 100);
            this.trainerAnzeigen.TabIndex = 0;
            this.trainerAnzeigen.TabStop = false;
            this.trainerAnzeigen.Text = "Trainer anzeigen";
            // 
            // trainerComboBox
            // 
            this.trainerComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.trainerComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.trainerComboBox.FormattingEnabled = true;
            this.trainerComboBox.Location = new System.Drawing.Point(56, 39);
            this.trainerComboBox.Name = "trainerComboBox";
            this.trainerComboBox.Size = new System.Drawing.Size(179, 21);
            this.trainerComboBox.TabIndex = 1;
            this.trainerComboBox.SelectedIndexChanged += new System.EventHandler(this.TrainerComboBox_SelectedIndexChanged);
            // 
            // trainerLabel
            // 
            this.trainerLabel.AutoSize = true;
            this.trainerLabel.Location = new System.Drawing.Point(7, 39);
            this.trainerLabel.Name = "trainerLabel";
            this.trainerLabel.Size = new System.Drawing.Size(43, 13);
            this.trainerLabel.TabIndex = 0;
            this.trainerLabel.Text = "Trainer:";
            // 
            // trainerVerwalten
            // 
            this.trainerVerwalten.Controls.Add(this.externRadioButton);
            this.trainerVerwalten.Controls.Add(this.internRadioButton);
            this.trainerVerwalten.Controls.Add(this.zuerucksetzenButton);
            this.trainerVerwalten.Controls.Add(this.loeschenButton);
            this.trainerVerwalten.Controls.Add(this.speichernButton);
            this.trainerVerwalten.Controls.Add(this.kuerzelLabel);
            this.trainerVerwalten.Controls.Add(this.vornameLabel);
            this.trainerVerwalten.Controls.Add(this.kuerzelTextBox);
            this.trainerVerwalten.Controls.Add(this.vornameTextBox);
            this.trainerVerwalten.Controls.Add(this.nachnameTextBox);
            this.trainerVerwalten.Controls.Add(this.nachnameLabel);
            this.trainerVerwalten.Cursor = System.Windows.Forms.Cursors.Default;
            this.trainerVerwalten.Dock = System.Windows.Forms.DockStyle.Top;
            this.trainerVerwalten.Location = new System.Drawing.Point(0, 100);
            this.trainerVerwalten.Name = "trainerVerwalten";
            this.trainerVerwalten.Size = new System.Drawing.Size(481, 214);
            this.trainerVerwalten.TabIndex = 1;
            this.trainerVerwalten.TabStop = false;
            this.trainerVerwalten.Text = "Trainer verwalten";
            this.trainerVerwalten.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // externRadioButton
            // 
            this.externRadioButton.AutoSize = true;
            this.externRadioButton.Location = new System.Drawing.Point(101, 111);
            this.externRadioButton.Name = "externRadioButton";
            this.externRadioButton.Size = new System.Drawing.Size(55, 17);
            this.externRadioButton.TabIndex = 13;
            this.externRadioButton.TabStop = true;
            this.externRadioButton.Text = "Extern";
            this.externRadioButton.UseVisualStyleBackColor = true;
          
            // 
            // internRadioButton
            // 
            this.internRadioButton.AutoSize = true;
            this.internRadioButton.Location = new System.Drawing.Point(10, 111);
            this.internRadioButton.Name = "internRadioButton";
            this.internRadioButton.Size = new System.Drawing.Size(52, 17);
            this.internRadioButton.TabIndex = 12;
            this.internRadioButton.TabStop = true;
            this.internRadioButton.Text = "Intern";
            this.internRadioButton.UseVisualStyleBackColor = true;
            // 
            // zuerucksetzenButton
            // 
            this.zuerucksetzenButton.Location = new System.Drawing.Point(171, 155);
            this.zuerucksetzenButton.Name = "zuerucksetzenButton";
            this.zuerucksetzenButton.Size = new System.Drawing.Size(84, 23);
            this.zuerucksetzenButton.TabIndex = 11;
            this.zuerucksetzenButton.Text = "Zurücksetzen";
            this.zuerucksetzenButton.UseVisualStyleBackColor = true;
            this.zuerucksetzenButton.Click += new System.EventHandler(this.ZuruecksetzenButton_Click);
            // 
            // loeschenButton
            // 
            this.loeschenButton.Location = new System.Drawing.Point(89, 156);
            this.loeschenButton.Name = "loeschenButton";
            this.loeschenButton.Size = new System.Drawing.Size(75, 23);
            this.loeschenButton.TabIndex = 10;
            this.loeschenButton.Text = "Löschen";
            this.loeschenButton.UseVisualStyleBackColor = true;
            this.loeschenButton.Click += new System.EventHandler(this.LoeschenButton_Click);
            // 
            // speichernButton
            // 
            this.speichernButton.Location = new System.Drawing.Point(7, 157);
            this.speichernButton.Name = "speichernButton";
            this.speichernButton.Size = new System.Drawing.Size(75, 23);
            this.speichernButton.TabIndex = 9;
            this.speichernButton.Text = "Speichern";
            this.speichernButton.UseVisualStyleBackColor = true;
            this.speichernButton.Click += new System.EventHandler(this.SpeichernButton_Click);
            // 
            // kuerzelLabel
            // 
            this.kuerzelLabel.AutoSize = true;
            this.kuerzelLabel.Location = new System.Drawing.Point(4, 88);
            this.kuerzelLabel.Name = "kuerzelLabel";
            this.kuerzelLabel.Size = new System.Drawing.Size(36, 13);
            this.kuerzelLabel.TabIndex = 6;
            this.kuerzelLabel.Text = "Kürzel";
            // 
            // vornameLabel
            // 
            this.vornameLabel.AutoSize = true;
            this.vornameLabel.Location = new System.Drawing.Point(4, 61);
            this.vornameLabel.Name = "vornameLabel";
            this.vornameLabel.Size = new System.Drawing.Size(52, 13);
            this.vornameLabel.TabIndex = 5;
            this.vornameLabel.Text = "Vorname:";
            // 
            // kuerzelTextBox
            // 
            this.kuerzelTextBox.Location = new System.Drawing.Point(72, 85);
            this.kuerzelTextBox.Name = "kuerzelTextBox";
            this.kuerzelTextBox.Size = new System.Drawing.Size(163, 20);
            this.kuerzelTextBox.TabIndex = 3;
            // 
            // vornameTextBox
            // 
            this.vornameTextBox.Location = new System.Drawing.Point(72, 58);
            this.vornameTextBox.Name = "vornameTextBox";
            this.vornameTextBox.Size = new System.Drawing.Size(163, 20);
            this.vornameTextBox.TabIndex = 2;
            // 
            // nachnameTextBox
            // 
            this.nachnameTextBox.Location = new System.Drawing.Point(72, 32);
            this.nachnameTextBox.Name = "nachnameTextBox";
            this.nachnameTextBox.Size = new System.Drawing.Size(163, 20);
            this.nachnameTextBox.TabIndex = 1;
            // 
            // nachnameLabel
            // 
            this.nachnameLabel.AutoSize = true;
            this.nachnameLabel.Location = new System.Drawing.Point(4, 35);
            this.nachnameLabel.Name = "nachnameLabel";
            this.nachnameLabel.Size = new System.Drawing.Size(62, 13);
            this.nachnameLabel.TabIndex = 0;
            this.nachnameLabel.Text = "Nachname:";
            // 
            // TrainerVerwaltenUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trainerVerwalten);
            this.Controls.Add(this.trainerAnzeigen);
            this.Name = "TrainerVerwaltenUserControl";
            this.Size = new System.Drawing.Size(481, 344);
            this.trainerAnzeigen.ResumeLayout(false);
            this.trainerAnzeigen.PerformLayout();
            this.trainerVerwalten.ResumeLayout(false);
            this.trainerVerwalten.PerformLayout();
            this.ResumeLayout(false);

        }

        private void groupBox2_Enter(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.GroupBox trainerAnzeigen;
        private System.Windows.Forms.ComboBox trainerComboBox;
        private System.Windows.Forms.Label trainerLabel;
        private System.Windows.Forms.GroupBox trainerVerwalten;
        private System.Windows.Forms.Label kuerzelLabel;
        private System.Windows.Forms.Label vornameLabel;
        private System.Windows.Forms.TextBox kuerzelTextBox;
        private System.Windows.Forms.TextBox vornameTextBox;
        private System.Windows.Forms.TextBox nachnameTextBox;
        private System.Windows.Forms.Label nachnameLabel;
        private System.Windows.Forms.Button speichernButton;
        private System.Windows.Forms.Button zuerucksetzenButton;
        private System.Windows.Forms.Button loeschenButton;
        private System.Windows.Forms.RadioButton externRadioButton;
        private System.Windows.Forms.RadioButton internRadioButton;

    }
}
