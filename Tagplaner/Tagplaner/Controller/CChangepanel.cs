using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    public class CChangepanel : IChangepanel
    {
        /// <summary>
        /// Autor: Matthias Ohm
        /// Datum: 14.01.15
        /// Dieser Controler dient dazu um ein MCalenderEntry Objekt anzunehmen und in die Comboboxen zu schreiben.
        /// Dazu gibt er die gewählten Daten wieder an das Grid zurück
        /// </summary>
        private CDatabase cdb = CDatabase.GetInstance();
        

        private TagplanChangepanelUserControl tcp = new TagplanChangepanelUserControl();
        private TagplanBearbeitenUserControl tagplanBearbeitenUserControl = new TagplanBearbeitenUserControl();
        
        private int ortid;
        private MCalendarEntry ccalendarentry;
     
        
        /// <summary>
        /// Diese Methode nimmt das MCalenderEntry, Combo- und Textboxen Objekte und füllt damit die Comboboxen und das Textfeld
        /// </summary>
        /// <param name="calendarentry"></param>
        /// <param name="tagartb"></param>
        /// <param name="seminarb"></param>
        /// <param name="trainerb"></param>
        /// <param name="cotrainerb"></param>
        /// <param name="ortb"></param>
        /// <param name="raumb"></param>
        /// <param name="kommentarb"></param>
        //public void ChangeCalendarEntry(MCalendarEntry calendarentry, ComboBox tagartb, ComboBox seminarb,
        //    ComboBox trainerb, ComboBox cotrainerb, ComboBox ortb, ComboBox raumb, TextBox kommentarb)
        //{
        //    ccalendarentry = calendarentry;

        //    FillDayType(tagartb);
        //    FillSeminar(seminarb);
        //    FillTrainer(trainerb);
        //    FillCoTrainer(cotrainerb);
        //    FillLocation(ortb);

        //    SetDayTyp(ccalendarentry, tagartb);
        //    SetSeminar(ccalendarentry, seminarb);
        //    SetTrainer(ccalendarentry, trainerb);
        //    SetCoTrainer(ccalendarentry, cotrainerb);
        //    SetLocation(ccalendarentry, raumb);
        //    SetRoom(ccalendarentry, raumb);

        //}

        public void ChangeCalendarEntry(MCalendarEntry calendarentry)
        {
            ccalendarentry = calendarentry;

            FillDayType(tcp.Tagart);
            FillSeminar(tcp.Seminar);
            FillTrainer(tcp.Trainer);
            FillCoTrainer(tcp.CoTrainer);
            FillLocation(tcp.Ort);

            SetDayTyp(ccalendarentry, tcp.Tagart);
            SetSeminar(ccalendarentry, tcp.Seminar);
            SetTrainer(ccalendarentry, tcp.Trainer);
            SetCoTrainer(ccalendarentry, tcp.CoTrainer);
            SetLocation(ccalendarentry, tcp.Ort);
            SetRoom(ccalendarentry, tcp.Raum);

        }

        /// <summary>
        /// Diese Methode nimmt die Objekte aus den Combo- und Textboxen und fügt diese einem MCalenderEntry Objekt zu
        /// </summary>
        /// <param name="tagartb"></param>
        /// <param name="seminarb"></param>
        /// <param name="trainerb"></param>
        /// <param name="cotrainerb"></param>
        /// <param name="ortb"></param>
        /// <param name="raumb"></param>
        /// <param name="kommentarb"></param>
        //public void PasteEntry(ComboBox tagartb, ComboBox seminarb,
        //    ComboBox trainerb, ComboBox cotrainerb, ComboBox ortb, ComboBox raumb, TextBox kommentarb)
        //{

        //    GetSeminar(ccalendarentry, seminarb);
        //    GetTrainer(ccalendarentry, trainerb);
        //    GetCoTrainer(ccalendarentry, cotrainerb);
        //    GetLocation(ccalendarentry, raumb);
        //    GetRoom(ccalendarentry, raumb);
        //    GetComment(ccalendarentry, kommentarb, tagartb);


        //    cep.ApplyChangesToGrid(1, ccalendarentry);

        //}

        public void PasteEntry()
        {

            GetSeminar(ccalendarentry, tcp.Seminar);
            GetTrainer(ccalendarentry, tcp.Trainer);
            GetCoTrainer(ccalendarentry, tcp.CoTrainer);
            GetLocation(ccalendarentry, tcp.Ort);
            GetRoom(ccalendarentry, tcp.Raum);
            GetComment(ccalendarentry, tcp.Kommentar, tcp.Tagart);
            GetInterationNumber(tcp.Weiterführung, tcp.AnzahlTage);


            tagplanBearbeitenUserControl.ApplyChangesToGrid(GetInterationNumber(tcp.Weiterführung, tcp.AnzahlTage), ccalendarentry);

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
            catch(FormatException)
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
            catch(FormatException)
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
            catch(FormatException)
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

        public int GetInterationNumber(CheckBox weitercheck,NumericUpDown anzahltage)
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