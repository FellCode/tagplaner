using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MCalendarDay
    {
        private DateTime calendarDate;
        private string holidayName;
        private string calendarWeek;

        #region getter
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
        #endregion

        #region constructor
        public MCalendarDay(DateTime calenderDate, String holidayName, String calenderWeek)
        {
            this.calendarDate = calenderDate;
            this.holidayName = holidayName;
            this.calendarWeek = calenderWeek;
        }
        #endregion
    }
}
