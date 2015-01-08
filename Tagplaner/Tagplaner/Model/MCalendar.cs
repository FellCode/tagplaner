using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    class MCalendar
    {
        private List<MCalendarDay> calendarList = new List<MCalendarDay>();
        private static MCalendar instance;
        public string firstDayString { get; set; }
        public string endDayString { get; set; }

        #region getter
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public List<MCalendarDay> CalendarList
        {
            get { return calendarList; }
        }

        private MCalendar(DateTime start,DateTime end)
        {
            startdate = start;
            enddate = end;

            CalendarUtilitys calendarUtilitys = new CalendarUtilitys(this.startdate, this.enddate, this.CalendarList);
            calendarUtilitys.generateCalenderDayEntrys();
        }

        public static MCalendar getInstance(DateTime start, DateTime end)
        {
            if (instance == null)
            {
                instance = new MCalendar(start,end);
            } return instance;
        }
        #endregion

        #region AddDay-Methods
     
#endregion
        
    }
}
