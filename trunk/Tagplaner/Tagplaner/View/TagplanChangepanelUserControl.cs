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
    /// Autor: Matthias Ohm
    /// Datum: 14.01.15
    /// Dieser Controler dient dazu um ein MCalenderEntry Objekt anzunehmen und in die Comboboxen zu schreiben.
    /// Dazu gibt er die gewählten Daten wieder an das Grid zurück
    /// </summary>
    public partial class TagplanChangepanelUserControl : UserControl
    {

        private CDatabase cdb = CDatabase.GetInstance();
        //TagplanBearbeitenUserControl tagplanBearbeitenUserControl = TagplanBearbeitenUserControl.GetInstance();

        /// <summary>
        /// 
        /// </summary>
        public TagplanChangepanelUserControl()
        {
            InitializeComponent();

            FillDayType(Tagart);


        }

        /// <summary>
        /// Prüft ob der Einfügen Button geklickt wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Einfügen_Click(object sender, EventArgs e)
        {

            PasteEntry();

        }
        /// <summary>
        /// Prüft ob die Auswahl der Tagart geändert wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ort_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRoom(Ort, Raum);
        }

        /// <summary>
        /// Prüft ob die Auswahl der Tagart geändert wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tagart_SelectedIndexChanged(object sender, EventArgs e)
        {

            ChangeVisibility(Tagart, Seminarpanel);


        }

        /// <summary>
        /// Prüft ob die CheckBoc Weiterführen gesetzt ist und verändert dementsprechend die Sichtbarkeit der Anzahl Tage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Weiterführung_CheckedChanged(object sender, EventArgs e)
        {
            if (Weiterführung.Checked == true)
            {
                AnzahlTage.Visible = true;
            }
            else
            {
                AnzahlTage.Visible = false;
            }
        }

        /// <summary>
        /// Diese Methode nimmt das MCalenderEntry, Combo- und Textboxen Objekte und füllt damit die Comboboxen und das Textfeld
        /// </summary>
        /// <param name="calendarentry"></param>
        public void ChangeCalendarEntry(MCalendarEntry calendarentry)
        {

            cdb.FillSeminarComboBox(Seminar);
            cdb.FillTrainerComboBox(Trainer);
            cdb.FillTrainerComboBox(CoTrainer);
            cdb.FillPlaceComboBox(Ort);

            SetDayTyp(calendarentry, Tagart);
            SetSeminar(calendarentry, Seminar);
            SetTrainer(calendarentry, Trainer);
            SetCoTrainer(calendarentry, CoTrainer);
            SetLocation(calendarentry, Ort);
            SetRoom(calendarentry, Raum);

        }

        /// <summary>
        /// Diese Methode nimmt die Objekte aus den Combo- und Textboxen und fügt diese einem MCalenderEntry Objekt zu
        /// </summary>
        public void PasteEntry()
        {
            MCalendarEntry ccalendarentry = new MCalendarEntry();

            GetSeminar(ccalendarentry, Seminar);
            GetTrainer(ccalendarentry, Trainer);
            GetCoTrainer(ccalendarentry, CoTrainer);
            GetLocation(ccalendarentry, Ort);
            GetRoom(ccalendarentry, Raum);
            GetComment(ccalendarentry, Kommentar, Tagart);
            GetInterationNumber(Weiterführung, AnzahlTage);

            TagplanBearbeitenUserControl tagplanBearbeitenUserControl = TagplanBearbeitenUserControl.GetInstance();
            tagplanBearbeitenUserControl.ApplyChangesToGrid(GetInterationNumber(Weiterführung, AnzahlTage), ccalendarentry);

            Weiterführung.Checked = false;
            AnzahlTage.Value = 0;
        }

        /// <summary>
        /// Füllt die ComboBox Tagart mit den Werten Smeinar, Berufsschule und Praxis
        /// </summary>
        /// <param name="Tagart"></param>
        /// <returns></returns>
        public bool FillDayType(ComboBox Tagart)
        {
            try
            {
                Tagart.Items.Add("Seminar");
                Tagart.Items.Add("Berufsschule");
                Tagart.Items.Add("Praxis");

                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }

        /// <summary>
        /// Nimmt den Ort und füllt die ComboBox Raum mit den zum Ort entsprechenden Räumen
        /// </summary>
        /// <param name="Ort"></param>
        /// <param name="Raum"></param>
        public void FillRoom(ComboBox Ort, ComboBox Raum)
        {

            MPlace mort = (MPlace)Ort.SelectedItem;
            cdb.FillRoomComboBox(Raum, mort.Id);
        } 

        /// <summary>
        /// Verädert die Sichtbarkeit des Panels anhand der Tagart
        /// </summary>
        /// <param name="Tagart"></param>
        /// <param name="Seminarpanel"></param>
        public void ChangeVisibility(ComboBox Tagart, Panel Seminarpanel)
        {

            switch (Convert.ToString(Tagart.SelectedItem))
            {
                case "Seminar":
                    Seminarpanel.Visible = false;
                    break;
                case "Berufsschule":
                    Seminarpanel.Visible = true;
                    break;
                case "Praxis":
                    Seminarpanel.Visible = true;
                    break;
            }

        }

        /// <summary>
        /// Setzt die Tagart anhand der im CalenderEntry enhaltenen Objekte
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="tagartb"></param>
        public void SetDayTyp(MCalendarEntry calendarentry, ComboBox tagartb)
        {
            if (calendarentry.Practice == null)
            {
                if (calendarentry.School == null)
                {
                    if (calendarentry.Seminar == null)
                    {
                        tagartb.SelectedIndex = -1;
                        tagartb.Text = "";
                    }
                    else
                    {
                        tagartb.SelectedIndex = 0;
                    }
                }
                else
                {
                    tagartb.SelectedIndex = 1;
                }
            }
            else
            {
                tagartb.SelectedIndex = 2;
            }
            tagartb.Refresh();
        }

        /// <summary>
        /// Setzt das Semianr anhand der im CalenderEntry befindenen Seminar 
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="seminarb"></param>
        public void SetSeminar(MCalendarEntry calendarentry, ComboBox seminarb)
        {
            if (calendarentry.Seminar == null)
            {
                seminarb.SelectedIndex = -1;
                seminarb.Text = "";
            }
            else
            {
                seminarb.SelectedItem = calendarentry.Seminar;
            }
            seminarb.Refresh();
        }

        /// <summary>
        /// Setzt den Trainer anhand des im CalenderEntry befindenen Trainer
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="trainerb"></param>
        public void SetTrainer(MCalendarEntry calendarentry, ComboBox trainerb)
        {
            if (calendarentry.Trainer == null)
            {
                trainerb.SelectedIndex = -1;
                trainerb.Text = "";
            }
            else
            {
                trainerb.SelectedItem = calendarentry.Trainer;
            }
            trainerb.Refresh();
        }

        /// <summary>
        /// Setzt den CoTrainer anhand des im CalenderEntry befindenen CoTrainer
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="cotrainerb"></param>
        public void SetCoTrainer(MCalendarEntry calendarentry, ComboBox cotrainerb)
        {
            if (calendarentry.Cotrainer == null)
            {
                cotrainerb.SelectedIndex = -1;
                cotrainerb.Text = "";
            }
            else
            {
                cotrainerb.SelectedItem = calendarentry.Cotrainer;
            }
            cotrainerb.Refresh();
        }

        /// <summary>
        /// Setzt die Location anahnd der im CalenderEntry vorhanden Location
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="ortb"></param>
        public void SetLocation(MCalendarEntry calendarentry, ComboBox ortb)
        {
            if (calendarentry.Room == null)
            {
                ortb.SelectedIndex = -1;
                ortb.Text = "";
            }
            else
            {
                ortb.SelectedItem = calendarentry.Place;
            }
            ortb.Refresh();
        }

        /// <summary>
        /// Setzt den Raum aus dem CalenderEntry in die ComboBox des Raumes
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="raumb"></param>
        public void SetRoom(MCalendarEntry calendarentry, ComboBox raumb)
        {
            if (calendarentry.Room == null)
            {
                raumb.SelectedIndex = -1;
                raumb.Text = "";
            }
            else
            {
                raumb.SelectedItem = calendarentry.Room;
            }
            raumb.Refresh();
        }

        /// <summary>
        /// Setzt das Kommentar aus der entsprechenden Tagart aus der CalenderEntry in das Kommentarfeld
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="kommentarb"></param>
        public void SetComment(MCalendarEntry calendarentry, ComboBox kommentarb)
        {
            if (calendarentry.Practice == null)
            {
                if (calendarentry.School == null)
                {
                    kommentarb.Text = calendarentry.Seminar.Comment;
                }
                if (calendarentry.Seminar == null)
                {
                    kommentarb.Text = calendarentry.School.Comment;
                }
                kommentarb.Text = " ";
            }
            else
            {
                kommentarb.Text = calendarentry.Practice.Comment;
            }
            kommentarb.Refresh();
        }

        /// <summary>
        /// Holt das Seminar aus der ComboBox und schiebt es in den CalenderEntry
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="seminarb"></param>
        public void GetSeminar(MCalendarEntry calendarentry, ComboBox seminarb)
        {
            calendarentry.Seminar = (MSeminar)seminarb.SelectedItem;
        }

        /// <summary>
        /// Holt den Trainer aus der ComboBox und schiebt es in den CalenderEntry
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="trainerb"></param>
        public void GetTrainer(MCalendarEntry calendarentry, ComboBox trainerb)
        {
            calendarentry.Trainer = (MTrainer)trainerb.SelectedItem;
        }

        /// <summary>
        /// Holt den CoTrainer aus der ComBobox und schiebt es in den CalenderEntry
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="cotrainerb"></param>
        public void GetCoTrainer(MCalendarEntry calendarentry, ComboBox cotrainerb)
        {
            calendarentry.Cotrainer = (MTrainer)cotrainerb.SelectedItem;
        }

        /// <summary>
        /// Holt den Ort aus der ComboBox und schiebt es in den CalenderEntry
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="ortb"></param>
        public void GetLocation(MCalendarEntry calendarentry, ComboBox ortb)
        {
            calendarentry.Place = (MPlace)ortb.SelectedItem;
        }

        /// <summary>
        /// Holt sich den Raum aus der ComboBox und schiebt es in den CalenderEntry
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="raumb"></param>
        public void GetRoom(MCalendarEntry calendarentry, ComboBox raumb)
        {
            calendarentry.Room = (MRoom)raumb.SelectedItem;
        }

        /// <summary>
        /// Holt sich das Kommentar und schiebt es in die entsprechenden Tagart im CalenderEntry
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="kommentarb"></param>
        /// <param name="tagartb"></param>
        public void GetComment(MCalendarEntry calendarentry, TextBox kommentarb, ComboBox tagartb)
        {
            switch (tagartb.SelectedIndex)
            {
                case 0:
                    calendarentry.Seminar.Comment = kommentarb.Text;
                    break;
                case 1:
                    MSchool mschool = new MSchool(kommentarb.Text);
                    calendarentry.School = mschool;
                    break;
                case 2:
                    MPractice mpractice = new MPractice(kommentarb.Text);
                    calendarentry.Practice = mpractice;
                    break;
            }
        }

        /// <summary>
        /// Prüft ob die CheckBox Weiterführen gesetzt ist und nimmt dann den dahinter gewählten zahlenwert und returnt einen int
        /// </summary>
        /// <param name="weitercheck"></param>
        /// <param name="anzahltage"></param>
        /// <returns></returns>
        public int GetInterationNumber(CheckBox weitercheck, NumericUpDown anzahltage)
        {
            if (weitercheck.Checked == true)
            {
                if (Convert.ToInt32(anzahltage.Value) >= 0)
                {
                    return Convert.ToInt32(anzahltage.Value);
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }

        }

    }
}
