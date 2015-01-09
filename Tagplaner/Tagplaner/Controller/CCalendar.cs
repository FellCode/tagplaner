using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class CCalendar
    {
        public List<MCalendarDay> fillDaysInitial(DateTime startdate,DateTime enddate,List<MCalendarDay> listDays)
        {
            CalendarUtilitys calendarUtilitys = new CalendarUtilitys(startdate, enddate, listDays);
            calendarUtilitys.generateCalenderDayEntrys();

            ICalCSVConverter iCalCSVConverter = new ICalCSVConverter("C:\\Users\\choller\\Documents\\Visual Studio 2013\\Projects\\Tagplaner\\Tagplaner\\Tagplaner\\Ferien_Hessen_2015.ics", "C:\\Users\\choller\\Documents\\Visual Studio 2013\\Projects\\Tagplaner\\Tagplaner\\Tagplaner\\Ferien_Hessen_2016.ics");
            foreach (MVacation vacation in iCalCSVConverter.GetICalEntrys(startdate, enddate))
            {
                foreach (MCalendarDay day in listDays)
                {
                    if (vacation.VacationDate.Equals(day.CalendarDate))
                    {
                        day.VacationName = vacation.VacationName;
                    }
                }
            }
            return listDays;
        }

        public List<MCalendarDay> fillHolidaysInitial(DateTime startdate, DateTime enddate, List<MCalendarDay> listDays)
        {
            CHoliday holiday = new CHoliday();
            foreach (MHoliday tempHoliday in holiday.GetHoliday("Nordrhein-Westfalen", startdate, enddate))
            {
                foreach (MCalendarDay day in listDays)
                {
                    if (tempHoliday.HolidayDate.Equals(day.CalendarDate))
                    {
                        day.HolidayName = tempHoliday.HolidayName;
                    }
                }
            }
            return listDays;
        }
    }
}
