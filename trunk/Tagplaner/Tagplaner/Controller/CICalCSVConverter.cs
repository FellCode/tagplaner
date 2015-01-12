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


namespace Tagplaner
{
	/// <summary>
	/// Konstruktor erwartet einen string mit der URL der ics-Datei
	/// </summary>
	
	public class CICalCSVConverter: IICalCSVConverter
	{
		private string currentYearURL;
		private string nextYearURL;		
		private DateTime startdate;
		private DateTime enddate;
		private IICalendarCollection icalendar;
		private string exceptionMessage;
		private List<MVacation> vacationList;
		
		public CICalCSVConverter(string currentYearURL, string nextYearURL)
		{
			CurrentYearURL = currentYearURL;
			NextYearURL = nextYearURL;
			VacationList = new List<MVacation>();
		}
		
		public List<MVacation> GetICalEntrys(DateTime startdate, DateTime enddate){
            bool iCalFilesReady = ReadICalEntrysCurrentYear(new DateTime(startdate.Year, startdate.Month, startdate.Day), new DateTime(startdate.Year, 12, 31));
            iCalFilesReady = ReadICalEntrysNextYear(new DateTime(enddate.Year, 01, 01), new DateTime(enddate.Year, enddate.Month, enddate.Day));
			if(iCalFilesReady) return VacationList;
            return null;
		}
		
		public bool ReadICalEntrysCurrentYear(DateTime startdate, DateTime enddate){
			try {
				ICalendar= iCalendar.LoadFromFile(currentYearURL);
			}
			catch(Exception exception){
				ExceptionMessage = exception.Message;
				return false;
			}
			
			Startdate = startdate;
			
			IList<Occurrence> occurrences = icalendar.GetOccurrences
				(startdate,enddate);

			Console.WriteLine("Entrys from " + Startdate.ToString() + " >");

			foreach (Occurrence occurrence in occurrences)
			{
				//DateTime occurrenceTime = occurrence.Period.StartTime.Local;
				IRecurringComponent rc = occurrence.Source as IRecurringComponent;
				IEvent evt = occurrence.Source as IEvent;
				//MHoliday mHoliday = new MHoliday(evt.Start.Local, evt.End.Local, evt.Summary);
				//HolidayList.Add(mHoliday);
				GenerateMVacationCurrentYear(evt.Start.Local, evt.End.Local, evt.Summary);
				
			}
			//return VacationList;
            return true;
		}
		
		public bool ReadICalEntrysNextYear(DateTime startdate, DateTime enddate){
			
			try {
				ICalendar= iCalendar.LoadFromFile(nextYearURL);
			}
			catch(Exception exception){
				ExceptionMessage = exception.Message;
				return false;
			}
			
			Enddate = enddate;
			
			IList<Occurrence> occurrences = icalendar.GetOccurrences
				(startdate,enddate);

			//Console.WriteLine("Entrys until " + enddate.ToString() + " >");

			foreach (Occurrence occurrence in occurrences)
			{
				//DateTime occurrenceTime = occurrence.Period.StartTime.Local;
				IRecurringComponent rc = occurrence.Source as IRecurringComponent;
				IEvent evt = occurrence.Source as IEvent;
				//MHoliday mHoliday = new MHoliday(evt.Start.Local, evt.End.Local, evt.Summary);
				//HolidayList.Add(mHoliday);
				GenerateMVacationNextYear(evt.Start.Local, evt.End.Local, evt.Summary);
				
			}
			//return VacationList;
            return true;
		}
		
		private void GenerateMVacationCurrentYear(DateTime start, DateTime end, string vacationName){
			for(var day = start; day.Date <= end; day = day.AddDays(1)){				
				MVacation mVacation = null;
				if(day.Date >= Startdate.Date) {
					mVacation= new MVacation(vacationName, day);
					VacationList.Add(mVacation);
				}
			}
		}
		
		private void GenerateMVacationNextYear(DateTime start, DateTime end, string vacationName){
			for(var day = start; day.Date <= end; day = day.AddDays(1)){				
				MVacation mVacation = null;
				if(day.Date <= Enddate.Date) {
					mVacation= new MVacation(vacationName, day);
					VacationList.Add(mVacation);
				}
			}
		}
		
		public string CurrentYearURL {
			get { return currentYearURL; }
			set { currentYearURL = value; }
		}
		
		public string NextYearURL {
			get { return nextYearURL; }
			set { nextYearURL = value; }
		}

		
		public DateTime Startdate {
			get { return startdate; }
			set { startdate = value; }
		}

		public DateTime Enddate {
			get { return enddate; }
			set { enddate = value; }
		}
		
		public IICalendarCollection ICalendar {
			get { return icalendar; }
			set { icalendar = value; }
		}
				
		public string ExceptionMessage {
			get { return exceptionMessage; }
			set { exceptionMessage = value; }
		}
		
		public List<MVacation> VacationList {
			get { return vacationList; }
			set { vacationList = value; }
		}
		
	}
}
