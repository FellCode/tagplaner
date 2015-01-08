using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tagplaner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormInit());

            //EXCEL DUMMY
            /*List<MRoom> rooms = new List<MRoom>();
            rooms.Add(new MRoom(109));
            MCalendar calendar = new MCalendar();
            calendar.AddSeminarDay(new MTrainer("Max", "Mustermann", "MM", true, false), 
                null,new MCalendarDay(new DateTime().AddYears(2014).AddMonths(10).AddDays(20), "Feiertag1", "KW01")
                ,new MSpeciality("AE","Jahr 1", "NRW"), new MSeminar("dotNet & C#","macht Spaß","C#", true, "bestes Seminar")
                , new MPlace("Akademie Köln", "S. Gehm", rooms), new MRoom(109));

            WorksheetGenerator ws = new WorksheetGenerator();
            ws.WriteFile(MCalendar);*/

        }
    }
}

