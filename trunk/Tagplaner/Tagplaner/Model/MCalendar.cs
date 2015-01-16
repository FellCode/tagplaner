using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Die Klasse bildet einen Tagplan ab.
    /// </summary>
    [Serializable()]
    public class MCalendar
    {
        private int id;
        private List<MCalendarDay> calendarList = new List<MCalendarDay>();
        private static MCalendar instance;
        private DateTime startdate;
        private DateTime enddate;
        private string startdateString;
        private string enddateString;
        private List<MSpeciality> speciality = new List<MSpeciality>();
        private bool saved;
        private DateTime lastModified;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public DateTime Startdate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        public DateTime Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }
        public string StartdateString
        {
            get { return startdateString; }
            set { startdateString = value; }
        }
        public string EnddateString
        {
            get { return enddateString; }
            set { enddateString = value; }
        }
        public List<MCalendarDay> CalendarList
        {
            get { return calendarList; }
            set { calendarList = value; }
        }
        public List<MSpeciality> Speciality
        {
            get { return speciality; }
            set { speciality = value; }
        }
        public bool Saved
        {
            get { return saved; }
            set { saved = value; }
        }
        public DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }
        #endregion

        private MCalendar()
        {

        }
        /// <summary>
        /// Prüft, ob bereits eine Instanz der Klasse existiert. Diese wird zurück gegeben bzw. zuvor eine neue Instanz erzeugt.
        /// </summary>
        /// <returns>Enthält die Instanz der Klasse</returns>
        public static MCalendar getInstance()
        {
            if (instance == null)
            {
                instance = new MCalendar();
            } return instance;
        }
        /// <summary>
        /// Befüllt den Kalender initial mit den Tagen, beginnend beim angegebenen Anfangsdatum bis zum Enddatum sowie den dazugehörigen Kalenderwochen, Feiertagen und Ferien.
        /// </summary>
        /// <param name="start">Beginn des Kalenders.</param>
        /// <param name="end">Ende des Kalenders.</param>
        /// <param name="numberOfYears">Anzahl an Jahrgängen für die der Kalender erstellt wird.</param>
        /// <param name="typesOfClasses">Gibt die Fachrichtungen der Jahrgänge an.</param>
        /// <param name="vacationCurrentYearUrl">Pfad zur Datei die die Ferientage des ersten Kalenderjahres enthält.</param>
        /// <param name="vacationNextYearUrl">Pfad zur Datei die die Ferientage des zweiten Kalenderjahres enthält.</param>
        /// <param name="holidayCurrentYearUrl">Pfad zur Datei die die Feiertage des ersten Kalenderjahres enthält.</param>
        /// <param name="holidayNextYearUrl">Pfad zur Datei die die Feiertage des zweiten Kalenderjahres enthält.</param>
        public void FillCalendarInitial(DateTime start, DateTime end, int numberOfYears, List<String> typesOfClasses, String vacationCurrentYearUrl, String vacationNextYearUrl, String holidayCurrentYearUrl, String holidayNextYearUrl)
        {
            startdate = start;
            enddate = end;

            StartdateString = String.Format("{0:D}", start);
            EnddateString = String.Format("{0:D}", end);

            Saved = false;

            CalendarList.Clear();

            //Befüllen der Ferientagen
            CalendarList = CCalendar.FillDaysInitial(start, end, CalendarList, vacationCurrentYearUrl, vacationNextYearUrl);
            //Befüllen der Ferientagen
            CalendarList = CCalendar.FillHolidaysInitial(start, end, CalendarList, holidayCurrentYearUrl, holidayNextYearUrl);
            //Befüllen der Speciality
            Speciality = CCalendar.FillSpeziallityInitial(Speciality, numberOfYears, typesOfClasses);
        }
        /// <summary>
        /// Ersetzt die vorhandene Instanz des Kalenders durch die, in der Parameterliste angegebene.
        /// </summary>
        /// <param name="calendar">Kalenderinstanz die eingefügt werden soll.</param>
        public static void SetInstance(MCalendar calendar)
        {
            instance = calendar;
        }
    }
}
