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
            int i_day = 1;
            Object[] data = new Object[1];
            string calendarWeek = "";
            string vacation = "";
            var german = new System.Globalization.CultureInfo("de-DE");
            
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

            aRange = ws.get_Range(cell1, cell1);
            aRange.EntireColumn.ColumnWidth = 3;
            cell1 = "B1";
            aRange = ws.get_Range(cell1, cell1);
            aRange.EntireColumn.ColumnWidth = 10;
            aRange = ws.get_Range("A1", "Z1");
            aRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            aRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            //Schleife fuer jeden Tag in der calendarList
            foreach (MCalendarDay calendarDay in calendar.CalendarList)
            {

                //Findet heraus, ob eine neue Kalenderwoche startet
                #region calendarWeek
                if (calendarWeek != calendarDay.CalendarWeek)
                {
                    cell1 = "A" + i_day;
                    cell2 = "B" + i_day;
                    aRange = ws.get_Range(cell1, cell2);
                    aRange.Merge(Missing.Value);
                    aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    data[0] = "KW" + calendarDay.CalendarWeek;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);


                    cell1 = "C" + i_day;
                    cell2 = "Q" + i_day;
                    aRange = ws.get_Range(cell1, cell2);
                    aRange.Merge(Missing.Value);
                    aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    i_day++;
                    calendarWeek = calendarDay.CalendarWeek;

                }
                #endregion

                    //Erstellt das Datum-Feld
                    cell1 = "A" + i_day;
                    aRange = ws.get_Range(cell1, cell1);
                    data[0] = german.DateTimeFormat.GetDayName(calendarDay.CalendarDate.DayOfWeek).ToString().Substring(0, 2).ToUpper();

                    if (!(data[0].ToString().Equals("SA") || data[0].ToString().Equals("SO")))
                    {
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                        aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);


                        cell1 = "B" + i_day;
                        aRange = ws.get_Range(cell1, cell1);
                        data[0] = calendarDay.CalendarDate;
                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                        aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                        
                        int i_entry = 1;

                        #region CalendarEntry
                        //Schleife für jede Spalte des Tages (1 oder 2 Jahrgänge, FIA / FISI)
                        //1-4 Durchläufe

                        
                        foreach (MCalendarEntry calendarEntry in calendarDay.CalendarEntry)
                        {
                            if (calendarDay.CalendarEntry[i_entry-1].Holiday != null)
                            {
                                #region Holiday
                                #endregion
                            }
                            else if (calendarDay.CalendarEntry[i_entry-1].School != null)
                            {
                                #region School
                                string comment = calendarDay.CalendarEntry[i_entry-1].School.Comment;
                                switch (i_entry-1)
                                {

                                    case 1: cell1 = "D" + i_day;
                                        break;

                                    case 2: cell1 = "K" + i_day;
                                        break;

                                    case 3: cell1 = "R" + i_day;
                                        break;

                                    case 4: cell1 = "Y" + i_day;
                                        break;

                                }
                                aRange.Merge(Missing.Value);
                                aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                                if (comment != null)
                                {
                                    data[0] = comment;
                                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                                    /*aRange.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;*/
                                }
                                #endregion
                            }
                            else if (calendarDay.CalendarEntry[i_entry-1].Practice != null)
                            {

                                if (calendarDay.CalendarEntry[i_entry-1].Seminar != null)
                                {
                                    #region Practice/Seminar
                                    string comment_p = calendarDay.CalendarEntry[i_entry-1].Practice.Comment;
                                    #endregion

                                }
                                else
                                {
                                    #region Practice
                                    string comment = calendarDay.CalendarEntry[i_entry-1].Practice.Comment;
                                    switch (i_entry-1)
                                    {
                                        case 1: cell1 = "D" + i_day;
                                            break;

                                        case 2: cell1 = "K" + i_day;
                                            break;

                                        case 3: cell1 = "R" + i_day;
                                            break;

                                        case 4: cell1 = "Y" + i_day;
                                            break;

                                    }

                                    aRange.Merge(Missing.Value);
                                    aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                                    if (comment != null)
                                    {
                                        data[0] = comment;
                                        aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                                    }
                                    #endregion

                                }

                            }
                            else
                            {
                                #region Seminar
                                                        string comment = calendarDay.CalendarEntry[i_entry-1].Seminar.Comment;
                        string title = calendarDay.CalendarEntry[i_entry-1].Seminar.Title;
                        string abbrevaiation = calendarDay.CalendarEntry[i_entry-1].Seminar.Abbreviation;
                        string subtitle = calendarDay.CalendarEntry[i_entry-1].Seminar.Subtitle;
                        bool tech = calendarDay.CalendarEntry[i_entry-1].Seminar.HasTechnology;

                        switch (i_entry-1)
                        {
                            case 1: cell1 = "D" + i_day;
                                break;

                            case 2: cell1 = "K" + i_day;
                                break;

                            case 3: cell1 = "R" + i_day;
                                break;

                            case 4: cell1 = "Y" + i_day;
                                break;
                        }
                        if (abbrevaiation != null)
                        {
                            data[0] = abbrevaiation;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                        }
                        if (title != null)
                        {
                            data[0] = title;
                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                        }
                        aRange.Merge(Missing.Value);
                        aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);                        
                        #endregion
                            }
                        }
                        #endregion
                        i_day++;
                    }
                
            }
            return true;
        }

    }
}
