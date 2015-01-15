namespace Tagplaner
{
    partial class TagplanAnlegenUserControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.dateTimePickerVon = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerBis = new System.Windows.Forms.DateTimePicker();
            this.checkBoxErsterJahrgangSI = new System.Windows.Forms.CheckBox();
            this.checkBoxErsterJahrgangAE = new System.Windows.Forms.CheckBox();
            this.comboBoxBundesland = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxZweiterJahrgangSI = new System.Windows.Forms.CheckBox();
            this.checkBoxZweiterJahrgangAE = new System.Windows.Forms.CheckBox();
            this.buttonWeiter = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_ferienOeffnen = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxDritterJahrgangSI = new System.Windows.Forms.CheckBox();
            this.checkBoxDritterJahrgangAE = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBoxVierterJahrgangSI = new System.Windows.Forms.CheckBox();
            this.checkBoxVierterJahrgangAE = new System.Windows.Forms.CheckBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Von";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bis";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(88, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(414, 55);
            this.panel1.TabIndex = 3;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(112, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(31, 17);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged_1);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(8, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(31, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // dateTimePickerVon
            // 
            this.dateTimePickerVon.Location = new System.Drawing.Point(88, 27);
            this.dateTimePickerVon.Name = "dateTimePickerVon";
            this.dateTimePickerVon.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerVon.TabIndex = 4;
            // 
            // dateTimePickerBis
            // 
            this.dateTimePickerBis.Location = new System.Drawing.Point(88, 53);
            this.dateTimePickerBis.Name = "dateTimePickerBis";
            this.dateTimePickerBis.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerBis.TabIndex = 5;
            this.dateTimePickerBis.ValueChanged += new System.EventHandler(this.dateTimePickerBis_ValueChanged);
            // 
            // checkBoxErsterJahrgangSI
            // 
            this.checkBoxErsterJahrgangSI.AutoSize = true;
            this.checkBoxErsterJahrgangSI.Location = new System.Drawing.Point(6, 40);
            this.checkBoxErsterJahrgangSI.Name = "checkBoxErsterJahrgangSI";
            this.checkBoxErsterJahrgangSI.Size = new System.Drawing.Size(36, 17);
            this.checkBoxErsterJahrgangSI.TabIndex = 10;
            this.checkBoxErsterJahrgangSI.Text = "SI";
            this.checkBoxErsterJahrgangSI.UseVisualStyleBackColor = true;
            // 
            // checkBoxErsterJahrgangAE
            // 
            this.checkBoxErsterJahrgangAE.AutoSize = true;
            this.checkBoxErsterJahrgangAE.Location = new System.Drawing.Point(6, 19);
            this.checkBoxErsterJahrgangAE.Name = "checkBoxErsterJahrgangAE";
            this.checkBoxErsterJahrgangAE.Size = new System.Drawing.Size(40, 17);
            this.checkBoxErsterJahrgangAE.TabIndex = 9;
            this.checkBoxErsterJahrgangAE.Text = "AE";
            this.checkBoxErsterJahrgangAE.UseVisualStyleBackColor = true;
            // 
            // comboBoxBundesland
            // 
            this.comboBoxBundesland.FormattingEnabled = true;
            this.comboBoxBundesland.Location = new System.Drawing.Point(88, 80);
            this.comboBoxBundesland.Name = "comboBoxBundesland";
            this.comboBoxBundesland.Size = new System.Drawing.Size(200, 21);
            this.comboBoxBundesland.TabIndex = 7;
            this.comboBoxBundesland.Text = "Bitte Bundesland wählen";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Bundesland";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxErsterJahrgangSI);
            this.groupBox1.Controls.Add(this.checkBoxErsterJahrgangAE);
            this.groupBox1.Location = new System.Drawing.Point(88, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(94, 65);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jahrgang 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxZweiterJahrgangSI);
            this.groupBox2.Controls.Add(this.checkBoxZweiterJahrgangAE);
            this.groupBox2.Location = new System.Drawing.Point(194, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(94, 65);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Jahrgang 2";
            this.groupBox2.Visible = false;
            // 
            // checkBoxZweiterJahrgangSI
            // 
            this.checkBoxZweiterJahrgangSI.AutoSize = true;
            this.checkBoxZweiterJahrgangSI.Location = new System.Drawing.Point(6, 40);
            this.checkBoxZweiterJahrgangSI.Name = "checkBoxZweiterJahrgangSI";
            this.checkBoxZweiterJahrgangSI.Size = new System.Drawing.Size(36, 17);
            this.checkBoxZweiterJahrgangSI.TabIndex = 10;
            this.checkBoxZweiterJahrgangSI.Text = "SI";
            this.checkBoxZweiterJahrgangSI.UseVisualStyleBackColor = true;
            // 
            // checkBoxZweiterJahrgangAE
            // 
            this.checkBoxZweiterJahrgangAE.AutoSize = true;
            this.checkBoxZweiterJahrgangAE.Location = new System.Drawing.Point(6, 19);
            this.checkBoxZweiterJahrgangAE.Name = "checkBoxZweiterJahrgangAE";
            this.checkBoxZweiterJahrgangAE.Size = new System.Drawing.Size(40, 17);
            this.checkBoxZweiterJahrgangAE.TabIndex = 9;
            this.checkBoxZweiterJahrgangAE.Text = "AE";
            this.checkBoxZweiterJahrgangAE.UseVisualStyleBackColor = true;
            // 
            // buttonWeiter
            // 
            this.buttonWeiter.Enabled = false;
            this.buttonWeiter.Location = new System.Drawing.Point(429, 358);
            this.buttonWeiter.Name = "buttonWeiter";
            this.buttonWeiter.Size = new System.Drawing.Size(73, 23);
            this.buttonWeiter.TabIndex = 14;
            this.buttonWeiter.Text = "Weiter";
            this.buttonWeiter.UseVisualStyleBackColor = true;
            this.buttonWeiter.Click += new System.EventHandler(this.buttonWeiter_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btn_ferienOeffnen);
            this.groupBox4.Location = new System.Drawing.Point(12, 239);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(490, 111);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ferien- und Feiertagedatei öffnen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // btn_ferienOeffnen
            // 
            this.btn_ferienOeffnen.Location = new System.Drawing.Point(13, 27);
            this.btn_ferienOeffnen.Name = "btn_ferienOeffnen";
            this.btn_ferienOeffnen.Size = new System.Drawing.Size(75, 23);
            this.btn_ferienOeffnen.TabIndex = 15;
            this.btn_ferienOeffnen.Text = "Öffnen";
            this.btn_ferienOeffnen.UseVisualStyleBackColor = true;
            this.btn_ferienOeffnen.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxDritterJahrgangSI);
            this.groupBox3.Controls.Add(this.checkBoxDritterJahrgangAE);
            this.groupBox3.Location = new System.Drawing.Point(301, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(94, 65);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Jahrgang 3";
            this.groupBox3.Visible = false;
            // 
            // checkBoxDritterJahrgangSI
            // 
            this.checkBoxDritterJahrgangSI.AutoSize = true;
            this.checkBoxDritterJahrgangSI.Location = new System.Drawing.Point(6, 40);
            this.checkBoxDritterJahrgangSI.Name = "checkBoxDritterJahrgangSI";
            this.checkBoxDritterJahrgangSI.Size = new System.Drawing.Size(36, 17);
            this.checkBoxDritterJahrgangSI.TabIndex = 10;
            this.checkBoxDritterJahrgangSI.Text = "SI";
            this.checkBoxDritterJahrgangSI.UseVisualStyleBackColor = true;
            // 
            // checkBoxDritterJahrgangAE
            // 
            this.checkBoxDritterJahrgangAE.AutoSize = true;
            this.checkBoxDritterJahrgangAE.Location = new System.Drawing.Point(6, 19);
            this.checkBoxDritterJahrgangAE.Name = "checkBoxDritterJahrgangAE";
            this.checkBoxDritterJahrgangAE.Size = new System.Drawing.Size(40, 17);
            this.checkBoxDritterJahrgangAE.TabIndex = 9;
            this.checkBoxDritterJahrgangAE.Text = "AE";
            this.checkBoxDritterJahrgangAE.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBoxVierterJahrgangSI);
            this.groupBox5.Controls.Add(this.checkBoxVierterJahrgangAE);
            this.groupBox5.Location = new System.Drawing.Point(408, 168);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(94, 65);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Jahrgang 4";
            this.groupBox5.Visible = false;
            // 
            // checkBoxVierterJahrgangSI
            // 
            this.checkBoxVierterJahrgangSI.AutoSize = true;
            this.checkBoxVierterJahrgangSI.Location = new System.Drawing.Point(6, 40);
            this.checkBoxVierterJahrgangSI.Name = "checkBoxVierterJahrgangSI";
            this.checkBoxVierterJahrgangSI.Size = new System.Drawing.Size(36, 17);
            this.checkBoxVierterJahrgangSI.TabIndex = 10;
            this.checkBoxVierterJahrgangSI.Text = "SI";
            this.checkBoxVierterJahrgangSI.UseVisualStyleBackColor = true;
            // 
            // checkBoxVierterJahrgangAE
            // 
            this.checkBoxVierterJahrgangAE.AutoSize = true;
            this.checkBoxVierterJahrgangAE.Location = new System.Drawing.Point(6, 19);
            this.checkBoxVierterJahrgangAE.Name = "checkBoxVierterJahrgangAE";
            this.checkBoxVierterJahrgangAE.Size = new System.Drawing.Size(40, 17);
            this.checkBoxVierterJahrgangAE.TabIndex = 9;
            this.checkBoxVierterJahrgangAE.Text = "AE";
            this.checkBoxVierterJahrgangAE.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(219, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(31, 17);
            this.radioButton3.TabIndex = 6;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(327, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(31, 17);
            this.radioButton4.TabIndex = 7;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "4";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Anzahl der Jahrgänge";
            // 
            // TagplanAnlegenUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.buttonWeiter);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxBundesland);
            this.Controls.Add(this.dateTimePickerBis);
            this.Controls.Add(this.dateTimePickerVon);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TagplanAnlegenUserControl";
            this.Size = new System.Drawing.Size(515, 398);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePickerVon;
        private System.Windows.Forms.DateTimePicker dateTimePickerBis;
        private System.Windows.Forms.CheckBox checkBoxErsterJahrgangSI;
        private System.Windows.Forms.CheckBox checkBoxErsterJahrgangAE;
        private System.Windows.Forms.ComboBox comboBoxBundesland;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxZweiterJahrgangSI;
        private System.Windows.Forms.CheckBox checkBoxZweiterJahrgangAE;
        private System.Windows.Forms.Button buttonWeiter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_ferienOeffnen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxDritterJahrgangSI;
        private System.Windows.Forms.CheckBox checkBoxDritterJahrgangAE;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBoxVierterJahrgangSI;
        private System.Windows.Forms.CheckBox checkBoxVierterJahrgangAE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}
