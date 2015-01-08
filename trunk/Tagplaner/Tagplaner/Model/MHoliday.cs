using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MHoliday
    {
        private DateTime holidayDate;
        private string holidayName;
        private string comment;

        #region getter
        public DateTime HolidayDate
        {
            get { return holidayDate; }
        }
        public string HolidayName
        {
            get { return holidayName; }
        }
        public string Comment
        {
            get { return comment; }
        }
        #endregion

        #region constructor
        public MHoliday(DateTime holidayDate, string holidayName, string comment)
        {
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
            this.comment = comment;
        }
        public MHoliday(DateTime holidayDate, string holidayName)
        {
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
        }
        #endregion
    }
}
