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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_ferienOeffnen = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_feiertageOeffnen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(88, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 56);
            this.panel1.TabIndex = 3;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(9, 26);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(84, 17);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2 Jahrgänge";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged_1);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(78, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1 Jahrgang";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePickerVon.Location = new System.Drawing.Point(88, 27);
            this.dateTimePickerVon.Name = "dateTimePicker1";
            this.dateTimePickerVon.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerVon.TabIndex = 4;
            // 
            // dateTimePicker2
            // 
            this.dateTimePickerBis.Location = new System.Drawing.Point(88, 53);
            this.dateTimePickerBis.Name = "dateTimePicker2";
            this.dateTimePickerBis.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerBis.TabIndex = 5;
            // 
            // checkBox4
            // 
            this.checkBoxErsterJahrgangSI.AutoSize = true;
            this.checkBoxErsterJahrgangSI.Location = new System.Drawing.Point(6, 40);
            this.checkBoxErsterJahrgangSI.Name = "checkBox4";
            this.checkBoxErsterJahrgangSI.Size = new System.Drawing.Size(36, 17);
            this.checkBoxErsterJahrgangSI.TabIndex = 10;
            this.checkBoxErsterJahrgangSI.Text = "SI";
            this.checkBoxErsterJahrgangSI.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBoxErsterJahrgangAE.AutoSize = true;
            this.checkBoxErsterJahrgangAE.Location = new System.Drawing.Point(6, 19);
            this.checkBoxErsterJahrgangAE.Name = "checkBox3";
            this.checkBoxErsterJahrgangAE.Size = new System.Drawing.Size(40, 17);
            this.checkBoxErsterJahrgangAE.TabIndex = 9;
            this.checkBoxErsterJahrgangAE.Text = "AE";
            this.checkBoxErsterJahrgangAE.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBoxBundesland.FormattingEnabled = true;
            this.comboBoxBundesland.Location = new System.Drawing.Point(88, 80);
            this.comboBoxBundesland.Name = "comboBox1";
            this.comboBoxBundesland.Size = new System.Drawing.Size(138, 21);
            this.comboBoxBundesland.TabIndex = 7;
            this.comboBoxBundesland.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
            this.groupBox1.Location = new System.Drawing.Point(88, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(94, 65);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jahrgang 1";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxZweiterJahrgangSI);
            this.groupBox2.Controls.Add(this.checkBoxZweiterJahrgangAE);
            this.groupBox2.Location = new System.Drawing.Point(194, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(94, 65);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Jahrgang 2";
            this.groupBox2.Visible = false;
            // 
            // checkBox5
            // 
            this.checkBoxZweiterJahrgangSI.AutoSize = true;
            this.checkBoxZweiterJahrgangSI.Location = new System.Drawing.Point(6, 40);
            this.checkBoxZweiterJahrgangSI.Name = "checkBox5";
            this.checkBoxZweiterJahrgangSI.Size = new System.Drawing.Size(36, 17);
            this.checkBoxZweiterJahrgangSI.TabIndex = 10;
            this.checkBoxZweiterJahrgangSI.Text = "SI";
            this.checkBoxZweiterJahrgangSI.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBoxZweiterJahrgangAE.AutoSize = true;
            this.checkBoxZweiterJahrgangAE.Location = new System.Drawing.Point(6, 19);
            this.checkBoxZweiterJahrgangAE.Name = "checkBox6";
            this.checkBoxZweiterJahrgangAE.Size = new System.Drawing.Size(40, 17);
            this.checkBoxZweiterJahrgangAE.TabIndex = 9;
            this.checkBoxZweiterJahrgangAE.Text = "AE";
            this.checkBoxZweiterJahrgangAE.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.buttonWeiter.Location = new System.Drawing.Point(428, 225);
            this.buttonWeiter.Name = "button4";
            this.buttonWeiter.Size = new System.Drawing.Size(73, 23);
            this.buttonWeiter.TabIndex = 14;
            this.buttonWeiter.Text = "Weiter";
            this.buttonWeiter.UseVisualStyleBackColor = true;
            this.buttonWeiter.Click += new System.EventHandler(this.buttonWeiter_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(12, 254);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(489, 56);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tagplan";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(94, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Speichern";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Öffnen";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(175, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Erstellen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btn_ferienOeffnen);
            this.groupBox4.Location = new System.Drawing.Point(12, 324);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(489, 56);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ferien";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.btn_feiertageOeffnen);
            this.groupBox5.Location = new System.Drawing.Point(12, 396);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(489, 56);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Feiertage";
            // 
            // btn_feiertageOeffnen
            // 
            this.btn_feiertageOeffnen.Location = new System.Drawing.Point(13, 27);
            this.btn_feiertageOeffnen.Name = "btn_feiertageOeffnen";
            this.btn_feiertageOeffnen.Size = new System.Drawing.Size(75, 23);
            this.btn_feiertageOeffnen.TabIndex = 15;
            this.btn_feiertageOeffnen.Text = "Öffnen";
            this.btn_feiertageOeffnen.UseVisualStyleBackColor = true;
            this.btn_feiertageOeffnen.Click += new System.EventHandler(this.btn_feiertageOeffnen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "label3";
            this.label3.Visible = false;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "label6";
            this.label6.Visible = false;
            // 
            // TagplanAnlegenUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
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
            this.Size = new System.Drawing.Size(520, 475);
            this.Load += new System.EventHandler(this.TagplanAnlegenUserControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_ferienOeffnen;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_feiertageOeffnen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}
