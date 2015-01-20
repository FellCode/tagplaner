using System;
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
    /// In dieser Klasse werden alle Aufrufe der View RaumVerwalten gesteuert
    /// </summary>
    public partial class RaumVerwaltenUserControl : UserControl
    {
        CDatabase db;
        /// <summary>
        /// Datenbank Instanz wird geholt und die SeminarOrtCombobox wird gefüllt.
        /// </summary>
        public RaumVerwaltenUserControl()
        {
            db = CDatabase.GetInstance();
            InitializeComponent();
            db.FillPlaceComboBox(seminarOrtComboBox);
            loeschenButton.Enabled = false;
            speichernButton.Enabled = false;
            zuruecksetzenButton.Enabled = false;

        }
        /// <summary>
        /// Bei klicken auf den Zurücksetzen werden die Felder geleert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZuruecksetzenButton_Click(object sender, EventArgs e)
        {
            speichernButton.Enabled = false;
            raumTextBox.Clear();
            raeumeComboBox.Text = "";
            loeschenButton.Enabled = false;
            zuruecksetzenButton.Enabled = false;    
        }
        /// <summary>
        ///  Wenn in der RäumeComboBox ein Objekt ausgewählt wird, wird die Raumnummer in das
        ///  Textfeld geschrieben. Ebenfalls wird der Speichern Button gesperrt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RaeumeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MRoom room = (MRoom)raeumeComboBox.SelectedItem;

            zuruecksetzenButton.Enabled = true;
            speichernButton.Enabled = false;
            loeschenButton.Enabled = true;
            raumTextBox.Text = room.Number;
        }
        /// <summary>
        ///  Wenn der Speichern Button betätigt wird, werden die Inhalte der Textfelder in ein neues MRoom
        ///  Objekt geschrieben. Anschliessend wird das Objekt in die Datenbank geschrieben und alle Felder geleert und die Combobox aktualisiert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeichernButton_Click(object sender, EventArgs e)
        {
            MPlace place = (MPlace)seminarOrtComboBox.SelectedItem;
            MRoom room = new MRoom(raumTextBox.Text, place.Id);

            bool erg = db.InsertRoom(room);
            if (erg == true)
            {
                raeumeComboBox.Text = "";
                raeumeComboBox.Items.Clear();
                raumTextBox.Clear();
                db.FillRoomComboBox(raeumeComboBox, place.Id);
                speichernButton.Enabled = false;
                zuruecksetzenButton.Enabled = false;
            }
        }
        /// <summary>
        /// Wenn bei der SeminarOrt ComboBox ein neues Item ausgewählt wird, die RäumeComboBox befüllt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeminarOrtComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            raeumeComboBox.Text = "";
            raeumeComboBox.Items.Clear();

            MPlace place = (MPlace)seminarOrtComboBox.SelectedItem;
            db.FillRoomComboBox(raeumeComboBox, place.Id);

        }
        /// <summary>
        /// Wenn der Löschen button betätigt wird, wird der ausgewählte Raum aus der RäumeCombobox entfernt
        /// und alle Felder werden geleert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoeschenButton_Click(object sender, EventArgs e)
        {
            MRoom room = (MRoom)raeumeComboBox.SelectedItem;
            bool erg = db.DeleteRoom(room);
            if (erg == true)
            {
                raumTextBox.Clear();
                raeumeComboBox.Text = "";
                seminarOrtComboBox.Text = "Bitte Seminarort wählen!";
                MPlace place = (MPlace) seminarOrtComboBox.SelectedItem;

                raeumeComboBox.Items.Clear();
                raeumeComboBox.Text = "";
                loeschenButton.Enabled = false;
                zuruecksetzenButton.Enabled = false;

            }
        }
        /// <summary>
        /// Wenn in die ComboBox getippt wird, wird der zurücksetzen Button wieder aktiv geschaltet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void raeumeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            zuruecksetzenButton.Enabled = true;
        }

        /// <summary>
        /// Wenn in die Textbox geschrieben wird, wird der Speichern und der Zurücksetzen Button 
        /// wieder aktiv geschaltet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void raumTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            speichernButton.Enabled = true;
            zuruecksetzenButton.Enabled = true;
        }
    }
}
