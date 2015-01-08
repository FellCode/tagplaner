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
	/// Description of MVacation.
	/// </summary>
	
	public class MVacation
    {
        public String vacationName;
        public DateTime vacationDate;

        public MVacation(String vacationName, DateTime vacationDate)
        {
            this.vacationName = vacationName;
            this.vacationDate = vacationDate;
        }
        
		public string VacationName {
			get { return vacationName; }
			set { vacationName = value; }
		}
        
        public DateTime VacationDate {
			get { return vacationDate; }
			set { vacationDate = value; }
		}

    }
}
