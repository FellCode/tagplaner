using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    public class MCalendar
    {
        private int id;
        private List<MCalendarDay> calendarList = new List<MCalendarDay>();
        private static MCalendar instance;
        private DateTime startdate;
        private DateTime enddate;
        private string startdateString;
        private string enddateString;
        private List<MSpeciality> speciality = new List<MSpeciality>();
        private bool saved;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public DateTime Startdate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        public DateTime Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }
        public string StartdateString
        {
            get { return startdateString; }
            set { startdateString = value; }
        }
        public string EnddateString
        {
            get { return enddateString; }
            set { enddateString = value; }
        }
        public List<MCalendarDay> CalendarList
        {
            get { return calendarList; }
            set { calendarList = value; }
        }
        public List<MSpeciality> Speciality
        {
            get { return speciality; }
            set { speciality = value; }
        }
        public bool Saved
        {
            get { return saved; }
            set { saved = value; }
        }
        #endregion
        private MCalendar()
        {

        }

        public static MCalendar getInstance()
        {
            if (instance == null)
            {
                instance = new MCalendar();
            } return instance;
        }

        public void FillCalendarInitial(DateTime start, DateTime end, int numberOfYears, List<String> typesOfClasses, String vacationCurrentYearUrl, String vacationNextYearUrl, String holidayCurrentYearUrl, String holidayNextYearUrl)
        {
            startdate = start;
            enddate = end;

            StartdateString = String.Format("{0:D}", start);
            EnddateString = String.Format("{0:D}", end);

            Saved = false;

            CalendarList.Clear();

            CCalendar ccalendar = new CCalendar();
            //Befüllen der Ferientagen
            CalendarList = ccalendar.FillDaysInitial(start, end, CalendarList, vacationCurrentYearUrl, vacationNextYearUrl);
            //Befüllen der Ferientagen
            CalendarList = ccalendar.FillHolidaysInitial(start, end, CalendarList, holidayCurrentYearUrl, holidayNextYearUrl);
            //Befüllen der Speciality
            Speciality = ccalendar.FillSpeziallityInitial(Speciality, numberOfYears, typesOfClasses);
        }

        public static void SetInstance(MCalendar calendar)
        {
            instance = calendar;
        }
        #region AddDay-Methods

        #endregion
    }
}
