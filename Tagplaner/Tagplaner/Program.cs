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

            #region TESTDATEN
            /*
             * List<MRoom> rooms = new List<MRoom>();
            rooms.Add(new MRoom(109));
            
            MCalendar calendar = MCalendar.getInstance();
            calendar.fillCalendarInitial(new DateTime().AddYears(2014).AddMonths(0).AddDays(0), new DateTime().AddYears(2014).AddMonths(11).AddDays(30));

            MTrainer trainer = new MTrainer("Arnold", "Bechtold", "AB", false, false);
            MTrainer trainer_co = new MTrainer("","","",false,true);
            MSpeciality mspec = new MSpeciality("AE", "2104", "Koeln");
            MSeminar seminar = new MSeminar("Titel", "Subtitel", "SAP", false, "commment");
            MPlace ort = new MPlace("Koeln", "Arnold", rooms);
            MRoom roomm = new MRoom(109);

           // calendar.CalendarList[0].CalendarEntry = new List<MCalendarEntry>;
            
            calendar.CalendarList[0].CalendarEntry.Add(new MCalendarEntry(trainer,trainer_co,mspec,seminar,ort,roomm));
            WorksheetGenerator ws = new WorksheetGenerator();
            ws.WriteFile(calendar);
             * 
             * */

            #endregion

        }
    }
}

