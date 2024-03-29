﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    /// <summary>
    /// Autor: Isabella Pfeuster
    /// Datum: 16.01.2015
    /// In dieser Klasse werden alle Aufrufe der View TrainerVerwalten gesteuert
    /// </summary>
    public partial class TrainerVerwaltenUserControl : UserControl
    {
        CDatabase db;

        /// <summary>
        /// Datenbank Instanz wird geholt und die TrainerComboBox wird gefüllt
        /// </summary>
        public TrainerVerwaltenUserControl()
        {
            db = CDatabase.GetInstance();
            InitializeComponent();
            db.FillTrainerComboBox(trainerComboBox);
            loeschenButton.Enabled = false;
            speichernButton.Enabled = false;
            zuruecksetzenButton.Enabled = false;
            internRadioButton.Checked = true;
        }
        /// <summary>
        /// Wenn der Zurücksetzen Button geklickt wird, werden alle vorhandenen Felder leer gemacht. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZuruecksetzenButton_Click(object sender, EventArgs e)
        {
            speichernButton.Enabled = false;
            trainerComboBox.Text = "";
            nachnameTextBox.Clear();
            vornameTextBox.Clear();
            kuerzelTextBox.Clear();
            internRadioButton.Checked = true;
            externRadioButton.Checked = false;
            loeschenButton.Enabled = false;
            zuruecksetzenButton.Enabled = false;        
        }
        /// <summary>
        /// Wenn ein Trainer in der ComboBox ausgewählt wird, wird der Speichern Button ausgeblendet
        /// und alle Felder mit den Informationen zu diesem Trainer gefüllt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrainerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            zuruecksetzenButton.Enabled = true;
            speichernButton.Enabled = false;
            loeschenButton.Enabled = true;

            MTrainer trainer = (MTrainer)trainerComboBox.SelectedItem;
            nachnameTextBox.Text = trainer.Name;
            vornameTextBox.Text = trainer.Surname;
            kuerzelTextBox.Text = trainer.Abbreviation;

            //  Trainer intern
            if (trainer.IsInternal == true)
            {
                internRadioButton.Checked = true;
                externRadioButton.Checked = false;
            }
                // Trainer Extern
            else if (trainer.IsInternal == false)
            {
                externRadioButton.Checked = true;
                internRadioButton.Checked = false;
            }

        }
        /// <summary>
        /// Wenn der Speichern Button gedrückt wird, wird der Inhalt der Felder in ein 
        /// MTrainer Objekt geschrieben und dieses in die Datenbank geschrieben, anschliessend wird 
        /// die Combobox neu befüllt und alle Felder wieder leer gemacht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeichernButton_Click(object sender, EventArgs e)
        {
            bool erg = false;
            MTrainer trainer = null;
            // Trainer intern
            if (internRadioButton.Checked == true)
            {
                trainer = new MTrainer(nachnameTextBox.Text, vornameTextBox.Text, kuerzelTextBox.Text, true);
            }
            // Trainer extern
            else if (externRadioButton.Checked == true)
            {
                trainer = new MTrainer(nachnameTextBox.Text, vornameTextBox.Text, kuerzelTextBox.Text, false);
            }

            erg = db.InsertTrainer(trainer);
            if (erg == true)
            {
                trainerComboBox.Text = "";
                trainerComboBox.Items.Clear();
                
                db.FillTrainerComboBox(trainerComboBox);
                nachnameTextBox.Clear();
                vornameTextBox.Clear();
                kuerzelTextBox.Clear();
                internRadioButton.Checked = true;
                externRadioButton.Checked = false;
                speichernButton.Enabled = false;
                zuruecksetzenButton.Enabled = false;
            }



        }
        /// <summary>
        /// Wenn der Löschen Button gedrückt wird, wird das aktuell ausgewählte Objekt aus der ComboBox 
        /// gelöscht, anschliessend wird 
        /// die Combobox neu befüllt und alle Felder wieder leer gemacht.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoeschenButton_Click(object sender, EventArgs e)
        {
            MTrainer trainer = (MTrainer)trainerComboBox.SelectedItem;
            bool erg = db.DeleteTrainer(trainer);

            if (erg == true)
            {
                trainerComboBox.Text = "";
                nachnameTextBox.Clear();
                vornameTextBox.Clear();
                kuerzelTextBox.Clear();
                internRadioButton.Checked = true;
                externRadioButton.Checked = false;

                trainerComboBox.Items.Clear();
                trainerComboBox.Text = "";
                db.FillTrainerComboBox(trainerComboBox);
                loeschenButton.Enabled = false;
                zuruecksetzenButton.Enabled = false;
            }
        }
        /// <summary>
        /// Wenn in die Textbox geschrieben wird, wird der Speichern und der Zurücksetzen Button 
        /// wieder aktiv geschaltet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nachnameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            speichernButton.Enabled = true;
            zuruecksetzenButton.Enabled = true;
        }

        /// <summary>
        /// Wenn in die ComboBox getippt wird, wird der zurücksetzen Button wieder aktiv geschaltet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trainerComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            zuruecksetzenButton.Enabled = true;
        }

    }
}
