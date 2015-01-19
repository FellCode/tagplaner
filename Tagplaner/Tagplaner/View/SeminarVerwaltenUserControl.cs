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
    /// In dieser Klasse werden alle Aufrufe der View SeminarVerwalten gesteuert
    /// </summary>
    public partial class SeminarVerwaltenUserControl : UserControl
    {
        CDatabase db;
        /// <summary>
        /// Datenbank Instanz wird geholt und die SeminarComboBox wird gefüllt.
        /// </summary>
        public SeminarVerwaltenUserControl()
        {
            InitializeComponent();
            db = CDatabase.GetInstance();
            db.FillSeminarComboBox(seminarComboBox);
            loeschenButton.Enabled = false;
            speichernButton.Enabled = false;
            zuruecksetzenButton.Enabled = false;
        }
        /// <summary>
        ///  Wenn in der Combobox ein Objekt ausgewählt wird, wird der Speichern Button ausgeblendet.
        ///  Ebenfalls werden die Felder mit den Attributen des Objekts gefüllt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeminarComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSeminar seminar = (MSeminar)seminarComboBox.SelectedItem;
            zuruecksetzenButton.Enabled = true;
            speichernButton.Enabled = false;
            loeschenButton.Enabled = true;

            titelTextBox.Text = seminar.Title;
            untertitelTextBox.Text = seminar.Subtitle;
            kuerzelTextBox.Text = seminar.Abbreviation;
            technikTextBox.Text = seminar.HasTechnology;
        }

        /// <summary>
        /// Wenn der Speichern Button geklickt wird, werden alle Inhalte der Felder in ein Seminar
        /// Objekt geschrieben und dann wird dieses Objekt in die Datenbank geschrieben.
        /// Abschliessend wird die ComboBox neu geladen und die Felder geleert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeichernButton_Click(object sender, EventArgs e)
        {
            MSeminar seminar = new MSeminar(titelTextBox.Text, untertitelTextBox.Text, kuerzelTextBox.Text, technikTextBox.Text);

            bool erg = db.InsertSeminar(seminar);
            if (erg == true)
            {
                seminarComboBox.Items.Clear();
                seminarComboBox.Text = "";
                db.FillSeminarComboBox(seminarComboBox);
                titelTextBox.Clear();
                untertitelTextBox.Clear();
                kuerzelTextBox.Clear();
                technikTextBox.Clear();
                speichernButton.Enabled = false;
                zuruecksetzenButton.Enabled = false;
            }
        }
        /// <summary>
        /// Wenn der zurücksetzen Button gedrückt wird,werden alle Felder geleert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZuruecksetzenButton_Click(object sender, EventArgs e)
        {
            speichernButton.Enabled = false;
            seminarComboBox.Text = "";
            titelTextBox.Clear();
            untertitelTextBox.Clear();
            kuerzelTextBox.Clear();
            technikTextBox.Clear();
            loeschenButton.Enabled = false;
            zuruecksetzenButton.Enabled = false;
        }
        /// <summary>
        ///  Wenn der Löschen Button betätigt wird, wird das ausgewählte Objekt aus der Combobox gelöscht
        ///  und die Felder geleert und die ComboBox aktualisiert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoeschenButton_Click(object sender, EventArgs e)
        {
            MSeminar seminar = (MSeminar)seminarComboBox.SelectedItem;
            bool erg = db.DeleteSeminar(seminar);
            if (erg == true)
            {
                speichernButton.Enabled = false;
                seminarComboBox.Text = "";
                titelTextBox.Clear();
                untertitelTextBox.Clear();
                kuerzelTextBox.Clear();
                technikTextBox.Clear();

                seminarComboBox.Text = "";
                seminarComboBox.Items.Clear();
                db.FillSeminarComboBox(seminarComboBox);
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
        private void titelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            speichernButton.Enabled = true;
            zuruecksetzenButton.Enabled = true;
        }

        /// <summary>
        /// Wenn in die ComboBox getippt wird, wird der zurücksetzen Button wieder aktiv geschaltet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void seminarComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            zuruecksetzenButton.Enabled = true;
        }
    }
}
