using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Diese Klasse bildet einen Kalendertag ab. Der Kalendertag enthält das Datum, eventuell den Ferien und/oder den Feiertagsnamen des Tages sowie eine
    /// Liste mit Kalendereinträgen.
    /// </summary>
    [Serializable()]
    public class MCalendarDay
    {
        private int id;
        private DateTime calendarDate;
        private string holidayName;
        private string vacationName;
        private string calendarWeek;
        private List<MCalendarEntry> calendarEntry;
        
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
            set { calendarEntry = value; }
        }

        //Printversion des CalendarDate
        public String GetCalendarDatePrintDate()
        {
            return String.Format("{0:dd.MM.yyyy}", calendarDate);
        }
        #endregion

        #region constructor
        /// <summary>
        /// Konstruktor zum anlegen eines neuen Kalendertages.
        /// </summary>
        /// <param name="calenderDate">Datum des Kalendertages</param>
        /// <param name="holidayName">Name des Feiertages</param>
        /// <param name="vacationName">Name des Ferientages</param>
        /// <param name="calenderWeek">Kalenderwoche</param>
        public MCalendarDay(DateTime calenderDate, String holidayName, String vacationName, String calenderWeek)
        {
            this.calendarDate = calenderDate;
            this.holidayName = holidayName;
            this.vacationName = vacationName; 
            this.calendarWeek = calenderWeek;
            calendarEntry = new List<MCalendarEntry>();

        }

        /// <summary>
        /// Konstruktor zum anlegen eines bereits existierenden Kalendertages aus der Datenbank.
        /// </summary>
        /// <param name="id">ID des Objektes aus der Datenbank</param>
        /// <param name="calenderDate">Datum des Kalendertages</param>
        /// <param name="holidayName">Name des Feiertages</param>
        /// <param name="vacationName">Name des Ferientages</param>
        /// <param name="calenderWeek">Kalenderwoche</param>
        public MCalendarDay(int id, DateTime calenderDate, String holidayName, String vacationName, String calenderWeek)
        {
            this.id = id;
            this.calendarDate = calenderDate;
            this.holidayName = holidayName;
            this.vacationName = vacationName;
            this.calendarWeek = calenderWeek;
            calendarEntry = new List<MCalendarEntry>();
        }
        #endregion
    }
}
