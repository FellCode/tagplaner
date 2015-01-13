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
        private Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        private Workbook wb;
        private Worksheet ws;
        private Range aRange;
        private string cell1 = "A1";
        private string cell2 = "B1";


        public WorksheetGenerator() {
        wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        ws = (Worksheet)wb.Worksheets[1];
        xlApp.Visible = true;
        aRange = ws.get_Range(cell1, cell2);
        }

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

        private void setRange(string cell){
            setRange(cell, cell);
        }
        private void setRange(string cell1, string cell2)
        {
            aRange = ws.get_Range(cell1, cell2);
        }
        private void setCell1(String cell1)
        {
            this.cell1 = cell1;
        }
        private void setCell2(String cell2)
        {
            this.cell2 = cell2;
        }
        private void setBackground(System.Drawing.Color color)
        {
            aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        private void setFontColor(System.Drawing.Color color)
        {
            aRange.Font.Color = System.Drawing.ColorTranslator.ToOle(color);
        }

        public bool WriteFile(MCalendar calendar)
        {
            int i_day = 1;
            int i_list = 0;
            Object[] data = new Object[1];
            string calendarWeek = "";
            string vacation = null;
            string vacation_cell = null;
            var german = new System.Globalization.CultureInfo("de-DE");

            setRange(cell1);
            aRange.EntireColumn.ColumnWidth = 3;
            setRange("B1");
            aRange.EntireColumn.ColumnWidth = 10;
            setRange("A1", "Z1");
            aRange.EntireColumn.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            aRange.EntireColumn.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            //Schleife fuer jeden Tag in der calendarList
            foreach (MCalendarDay calendarDay in calendar.CalendarList)
            {

                #region Dokumentkopf
                aRange = ws.get_Range("D1");
                aRange.EntireColumn.ColumnWidth = 1;
                //aRange.EntireColumn.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Pink);
                setBackground(System.Drawing.Color.Pink);
                aRange.EntireColumn.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                setRange("C1");
                aRange.EntireColumn.ColumnWidth = 5;

                #endregion

                //Findet heraus, ob eine neue Kalenderwoche startet
                #region calendarWeek
                if (calendarWeek != calendarDay.CalendarWeek)
                {
                    setCell1("A" + i_day);
                    setCell2("B" + i_day);
                    setRange(cell1, cell2);
                    aRange.Merge(Missing.Value);
                    aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    data[0] = "KW" + calendarDay.CalendarWeek;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);


                    cell1 = "E" + i_day;
                    cell2 = "Q" + i_day;
                    aRange = ws.get_Range(cell1, cell2);
                    aRange.Merge(Missing.Value);
                    aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);


                    cell1 = "C" + i_day;
                    aRange = ws.get_Range(cell1);
                    aRange.Merge(Missing.Value);
                    aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    i_day++;
                    calendarWeek = calendarDay.CalendarWeek;

                }
                #endregion

                //Erstellt das Datum-Feld
                cell1 = "A" + i_day;
                aRange = ws.get_Range(cell1);
                //Wochentag
                data[0] = german.DateTimeFormat.GetDayName(calendarDay.CalendarDate.DayOfWeek).ToString().Substring(0, 2).ToUpper();

                if (!(data[0].ToString().Equals("SA") || data[0].ToString().Equals("SO")))
                {
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);


                    cell1 = "B" + i_day;
                    aRange = ws.get_Range(cell1);
                    data[0] = calendarDay.CalendarDate;
                    aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                    aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);


                    #region Ferien
                    if (calendar.CalendarList[i_list].VacationName != null)
                    {
                        if (calendar.CalendarList[i_list].VacationName != vacation)
                        {
                            vacation = calendar.CalendarList[i_list].VacationName;
                            cell1 = "C" + i_day;
                            vacation_cell = cell1;
                            aRange = ws.get_Range(cell1);
                            data[0] = vacation.Substring(0, vacation.IndexOf(" "));

                            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);

                            //aRange.Borders[XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                            //aRange.Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThick;
                            aRange.Orientation = 90;
                            aRange.EntireRow.RowHeight = 15;
                            aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.GreenYellow);
                            aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                            aRange.Font.Size = 8;
                            aRange.WrapText = true;

                        }
                        else
                        {
                            cell2 = "C" + i_day;
                            aRange = ws.get_Range(vacation_cell, cell2);
                            aRange.Merge(Missing.Value);
                        }
                    }
                    #endregion

                    int i_entry = 1;
                    #region CalendarEntry
                    //Schleife für jede Spalte des Tages (1 oder 2 Jahrgänge, FIA / FISI)
                    //1-4 Durchläufe                        
                    foreach (MCalendarEntry calendarEntry in calendarDay.CalendarEntry)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].School != null)
                        {
                            #region School
                            string comment = calendarDay.CalendarEntry[i_entry - 1].School.Comment;
                            switch (i_entry)
                            {

                                case 1: cell1 = "H" + i_day;
                                    cell2 = "K" + i_day;
                                    break;

                                case 2: cell1 = "K" + i_day;
                                    break;

                                case 3: cell1 = "R" + i_day;
                                    break;

                                case 4: cell1 = "Y" + i_day;
                                    break;

                            }
                            aRange = ws.get_Range(cell1, cell2);
                            aRange.Merge(Missing.Value);
                            aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
                            if (comment != null)
                            {
                                data[0] = comment;
                                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                                /*aRange.Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlHairline;*/
                            }
                            #endregion

                        }
                        else if (calendarDay.CalendarEntry[i_entry - 1].Practice != null)
                        {

                            if (calendarDay.CalendarEntry[i_entry - 1].Seminar != null)
                            {
                                #region Practice/Seminar
                                string comment_p = calendarDay.CalendarEntry[i_entry - 1].Practice.Comment;
                                #endregion

                            }
                            else
                            {
                                #region Practice
                                string comment = calendarDay.CalendarEntry[i_entry - 1].Practice.Comment;
                                switch (i_entry - 1)
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
                            string comment = calendarDay.CalendarEntry[i_entry - 1].Seminar.Comment;
                            string title = calendarDay.CalendarEntry[i_entry - 1].Seminar.Title;
                            string abbrevaiation = calendarDay.CalendarEntry[i_entry - 1].Seminar.Abbreviation;
                            string subtitle = calendarDay.CalendarEntry[i_entry - 1].Seminar.Subtitle;
                            string tech = calendarDay.CalendarEntry[i_entry - 1].Seminar.HasTechnology;
                            string room = calendarDay.CalendarEntry[i_entry - 1].Room.Number;
                            string abbrevaiation_t = calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation;


                            switch (i_entry)
                            {
                                case 1: cell1 = "F" + i_day;
                                    cell2 = "F" + i_day;
                                    break;
                                case 2: cell1 = "M" + i_day;
                                    cell2 = "M" + i_day;
                                    break;
                            }
                            if (room != null)
                            {
                                aRange = ws.get_Range(cell1, cell2);
                                data[0] = room;
                                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                                data[0] = null;

                            }

                            switch (i_entry)
                            {
                                case 1: cell1 = "G" + i_day;
                                    cell2 = "G" + i_day;
                                    break;

                                case 2: cell1 = "N" + i_day;
                                    cell2 = "N" + i_day;
                                    break;
                            }
                            if (abbrevaiation_t != null)
                            {
                                aRange = ws.get_Range(cell1, cell2);
                                data[0] = abbrevaiation_t;
                                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                            }
                            switch (i_entry)
                            {

                                case 1: cell1 = "H" + i_day;
                                    cell2 = "K" + i_day;
                                    break;

                                case 2: cell1 = "O" + i_day;
                                    cell2 = "R" + i_day;
                                    break;

                                case 3: cell1 = "R" + i_day;
                                    break;

                                case 4: cell1 = "Y" + i_day;
                                    break;
                            }

                            aRange = ws.get_Range(cell1, cell2);
                            aRange.Merge(Missing.Value);
                            aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                            aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);

                            if (title != null)
                            {
                                data[0] = null;
                                data[0] = abbrevaiation;
                                aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
                            }
                            #endregion
                        }
                        i_entry++;
                    }
                    #endregion
                    i_day++;
                }
                else
                {
                    //Wenn Wochentage SA/SO -> zuletzt erstelltes Feld -> Null ("Merk-Variable")
                    vacation = null;
                }
                i_list++;


            }
            aRange = ws.get_Range("D1", "D" + (i_day - 1));
            aRange.Merge(Missing.Value);
            return true;
        }

    }
}
