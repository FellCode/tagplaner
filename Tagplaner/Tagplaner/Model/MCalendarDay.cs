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
        private DateTime calendarDate;
        private string holidayName;
        private string vacationName;
        private string calendarWeek;
        private List<MCalendarEntry> calendarEntry;

        #region getter
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
        }
        public string CalendarWeek
        {
            get { return calendarWeek; }
        }
        public List<MCalendarEntry> CalendarEntry
        {
            get { return calendarEntry}
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
        #endregion
    }
}
