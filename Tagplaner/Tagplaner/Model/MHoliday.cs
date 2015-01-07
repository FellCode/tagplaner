using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MHoliday
    {
        public DateTime HolidayDate { get; set; }
        public String holidayName { get; set; }

        public MHoliday(DateTime HolidayDate, String holidayName)
        {
            this.HolidayDate = HolidayDate;
            this.holidayName = holidayName;
        }
    }
}
