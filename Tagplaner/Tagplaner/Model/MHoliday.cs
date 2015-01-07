using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MHoliday
    {
        public DateTime startHolidayDate { get; set; }
        public DateTime endHolidayDate { get; set; }
        public String holidayName { get; set; }

        public MHoliday(DateTime startHolidayDate, DateTime endHolidayDate, String holidayName)
        {
            this.startHolidayDate = startHolidayDate;
            this.endHolidayDate = endHolidayDate;
            this.holidayName = holidayName;
        }
    }
}
