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
        public string firstDayString { get; set; }
        public string endDayString { get; set; }

        #region getter
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public List<MCalendarDay> CalendarList
        {
            get { return calendarList; }
        }

        private MCalendar(DateTime start, DateTime end)
        {
            startdate = start;
            enddate = end;

            //Befüllen der CalendarList von startdatum bis enddatum
            CalendarUtilitys calendarUtilitys = new CalendarUtilitys(this.startdate, this.enddate, this.CalendarList);
            calendarUtilitys.generateCalenderDayEntrys();

            ICalCSVConverter iCalCSVConverter = new ICalCSVConverter(
                AppDomain.CurrentDomain.BaseDirectory + @"\Ferien_Hessen_2015.ics",
                AppDomain.CurrentDomain.BaseDirectory + @"\Ferien_Hessen_2016.ics"
            );
            foreach (MVacation vacation in iCalCSVConverter.GetICalEntrys(this.startdate, this.enddate))
            {
                foreach (MCalendarDay day in this.CalendarList)
                {
                    if (vacation.VacationDate.Equals(day.CalendarDate))
                    {
                        day.VacationName = vacation.VacationName;
                    }
                }
            }
            CHoliday holiday = new CHoliday();
            foreach (MHoliday tempHoliday in holiday.GetHoliday("Nordrhein-Westfalen", this.startdate, this.enddate))
            {
                foreach (MCalendarDay day in this.CalendarList)
                {
                    if (tempHoliday.HolidayDate.Equals(day.CalendarDate))
                    {
                        day.HolidayName = tempHoliday.HolidayName;
                    }
                }
            }
        }

        public static MCalendar getInstance(DateTime start, DateTime end)
        {
            if (instance == null)
            {
                instance = new MCalendar(start, end);
            } return instance;
        }
        #endregion

        #region AddDay-Methods

        #endregion

    }
}
