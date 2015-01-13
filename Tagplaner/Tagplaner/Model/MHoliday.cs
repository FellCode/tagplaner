using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    public class MHoliday
    {
        private int id;
        private DateTime holidayDate;
        private string holidayName;
        private string comment;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
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

        #region setter
        public void SetComment(string comment){
            this.comment = comment;
        }
        #endregion
        #region constructor
        public MHoliday( DateTime holidayDate, string holidayName, string comment)
        {
          
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
            this.comment = comment;
        }
        public MHoliday(int id, DateTime holidayDate, string holidayName, string comment)
        {
            this.id = id;
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
            this.comment = comment;
        }
        public MHoliday( DateTime holidayDate, string holidayName)
        {
         
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
        }
        public MHoliday(int id, DateTime holidayDate, string holidayName)
        {
            this.id = id;
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
        }
        #endregion
    }
}
