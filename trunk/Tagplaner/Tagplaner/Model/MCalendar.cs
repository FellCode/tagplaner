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
        #endregion

        public void fillCalendarInitial(DateTime start, DateTime end, int numberOfYears, List<String> typesOfClasses, String vacationCurrentYearUrl, String vacationNextYearUrl, String holidayCurrentYearUrl, String holidayNextYearUrl)
        {
            startdate = start;
            enddate = end;

            StartdateString = String.Format("{0:D}", start);
            EnddateString = String.Format("{0:D}", end);

            Saved = false;

            CalendarList.Clear();

            //Befüllen der CalendarList mit Ferientagen von startdatum bis enddatum
            CCalendar ccalendar = new CCalendar();
            CalendarList = ccalendar.fillDaysInitial(start, end, CalendarList, vacationCurrentYearUrl, vacationNextYearUrl);

            //Befüllen der CalendarList mit Ferientagen von startdatum bis enddatum
            CalendarList = ccalendar.fillHolidaysInitial(start, end, CalendarList, holidayCurrentYearUrl, holidayNextYearUrl);

            Speciality = ccalendar.fillSpeziallityInitial(Speciality, numberOfYears, typesOfClasses);
        }

        #region AddDay-Methods

        #endregion

    }
}
