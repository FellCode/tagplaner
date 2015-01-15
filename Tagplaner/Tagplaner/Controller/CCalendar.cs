using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class CCalendar
    {
        /// <summary>
        /// Funktion zum füllen der Tage vom Startdatum bis zum Enddatum sowie den dazugehörigen Ferien.
        /// </summary>
        /// <param name="startdate">Beginn des Kalenders</param>
        /// <param name="enddate">Ende des Kalenders</param>
        /// <param name="listDays">Liste der Tage von start bis ende</param>
        /// <returns></returns>
        public List<MCalendarDay> fillDaysInitial(DateTime startdate, DateTime enddate, List<MCalendarDay> listDays, String vacationCurrentYearUrl, String vacationNextYearUrl)
        {
            CCalendarUtilitys calendarUtilitys = new CCalendarUtilitys(startdate, enddate, listDays);
            calendarUtilitys.generateCalenderDayEntrys();

            CICalCSVConverter iCalCSVConverter = new CICalCSVConverter(vacationCurrentYearUrl, vacationNextYearUrl);
            foreach (MVacation vacation in iCalCSVConverter.GetICalEntrys(startdate, enddate))
            {
                foreach (MCalendarDay day in listDays)
                {
                    if ((vacation.VacationDate.Year == day.CalendarDate.Year) &&
                        (vacation.VacationDate.Month == day.CalendarDate.Month) &&
                        (vacation.VacationDate.Day == day.CalendarDate.Day))
                    {
                        day.VacationName = vacation.VacationName;
                    }
                }
            }
            return listDays;
        }

        /// <summary>
        /// Funktion zum hinzufügen der Feiertage in die Tagliste
        /// </summary>
        /// <param name="startdate">Beginn des Kalenders</param>
        /// <param name="enddate">Ende des Kalenders</param>
        /// <param name="listDays">Liste der Tage von start bis ende</param>
        /// <returns></returns>
        public List<MCalendarDay> fillHolidaysInitial(DateTime startdate, DateTime enddate, List<MCalendarDay> listDays, String holidayCurrentYearUrl, String holidayNextYearUrl)
        {
            CHoliday holiday = new CHoliday();
            foreach (MHoliday tempHoliday in holiday.GetHoliday(holidayCurrentYearUrl, holidayNextYearUrl, startdate, enddate))
            {
                foreach (MCalendarDay day in listDays)
                {
                    if ((tempHoliday.HolidayDate.Year == day.CalendarDate.Year) &&
                        (tempHoliday.HolidayDate.Month == day.CalendarDate.Month) &&
                        (tempHoliday.HolidayDate.Day == day.CalendarDate.Day))
                    {
                        day.HolidayName = tempHoliday.HolidayName;
                    }
                }
            }
            return listDays;
        }

        public List<MSpeciality> fillSpeziallityInitial(List<MSpeciality> speciality, int classes, List<string> typeOfClasses)
        {
            switch (classes)
            {
                case 1:
                    if (String.IsNullOrEmpty(typeOfClasses[0]))
                    {
                        speciality.Add(new MSpeciality(typeOfClasses[0], ""));
                    }
                    if (String.IsNullOrEmpty(typeOfClasses[1]))
                    {
                        speciality.Add(new MSpeciality(typeOfClasses[1], ""));
                    }
                    break;
                case 2:
                    if (String.IsNullOrEmpty(typeOfClasses[0]))
                    {
                        speciality.Add(new MSpeciality(typeOfClasses[0], ""));
                    }
                    if (String.IsNullOrEmpty(typeOfClasses[1]))
                    {
                        speciality.Add(new MSpeciality(typeOfClasses[1], ""));
                    }
                    if (String.IsNullOrEmpty(typeOfClasses[2]))
                    {
                        speciality.Add(new MSpeciality(typeOfClasses[2], ""));
                    }
                    if (String.IsNullOrEmpty(typeOfClasses[3]))
                    {
                        speciality.Add(new MSpeciality(typeOfClasses[3], ""));
                    }
                    break;
            }
            return speciality;
        }
    }
}
