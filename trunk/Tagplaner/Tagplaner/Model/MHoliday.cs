using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
   /// Autor: Niklas Wazal, Felix Smuda
   /// <summary>
   /// Model Klasse die einen Feiertag beschreibt
   /// </summary>
  
    [Serializable()]
    public class MHoliday
    {
        private int id;
        private DateTime holidayDate;
        private string holidayName;
        private string comment;

        /// <summary>
        /// Implementation der Get Funktion für die ID Eigenschaft
        /// </summary>
        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        /// <summary>
        ///  /// Implementation der Get Funktion für die HolidayDate Eigenschaft
        /// </summary>
        public DateTime HolidayDate
        {
            get { return holidayDate; }
        }
        /// <summary>
        ///  /// Implementation der Get Funktion für die HolidayName Eigenschaft
        /// </summary>
        public string HolidayName
        {
            get { return holidayName; }
        }

        /// <summary>
        /// Implementation der Get Funktion für die Comment Eigenschaft
        /// </summary>
        public string Comment
        {
            get { return comment; }
        }
        #endregion
        /// <summary>
        /// Implementation der Set Funktion für die Comment Eigenschaft
        /// </summary>
        /// <param name="comment"></param>
        #region setter
        public void SetComment(string comment){
            this.comment = comment;
        }
        #endregion
        #region constructor
        /// <summary>
        /// Konstruktor des MHoliday Models
        /// </summary>
        /// <param name="holidayDate"></param>
        /// <param name="holidayName"></param>
        /// <param name="comment"></param>
        public MHoliday( DateTime holidayDate, string holidayName, string comment)
        {
          
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
            this.comment = comment;
        }
        /// <summary>
        /// Konstruktor des MHoliday Models
        /// </summary>
        /// <param name="id"></param>
        /// <param name="holidayDate"></param>
        /// <param name="holidayName"></param>
        /// <param name="comment"></param>
        public MHoliday(int id, DateTime holidayDate, string holidayName, string comment)
        {
            this.id = id;
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
            this.comment = comment;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="holidayDate"></param>
        /// <param name="holidayName"></param>
        public MHoliday( DateTime holidayDate, string holidayName)
        {
         
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
        }

        /// <summary>
        /// Konstruktor des MHoliday Models
        /// </summary>
        /// <param name="id"></param>
        /// <param name="holidayDate"></param>
        /// <param name="holidayName"></param>
        public MHoliday(int id, DateTime holidayDate, string holidayName)
        {
            this.id = id;
            this.holidayDate = holidayDate;
            this.holidayName = holidayName;
        }
        #endregion
    }
}
