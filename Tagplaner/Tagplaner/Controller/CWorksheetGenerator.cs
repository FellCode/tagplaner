using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace Tagplaner
{
    /// <summary>
    /// Diese Klasse generiert einen Tagplan mit Excel
    /// </summary>
    public class WorksheetGenerator : IWorksheetGenerator
    {
        private Application xlApp = new Application();
        private Workbook wb;
        private Worksheet ws;
        private Range aRange;
        private Object[] data = new Object[1];
        private bool test = true;
        private int i_day = 1;
        private int i_list = 0;
        private int i_speciality = 1;
        private string calendarWeek = "";
        private string vacation = null;
        private string[] mergecell = new string[9];
        private Object[] newMergeCell = new Object[9];
        //============Vergleichsvariablen================
        private Object[] entry = new Object[8];
        private MSchool[] school = new MSchool[8];
        private MSeminar[] seminar = new MSeminar[8];
        private MPractice[] practice = new MPractice[8];
        private Object[] secondentry = new Object[8];
        //===============================================
        private string day = null;
        private System.Globalization.CultureInfo german = new System.Globalization.CultureInfo("de-DE");
        private int i_entry = 1;
        private int col = 1;

        /// <summary>
        /// Die Methode enthält die Logik, nach der entschieden wird, was in das Excel-Worksheet geschrieben wird.
        /// Beschrieben wird das Worksheet durch die Methoden die in "WriteFile" aufgerufen werden.
        /// </summary>
        /// <param name="calendar"></param>
        /// <returns></returns>
        public bool WriteFile(MCalendar calendar)
        {
            i_day = 1;
            prepareGeneration();
            generateHeader(calendar);
            //Schleife fuer jeden Tag in der calendarList
            if (calendar.CalendarList != null)
                foreach (MCalendarDay calendarDay in calendar.CalendarList)
                {
                    i_entry = 1;
                    col = 1;

                    //Findet heraus, ob eine neue Kalenderwoche startet
                    if (calendarWeek != calendarDay.CalendarWeek)
                    {
                        generateCalendarWeek(calendarDay, calendar);
                    }

                    //Wochentag
                    day = german.DateTimeFormat.GetDayName(calendarDay.CalendarDate.DayOfWeek).ToString().Substring(0, 2).ToUpper();
                    if (!(day.Equals("SA") || day.Equals("SO")))
                    {
                        
                        generateDate(day, calendarDay);

                        //Generiert Urlaubs-/Feiertage
                        if (calendar.CalendarList[i_list].VacationName != null)
                        {
                            generateVacation(calendar);
                        }
                        if (calendar.CalendarList[i_list].HolidayName != null)
                        {
                            generateHoliday(calendar, calendarDay);
                        }
                        else
                        {
                            //Schleife für jede Spalte des Tages 
                            if (calendarDay.CalendarEntry != null)
                                foreach (MCalendarEntry calendarEntry in calendarDay.CalendarEntry)
                                {

                                    if (calendarDay.CalendarEntry[i_entry - 1].School != null)
                                    {
                                        generateSchool(calendarDay);
                                    }
                                    else if (calendarDay.CalendarEntry[i_entry - 1].Practice != null)
                                    {
                                        if (calendarDay.CalendarEntry[i_entry - 1].Seminar != null)
                                        {
                                            generatePractice_Seminar(calendarDay);
                                        }
                                        else
                                        {
                                            generatePractice(calendarDay);
                                        }
                                    }
                                    else
                                    {
                                        generateSeminar(calendarDay);
                                    }
                                    i_entry++;
                                }
                        }
                        i_day++;
                    }
                    else
                    {
                        vacation = null;
                        setNewWeek();
                    }
                    i_list++;
                }
            finishDocument(calendar);
            return true;
        }

        /// <summary>
        /// Konstruktor der Klasse WorksheetGenerator
        /// Erstellt außerdem das zu befüllende Worksheet
        /// </summary>
        public WorksheetGenerator()
        {
            wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            ws = (Worksheet)wb.Worksheets[1];
            xlApp.Visible = true;
            aRange = ws.get_Range("A1", "B1");
        }
        /// <summary>
        /// Schließt Objekte. So werden am Ende des Generierens die "im Raum fliegenden Excel Objekte" entfernt.
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Diese Methode soll ein Zellen-Objekt aus Koordinaten erstellen und zurückgeben
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        private Object getCell(string cells)
        {
            int row = Convert.ToInt32(cells.Substring(cells.IndexOf(",") + 1, (cells.Length - 1) - cells.IndexOf(",")));
            int col = Convert.ToInt32(cells.Substring(0, cells.IndexOf(",")));
            return ws.Cells[row, col];
        }
        /// <summary>
        /// Setzt die Range auf angegebene Koordinaten
        /// </summary>
        /// <param name="cell"></param>
        private void setRange(Object cell)
        {
            setRange(cell, cell);
        }
        /// <summary>
        /// Setzt die Range auf angegebene Koordinaten
        /// </summary>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        private void setRange(Object cell1, Object cell2)
        {
            aRange = ws.get_Range(cell1, cell2);
        }
        /// <summary>
        /// Setzt die Hintergundfarbe für die aktuelle Range
        /// </summary>
        /// <param name="color"></param>
        private void setBackgroundColor(System.Drawing.Color color)
        {
            aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        /// <summary>
        /// Setzt die Schriftfarbe für die aktuelle Range
        /// </summary>
        /// <param name="color"></param>
        private void setFontColor(System.Drawing.Color color)
        {
            aRange.Font.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        /// <summary>
        /// Setzt die Rahmenfarbe für die aktuelle Range
        /// </summary>
        /// <param name="color"></param>
        private void setBorderColor(System.Drawing.Color color)
        {
            aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(color);
        }
        /// <summary>
        /// Schreibt den übergebenen Wert in die aktuelle Range
        /// </summary>
        /// <param name="value"></param>
        private void setValue(Object value)
        {
            data[0] = value;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, data);
        }
        /// <summary>
        /// Verbindet die Zellen der aktuellen Range
        /// </summary>
        private void merge()
        {
            aRange.Merge(Missing.Value);
        }
        /// <summary>
        /// Reset der zuletzt gesetzten Felder in den Verleichsvariablen beim Wochenwechsel
        /// </summary>
        private void setNewWeek()
        {
            school = new MSchool[8];
            seminar = new MSeminar[8];
            practice = new MPractice[8];
            secondentry = new Object[8];
        }
        /// <summary>
        /// Formatiert alle Spalten bezüglich Größe und zentriert Texte
        /// </summary>
        private void prepareGeneration()
        {
            setRange("A1");
            aRange.EntireColumn.ColumnWidth = 4;
            setRange("B1");
            aRange.EntireColumn.ColumnWidth = 10;
            setRange("C1");
            aRange.EntireColumn.ColumnWidth = 5;
            setRange("A1", "BB1");
            aRange.EntireColumn.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            aRange.EntireColumn.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            aRange.EntireColumn.WrapText = true;
            //aRange.EntireColumn.Font.Name = "Verdana";
        }
        /// <summary>
        /// Erstellt den Dokumentkopf
        /// </summary>
        /// <param name="calendar"></param>
        private void generateHeader(MCalendar calendar)
        {



            i_speciality = 1;
            foreach (MSpeciality speciality in calendar.Speciality)
            {
                setRange(getCell((i_speciality * 7) - 3 + "," + 1), getCell((i_speciality * 7)  + 3 + "," + 3));
                aRange.EntireColumn.ColumnWidth = 5;

                setRange(getCell((i_speciality * 7) - 3 + "," + 1));
                aRange.EntireColumn.ColumnWidth = 1;

                i_day = 1;
                if (i_speciality % 2 == 0)
                {


                    setRange(getCell((i_speciality * 7) - 3 + "," + 1));
                    aRange.EntireColumn.ColumnWidth = 1;

                    setRange(getCell((i_speciality * 7) - 9 + "," + i_day), getCell((i_speciality * 7) + 3 + "," + i_day));
                    merge();
                    setValue("Ausbildung Fachinformatiker " + speciality.IdentifierOfYear);
                    i_day++;

                    setRange(getCell((i_speciality * 7) - 8 + "," + i_day), getCell((i_speciality * 7) + 2 + "," + i_day));
                    merge();
                    setBackgroundColor(System.Drawing.Color.Moccasin);
                    setValue("Legende");
                    i_day++;

                    setRange(getCell((i_speciality * 7) - 8 + "," + i_day), getCell((i_speciality * 7) + 2 + "," + i_day));
                    merge();
                    setValue("Betriebliche Praxisphase");
                    setBackgroundColor(System.Drawing.Color.Yellow);
                    i_day++;

                    setRange(getCell((i_speciality * 7) - 8 + "," + i_day), getCell((i_speciality * 7) + 2 + "," + i_day));
                    setBackgroundColor(System.Drawing.Color.Blue);
                    setFontColor(System.Drawing.Color.White);
                    merge();
                    setValue("Berufsschule");
                    i_day++;
                    setRange(getCell((i_speciality * 7) - 8 + "," + i_day), getCell((i_speciality * 7) + 2 + "," + i_day));
                    setBackgroundColor(System.Drawing.Color.PaleTurquoise);
                    merge();
                    setValue("Seminar");

                    setRange(getCell((i_speciality * 7) - 9 + "," + 2), getCell((i_speciality * 7) - 9 + "," + i_day));
                    merge();
                    setBackgroundColor(System.Drawing.Color.Moccasin);
                    setRange(getCell((i_speciality * 7) + 3 + "," + 2), getCell((i_speciality * 7) + 3 + "," + i_day));
                    merge();
                    setBackgroundColor(System.Drawing.Color.Moccasin);
                }
                i_speciality++;
            }
            setRange("A1", "C" + i_day);
            merge();
            setBackgroundColor(System.Drawing.Color.Moccasin);
            i_day++;

   
        }
        /// <summary>
        /// Schreibt bei Wochenwechsel eine Zeile, die die jeweilige Kalenderwoche anzeigt
        /// </summary>
        /// <param name="calendarDay"></param>
        private void generateCalendarWeek(MCalendarDay calendarDay, MCalendar calendar)
        {
            setRange("A" + i_day, "B" + i_day);
            merge();
            setBackgroundColor(System.Drawing.Color.LightGray);
            setValue("KW" + calendarDay.CalendarWeek);

            setRange("C" + i_day);
            merge();
            setBackgroundColor(System.Drawing.Color.LightSeaGreen);
            calendarWeek = calendarDay.CalendarWeek;

            i_speciality = 1;
            foreach (MSpeciality speciality in calendar.Speciality)
            {
                if (i_speciality % 2 == 0)
                {
                    setRange(getCell((i_speciality * 7) - 9 + "," + i_day), getCell((i_speciality * 7) + 3 + "," + i_day));
                    merge();
                    setBackgroundColor(System.Drawing.Color.LightSeaGreen);

                }
                i_speciality++;
            }
            i_day++;
        }
        /// <summary>
        /// Schreibt das Datum und den Tag in die jeweilige Zeile
        /// </summary>
        /// <param name="day"></param>
        /// <param name="calendarDay"></param>
        private void generateDate(string day, MCalendarDay calendarDay)
        {
            setRange("A" + i_day);
            setValue(day);
            setBorderColor(System.Drawing.Color.Black);

            setRange("B" + i_day);
            setValue(calendarDay.CalendarDate.ToString().Substring(0, 10));
        }
        /// <summary>
        /// Generiert einen Ferieneintrag
        /// </summary>
        /// <param name="calendar"></param>
        private void generateVacation(MCalendar calendar)
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
        /// <summary>
        /// Generiert einen Feiertagseintrag
        /// </summary>
        /// <param name="calendar"></param>
        /// <param name="calendarDay"></param>
        private void generateHoliday(MCalendar calendar, MCalendarDay calendarDay)
        {
            setRange("E" + i_day, "P" + i_day);
            merge();
            setValue(calendar.CalendarList[i_list].HolidayName);
            setBackgroundColor(System.Drawing.Color.GreenYellow);
            setNewWeek();
        }
        /// <summary>
        /// Generiert einen Schuleintrag
        /// </summary>
        /// <param name="calendarDay"></param>

        private void generateSchool(MCalendarDay calendarDay)
        {
            if (calendarDay.CalendarEntry[i_entry - 1].School != null)
            {
                if (i_entry % 2 != 0)
                {
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7) + 3 + "," + i_day)));
                }
                else
                {
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7) + 3 + "," + i_day)));
                }
                merge();
                setBackgroundColor(System.Drawing.Color.Blue);
                if (calendarDay.CalendarEntry[i_entry - 1].School.Comment != null)
                {
                    setValue(calendarDay.CalendarEntry[i_entry - 1].School.Comment);
                }
                setFontColor(System.Drawing.Color.White);
            }
        }
        /// <summary>
        /// Generiert einen Seminareintrag
        /// </summary>
        /// <param name="calendarDay"></param>
        private void generateSeminar(MCalendarDay calendarDay)
        {
            if (calendarDay.CalendarEntry[i_entry - 1].Seminar != null)
            {
                if (i_entry % 2 != 0)
                {
                    setRange(getCell((i_entry * 7) - 2 + "," + i_day), (getCell((i_entry * 7) + "," + i_day)));
                    if (calendarDay.CalendarEntry[i_entry - 1].Room != null)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].Room.Number != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry - 1].Room.Number);
                        }
                    }
                    setRange(getCell((i_entry * 7) - 1 + "," + i_day), (getCell((i_entry * 7) + "," + i_day)));
                    if (calendarDay.CalendarEntry[i_entry - 1].Trainer != null)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation);
                        }
                    }
                    else if (calendarDay.CalendarEntry[i_entry-1].Trainer.Name !=null)
                    {
                        setValue(calendarDay.CalendarEntry[i_entry - 1].Trainer.Name);
                        if (calendarDay.CalendarEntry[i_entry-1].Trainer.Surname != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry-1].Trainer.Surname);
                        }
                    }
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7) + 3 + "," + i_day)));
                }
                else
                {
                    setRange(getCell((i_entry * 7) -2+ "," + i_day), (getCell((i_entry * 7)  + "," + i_day)));
                    if (calendarDay.CalendarEntry[i_entry - 1].Room != null)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].Room.Number != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry - 1].Room.Number);
                        }
                    }
                    setRange(getCell((i_entry * 7) -1+ "," + i_day), (getCell((i_entry * 7)  + "," + i_day)));
                    if (calendarDay.CalendarEntry[i_entry - 1].Trainer != null)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation);
                        }
                    }
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7) + 3 + "," + i_day)));
                }
                merge();
                setBackgroundColor(System.Drawing.Color.PaleTurquoise);
                setValue(calendarDay.CalendarEntry[i_entry - 1].Seminar.Title);
            }
        }
        /// <summary>
        /// Generiert einen Praxiseintrag
        /// </summary>
        /// <param name="calendarDay"></param>
        private void generatePractice(MCalendarDay calendarDay)
        {
                if (i_entry % 2 != 0)
                {
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7) + 3 + "," + i_day)));
                }
                else
                {
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7) + 3 + "," + i_day)));
                }
                merge();
                setBackgroundColor(System.Drawing.Color.Yellow);
        }
        /// <summary>
        /// Merge der Trennlinien, Aufräumen von Objekten, Speichern des Dokuments
        /// </summary>
        private void generatePractice_Seminar(MCalendarDay calendarDay)
        {
            if (calendarDay.CalendarEntry[i_entry - 1].Seminar != null)
            {
                if (i_entry % 2 != 0)
                {
                    setRange(getCell((i_entry * 7) -2+ "," + i_day), (getCell((i_entry * 7) + "," + i_day)));   
                    if (calendarDay.CalendarEntry[i_entry - 1].Room != null)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].Room.Number != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry - 1].Room.Number);
                        }
                    }
                    setRange(getCell((i_entry * 7) -1+ "," + i_day), (getCell((i_entry * 7) + "," + i_day)));
                    if (calendarDay.CalendarEntry[i_entry - 1].Trainer != null)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation);
                        }
                    }
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7) +1+ "," + i_day)));
                    merge();
                    setBackgroundColor(System.Drawing.Color.Yellow);
                    setValue(calendarDay.CalendarEntry[i_entry - 1].Practice.Comment);
                    setRange(getCell((i_entry * 7) +3+ "," + i_day), (getCell((i_entry * 7)+2+ "," + i_day)));
                    merge();
                    setBackgroundColor(System.Drawing.Color.PaleTurquoise);
                    setValue(calendarDay.CalendarEntry[i_entry - 1].Seminar.Title);
                }
                else
                {
                    setRange(getCell((i_entry * 7) -2+ "," + i_day), (getCell((i_entry * 7)+ "," + i_day)));
                    if (calendarDay.CalendarEntry[i_entry - 1].Room != null)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].Room.Number != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry - 1].Room.Number);
                        }
                    }
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7) -1+ "," + i_day)));
                    if (calendarDay.CalendarEntry[i_entry - 1].Trainer != null)
                    {
                        if (calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation != null)
                        {
                            setValue(calendarDay.CalendarEntry[i_entry - 1].Trainer.Abbreviation);
                        }
                    }
                    setRange(getCell((i_entry * 7) + "," + i_day), (getCell((i_entry * 7)  +1+ "," + i_day)));
                    merge();
                    setBackgroundColor(System.Drawing.Color.Yellow);
                    setValue(calendarDay.CalendarEntry[i_entry - 1].Practice.Comment);
                    setRange(getCell((i_entry * 7) +3+ "," + i_day), (getCell((i_entry * 7) +2+ "," + i_day)));
                    merge();
                    setBackgroundColor(System.Drawing.Color.PaleTurquoise);
                    setValue(calendarDay.CalendarEntry[i_entry - 1].Seminar.Title);
                }
            }
        }
        
        
        private void finishDocument(MCalendar calendar)
        {
            i_speciality = 1;
            var count = calendar.Speciality.Count;
            foreach (MSpeciality speciality in calendar.Speciality)
            {
                if (i_speciality % 2 != 0)
                {
                    setRange(getCell((i_speciality * 7) - 3 + "," + 1), getCell((i_speciality * 7) - 3 + "," + i_day));
                    merge();
                }
                if (++i_speciality == count)
                {
                    aRange = ws.get_Range("A1", getCell((i_speciality * 7) + 3 + "," + i_day));
                    setBorderColor(System.Drawing.Color.Black);
                } 
            }
            //wb.SaveCopyAs(@"C:\TAGPLAN1.xlsx");
            wb.Close(true, Missing.Value, Missing.Value);
            xlApp.Quit();
            releaseObject(xlApp);
            releaseObject(wb);
            releaseObject(ws);
        }
    }
}
