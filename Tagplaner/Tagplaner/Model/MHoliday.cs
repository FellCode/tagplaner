using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MHoliday
    {
        private DateTime holidayDate { get; set; }
        private String holidayName { get; set; }
        private String comment { get; set; }

        public MHoliday(DateTime holidayDate, String holidayName)
        {
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
            this.comment = comment;
        }
    }
}
