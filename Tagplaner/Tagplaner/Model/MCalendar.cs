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
        private List<MCalendarDay> calendarList = new List<MCalendarDay>();
        private static MCalendar instance;
        private DateTime startdate;
        private DateTime enddate;
        private string startdateString;
        private string enddateString;

        #region getter
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

        public void fillCalendarInitial(DateTime start, DateTime end)
        {

            startdate = start;
            enddate = end;

            StartdateString = String.Format("{0:D}", start);
            EnddateString = String.Format("{0:D}", end);

            CalendarList.Clear();

            //Befüllen der CalendarList von startdatum bis enddatum
            CCalendar ccalendar = new CCalendar();
            CalendarList = ccalendar.fillDaysInitial(start, end, CalendarList);
            CalendarList = ccalendar.fillHolidaysInitial(start, end, CalendarList);
        }

        #region AddDay-Methods
     
#endregion
        
    }
}
