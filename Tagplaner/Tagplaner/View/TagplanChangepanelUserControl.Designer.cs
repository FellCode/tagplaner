﻿namespace Tagplaner
{
    partial class TagplanChangepanelUserControl
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
            this.Tagart = new System.Windows.Forms.ComboBox();
            this.Seminar = new System.Windows.Forms.ComboBox();
            this.Trainer = new System.Windows.Forms.ComboBox();
            this.CoTrainer = new System.Windows.Forms.ComboBox();
            this.Ort = new System.Windows.Forms.ComboBox();
            this.Raum = new System.Windows.Forms.ComboBox();
            this.Kommentar = new System.Windows.Forms.TextBox();
            this.Einfügen = new System.Windows.Forms.Button();
            this.tagartl = new System.Windows.Forms.Label();
            this.seminarl = new System.Windows.Forms.Label();
            this.trainerl = new System.Windows.Forms.Label();
            this.cotrainerl = new System.Windows.Forms.Label();
            this.ortl = new System.Windows.Forms.Label();
            this.rauml = new System.Windows.Forms.Label();
            this.kommentarl = new System.Windows.Forms.Label();
            this.Weiterführung = new System.Windows.Forms.CheckBox();
            this.AnzahlTage = new System.Windows.Forms.NumericUpDown();
            this.tagel = new System.Windows.Forms.Label();
            this.Seminarpanel = new System.Windows.Forms.Panel();
            this.ZweiterTrainer = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.AnzahlTage)).BeginInit();
            this.SuspendLayout();
            // 
            // Tagart
            // 
            this.Tagart.DropDownWidth = 140;
            this.Tagart.FormattingEnabled = true;
            this.Tagart.Location = new System.Drawing.Point(35, 39);
            this.Tagart.Name = "Tagart";
            this.Tagart.Size = new System.Drawing.Size(121, 21);
            this.Tagart.TabIndex = 0;
            this.Tagart.SelectedIndexChanged += new System.EventHandler(this.Tagart_SelectedIndexChanged);
            // 
            // Seminar
            // 
            this.Seminar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Seminar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Seminar.DropDownWidth = 250;
            this.Seminar.FormattingEnabled = true;
            this.Seminar.Location = new System.Drawing.Point(184, 39);
            this.Seminar.Name = "Seminar";
            this.Seminar.Size = new System.Drawing.Size(121, 21);
            this.Seminar.TabIndex = 1;
            // 
            // Trainer
            // 
            this.Trainer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Trainer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Trainer.DropDownWidth = 150;
            this.Trainer.FormattingEnabled = true;
            this.Trainer.Location = new System.Drawing.Point(330, 39);
            this.Trainer.Name = "Trainer";
            this.Trainer.Size = new System.Drawing.Size(121, 21);
            this.Trainer.TabIndex = 2;
            // 
            // CoTrainer
            // 
            this.CoTrainer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CoTrainer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CoTrainer.DropDownWidth = 150;
            this.CoTrainer.FormattingEnabled = true;
            this.CoTrainer.Location = new System.Drawing.Point(330, 90);
            this.CoTrainer.Name = "CoTrainer";
            this.CoTrainer.Size = new System.Drawing.Size(121, 21);
            this.CoTrainer.TabIndex = 3;
            // 
            // Ort
            // 
            this.Ort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Ort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Ort.DropDownWidth = 180;
            this.Ort.FormattingEnabled = true;
            this.Ort.Location = new System.Drawing.Point(479, 39);
            this.Ort.Name = "Ort";
            this.Ort.Size = new System.Drawing.Size(121, 21);
            this.Ort.TabIndex = 4;
            this.Ort.SelectedIndexChanged += new System.EventHandler(this.Ort_SelectedIndexChanged);
            // 
            // Raum
            // 
            this.Raum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Raum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Raum.DropDownWidth = 140;
            this.Raum.FormattingEnabled = true;
            this.Raum.Location = new System.Drawing.Point(479, 90);
            this.Raum.Name = "Raum";
            this.Raum.Size = new System.Drawing.Size(121, 21);
            this.Raum.TabIndex = 5;
            // 
            // Kommentar
            // 
            this.Kommentar.AcceptsTab = true;
            this.Kommentar.Location = new System.Drawing.Point(617, 39);
            this.Kommentar.Multiline = true;
            this.Kommentar.Name = "Kommentar";
            this.Kommentar.Size = new System.Drawing.Size(227, 83);
            this.Kommentar.TabIndex = 6;
            // 
            // Einfügen
            // 
            this.Einfügen.Location = new System.Drawing.Point(885, 99);
            this.Einfügen.Name = "Einfügen";
            this.Einfügen.Size = new System.Drawing.Size(75, 23);
            this.Einfügen.TabIndex = 7;
            this.Einfügen.Text = "Einfügen";
            this.Einfügen.UseVisualStyleBackColor = true;
            this.Einfügen.Click += new System.EventHandler(this.Einfügen_Click);
            // 
            // tagartl
            // 
            this.tagartl.AutoSize = true;
            this.tagartl.Location = new System.Drawing.Point(32, 23);
            this.tagartl.Name = "tagartl";
            this.tagartl.Size = new System.Drawing.Size(41, 13);
            this.tagartl.TabIndex = 8;
            this.tagartl.Text = "Tagart:";
            // 
            // seminarl
            // 
            this.seminarl.AutoSize = true;
            this.seminarl.Location = new System.Drawing.Point(181, 23);
            this.seminarl.Name = "seminarl";
            this.seminarl.Size = new System.Drawing.Size(48, 13);
            this.seminarl.TabIndex = 9;
            this.seminarl.Text = "Seminar:";
            // 
            // trainerl
            // 
            this.trainerl.AutoSize = true;
            this.trainerl.Location = new System.Drawing.Point(327, 23);
            this.trainerl.Name = "trainerl";
            this.trainerl.Size = new System.Drawing.Size(43, 13);
            this.trainerl.TabIndex = 10;
            this.trainerl.Text = "Trainer:";
            // 
            // cotrainerl
            // 
            this.cotrainerl.AutoSize = true;
            this.cotrainerl.Location = new System.Drawing.Point(327, 74);
            this.cotrainerl.Name = "cotrainerl";
            this.cotrainerl.Size = new System.Drawing.Size(56, 13);
            this.cotrainerl.TabIndex = 11;
            this.cotrainerl.Text = "CoTrainer:";
            // 
            // ortl
            // 
            this.ortl.AutoSize = true;
            this.ortl.Location = new System.Drawing.Point(476, 23);
            this.ortl.Name = "ortl";
            this.ortl.Size = new System.Drawing.Size(24, 13);
            this.ortl.TabIndex = 12;
            this.ortl.Text = "Ort:";
            // 
            // rauml
            // 
            this.rauml.AutoSize = true;
            this.rauml.Location = new System.Drawing.Point(476, 74);
            this.rauml.Name = "rauml";
            this.rauml.Size = new System.Drawing.Size(38, 13);
            this.rauml.TabIndex = 13;
            this.rauml.Text = "Raum:";
            // 
            // kommentarl
            // 
            this.kommentarl.AutoSize = true;
            this.kommentarl.Location = new System.Drawing.Point(614, 23);
            this.kommentarl.Name = "kommentarl";
            this.kommentarl.Size = new System.Drawing.Size(63, 13);
            this.kommentarl.TabIndex = 14;
            this.kommentarl.Text = "Kommentar:";
            // 
            // Weiterführung
            // 
            this.Weiterführung.AutoSize = true;
            this.Weiterführung.Location = new System.Drawing.Point(850, 39);
            this.Weiterführung.Name = "Weiterführung";
            this.Weiterführung.Size = new System.Drawing.Size(102, 17);
            this.Weiterführung.TabIndex = 21;
            this.Weiterführung.Text = "Weiterführen für";
            this.Weiterführung.UseVisualStyleBackColor = true;
            this.Weiterführung.CheckedChanged += new System.EventHandler(this.Weiterführung_CheckedChanged);
            // 
            // AnzahlTage
            // 
            this.AnzahlTage.Location = new System.Drawing.Point(948, 36);
            this.AnzahlTage.Name = "AnzahlTage";
            this.AnzahlTage.Size = new System.Drawing.Size(41, 20);
            this.AnzahlTage.TabIndex = 22;
            // 
            // tagel
            // 
            this.tagel.AutoSize = true;
            this.tagel.Location = new System.Drawing.Point(995, 38);
            this.tagel.Name = "tagel";
            this.tagel.Size = new System.Drawing.Size(32, 13);
            this.tagel.TabIndex = 23;
            this.tagel.Text = "Tage";
            // 
            // Seminarpanel
            // 
            this.Seminarpanel.Location = new System.Drawing.Point(183, 139);
            this.Seminarpanel.Name = "Seminarpanel";
            this.Seminarpanel.Size = new System.Drawing.Size(417, 112);
            this.Seminarpanel.TabIndex = 24;
            // 
            // ZweiterTrainer
            // 
            this.ZweiterTrainer.AutoSize = true;
            this.ZweiterTrainer.Location = new System.Drawing.Point(309, 93);
            this.ZweiterTrainer.Name = "ZweiterTrainer";
            this.ZweiterTrainer.Size = new System.Drawing.Size(15, 14);
            this.ZweiterTrainer.TabIndex = 25;
            this.ZweiterTrainer.UseVisualStyleBackColor = true;
            this.ZweiterTrainer.CheckedChanged += new System.EventHandler(this.ZweiterTrainer_CheckedChanged);
            // 
            // TagplanChangepanelUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Seminarpanel);
            this.Controls.Add(this.tagel);
            this.Controls.Add(this.AnzahlTage);
            this.Controls.Add(this.Weiterführung);
            this.Controls.Add(this.kommentarl);
            this.Controls.Add(this.rauml);
            this.Controls.Add(this.ortl);
            this.Controls.Add(this.cotrainerl);
            this.Controls.Add(this.trainerl);
            this.Controls.Add(this.seminarl);
            this.Controls.Add(this.tagartl);
            this.Controls.Add(this.Einfügen);
            this.Controls.Add(this.Kommentar);
            this.Controls.Add(this.Raum);
            this.Controls.Add(this.Ort);
            this.Controls.Add(this.CoTrainer);
            this.Controls.Add(this.Trainer);
            this.Controls.Add(this.Seminar);
            this.Controls.Add(this.Tagart);
            this.Controls.Add(this.ZweiterTrainer);
            this.Name = "TagplanChangepanelUserControl";
            this.Size = new System.Drawing.Size(1073, 155);
            ((System.ComponentModel.ISupportInitialize)(this.AnzahlTage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Tagart;
        private System.Windows.Forms.ComboBox Seminar;
        private System.Windows.Forms.ComboBox Trainer;
        private System.Windows.Forms.ComboBox CoTrainer;
        private System.Windows.Forms.ComboBox Ort;
        private System.Windows.Forms.ComboBox Raum;
        private System.Windows.Forms.TextBox Kommentar;
        private System.Windows.Forms.Button Einfügen;
        private System.Windows.Forms.Label tagartl;
        private System.Windows.Forms.Label seminarl;
        private System.Windows.Forms.Label trainerl;
        private System.Windows.Forms.Label cotrainerl;
        private System.Windows.Forms.Label ortl;
        private System.Windows.Forms.Label rauml;
        private System.Windows.Forms.Label kommentarl;
        private System.Windows.Forms.CheckBox Weiterführung;
        private System.Windows.Forms.NumericUpDown AnzahlTage;
        private System.Windows.Forms.Label tagel;
        private System.Windows.Forms.Panel Seminarpanel;
        private System.Windows.Forms.CheckBox ZweiterTrainer;
    }
}
