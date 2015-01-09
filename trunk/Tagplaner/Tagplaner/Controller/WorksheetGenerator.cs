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

            /*
            aRange.Merge(Missing.Value);
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
            string cell1 = "A10";
            string cell2 = "B10";
            int i_day = 0;
            Object[] data = new Object[1];
            string calendarWeek = "KW00";
            
            #region errormessages
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

            Range aRange = ws.get_Range(cell1, cell2);

            if (aRange == null)
            {
                Console.WriteLine("Zellen konnten nicht markiert werden");
                return false;
            }
            #endregion
            
            //Schleife fuer jeden Tag in der calendarList
            foreach (MCalendarDay calendarDay in calendar.CalendarList)
            {

                //Findet heraus, ob eine neue Kalenderwoche startet
                #region calendarWeek
                if (calendarWeek != calendarDay.CalendarWeek)
                {
                    aRange.Merge(Missing.Value);
                    aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    aRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    aRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    data[0] = calendarDay.CalendarWeek;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);


                    cell1 = "C" + i_day +1;
                    cell2 = "Q" + i_day + 1;
                    aRange = ws.get_Range(cell1, cell2);
                    aRange.Merge(Missing.Value);
                    aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    break;
                }
                #endregion

                

                int i_entry = 0;
                //Schleife für jede Spalte des Tages (1 oder 2 Jahrgänge, FIA / FISI)
                //1-4 Durchläufe
                foreach (MCalendarEntry calendarEntry in calendarDay.CalendarEntry)
                {
                    if (calendarDay.CalendarEntry[i_entry].Holiday != null)
                    {
                        #region Holiday
                        /*DateTime calendarDate = calendarEntry.CalendarDay.CalendarDate;
                    string holidayName = calendarEntry.CalendarDay.HolidayName;
                    string calendarWeek = calendarEntry.CalendarDay.CalendarWeek;
                    

                    data[0] = calendarDate;
                    aRange.set_Value("C1", "C1");
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);*/
                        #endregion
                    }
                    else if (calendarDay.CalendarEntry[i_entry].School != null)
                    {
                        #region School
                        #endregion
                    }
                    else if (calendarDay.CalendarEntry[i_entry].Practice != null)
                    {

                        if (calendarDay.CalendarEntry[i_entry].Seminar != null)
                        {
                            #region Practice/Seminar
                            #endregion

                        }
                        else
                        {
                            #region Practice
                            #endregion

                        }

                    }
                    else
                    {
                        #region Seminar
                        /*DateTime calendarDate = calendarEntry.CalendarDay.CalendarDate;
                    string holidayName = calendarEntry.CalendarDay.HolidayName;
                    string calendarWeek = calendarEntry.CalendarDay.CalendarWeek;
                    

                    data[0] = calendarDate;
                    aRange.set_Value("C1", "C1");
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);*/



                        #endregion
                    }
                }
                i_day++;
            }
            return true;
        }

    }
}
