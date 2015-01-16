using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Autor: Niklas Wazal, Felix Smuda
    /// Interface zur Implementierung der GetHoliday Funktion zum auslesen der Bundeslandabhängigen Feiertage
    /// </summary>
    public interface IHoliday    
    {
        /// <summary>
        /// Verweise auf die Funktion GetHoliday
        /// </summary>
        /// <param name="pfad1"></param>
        /// <param name="pfad2"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<MHoliday> GetHoliday(String pfad1 ,String pfad2, DateTime startDate, DateTime endDate);
    }
}
