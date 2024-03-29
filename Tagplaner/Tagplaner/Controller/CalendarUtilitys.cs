﻿/*
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
	/// Description of GenerateCalendarDays.
	/// </summary>
    public class CalendarUtilitys : ICalendarUtilitys
	{
		private DateTime startdate;
		private DateTime enddate;
        private List<MCalendarDay> tempCalendarDay;

        public CalendarUtilitys(DateTime startdate, DateTime enddate, List<MCalendarDay> mCalendarDay)
		{
			Startdate = startdate;
			Enddate = enddate;
            TempCalendarDay = mCalendarDay;
		}

        public List<MCalendarDay> generateCalenderDayEntrys()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dfi.Calendar;

			for(var day = Startdate; day.Date <= Enddate; day = day.AddDays(1)){
				//Console.WriteLine(day.Date);
                /*Console.WriteLine("{0:d}: Week {1}", day.Date,
				                  calendar.GetWeekOfYear(day.Date, dfi.CalendarWeekRule,
				                                    dfi.FirstDayOfWeek));*/
				//CalendarDays.Add(day);
                MCalendarDay mCalendarDay = new MCalendarDay(day, null, null, calendar.GetWeekOfYear(day.Date, dfi.CalendarWeekRule,
                                                    dfi.FirstDayOfWeek).ToString());
                TempCalendarDay.Add(mCalendarDay);
                //MCalendarDay.Add(new MCalendarDay();
                
			}
            return TempCalendarDay;
		}
		
		public DateTime Startdate {
			get { return startdate; }
			set { startdate = value; }
		}

		public DateTime Enddate {
			get { return enddate; }
			set { enddate = value; }
		}

        public List<MCalendarDay> TempCalendarDay
        {
            get { return tempCalendarDay; }
            set { tempCalendarDay = value; }
		}

	}
}
