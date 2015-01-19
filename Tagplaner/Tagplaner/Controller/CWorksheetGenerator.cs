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


    public class WorksheetGenerator : IWorksheetGenerator
    {
        private Application xlApp = new Application();
        private Workbook wb;
        private Worksheet ws;
        private Range aRange;
        private Object[] data = new Object[1];


        public WorksheetGenerator() {
        wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        ws = (Worksheet)wb.Worksheets[1];
        xlApp.Visible = true;
        aRange = ws.get_Range("A1", "B1");
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        } 
        private void setRange(string cell){
            setRange(cell, cell);
        }
        private void setRange(string cell1, string cell2)
        {
            aRange = ws.get_Range(cell1, cell2);
        }
        private void setBackgroundColor(System.Drawing.Color color)
        {
            aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        private void setFontColor(System.Drawing.Color color)
        {
            aRange.Font.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        private void setBorderColor(System.Drawing.Color color)
        {
            aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        private void setValue(Object value)
        {
            data[0] = value;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
        }
        private void merge()
        {
            aRange.Merge(Missing.Value);
        }


        public bool WriteFile(MCalendar calendar)
        {
            #region setup
            int i_day = 1;
            int i_list = 0;
            string calendarWeek = "";
            string vacation = null;
            string[] mergecell = new string[6];
            Object[] entry = new Object[4];
            MSchool[] school = new MSchool[4];
            MSeminar[] seminar = new MSeminar[4];
            MPractice[] practice = new MPractice[4];
            string day = null;
            var german = new System.Globalization.CultureInfo("de-DE");

            setRange("A1");
            aRange.EntireColumn.ColumnWidth = 4;
            setRange("B1");
            aRange.EntireColumn.ColumnWidth = 10;
            setRange("A1", "Z1");
            aRange.EntireColumn.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            aRange.EntireColumn.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            aRange.EntireColumn.WrapText = true;
            //aRange.EntireColumn.Font.Name = "Verdana";
            #endregion

            #region Dokumentkopf
            setRange("D1");
            aRange.EntireColumn.ColumnWidth = 1;
            setBackgroundColor(System.Drawing.Color.White);
            setRange("Q1");
            aRange.EntireColumn.ColumnWidth = 1;
            setBackgroundColor(System.Drawing.Color.White);
            setRange("C1");
            aRange.EntireColumn.ColumnWidth = 5;
            setRange("E1", "P1");
            aRange.EntireColumn.ColumnWidth = 5;

            //TODO rausfinden, wie viele jahre beteiligt sind (1/2)
            setRange("E" + i_day, "P" + i_day);
            merge();
            setValue("Ausbildung Fachinformatiker  Ausbildungsjahr X/Y");
            i_day++;

            setRange("F" + i_day, "O" + i_day);
            merge();
            setBackgroundColor(System.Drawing.Color.Moccasin);
            setValue("Legende");
            i_day++;

            setRange("F" + i_day, "O" + i_day);
            merge();
            setValue("Betriebliche Praxisphase");
            setBackgroundColor(System.Drawing.Color.Yellow);
            i_day++;

            setRange("F" + i_day, "O" + i_day);
            setBackgroundColor(System.Drawing.Color.Blue);
            setFontColor(System.Drawing.Color.White);
            merge();
            setValue("Berufsschule");
            i_day++;

            setRange("F" + i_day, "O" + i_day);
            setBackgroundColor(System.Drawing.Color.PaleTurquoise);
            merge();
            setValue("Seminar");

            setRange("A1", "C" + i_day);
            merge();
            setBackgroundColor(System.Drawing.Color.Moccasin);
            setRange("E2", "E" + i_day);
            merge();
            setBackgroundColor(System.Drawing.Color.Moccasin);
            setRange("P2", "p" + i_day);
            merge();
            setBackgroundColor(System.Drawing.Color.Moccasin);
            i_day++;

            #endregion
            //Schleife fuer jeden Tag in der calendarList
            foreach (MCalendarDay calendarDay in calendar.CalendarList)
            {
                //Findet heraus, ob eine neue Kalenderwoche startet
                #region calendarWeek
                if (calendarWeek != calendarDay.CalendarWeek)
                {
                    setRange("A" + i_day, "B" + i_day);
                    merge();
                    setBackgroundColor(System.Drawing.Color.LightGray);
                    setValue("KW" + calendarDay.CalendarWeek);


                    setRange("E" + i_day, "P" + i_day);
                    merge();
                    setBackgroundColor(System.Drawing.Color.LightBlue);


                    setRange("C" + i_day);
                    //setRange(cell1);
                    merge();
                    setBackgroundColor(System.Drawing.Color.LightBlue);
                    i_day++;
                    calendarWeek = calendarDay.CalendarWeek;

                }
                #endregion

                //Erstellt das Datum-Feld
                setRange("A" + i_day);

                //Wochentag
                day = german.DateTimeFormat.GetDayName(calendarDay.CalendarDate.DayOfWeek).ToString().Substring(0, 2).ToUpper();

                if (!(day.Equals("SA") || day.Equals("SO")))
                {
                    setValue(day);
                    setBorderColor(System.Drawing.Color.Black);

                    //setCell1("B" + i_day);
                    setRange("B" + i_day);
                    setValue(calendarDay.CalendarDate.ToString().Substring(0,10));

                    #region Ferien
                    if (calendar.CalendarList[i_list].VacationName != null)
                    {
                        if (calendar.CalendarList[i_list].VacationName != vacation)
                        {
                            vacation = calendar.CalendarList[i_list].VacationName;
                            mergecell[0] = "C" + i_day;
                            setRange(mergecell[0]);
                            setValue(vacation.Substring(0, vacation.IndexOf(" ")));
                            aRange.Orientation = 90;
                            aRange.EntireRow.RowHeight = 15;
                            setBackgroundColor(System.Drawing.Color.GreenYellow);
                            aRange.Font.Size = 8;
                        }
                        else
                        {
                            setRange(mergecell[0], "C" + i_day);
                            merge();
                        }
                    }
                    #endregion

                    if (calendar.CalendarList[i_list].HolidayName != null)
                    {
                        #region Feiertag

                        setRange("E" + i_day, "P" + i_day);
                        merge();
                        setValue(calendar.CalendarList[i_list].HolidayName);
                        setBackgroundColor(System.Drawing.Color.GreenYellow);

                        school = new MSchool[4];
                        seminar = new MSeminar[4];
                        practice = new MPractice[4];
                        #endregion
                    }
                    else
                    {
                        int i_entry = 1;
                        #region CalendarEntry
                        //Schleife für jede Spalte des Tages (1 oder 2 Jahrgänge, FIA / FISI)
                        //1-4 Durchläufe                        
                        foreach (MCalendarEntry calendarEntry in calendarDay.CalendarEntry)
                        {
                            if (calendarDay.CalendarEntry[i_entry - 1].School != null)
                            {
                                #region School
                                if (calendarDay.CalendarEntry[i_entry - 1].School.Comment != null)
                                {
                                    if (i_entry == 1)
                                    {
                                        if (school[i_entry - 1] == null || calendarDay.CalendarEntry[i_entry - 1].School.Comment != school[i_entry - 1].Comment)
                                        {
                                            //ENTRY 1
                                            entry[i_entry - 1] = calendarDay.CalendarEntry[i_entry - 1].School;
                                            mergecell[i_entry] = "G" + i_day;
                                            setRange(mergecell[i_entry], "J" + i_day);
                                            school[i_entry - 1] = (MSchool)entry[i_entry - 1];
                                            merge();
                                            setBackgroundColor(System.Drawing.Color.Blue);
                                            if (calendarDay.CalendarEntry[i_entry - 1].School.Comment != null)
                                            {
                                                setValue(calendarDay.CalendarEntry[i_entry - 1].School.Comment);
                                            }
                                            setFontColor(System.Drawing.Color.White);
                                        }
                                        else
                                        {
                                            setRange(mergecell[i_entry], "J" + i_day);
                                            merge();
                                        }
                                    }
                                    else if (i_entry == 2)
                                    {
                                        //ENTRY 2
                                        //if (school[i_entry - 2] != null && school[i_entry - 1] != school[i_entry - 2])
                                        {
                                            if (school[i_entry - 1] == null || calendarDay.CalendarEntry[i_entry - 1].School.Comment != school[i_entry - 1].Comment)
                                            {

                                                entry[i_entry - 1] = calendarDay.CalendarEntry[i_entry - 1].School;
                                                mergecell[i_entry] = "M" + i_day;
                                                setRange(mergecell[i_entry], "P" + i_day);
                                                school[i_entry - 1] = (MSchool)entry[i_entry - 1];


                                                if (school[i_entry - 2] != null && school[i_entry - 1].Comment != school[i_entry - 2].Comment)
                                                {
                                                    merge();
                                                    setBackgroundColor(System.Drawing.Color.Blue);
                                                    if (calendarDay.CalendarEntry[i_entry - 1].School.Comment != null)
                                                    {
                                                        setValue(calendarDay.CalendarEntry[i_entry - 1].School.Comment);
                                                    }
                                                    setFontColor(System.Drawing.Color.White);
                                                }
                                                else
                                                {
                                                    setRange(mergecell[i_entry], "P" + i_day);
                                                    merge();
                                                }
                                            }
                                            else
                                            {
                                                setRange(mergecell[i_entry], "P" + i_day);
                                                merge();

                                            }
                                        }

                                    }
                                }
                                #endregion
                            }
                            else if (calendarDay.CalendarEntry[i_entry - 1].Practice != null)
                            {

                                if (calendarDay.CalendarEntry[i_entry - 1].Seminar != null)
                                {
                                    #region Practice/Seminar
                                    //BLABLA
                                    #endregion

                                }
                                else
                                {
                                    #region Practice
                                    if (calendarDay.CalendarEntry[i_entry - 1].Practice.Comment != null)
                                    {
                                        if (i_entry == 1)
                                        {
                                            if (practice[i_entry - 1] == null || calendarDay.CalendarEntry[i_entry - 1].Practice.Comment != practice[i_entry - 1].Comment)
                                            {
                                                entry[i_entry - 1] = calendarDay.CalendarEntry[i_entry - 1].Practice;
                                                mergecell[i_entry] = "G" + i_day;
                                                setRange(mergecell[i_entry], "J" + i_day);
                                                practice[i_entry - 1] = (MPractice)entry[i_entry - 1];

                                                if (practice[i_entry] != null)
                                                {
                                                    if (practice[i_entry - 1].Comment == practice[i_entry].Comment)
                                                    {
                                                        //setRange(mergecell[i_entry - 1], "P" + i_day);
                                                        merge();
                                                    }
                                                }
                                                else
                                                {
                                                    mergecell[i_entry] = "G" + i_day;
                                                    setRange(mergecell[i_entry], "J" + i_day);
                                                    merge();

                                                    setBackgroundColor(System.Drawing.Color.Yellow);
                                                    if (calendarDay.CalendarEntry[i_entry - 1].Practice.Comment != null)
                                                    {
                                                        setValue(calendarDay.CalendarEntry[i_entry - 1].Practice.Comment);
                                                    }
                                                }
                                            }
                                            else
                                            {

                                                setRange(mergecell[i_entry], "J" + i_day);
                                                merge();
                                            }
                                        }
                                        else if (i_entry == 2)
                                        {
                                            //ENTRY 2
                                            //if (school[i_entry - 2] != null && school[i_entry - 1] != school[i_entry - 2])
                                            {
                                                if (practice[i_entry - 1] == null || calendarDay.CalendarEntry[i_entry - 1].Practice.Comment != practice[i_entry - 1].Comment)
                                                {
                                                    entry[i_entry - 1] = calendarDay.CalendarEntry[i_entry - 1].Practice;
                                                    mergecell[i_entry] = "M" + i_day;
                                                    setRange(mergecell[i_entry], "P" + i_day);
                                                    practice[i_entry - 1] = (MPractice)entry[i_entry - 1];

                                                    if (practice[i_entry - 2] != null)
                                                    {
                                                        if (practice[i_entry - 1].Comment == practice[i_entry - 2].Comment)
                                                        {
                                                            setRange(mergecell[i_entry - 1], "P" + i_day);
                                                            merge();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        merge();

                                                        setBackgroundColor(System.Drawing.Color.PaleTurquoise);
                                                        if (calendarDay.CalendarEntry[i_entry - 1].Practice.Comment != null)
                                                        {
                                                            setValue(calendarDay.CalendarEntry[i_entry - 1].Practice.Comment);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    setRange(mergecell[i_entry], "P" + i_day);
                                                    merge();

                                                }
                                            }

                                        }
                                    }
                                    #endregion

                                }

                            }
                            else
                            {

                                #region Seminar
                                if (calendarDay.CalendarEntry[i_entry - 1].Seminar.Title != null)
                                {
                                    if (i_entry == 1)
                                    {
                                        if (seminar[i_entry - 1] == null || calendarDay.CalendarEntry[i_entry - 1].Seminar.Title != seminar[i_entry - 1].Title)
                                        {
                                            //ENTRY 1
                                            entry[i_entry - 1] = calendarDay.CalendarEntry[i_entry - 1].Seminar;
                                            mergecell[i_entry] = "G" + i_day;
                                            setRange(mergecell[i_entry], "J" + i_day);
                                            seminar[i_entry - 1] = (MSeminar)entry[i_entry - 1];
                                            merge();
                                            setBackgroundColor(System.Drawing.Color.PaleTurquoise);
                                            if (calendarDay.CalendarEntry[i_entry - 1].Seminar.Title != null)
                                            {
                                                setValue(calendarDay.CalendarEntry[i_entry - 1].Seminar.Title);
                                            }
                                        }
                                        else
                                        {
                                            setRange(mergecell[i_entry], "J" + i_day);
                                            merge();
                                        }
                                    }
                                    else if (i_entry == 2)
                                    {
                                        //ENTRY 2
                                        //if (school[i_entry - 2] != null && school[i_entry - 1] != school[i_entry - 2])
                                        {
                                            if (seminar[i_entry - 1] == null || calendarDay.CalendarEntry[i_entry - 1].Seminar.Title != seminar[i_entry - 1].Title)
                                            {

                                                entry[i_entry - 1] = calendarDay.CalendarEntry[i_entry - 1].Seminar;
                                                mergecell[i_entry] = "M" + i_day;
                                                setRange(mergecell[i_entry], "P" + i_day);
                                                seminar[i_entry - 1] = (MSeminar)entry[i_entry - 1];

                                                if (seminar[i_entry - 2] != null)
                                                {
                                                    if (seminar[i_entry - 1].Title == seminar[i_entry - 2].Title)
                                                    {
                                                        setRange(mergecell[i_entry - 1], "P" + i_day);
                                                        merge();
                                                    }
                                                }
                                                else
                                                {
                                                    merge();

                                                    setBackgroundColor(System.Drawing.Color.PaleTurquoise);
                                                    if (calendarDay.CalendarEntry[i_entry - 1].Seminar.Title != null)
                                                    {
                                                        setValue(calendarDay.CalendarEntry[i_entry - 1].Seminar.Title);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                setRange(mergecell[i_entry], "P" + i_day);
                                                merge();

                                            }
                                        }

                                    }
                                }
                                #endregion
                            }
                            i_entry++;
                        }
                        #endregion
                    }
                    i_day++;
                }
                else
                {
                    //Wenn Wochentage SA/SO -> zuletzt erstelltes Feld -> Null ("Merk-Variable")
                    vacation = null;
                    school = new MSchool[4];
                    seminar = new MSeminar[4];
                    practice = new MPractice[4];
                }
                i_list++;
            }

            #region Abschluss
            aRange = ws.get_Range("D1", "D" + (i_day - 1));
            merge();
            aRange = ws.get_Range("Q1", "Q" + (i_day - 1));
            merge();
            aRange = ws.get_Range("A1", "Q" + (i_day - 1));
            setBorderColor(System.Drawing.Color.Black);

            //wb.SaveCopyAs(@"C:\TAGPLAN1.xlsx");
            wb.Close(true, Missing.Value, Missing.Value);
            xlApp.Quit();
            releaseObject(xlApp);
            releaseObject(wb);
            releaseObject(ws);
            #endregion
            //MessageBox.Show("File created !");
            return true;
        }


    }
}
