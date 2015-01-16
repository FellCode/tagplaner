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
    public partial class TagplanChangepanelUserControl : UserControl
    {
        /// <summary>
        /// Autor: Matthias Ohm
        /// Datum: 14.01.15
        /// Dieser Controler dient dazu um ein MCalenderEntry Objekt anzunehmen und in die Comboboxen zu schreiben.
        /// Dazu gibt er die gewählten Daten wieder an das Grid zurück
        /// </summary>

        CDatabase cdb = CDatabase.GetInstance();
        private TagplanBearbeitenUserControl tagplanBearbeitenUserControl;

        private int ortid;
        private MCalendarEntry ccalendarentry;

        public TagplanChangepanelUserControl()
        {
            InitializeComponent();
        }

        private void TagplanChangepanelUserControl_Load(object sender, EventArgs e)
        {

        }

        private void Einfügen_Click(object sender, EventArgs e)
        {

            PasteEntry();

        }

        private void Raum_SelectedIndexChanged(object sender, EventArgs e)
        {
            MRoom sraum = (MRoom)Raum.SelectedItem;

            cdb.FillRoomComboBox(Raum, sraum.Id);
        }

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
            ccalendarentry = calendarentry;

            FillDayType(Tagart);
            FillSeminar(Seminar);
            FillTrainer(Trainer);
            FillCoTrainer(CoTrainer);
            FillLocation(Ort);

            SetDayTyp(ccalendarentry, Tagart);
            SetSeminar(ccalendarentry, Seminar);
            SetTrainer(ccalendarentry, Trainer);
            SetCoTrainer(ccalendarentry, CoTrainer);
            SetLocation(ccalendarentry, Ort);
            SetRoom(ccalendarentry, Raum);

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


            tagplanBearbeitenUserControl.ApplyChangesToGrid(GetInterationNumber(Weiterführung, AnzahlTage), ccalendarentry);

        }


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

        public bool FillSeminar(ComboBox Seminar)
        {
            try
            {
                cdb.FillSeminarComboBox(Seminar);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool FillTrainer(ComboBox Trainer)
        {
            try
            {
                cdb.FillTrainerComboBox(Trainer);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool FillCoTrainer(ComboBox CoTrainer)
        {
            try
            {
                cdb.FillTrainerComboBox(CoTrainer);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool FillLocation(ComboBox Ort)
        {
            try
            {
                cdb.FillPlaceComboBox(Ort);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool FillRoom(ComboBox Ort, ComboBox Raum)
        {

            try
            {

                string idort = Convert.ToString(Ort.SelectedValue);
                ortid = Convert.ToInt32(idort);
                cdb.FillRoomComboBox(Raum, ortid);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }

        }

        public void UpdateCalenderEntry(ComboBox Tagart, TextBox Kommentar)
        {

            switch (Convert.ToString(Tagart.SelectedValue))
            {
                case "Seminar":
                    break;
                case "Berufsschule":
                    break;
                case "Praxis":
                    break;
            }
        }

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

        public void SetDayTyp(MCalendarEntry calendarentry, ComboBox tagartb)
        {
            if (calendarentry.Practice == null)
            {
                if (calendarentry.School == null)
                {
                    tagartb.SelectedIndex = 1;
                }
                if (calendarentry.Seminar == null)
                {
                    tagartb.SelectedIndex = 2;
                }

            }
            else
            {
                tagartb.SelectedIndex = 3;
            }
        }

        public void SetSeminar(MCalendarEntry calendarentry, ComboBox seminarb)
        {
            if (calendarentry.Seminar == null)
            {

            }
            else
            {
                seminarb.SelectedItem = calendarentry.Seminar;
            }
        }

        public void SetTrainer(MCalendarEntry calendarentry, ComboBox trainerb)
        {
            if (calendarentry.Trainer == null)
            {

            }
            else
            {
                trainerb.SelectedItem = calendarentry.Trainer;
            }
        }

        public void SetCoTrainer(MCalendarEntry calendarentry, ComboBox cotrainerb)
        {
            if (calendarentry.Cotrainer == null)
            {

            }
            else
            {
                cotrainerb.SelectedItem = calendarentry.Cotrainer;
            }
        }

        public void SetLocation(MCalendarEntry calendarentry, ComboBox ortb)
        {
            if (calendarentry.Room == null)
            {

            }
            else
            {

            }
        }

        public void SetRoom(MCalendarEntry calendarentry, ComboBox raumb)
        {
            if (calendarentry.Room == null)
            {

            }
            else
            {
                raumb.SelectedItem = calendarentry.Room;
            }
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
            }
            else
            {
                kommentarb.SelectedItem = calendarentry.Practice;
            }
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
                case 1:
                    calendarentry.Seminar.Comment = kommentarb.SelectedText;
                    break;
                case 2:
                    calendarentry.School.Comment = kommentarb.SelectedText;
                    break;
                case 3:
                    calendarentry.Practice.Comment = kommentarb.SelectedText;
                    break;
            }
        }

        public int GetInterationNumber(CheckBox weitercheck, NumericUpDown anzahltage)
        {
            if (weitercheck.Checked == true)
            {
                return Convert.ToInt32(anzahltage.Value);
            }
            else
            {
                return 1;
            }

        }

    }
}
