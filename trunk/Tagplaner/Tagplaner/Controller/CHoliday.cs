using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    class CHoliday : IHoliday
    {
        public ArrayList GetHoliday(String Region)
        {
            ArrayList holidayArray = new ArrayList();
            MHoliday mHoliday = new MHoliday(new DateTime(2012, 12, 25), "1. Weihnachtstag");
            holidayArray.Add(mHoliday);
            return holidayArray;
        }
    }
}
