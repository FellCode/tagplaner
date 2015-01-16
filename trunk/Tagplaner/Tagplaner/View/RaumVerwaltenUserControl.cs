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

        public RaumVerwaltenUserControl()
        {
            db = CDatabase.GetInstance();
            InitializeComponent();
            db.FillPlaceComboBox(seminarOrtComboBox);
        }

        private void ZuruecksetzenButton_Click(object sender, EventArgs e)
        {
            speichernButton.Enabled = true;
            raumTextBox.Clear();
            raeumeComboBox.Text = "";
        }

        private void RaeumeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MRoom room = (MRoom)raeumeComboBox.SelectedItem;

            speichernButton.Enabled = false;
            raumTextBox.Text = room.Number;
        }

        private void SpeichernButton_Click(object sender, EventArgs e)
        {
            MPlace place = (MPlace)seminarOrtComboBox.SelectedItem;
            MRoom room = new MRoom(raumTextBox.Text, place.Id);

            bool erg = db.InsertRoom(room);
            if (erg == true)
            {
                raeumeComboBox.Text = "";
                raeumeComboBox.Items.Clear();
                db.FillRoomComboBox(raeumeComboBox, place.Id);
            }
        }

        private void SeminarOrtComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            raeumeComboBox.Text = "";
            raeumeComboBox.Items.Clear();

            MPlace place = (MPlace)seminarOrtComboBox.SelectedItem;
            db.FillRoomComboBox(raeumeComboBox, place.Id);

        }

        private void LoeschenButton_Click(object sender, EventArgs e)
        {
            MRoom room = (MRoom)raeumeComboBox.SelectedItem;
            bool erg = db.DeleteRoom(room);
            if (erg == true)
            {
                speichernButton.Enabled = true;
                raumTextBox.Clear();
                raeumeComboBox.Text = "";
                seminarOrtComboBox.Text = "bitte Seminarort wählen!";
                MPlace place = (MPlace) seminarOrtComboBox.SelectedItem;

                raeumeComboBox.Items.Clear();
                raeumeComboBox.Text = "";
                db.FillRoomComboBox(raeumeComboBox, place.Id);
            }
        }
    }
}
