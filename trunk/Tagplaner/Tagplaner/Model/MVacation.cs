/*
 * Created by SharpDevelop.
 * User: tbender2
 * Date: 07.01.2015
 * Time: 12:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Tagplaner
{
	/// <summary>
	/// Die Klasse repräsentiert die Ferien-Einträge in den ICS-Dateien
	/// </summary>
	public class MVacation
    {
		/// <summary>
		/// Name des Ferieneintrags
		/// </summary>
        public String vacationName;
        /// <summary>
		/// Datum des Ferieneintrags
		/// </summary>
        public DateTime vacationDate;

        /// <summary>
		/// Konstruktor der MVacation-Klasse
 		/// <para name="vacationName">Beschreibender Name eines Ferieneintrags in der ICS-Datei</para>
 		/// <para name="vacationDate">Datum des Ferientags</para>
 		/// <seealso cref="CICalCSVConverter"></seealso>
    	/// </summary>
        public MVacation(String vacationName, DateTime vacationDate)
        {
            this.vacationName = vacationName;
            this.vacationDate = vacationDate;
        }
        
        /// <summary>
		/// Property für das Feld vacationName
    	/// </summary>
		public string VacationName {
			get { return vacationName; }
			set { vacationName = value; }
		}
        
    	/// <summary>
		/// Property für das Feld vacationDate
    	/// </summary>
        public DateTime VacationDate {
			get { return vacationDate; }
			set { vacationDate = value; }
		}

    }
}
