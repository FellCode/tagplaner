using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tagplaner
{
    /// <summary>
    /// Author: Felix Smuda, Niklas Wazal
    /// Date: 07.01.2015
    /// </summary>
    class CHoliday : IHoliday
    {
        private List<MHoliday> holidayList = new List<MHoliday>();
        /// <summary>
        /// Die Funktion liest die CSV mit dem übergebenen Namen von Typ String ein und speichert das Datum 
        /// und den Namen des Feiertags in einem MHoliday Objekt.
        /// Dieses Objekt wird in einer List gespeichert und am Ende wird die Liste returned
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public List<MHoliday> GetHoliday(String region, DateTime startDate, DateTime endDate)
        {
            
            StreamReader file =
                new StreamReader("CSV\\" + region + startDate.Year +".csv");
            if (file.ReadLine() != null)
            {
                while (!file.EndOfStream)
                {
                    String[] values = file.ReadLine().Split(';');
                    if (Convert.ToDateTime(values[0]) >= startDate)
                        AddHoliday(values);
                }
                file = new StreamReader("CSV\\" + region + endDate.Year + ".csv");
                if (file.ReadLine() != null)
                {
                    while (!file.EndOfStream)
                    {
                        String[] values = file.ReadLine().Split(';');
                        if (Convert.ToDateTime(values[0]) <= endDate)
                            AddHoliday(values);
                        
                    }
                }
            }
            file.Close();
             
            return holidayList;
        }
        public void AddHoliday(String[] values)
        {
            MHoliday mHoliday = new MHoliday(Convert.ToDateTime(values[0]), values[1]);
            holidayList.Add(mHoliday);
            Console.WriteLine(values[0]);
        }
    }
}
