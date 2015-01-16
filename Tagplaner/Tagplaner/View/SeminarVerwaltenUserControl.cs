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

        public SeminarVerwaltenUserControl()
        {
            InitializeComponent();
            db = CDatabase.GetInstance();
            db.FillSeminarComboBox(seminarComboBox);
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
            speichernButton.Enabled = false;

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
            }
        }
        /// <summary>
        /// Wenn der zurücksetzen Button gedrückt wird, wird der Speichern Button wieder eingeblendet
        /// Ebenfalls werden alle Felder geleert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZuruecksetzenButton_Click(object sender, EventArgs e)
        {
            speichernButton.Enabled = true;
            seminarComboBox.Text = "";
            titelTextBox.Clear();
            untertitelTextBox.Clear();
            kuerzelTextBox.Clear();
            technikTextBox.Clear();
        }
        /// <summary>
        ///  Wenn der Löschen Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoeschenButton_Click(object sender, EventArgs e)
        {
            MSeminar seminar = (MSeminar)seminarComboBox.SelectedItem;
            bool erg = db.DeleteSeminar(seminar);
            if (erg == true)
            {
                speichernButton.Enabled = true;
                seminarComboBox.Text = "";
                titelTextBox.Clear();
                untertitelTextBox.Clear();
                kuerzelTextBox.Clear();
                technikTextBox.Clear();

                seminarComboBox.Text = "";
                seminarComboBox.Items.Clear();
                db.FillSeminarComboBox(seminarComboBox);
            }
        }
    }
}
