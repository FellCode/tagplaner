/*
 * Created by SharpDevelop.
 * User: tbender2
 * Date: 07.01.2015
 * Time: 09:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using DDay.iCal;
using System.IO;


namespace Tagplaner
{
	/// <summary>
	/// Die Klasse liest ICS-Dateien und liefert ein List-Object mit
	/// den MVacation-Objekten in einem gewählten Zeitraum
	/// </summary>
	public class CICalCSVConverter: IICalCSVConverter
	{
		/// <summary>
		/// Feld für die ICS-Datei (Pfad/Name) für das aktuelle Kalenderjahr
    	/// </summary>
		private string currentYearURL;
		/// <summary>
		/// Feld für die ICS-Datei (Pfad/Name) für das nächste Kalenderjahr
    	/// </summary>
		private string nextYearURL;
		/// <summary>
		/// Feld für das Startdatum im ersten Kalenderjahr
    	/// </summary>
		private DateTime startdate;
		/// <summary>
		/// Feld für das Enddatum im nächsten Kalenderjahr
    	/// </summary>
		private DateTime enddate;
		/// <summary>
		/// Feld für das ICalendar-Objekt aus der iDay-Bibliothek
    	/// </summary>
		private IICalendarCollection icalendar;
		/// <summary>
		/// Nachricht einer Ausnahme in lesbarer Form 
    	/// </summary>
		private string exceptionMessage;
		/// <summary>
		/// Nachricht einer Ausnahme in C#
    	/// </summary>
		private string IOException;
		/// <summary>
		/// Feld für die Liste der Ferieneinträge
 		/// <seealso cref="MVacation"/>
    	/// </summary>
		private List<MVacation> vacationList;
		
		/// <summary>
		/// Konstruktor der CICalCSVConverter-Klasse
 		/// <para name="currentYearURL">Pfad und Dateiname der ICS-Datei mit den Ferien für das Jahr an dem der Tagplan geginnt</para>
 		/// <para name="nextYearURL">Pfad und Dateiname der ICS-Datei mit den Ferien für das Jahr an dem der Tagplan endet</para>
    	/// </summary>
		public CICalCSVConverter(string currentYearURL, string nextYearURL)
		{
			CurrentYearURL = currentYearURL;
			NextYearURL = nextYearURL;
			VacationList = new List<MVacation>();
		}

        /// <summary>
        /// Standartkonstruktor der CICalCSVConverter-Klasse
        /// </summary>
        public CICalCSVConverter()
        {

        }
		
		/// <summary>
		/// Testet, ob in der ICS-Datei Ferien-Einträge für einen Zeitraum  (inklusiv) vorhandenen sind
 		/// <para name="startdate">Datum von (inklusiv)</para>
 		/// <para name="enddate">Datum bis (inklusiv)</para>
 		/// <returns>liefert true, wenn mindestens ein Ferien-Eintrag im Zeitraum liegt, sonst false</returns>
    	/// </summary>
		public bool CheckICSFile(DateTime startdate, DateTime enddate, string filename) {
			ExceptionMessage = null;
			
			try {
				ICalendar= iCalendar.LoadFromFile(filename);
			}
			catch(Exception exception){
				IOException = exception.Message;
				ExceptionMessage = "Datei " + filename + " nicht gefunden!";
				return false;
			}
			IList<Occurrence> occurrences = icalendar.GetOccurrences(startdate, enddate);
			
			if(occurrences.Count == 0){
				ExceptionMessage = "Datei " + filename + " enthält keine Einträge zwischen Start- oder Enddatum!";
				return false;
			}
			return true;
		}
		
    	/// <summary>
		/// Liest alle Einträge in den ICS-Dateien Ferien-Einträge für einen Zeitraum  (inklusiv)
 		/// <para name="startdate">Datum von</para>
 		/// <para name="enddate">Datum bis</para>
 		/// <returns>Liste der MVacation-Objekte, die im Zeitraum liegen, sonst null</returns>
    	/// </summary>
		public List<MVacation> GetICalEntrys(DateTime startdate, DateTime enddate){
			bool iCalFilesReady = ReadICalEntrysCurrentYear(new DateTime(startdate.Year, startdate.Month, startdate.Day), new DateTime(startdate.Year, 12, 31));
			iCalFilesReady = ReadICalEntrysNextYear(new DateTime(enddate.Year, 01, 01), new DateTime(enddate.Year, enddate.Month, enddate.Day));
			if(iCalFilesReady) return VacationList;
			return null;
		}
		
    	/// <summary>
		/// Liest alle Einträge in den ICS-Dateien Ferien-Einträge das aktuelle Jahr 
 		/// <para name="startdate">Datum von</para>
 		/// <para name ="enddate">Datum bis</para>
    	/// </summary>
		public bool ReadICalEntrysCurrentYear(DateTime startdate, DateTime enddate){
			
			try {
				ICalendar= iCalendar.LoadFromFile(currentYearURL);
			}
			catch(Exception exception){
				IOException = exception.Message;
				ExceptionMessage = "Datei " + currentYearURL + " nicht gefunden!";
				return false;
			}
			
			Startdate = startdate;
			
			IList<Occurrence> occurrences = icalendar.GetOccurrences
				(startdate,enddate);

			foreach (Occurrence occurrence in occurrences)
			{
				IEvent evt = occurrence.Source as IEvent;
				Console.WriteLine(evt.Summary);
				GenerateMVacationCurrentYear(evt.Start.Local, evt.End.Local, evt.Summary);
			}
			return true;
		}
		
    	/// <summary>
		/// Liest alle Einträge in den ICS-Dateien Ferien-Einträge das nächste Jahr 
 		/// <para name="startdate">Datum von</para>
 		/// <para name ="enddate">Datum bis</para>
    	/// </summary>
		public bool ReadICalEntrysNextYear(DateTime startdate, DateTime enddate){
			
			try {
				ICalendar= iCalendar.LoadFromFile(nextYearURL);
			}
			catch(Exception exception){
				IOException = exception.Message;
				ExceptionMessage = "Datei " + nextYearURL + " nicht gefunden!";;
				return false;
			}
			
			Enddate = enddate;
			
			IList<Occurrence> occurrences = icalendar.GetOccurrences
				(startdate,enddate);

			foreach (Occurrence occurrence in occurrences)
			{
				IEvent evt = occurrence.Source as IEvent;
				Console.WriteLine(evt.Summary);
				GenerateMVacationNextYear(evt.Start.Local, evt.End.Local, evt.Summary);
				
			}
			return true;
		}
		
    	/// <summary>
		/// Erzeugt die MVacation-Objekte für das aktuelle Jahr 
 		/// <para name="startdate">Datum von</para>
 		/// <para name ="enddate">Datum bis</para>
 		/// <para name ="vacationName">Bezeichnung Ferien</para>
    	/// </summary>
		private void GenerateMVacationCurrentYear(DateTime start, DateTime end, string vacationName){
			for(var day = start; day.Date <= end; day = day.AddDays(1)){
				MVacation mVacation = null;
				if(day.Date >= Startdate.Date) {
					mVacation= new MVacation(vacationName, day);
					VacationList.Add(mVacation);
				}
			}
		}
		
    	/// <summary>
		/// Erzeugt die MVacation-Objekte für das nächste Jahr 
 		/// <para name="startdate">Datum von</para>
 		/// <para name ="enddate">Datum bis</para>
 		/// <para name ="vacationName">Bezeichnung Ferien</para>
    	/// </summary>
		private void GenerateMVacationNextYear(DateTime start, DateTime end, string vacationName){
			for(var day = start; day.Date <= end; day = day.AddDays(1)){
				MVacation mVacation = null;
				if(day.Date <= Enddate.Date) {
					mVacation= new MVacation(vacationName, day);
					VacationList.Add(mVacation);
				}
			}
		}


        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public bool CheckCsvFile(DateTime startDate, DateTime endDate, String fileUrl)
        {
            bool csvOkay = false;

            var reader = new StreamReader(fileUrl);
            List<string> listOccurences = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                listOccurences.Add(values[0]);
            }

            foreach (DateTime dayInEnum in EachDay(startDate, endDate))
            {
                for (int counter = 1; counter < listOccurences.Count; counter++)
                {
                    if (dayInEnum.ToShortDateString() == listOccurences[counter])
                    {
                        csvOkay = true;
                    }
                }
            }
            return csvOkay;
        }

        public bool CheckCurrentyear(DateTime startdate, string filename)
        {
            ExceptionMessage = null;

            try
            {
                ICalendar = iCalendar.LoadFromFile(filename);
            }
            catch (Exception exception)
            {
                IOException = exception.Message;
                ExceptionMessage = "Datei " + filename + " nicht gefunden!";
                return false;
            }

            IList<Occurrence> occurrences = icalendar.GetOccurrences(startdate, new DateTime(startdate.Year, 12, 31));

            if (occurrences.Count == 0)
            {
                ExceptionMessage = "Datei " + filename + " enthält keine Einträge zwischen Start- oder Enddatum!";
                return false;
            }
            return true;
        }
		
    	/// <summary>
		/// Property für das Feld currentYearURL
    	/// </summary>
		public string CurrentYearURL {
			get { return currentYearURL; }
			set { currentYearURL = value; }
		}
		
    	/// <summary>
		/// Property für das Feld nextYearURL
    	/// </summary>
		public string NextYearURL {
			get { return nextYearURL; }
			set { nextYearURL = value; }
		}

		/// <summary>
		/// Property für das Feld startDate
    	/// </summary>
		public DateTime Startdate {
			get { return startdate; }
			set { startdate = value; }
		}

    	/// <summary>
		/// Property für das Feld endDate
    	/// </summary>
		public DateTime Enddate {
			get { return enddate; }
			set { enddate = value; }
		}
		
    	/// <summary>
		/// Property für das Feld iCalendar
    	/// </summary>
		public IICalendarCollection ICalendar {
			get { return icalendar; }
			set { icalendar = value; }
		}
		
    	/// <summary>
		/// Property für das Feld exceptionMessage
    	/// </summary>
		public string ExceptionMessage {
			get { return exceptionMessage; }
			set { exceptionMessage = value; }
		}
		
    	/// <summary>
		/// Property für das Feld vacationList
    	/// </summary>
		public List<MVacation> VacationList {
			get { return vacationList; }
			set { vacationList = value; }
		}
	}
}
