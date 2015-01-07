using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MCalendarDay
    {
        private DateTime calenderDate { get; set; }
        private String holidayName { get; set; }
        private String calenderWeek { get; set; }

        public MCalendarDay(DateTime calenderDate, String holidayName, String calenderWeek)
        {
            this.calenderDate = calenderDate;
            this.holidayName = holidayName;
            this.calenderWeek = calenderWeek;
        }
    }
}
