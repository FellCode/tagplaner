using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    class MHoliday
    {
        public DateTime holidayDate { get; set; }
        public String holidayName { get; set; }

        public MHoliday(DateTime hD, String hN)
        {
            this.holidayDate = hD;
            this.holidayName = hN;
        }
    }
}
