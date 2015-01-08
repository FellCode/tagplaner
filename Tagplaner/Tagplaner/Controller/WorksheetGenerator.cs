using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace Tagplaner
{
    //Author: Stefan, Arnold


    class WorksheetGenerator : IWorksheetGenerator
    {

        public WorksheetGenerator() { }

            /*aRange.Merge(Missing.Value);
            Object[] args = new Object[1];

            args[0] = 6;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.RosyBrown);
            aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PeachPuff);
            aRange.Merge(Missing.Value);
            aRange.EntireColumn.ColumnWidth = 20;

            aRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            aRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            aRange.Value2 = "C# SEMINAR";*/
     
        public bool WriteFile(MCalendar calendar)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            string range = "A1";
            string range1 = "A1";


            #region error
            if (xlApp == null)
            {
                Console.WriteLine("EXCEL konnte nicht gestartet werden :(");
                return false;
            }
            xlApp.Visible = true;

            Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            if (ws == null)
            {
                Console.WriteLine("Worksheet konnte nicht erstellt werden :'(");
                return false;
            }


            Range aRange = ws.get_Range(range, range1);

            if (aRange == null)
            {
                Console.WriteLine("Zellen konnten nicht markiert werden");
                return false;
            }
            #endregion

            MCalendarEntry calendarEntry = calendar[0];
            if (calendar[0].Holiday != null)
            {
                #region Holiday
                DateTime calendarDate = calendarEntry.CalendarDay.CalendarDate;
                string holidayName = calendarEntry.CalendarDay.HolidayName;
                string calendarWeek = calendarEntry.CalendarDay.CalendarWeek;
               
                Object[] args = new Object[1];

                args[0] = calendarDate;
                aRange.set_Value("C1", "C1");
                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
                return true;
                #endregion

            }
 /*           else if (calendar[0].School != null)
            {
                #region School
                return true;
                #endregion

            }
            else if (calendar[0].Practice != null)
            {

                if (calendar[0].Seminar != null)
                {
                    #region Practice/Seminar
                    return true;
                    #endregion

                }
                else
                {
                    #region Practice
                    return true;
                    #endregion
                   
                }

            }
            else
            {
                #region Seminar
                return true;
                #endregion
            }*/

        }

    }
}
