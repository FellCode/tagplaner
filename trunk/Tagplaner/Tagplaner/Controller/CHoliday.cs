using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tagplaner
{
    class CHoliday : IHoliday
    {
        public List<MHoliday> GetHolidayDummy(String region)
        {
            List<MHoliday> holidayArray = new List<MHoliday>();
            MHoliday mHoliday = new MHoliday(new DateTime(2012, 12, 25), "1. Weihnachtstag");
            holidayArray.Add(mHoliday);
            return holidayArray;
        }

        public List<MHoliday> GetHoliday(String region)
        {
            List<MHoliday> holidayArray = new List<MHoliday>();

            return holidayArray;

        }

  /*      private bool openCSV(String path)
        {
            StreamReader file =
                new StreamReader(path);
            while (file.ReadLine() != null)
            {
                String[] values = file.ReadLine().Split(";");
                MHoliday mHoliday = new MHoliday();
            }

            return true;
        }
    }*/
   
}
