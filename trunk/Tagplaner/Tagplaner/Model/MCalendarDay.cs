using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    public class MCalendarDay
    {
        private int id;
        private DateTime calendarDate;
        private string holidayName;
        private string vacationName;
        private string calendarWeek;
        private List<MCalendarEntry> calendarEntry = new List<MCalendarEntry>();
        
        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public string VacationName
        {
            get { return vacationName; }
            set { vacationName = value; }
        }
        public DateTime CalendarDate
        {
            get { return calendarDate; }
        }
        public string HolidayName
        {
            get { return holidayName; }
            set { holidayName = value; }
        }
        public string CalendarWeek
        {
            get { return calendarWeek; }
        }
        public List<MCalendarEntry> CalendarEntry
        {
            get { return calendarEntry; }
        }

        //Printversion des CalendarDate
        public String GetCalendarDatePrintDate()
        {
            return String.Format("{0:dd.MM.yyyy}", calendarDate);
        }
        #endregion

        #region constructor
        public MCalendarDay(DateTime calenderDate, String holidayName, String vacationName, String calenderWeek)
        {
            this.calendarDate = calenderDate;
            this.holidayName = holidayName;
            this.vacationName = vacationName; 
            this.calendarWeek = calenderWeek;

        }

        public MCalendarDay(int id, DateTime calenderDate, String holidayName, String vacationName, String calenderWeek)
        {
            this.id = id;
            this.calendarDate = calenderDate;
            this.holidayName = holidayName;
            this.vacationName = vacationName;
            this.calendarWeek = calenderWeek;

        }
        #endregion
    }
}
