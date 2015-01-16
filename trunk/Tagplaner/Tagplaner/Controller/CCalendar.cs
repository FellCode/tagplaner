using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Controler für die Klasse MCalendar.
    /// </summary>
    public class CCalendar
    {
        /// <summary>
        /// Funktion zum füllen der Tage mit Kalenderwoche vom Startdatum bis zum Enddatum sowie den dazugehörigen Ferien.
        /// </summary>
        /// <param name="startdate">Beginn des Kalenders</param>
        /// <param name="enddate">Ende des Kalenders</param>
        /// <param name="listDays">Liste der Tage von start bis ende</param>
        /// <param name="vacationCurrentYearUrl">Pfad zur Datei die die Ferientage des ersten Kalenderjahres enthält.</param>
        /// <param name="vacationNextYearUrl">Pfad zur Datei die die Ferientage des zweiten Kalenderjahres enthält.</param>
        /// <returns>Enthält die, für den Kalender angelegten MCalenderDay-Objekte</returns>
        public static List<MCalendarDay> FillDaysInitial(DateTime startdate, DateTime enddate, List<MCalendarDay> listDays, String vacationCurrentYearUrl, String vacationNextYearUrl)
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
        /// <param name="holidayCurrentYearUrl">Pfad zur Datei die die Feiertage des ersten Kalenderjahres enthält.</param>
        /// <param name="holidayNextYearUrl">Pfad zur Datei die die Feiertage des zweiten Kalenderjahres enthält.</param>
        /// <returns>Enthält die Liste der MCalendarDay-Objekte mit den hinzugefügten Feiertagen.</returns>
        public static List<MCalendarDay> FillHolidaysInitial(DateTime startdate, DateTime enddate, List<MCalendarDay> listDays, String holidayCurrentYearUrl, String holidayNextYearUrl)
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

        /// <summary>
        /// Füllt die übergebene Liste mit den selektierten Fachrichtungen der angegebenen Jahrgänge.
        /// </summary>
        /// <param name="speciality">Die Liste der Fcahrichtungen des MCalendar-Objektes.</param>
        /// <param name="classes">Die Anzahl der Jahrgänge des Kalenders.</param>
        /// <param name="typeOfClasses">Enthält die selektierten Fachrichtungen</param>
        /// <returns>Enthält die befüllte Liste der Fachrichtungen.</returns>
        public static List<MSpeciality> FillSpeziallityInitial(List<MSpeciality> speciality, int classes, List<string> typeOfClasses)
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
