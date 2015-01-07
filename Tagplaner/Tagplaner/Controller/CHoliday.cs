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

       /* public List<MHoliday> GetHoliday(String region)
        {
            List<MHoliday> holidayArray = new List<MHoliday>();
            MHoliday mHoliday = new MHoliday(new DateTime(2012, 12, 25), "1. Weihnachtstag");
            holidayArray.Add(mHoliday);
            return holidayArray;
        } */

        /// <summary>
        /// Die Funktion liest die CSV mit dem übergebenen Namen von Typ String ein und speichert das Datum 
        /// und den Namen des Feiertags in einem MHoliday Objekt.
        /// Dieses Objekt wird in einer List gespeichert und am Ende wird die Liste returned
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public List<MHoliday> GetHoliday(String region)
        {
            List<MHoliday> holidayList = new List<MHoliday>();
            Console.WriteLine("CSV\\" + region + "2015.csv");
            StreamReader file =
                new StreamReader("CSV\\" + region + "2015.csv");
            if (file.ReadLine() != null)
            {
                while (!file.EndOfStream)
                {
                    String[] values = file.ReadLine().Split(';');
                    MHoliday mHoliday = new MHoliday(Convert.ToDateTime(values[0]), Convert.ToDateTime(values[0]), values[1]);
                    holidayList.Add(mHoliday);
                }
            }
            file.Close();

            return holidayList;
        }
    }
}
