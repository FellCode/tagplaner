using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    class CStatisticUtilitys
    {
        private CStatisticUtilitys() { }

        /// <summary>
        /// Zählt die Anzahl alle Tage zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static int CountAllApprenticeshipDays()
        {
            return MCalendar.getInstance().CalendarList.Count();
        }

        /// <summary>
        /// Zählt alle Wochenendtage zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static int CountWeekendDays()
        {
            MCalendarDay calendarDay = null;
            int weekendCounter = 0;

            for (int i = 0; i < MCalendar.getInstance().CalendarList.Count(); i++)
            {
                calendarDay = MCalendar.getInstance().CalendarList.ElementAt(i);

                if (calendarDay.CalendarDate.DayOfWeek.ToString().Equals("Saturday") ||
                    calendarDay.CalendarDate.DayOfWeek.ToString().Equals("Sunday"))
                {
                    weekendCounter++;
                }
            }

            return weekendCounter;
        }

        /// <summary>
        /// Zählt alle Feiertage zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static int CountHolidayDays()
        {
            MCalendarDay calendarDay = null;
            int holidayCounter = 0;

            for (int i = 0; i < MCalendar.getInstance().CalendarList.Count(); i++)
            {
                calendarDay = MCalendar.getInstance().CalendarList.ElementAt(i);

                if (!String.IsNullOrEmpty(calendarDay.HolidayName))
                {
                    holidayCounter++;
                }
            }

            return holidayCounter;
        }

        /// <summary>
        /// Zählt alle Seminartage zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static int CountSeminarDays()
        {
            return 0;
        }

        /// <summary>
        /// Zählt alle Schultage zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück 
        /// </summary>
        /// <returns></returns>
        public static int CountSchoolDays()
        {
            return 0;
        }

        /// <summary>
        /// Zählt alle Praxistage  zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static int CountPraticeDays()
        {
            return 0;
        }
    }
}
