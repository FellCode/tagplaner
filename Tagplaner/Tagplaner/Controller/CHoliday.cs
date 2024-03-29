﻿using System;
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
    public class CHoliday : IHoliday
    {
        private List<MHoliday> holidayList = new List<MHoliday>();

       
        /// <summary>
        /// Die Funktion liest die CSV mit dem übergebenen Namen von Typ String ein und speichert das Datum 
        /// und den Namen des Feiertags in einem MHoliday Objekt.
        /// Dieses Objekt wird in einer List gespeichert und am Ende wird die Liste returned
        /// </summary>
        /// <param name="pfad1"></param>
        /// <param name="pfad2"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<MHoliday> GetHoliday(String pfad1,String pfad2, DateTime startDate, DateTime endDate)
        {
            /*  string remoteUri = "http://www.feiertage.net/csvfile.php?state=NW&year=2017&type=csv";
              string fileName = "Nordrhein-Westfalen2017.csv", myStringWebResource = null;

              // Create a new WebClient instance.
              using (WebClient myWebClient = new WebClient())
              {
                  myStringWebResource = remoteUri + fileName;
                  // Download the Web resource and save it into the current filesystem folder.
                  myWebClient.DownloadFile(myStringWebResource, fileName);
              } */

            StreamReader file = new StreamReader(pfad1);
            ValidateHoliday(file, startDate, endDate);

            file = new StreamReader(pfad2);
            ValidateHoliday(file, startDate, endDate);
            file.Close();

            return holidayList;

        }
        /// <summary>
        /// Fügt die Validierten Feiertage der Liste hinzu
        /// </summary>
        /// <param name="values"></param>
        public void AddHoliday(String[] values)
        {
            MHoliday mHoliday = new MHoliday(Convert.ToDateTime(values[0]), values[1]);
            holidayList.Add(mHoliday);
        }
        /// <summary>
        /// Validiert die eingelesenen Feiertage auf den angegebenen Tagplan Zeitraum
        /// </summary>
        /// <param name="file"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void ValidateHoliday(StreamReader file, DateTime startDate, DateTime endDate)
        {
            if (file.ReadLine() != null)
            {
                while (!file.EndOfStream)
                {
                    String[] values = file.ReadLine().Split(';');
                    if (Convert.ToDateTime(values[0]).Date >= startDate.Date && Convert.ToDateTime(values[0]).Date <= endDate.Date)
                        AddHoliday(values);
                }
            }

        }
    }
}