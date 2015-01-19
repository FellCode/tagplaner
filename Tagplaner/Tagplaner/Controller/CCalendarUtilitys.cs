/*
 * Created by SharpDevelop.
 * User: tbender2
 * Date: 07.01.2015
 * Time: 16:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Tagplaner
{
	/// <summary>
	/// Die Klasse liefert alle Kalendertage mit Datum, KW als List-Object mit
	/// den MVacation-Objekten in einem gewählten Zeitraum
	/// </summary>
	public class CCalendarUtilitys: ICalendarUtility
	{
		/// <summary>
		/// Feld für das Startdatum
    	/// </summary>
		private DateTime startdate;
		/// <summary>
		/// Feld für das Enddatum
    	/// </summary>
		private DateTime enddate;
		/// <summary>
		/// Feld für das die temporären MCalendarDay-Objekte
		/// <seealso cref="MCalendarDay"></seealso>
    	/// </summary>
		private List<MCalendarDay> tempCalendarDay;

		/// <summary>
		/// Konstruktor der Klasse CCalendarUtilitys
		/// <para name="startdate">Datum von</para>
 		/// <para name="enddate">Datum bis</para>
 		/// <para name="mCalendarDay">Liste aus dem MCalendar-Objekt</para>
		/// <seealso cref="MCalendarDay"></seealso>
 		/// </summary>
		public CCalendarUtilitys(DateTime startdate, DateTime enddate, List<MCalendarDay> mCalendarDay)
		{
			Startdate = startdate;
			Enddate = enddate;
            TempCalendarDay = mCalendarDay;
		}

		/// <summary>
		/// Erzeugt eine Liste von MCalendarDay-Objekten für den gewählten Zeitraum (inklusiv)
 		/// <returns>Liste von DateTime-Objekten</returns>
 		/// <seealso cref="MCalendarDay"></seealso>
    	/// </summary>		
		public List<MCalendarDay> GenerateCalenderDayEntrys()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dfi.Calendar;

			for(var day = Startdate; day.Date <= Enddate; day = day.AddDays(1)){
		
                MCalendarDay mCalendarDay = new MCalendarDay(day, null, null, calendar.GetWeekOfYear(day.Date, dfi.CalendarWeekRule,
                                                    dfi.FirstDayOfWeek).ToString());
                TempCalendarDay.Add(mCalendarDay);
			}
            return TempCalendarDay;
		}
		
		/// <summary>
		/// Property für das Attribut startDate
		/// <seealso cref="MCalendarDay"></seealso>
    	/// </summary>
		public DateTime Startdate {
			get { return startdate; }
			set { startdate = value; }
		}

    	/// <summary>
		/// Property für das Feld endDate
		/// <seealso cref="MCalendarDay"></seealso>
    	/// </summary>
		public DateTime Enddate {
			get { return enddate; }
			set { enddate = value; }
		}

    	/// <summary>
		/// Property für das Feld temCalendarDay
		/// <seealso cref="MCalendarDay"></seealso>
    	/// </summary>
        public List<MCalendarDay> TempCalendarDay
        {
            get { return tempCalendarDay; }
            set { tempCalendarDay = value; }
		}
	}
}
