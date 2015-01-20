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
            Application.Run(new SplashScreenForm());

            #region TESTDATEN

            /*
            List<MRoom> rooms = new List<MRoom>();
            rooms.Add(new MRoom("109"));

            MCalendar calendar = MCalendar.GetInstance();
            List<String> liste = new List<String>();
            liste.Add("blala");
            liste.Add("blala2");
            calendar.fillCalendarInitial(new DateTime().AddYears(2014).AddMonths(0).AddDays(0),new DateTime().AddYears(2014).AddMonths(11).AddDays(30), 1, liste,
                @"C:\Daten\Visual Studio 2013\Projects\Tagplaner\Tagplaner\Tagplaner\bin\Debug\CSV\Ferien_Hessen_2015.ics",
                @"C:\Daten\Visual Studio 2013\Projects\Tagplaner\Tagplaner\Tagplaner\bin\Debug\CSV\Ferien_Hessen_2016.ics",
                @"C:\Daten\Visual Studio 2013\Projects\Tagplaner\Tagplaner\Tagplaner\bin\Debug\CSV\Nordrhein-Westfalen2015.csv",
                @"C:\Daten\Visual Studio 2013\Projects\Tagplaner\Tagplaner\Tagplaner\bin\Debug\CSV\Nordrhein-Westfalen2015.csv");
            
            MTrainer trainer = new MTrainer("Arnold", "Bechtold", "AB", false, false);
            MTrainer trainer_co = new MTrainer("", "", "", false, true);
            MSpeciality mspec = new MSpeciality(1, "AE14", "2014");
            MSeminar seminar = new MSeminar("Titel", "Subtitle", "SAP", "bla", "commment");
            MPlace ort = new MPlace("Koeln", "Arnold", rooms);
            MRoom roomm = new MRoom("109");


            calendar.CalendarList[0].CalendarEntry.Add(new MCalendarEntry(1, trainer, trainer_co, seminar, ort, roomm));
            calendar.CalendarList[5].CalendarEntry.Add(new MCalendarEntry(new MSchool("a")));
            calendar.CalendarList[5].CalendarEntry.Add(new MCalendarEntry(new MSchool("a")));
            calendar.CalendarList[6].CalendarEntry.Add(new MCalendarEntry(new MSchool("a")));
            calendar.CalendarList[7].CalendarEntry.Add(new MCalendarEntry(new MSchool("")));
            calendar.CalendarList[8].CalendarEntry.Add(new MCalendarEntry(new MSchool("")));
            WorksheetGenerator ws = new WorksheetGenerator();
            
            ws.WriteFile(calendar);
            */
            #endregion

        }
    }
}

