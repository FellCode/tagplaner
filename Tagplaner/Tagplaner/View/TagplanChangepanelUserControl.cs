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
        //TagplanBearbeitenUserControl tagplanBearbeitenUserControl = TagplanBearbeitenUserControl.getInstance();

        private MCalendarEntry ccalendarentry = new MCalendarEntry();

        public TagplanChangepanelUserControl()
        {
            InitializeComponent();

            FillDayType(Tagart);


        }

        private void TagplanChangepanelUserControl_Load(object sender, EventArgs e)
        {

        }

        //Prüft ob der Einfügen Button geklickt wurde
        private void Einfügen_Click(object sender, EventArgs e)
        {

            PasteEntry();

        }
        //Prüft ob die Auswahl des Ortes geändert wurde und ändert dementsprechend die auswählbaren Räume
        private void Ort_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRoom(Ort, Raum);
        }

        //Prüft ob die Auswahl der Tagart geändert wurde
        private void Tagart_SelectedIndexChanged(object sender, EventArgs e)
        {

            ChangeVisibility(Tagart, Seminarpanel);


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

            GetSeminar(ccalendarentry, Seminar);
            GetTrainer(ccalendarentry, Trainer);
            GetCoTrainer(ccalendarentry, CoTrainer);
            GetLocation(ccalendarentry, Ort);
            GetRoom(ccalendarentry, Raum);
            GetComment(ccalendarentry, Kommentar, Tagart);
            GetInterationNumber(Weiterführung, AnzahlTage);

            TagplanBearbeitenUserControl tagplanBearbeitenUserControl = TagplanBearbeitenUserControl.getInstance();
            tagplanBearbeitenUserControl.ApplyChangesToGrid(GetInterationNumber(Weiterführung, AnzahlTage), ccalendarentry);

        }

        //Füllt die ComboBox Tagart mit den Werten Smeinar, Berufsschule und Praxis
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

        //Nimmt den Ort und füllt die ComboBox Raum mit den zum Ort entsprechenden Räumen
        public void FillRoom(ComboBox Ort, ComboBox Raum)
        {

            MPlace mort = (MPlace)Ort.SelectedItem;
            cdb.FillRoomComboBox(Raum, mort.Id);
        }

        //Verädert die Sichtbarkeit des Panels anhand der Tagart
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

        //Setzt die Tagart anhand der im CalenderEntry enhaltenen Objekte
        public void SetDayTyp(MCalendarEntry calendarentry, ComboBox tagartb)
        {
            if (calendarentry.Practice == null)
            {
                if (calendarentry.School == null)
                {
                    tagartb.SelectedIndex = 0;
                }
                if (calendarentry.Seminar == null)
                {
                    tagartb.SelectedIndex = 1;
                }
                tagartb.SelectedIndex = -1;
                tagartb.Text = "";
            }
            else
            {
                tagartb.SelectedIndex = 2;
            }
            tagartb.Refresh();
        }

        //Setzt das Semianr anhand der im CalenderEntry befindenen Seminar 
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

        //Setzt den Trainer anhand des im CalenderEntry befindenen Trainer
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

        //Setzt den CoTrainer anhand des im CalenderEntry befindenen CoTrainer
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

        //Setzt die Location anahnd der im CalenderEntry vorhanden Location
        public void SetLocation(MCalendarEntry calendarentry, ComboBox ortb)
        {
            if (calendarentry.Room == null)
            {
                ortb.SelectedIndex = -1;
                ortb.Text = "";
            }
            else
            {
                ortb.SelectedItem = calendarentry.Room;
            }
            ortb.Refresh();
        }

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

        public void SetComment(MCalendarEntry calendarentry, ComboBox kommentarb)
        {
            if (calendarentry.Practice == null)
            {
                if (calendarentry.School == null)
                {
                    kommentarb.SelectedItem = calendarentry.Seminar;
                }
                if (calendarentry.Seminar == null)
                {
                    kommentarb.SelectedItem = calendarentry.School;
                }
                kommentarb.SelectedIndex = -1;
                kommentarb.Text = "";
            }
            else
            {
                kommentarb.SelectedItem = calendarentry.Practice;
            }
            kommentarb.Refresh();
        }

        public void GetSeminar(MCalendarEntry calendarentry, ComboBox seminarb)
        {
            calendarentry.Seminar = (MSeminar)seminarb.SelectedItem;
        }

        public void GetTrainer(MCalendarEntry calendarentry, ComboBox trainerb)
        {
            calendarentry.Trainer = (MTrainer)trainerb.SelectedItem;
        }

        public void GetCoTrainer(MCalendarEntry calendarentry, ComboBox cotrainerb)
        {
            calendarentry.Cotrainer = (MTrainer)cotrainerb.SelectedItem;
        }

        public void GetLocation(MCalendarEntry calendarentry, ComboBox ortb)
        {
            calendarentry.Place = (MPlace)ortb.SelectedItem;
        }

        public void GetRoom(MCalendarEntry calendarentry, ComboBox raumb)
        {
            calendarentry.Room = (MRoom)raumb.SelectedItem;
        }

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

        public int GetInterationNumber(CheckBox weitercheck, NumericUpDown anzahltage)
        {
            if (weitercheck.Checked == true)
            {
                if (Convert.ToInt32(anzahltage.Value) >= 1)
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
