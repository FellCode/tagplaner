using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Klasse mit static Methoden zum erzeugen von Statistiken
    /// </summary>
    public class CStatisticUtilitys
    {
        private CStatisticUtilitys() { }

        /// <summary>
        /// Zählt die Anzahl alle Tage zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static int CountAllApprenticeshipDays()
        {
            return MCalendar.GetInstance().CalendarList.Count();
        }

        /// <summary>
        /// Zählt alle Wochenendtage zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static int CountWeekendDays()
        {
            int weekendCounter = 0;

            foreach (MCalendarDay calendarDay in MCalendar.GetInstance().CalendarList) {
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
            int holidayCounter = 0;

            foreach (MCalendarDay calendarDay in MCalendar.GetInstance().CalendarList) {
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
        public static int CountSeminarDays(int position)
        {
            int seminarCounter = 0;
            
            foreach (MCalendarDay calendarDay in MCalendar.GetInstance().CalendarList)
            {
                //Prüfung, ob ein Kalendereintrag vorhanden ist, und ob es kein Wochenende ist.
                if (calendarDay.CalendarEntry.Count > 0
                    && !(calendarDay.CalendarDate.DayOfWeek.ToString().Equals("Saturday")
                    || calendarDay.CalendarDate.DayOfWeek.ToString().Equals("Sunday")))
                {
                    if(calendarDay.CalendarEntry.ElementAt(position).Seminar != null)
                    {
                        seminarCounter++;
                    }
                }
            }
            
            return seminarCounter;
        }

        /// <summary>
        /// Zählt alle Schultage zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück 
        /// </summary>
        /// <returns></returns>
        public static int CountSchoolDays(int position)
        {
            int schoolCounter = 0;

            foreach (MCalendarDay calendarDay in MCalendar.GetInstance().CalendarList)
            {
                //Prüfung, ob ein Kalendereintrag vorhanden ist, und ob es kein Wochenende ist.
                if (calendarDay.CalendarEntry.Count > 0
                    && !(calendarDay.CalendarDate.DayOfWeek.ToString().Equals("Saturday")
                    || calendarDay.CalendarDate.DayOfWeek.ToString().Equals("Sunday")))
                {
                    if (calendarDay.CalendarEntry.ElementAt(position).School != null)
                    {
                        schoolCounter++;
                    }
                }
            }
            
            return schoolCounter;
        }

        /// <summary>
        /// Zählt alle Praxistage  zwischen dem Start- und Enddatum der MCalendar Instanz
        /// und gibt diese zurück
        /// </summary>
        /// <returns></returns>
        public static int CountPraticeDays(int position)
        {
            int praticeCounter = 0;

            foreach (MCalendarDay calendarDay in MCalendar.GetInstance().CalendarList)
            {
                //Prüfung, ob ein Kalendereintrag vorhanden ist, und ob es kein Wochenende ist.
                if (calendarDay.CalendarEntry.Count > 0
                    && !(calendarDay.CalendarDate.DayOfWeek.ToString().Equals("Saturday")
                    || calendarDay.CalendarDate.DayOfWeek.ToString().Equals("Sunday")))
                {
                    if (calendarDay.CalendarEntry.ElementAt(position).Practice != null)
                    {
                        praticeCounter++;
                    }
                }
            }

            return praticeCounter;
        }
    }
}
