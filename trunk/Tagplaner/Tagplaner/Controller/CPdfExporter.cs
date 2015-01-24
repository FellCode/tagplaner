using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Tagplaner 
{
    /// <summary>
    /// Klasse zum erzeugen von Tagplan-PDF-Dokumenten, die das Interface IPdfExporter implementiert
    /// </summary>
    public class CPdfExporter : IPdfExporter
    {
        private Document doc;
        private List<MTrainer> trainerList;
        private List<string> identifierOfYearList;
        private Dictionary<string, string> dayDictionary = new Dictionary<string, string>();
        private int numberOfApprenticeships;
        private bool showComments;
 
        // Font Size 7
        readonly Font FONT_SMALL_NORMAL = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL);
        readonly Font FONT_SMALL_ITALIC = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.ITALIC);
        readonly Font FONT_SMALL_BOLD = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD);
        readonly Font FONT_SMALL_BOLD_ITALIC = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLDITALIC);
        // Font Size 9
        readonly Font FONT_NORMAL = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL);
        readonly Font FONT_ITALIC = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.ITALIC);
        readonly Font FONT_BOLD = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD);
        readonly Font FONT_BOLD_ITALIC = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLDITALIC);
        // Font for Unuiqday
        readonly Font FONT_UNIQUE = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);

        readonly BaseColor COLOR_BLANK = BaseColor.WHITE;
        readonly BaseColor COLOR_HOLIDAY = new BaseColor(173, 255, 47);
        readonly BaseColor COLOR_SEMINAR = new BaseColor(135, 206, 250);
        readonly BaseColor COLOR_SCHOOL = BaseColor.BLUE;
        readonly BaseColor COLOR_PRATICE = new BaseColor(255, 215, 0);
        readonly BaseColor COLOR_UNIQUEDAY = BaseColor.RED;

        /// <summary>
        /// Erstellt eine neue Instanz von CPdfExporter
        /// </summary>
        /// <param name="identifierOfYearList">Liste mit Jahrgängen die angezeigt werden sollen</param>
        /// <param name="showComments">Information, ob Kommentare angezeigt werden soll</param>
        /// <param name="trainerList">Liste mit MTrainer Instanzen</param>
        public CPdfExporter(List<string> identifierOfYearList, bool showComments, List<MTrainer> trainerList)
        {
            this.identifierOfYearList = identifierOfYearList;
            this.numberOfApprenticeships = identifierOfYearList.Count;
            this.showComments = showComments;
            this.trainerList = trainerList;
            
            FillDayDictionary();
        }

        /// <summary>
        /// Erzeugt ein PDF-Dokument für den Tagplan und speichert diese an den angegebenen Ort
        /// </summary>
        /// <param name="filename">Speichertort für das PDF-Dokument</param>
        /// <returns>Ist true wenn die Datei erfolgreich erstellt wurde</returns>
        public bool ExportPdf(string filename)
        {
            float margin = Utilities.MillimetersToPoints(Convert.ToSingle(20));
            doc = new Document(
                getFormatByNumberOfApprenticeships(),
                margin,
                margin,
                margin,
                margin
            );

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
            writer.SetFullCompression();
            writer.CloseStream = true;

            doc.Open();
            doc.NewPage();

            CreatePdfHeader();
            CreatePdfBody();
            CreatePdfFooter();

            if (doc != null)
            {
                doc.Close();
                doc = null;

                return true;
            }
            else
            {
                return false;
            }       
        }

        /// <summary>
        /// Füllt das Dictionary dayDictionary mit Schlüsselwertpaaren
        /// </summary>
        private void FillDayDictionary()
        {
            dayDictionary.Add("Monday", "Mo");
            dayDictionary.Add("Tuesday", "Di");
            dayDictionary.Add("Wednesday", "Mi");
            dayDictionary.Add("Thursday", "Do");
            dayDictionary.Add("Friday", "Fr");
            dayDictionary.Add("Saturday", "Sa");
            dayDictionary.Add("Sunday", "So");
        }

        /// <summary>
        /// Erstellt eine neue Zelle mit den angegebenen Optionen
        /// </summary>
        /// <param name="text">Anzeigetext für die Zelle</param>
        /// <param name="font">Schriftformatierung</param>
        /// <param name="backgroundColor">Hintegrundfarbe der Zelle</param>
        /// <param name="colSpan">Anzahl der Zellen die zusammengefasst werden sollen</param>
        /// <param name="rowSpan">Anzahl der Reihen die zusammengefasst werden sollen</param>
        /// <returns></returns>
        private PdfPCell CreateTabeCell(string text, Font font, BaseColor backgroundColor, int colSpan, int rowSpan)
        {
            PdfPCell pdfCell = new PdfPCell();
            pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Phrase = new Phrase(text, font);
            pdfCell.BackgroundColor = backgroundColor;
            pdfCell.Colspan = colSpan;
            pdfCell.Rowspan = rowSpan;

            return pdfCell;
        }

        /// <summary>
        /// Erzeugt den oberen Teil des PDF-Dokuments mit der Legende
        /// </summary>
        private void CreatePdfHeader()
        {
            doc.Add(CreateTableTopic());
            doc.Add(CreateLegend());
            doc.Add(CreateTopRow());
        }

        /// <summary>
        /// Erzeugt den Hauptteil des Tagplans
        /// </summary>
        private void CreatePdfBody()
        {
            MCalendar calendar = MCalendar.GetInstance();

            for (int i = 0; i < calendar.CalendarList.Count; i++)
            {
                MCalendarDay calendarDay = calendar.CalendarList.ElementAt(i);
                MCalendarDay nextCalendarDay = null;

                if (calendar.CalendarList.Count - 1 > i)
                {
                    nextCalendarDay = calendar.CalendarList.ElementAt(i + 1);
                }

                switch (dayDictionary[calendarDay.CalendarDate.DayOfWeek.ToString()])
                {
                    case "Sa":
                        break;
                    case "So":
                        CreateBodyTableRowWeekend(calendarDay.CalendarWeek);
                        break;
                    default:
                        CreateBodyTableRow(calendarDay, nextCalendarDay);
                        break;
                }
            }
        }

        /// <summary>
        /// Erzeugt eine Tabelle am Ende des PDF-Dokuments mit den Namen und
        /// Kürzel der Seminarleiter
        /// </summary>
        private void CreatePdfFooter()
        {
            CreateFooterTableRow("Kürzel", "Name", FONT_BOLD, 20);

            foreach (MTrainer trainer in trainerList)
            {
                CreateFooterTableRow(trainer.Abbreviation, trainer.Surname + " " + trainer.Name, FONT_NORMAL, 0);
            }

        }

        /// <summary>
        /// Erstellt eine Tabelle mit einer Überschrift für die Tabelle pro Ausbildungsjahr
        /// </summary>
        /// <returns>Tabelle mit Überschrift</returns>
        private PdfPTable CreateTableTopic()
        {
            PdfPTable pdfTable = new PdfPTable(32);
            pdfTable.WidthPercentage = 100;
            pdfTable.AddCell(CreateTabeCell("", FONT_BOLD, COLOR_BLANK, 4, 1));

            for (int i = 0; i < identifierOfYearList.Count; i++)
            {
                foreach (string identifierOfYear in identifierOfYearList)
                {
                    pdfTable.AddCell(CreateTabeCell(
                       // "Ausbildung zum Fachinformatiker am Standort " + ort + " Jahrgang " + jahrgang + ". Jahr\n",
                        identifierOfYear,
                        FONT_BOLD,
                        COLOR_BLANK,
                        28 / identifierOfYearList.Count,
                        1));  
                }
            }

            return pdfTable;
        }

        /// <summary>
        ///  Erzeugt die Legende für die verschiedenen Arten von Tagplan 
        /// </summary>
        /// <returns>Tabelle für die Legende</returns>
        private PdfPTable CreateLegend()
        {
            PdfPTable pdfTable = new PdfPTable(32);
            pdfTable.WidthPercentage = 100;
            pdfTable.SpacingAfter = 20;

            pdfTable.AddCell(CreateTabeCell("Legende", FONT_SMALL_BOLD, COLOR_BLANK, 4, 4));

            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_SCHOOL, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Phase in Berufsschule", FONT_NORMAL, COLOR_BLANK, 13, 1));
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_SCHOOL, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Phase in Berufsschule", FONT_NORMAL, COLOR_BLANK, 13, 1));

            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_PRATICE, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Phase in Betrieb", FONT_NORMAL, COLOR_BLANK, 13, 1));
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_PRATICE, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Phase in Betrieb", FONT_NORMAL, COLOR_BLANK, 13, 1));

            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_SEMINAR, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Phase CSC Akademie", FONT_NORMAL, COLOR_BLANK, 13, 1));
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_SEMINAR, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Phase CSC Akademie", FONT_NORMAL, COLOR_BLANK, 13, 1));

            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_HOLIDAY, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Ferien/Feiertage", FONT_NORMAL, COLOR_BLANK, 13, 1));
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_HOLIDAY, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Ferien/Feiertage", FONT_NORMAL, COLOR_BLANK, 13, 1));

            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, calculateNumberOfCells(), 1));

            return pdfTable;
        }

        /// <summary>
        /// Erzeugt den Tabellenkopf für die Tagplan-Tabelle
        /// </summary>
        /// <returns>Tabelle mit überschriften für den Tagplan</returns>
        public PdfPTable CreateTopRow()
        {
            PdfPTable pdfTable = new PdfPTable(calculateNumberOfCells());
            pdfTable.WidthPercentage = 100;

            pdfTable.AddCell(CreateTabeCell("Tag", FONT_SMALL_BOLD, COLOR_BLANK, 1, 1));
            pdfTable.AddCell(CreateTabeCell("Datum", FONT_SMALL_BOLD, COLOR_BLANK, 2, 1));
            pdfTable.AddCell(CreateTabeCell("Ferien", FONT_SMALL_BOLD, COLOR_BLANK, 1, 1));

            for (int i = 0; i < numberOfApprenticeships; i++)
            {
                pdfTable.AddCell(CreateTabeCell("Technik", FONT_SMALL_BOLD, COLOR_BLANK, 1, 1));
                pdfTable.AddCell(CreateTabeCell("Ort", FONT_SMALL_BOLD, COLOR_BLANK, 1, 1));
                pdfTable.AddCell(CreateTabeCell("Trainer", FONT_SMALL_BOLD, COLOR_BLANK, 1, 1));
                pdfTable.AddCell(CreateTabeCell("Inhalt\nAnwendungsentwickler", FONT_SMALL_BOLD, COLOR_BLANK, 4, 1));

                pdfTable.AddCell(CreateTabeCell("Technik", FONT_SMALL_BOLD, COLOR_BLANK, 1, 1));
                pdfTable.AddCell(CreateTabeCell("Ort", FONT_SMALL_BOLD, COLOR_BLANK, 1, 1));
                pdfTable.AddCell(CreateTabeCell("Trainer", FONT_SMALL_BOLD, COLOR_BLANK, 1, 1));
                pdfTable.AddCell(CreateTabeCell("Inhalt\nSystemintegration", FONT_SMALL_BOLD, COLOR_BLANK, 4, 1));
            }

            return pdfTable;
        }

        /// <summary>
        /// Erstellt eine Tabellenreihe mit den eigentlichen Tagplan Informationen
        /// </summary>
        /// <param name="calendarDay">Aktueller Kalendartag</param>
        /// <param name="nextCalendarDay">Nächster Kalendartag</param>
        private void CreateBodyTableRow(MCalendarDay calendarDay, MCalendarDay nextCalendarDay)
        {
            PdfPTable pdfTable = new PdfPTable(calculateNumberOfCells());
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfTable.AddCell(
                CreateTabeCell(
                    dayDictionary[calendarDay.CalendarDate.DayOfWeek.ToString()],
                    FONT_NORMAL,
                    COLOR_BLANK,
                    1,
                    1));                                                                        // Wochentag
            pdfTable.AddCell(
                CreateTabeCell(
                    calendarDay.CalendarDate.Date.ToShortDateString(),
                    FONT_NORMAL,
                    COLOR_BLANK,
                    2,
                    1));                                                                        // Datum                  

            // Prüfen ob aktueller Tag ein Ferientag ist
            #region Ferien
            if (!String.IsNullOrEmpty(calendarDay.VacationName))
            {
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_HOLIDAY, 1, 1));            // Ferien
            }
            else
            {
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));              // Leere Zelle
            }
            #endregion

            // Prüfen, ob der aktuelle Tag ein Feiertag ist
            if (!String.IsNullOrEmpty(calendarDay.HolidayName))
            {
                #region Feiertag
                for (int i = 0; i < numberOfApprenticeships; i++)
                {
                    pdfTable.AddCell(CreateTabeCell(calendarDay.HolidayName,
                        FONT_NORMAL, COLOR_HOLIDAY, calculateNumberOfCells() / numberOfApprenticeships, 1));
                }
                #endregion
            }
            // Regulären Tagplan erzeugen
            else
            {
                // Year two - FIAE
                MCalendarEntry calendarEntry = null;
                int calendarEntryPosition = 0;
                
                // Durchlauf pro Anzahl der Ausbildungsjahrgänge
                for (int i = 0; i < numberOfApprenticeships; i++)
                {
                    foreach (string identifierOfYear in identifierOfYearList)
                    {
                        int numberOfSpecialitys = CountSpecialityByIdentifierOfYear(identifierOfYear);
                        calendarEntryPosition = GetSpecialityListPosition(identifierOfYear);

                        // Zeige Spalte nur für FIAE
                        if (numberOfSpecialitys == 1 && GetSpecialityNameByIdentifierOfYear(identifierOfYear).Equals("AE"))
                        {
                            CreateRow(pdfTable, calendarDay, nextCalendarDay, calendarEntryPosition);
                            CreateBlankRow(pdfTable);
                        }
                        // Zeige Spalte nur für FISI
                        else if (numberOfSpecialitys == 1 && GetSpecialityNameByIdentifierOfYear(identifierOfYear).Equals("SI"))
                        {
                            CreateBlankRow(pdfTable);
                            CreateRow(pdfTable, calendarDay, nextCalendarDay, calendarEntryPosition);
                        }
                        // Zeige Spalte für FIAE und FISI
                        else
                        {
                            calendarEntryPosition = GetSpecialityListPosition(identifierOfYear, "AE");
                            calendarEntry = calendarDay.CalendarEntry[calendarEntryPosition];
                            CreateRow(pdfTable, calendarDay, nextCalendarDay, calendarEntryPosition);

                            calendarEntryPosition = GetSpecialityListPosition(identifierOfYear, "SI");
                            calendarEntry = calendarDay.CalendarEntry[calendarEntryPosition];
                            CreateRow(pdfTable, calendarDay, nextCalendarDay, calendarEntryPosition);
                        }   
                    }
                }
            }

            doc.Add(pdfTable);
        }

        /// <summary>
        /// Erstellt eine Zeile für einen Seminartag, Schulltag oder Praxistag anhand der Information aus dem MCalendarEntry Onjekt
        /// </summary>
        /// <param name="pdfTable">Instanz der PdfTable in der die Zeile erzeugt werden soll</param>
        /// <param name="calendarDay">Aktueller Kalendartag</param>
        /// <param name="nextCalendarDay">Nächster Kalendartag</param>
        /// <param name="calendarEntryPosition">Position im CalendarEntry</param>
        private void CreateRow(PdfPTable pdfTable, MCalendarDay calendarDay, MCalendarDay nextCalendarDay, int calendarEntryPosition)
        {
            // Prüfen, ob der aktuelle Tag ein Seminartag ist
            if (calendarDay.CalendarEntry[calendarEntryPosition].Seminar != null &&
                calendarDay.CalendarEntry[calendarEntryPosition].School == null &&
                calendarDay.CalendarEntry[calendarEntryPosition].Practice == null)
            {
                CreateSeminarRow(pdfTable, calendarDay, nextCalendarDay, calendarEntryPosition);
            }
            // Prüfen, ob der aktuelle Tag ein Schultag ist
            if (calendarDay.CalendarEntry[calendarEntryPosition].Seminar == null &&
                calendarDay.CalendarEntry[calendarEntryPosition].School != null &&
                calendarDay.CalendarEntry[calendarEntryPosition].Practice == null)
            {
                CreateSchoolRow(pdfTable, calendarDay, nextCalendarDay, calendarEntryPosition);
            }
            // Prüfen, ob der aktuelle Tag ein Praxistag ist
            else if (calendarDay.CalendarEntry[calendarEntryPosition].Seminar == null &&
                calendarDay.CalendarEntry[calendarEntryPosition].School == null &&
                calendarDay.CalendarEntry[calendarEntryPosition].Practice != null)
            {
                CreatePraticeRow(pdfTable, calendarDay, nextCalendarDay, calendarEntryPosition);
            }
            // Prüfen, ob der aktuelle Tag ein Praxis und Seminartag ist
            else if (calendarDay.CalendarEntry[calendarEntryPosition].Seminar != null &&
                calendarDay.CalendarEntry[calendarEntryPosition].School == null &&
                calendarDay.CalendarEntry[calendarEntryPosition].Practice != null)
            {
                CreateSeminarAndPraticeRow(pdfTable, calendarDay, nextCalendarDay, calendarEntryPosition);
            }
            else if (calendarDay.CalendarEntry[calendarEntryPosition].Seminar == null &&
                calendarDay.CalendarEntry[calendarEntryPosition].School == null &&
                calendarDay.CalendarEntry[calendarEntryPosition].Practice == null)
            {
               CreateBlankRow(pdfTable);
            }
        }

        /// <summary>
        /// Fügt einen Eintrag für ein Seminartag zur angegebenen PdfTabelle hinzu
        /// </summary>
        /// <param name="pdfTable">Instanz der PdfTable in der die Zeile erzeugt werden soll</param>
        /// <param name="calendarDay">Aktueller Kalendartag</param>
        /// <param name="nextCalendarDay">Nächster Kalendartag</param>
        /// <param name="calendarEntryPosition">Position im CalendarEntry</param>
        private void CreateSeminarRow(PdfPTable pdfTable, MCalendarDay calendarDay, MCalendarDay nextCalendarDay, int calendarEntryPosition)
        {
            pdfTable.AddCell(CreateTabeCell(calendarDay.CalendarEntry[calendarEntryPosition].Seminar.HasTechnology, FONT_NORMAL, COLOR_BLANK, 1, 1));     // Technik

            // Prüfen, ob ein Trainer vorhanden ist
            if (calendarDay.CalendarEntry[calendarEntryPosition].Room != null)
            {
                pdfTable.AddCell(CreateTabeCell(calendarDay.CalendarEntry[calendarEntryPosition].Room.Number, FONT_NORMAL, COLOR_BLANK, 1, 1));           // RaumNr.
            }
            else {
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                  // Keine RaumNr. angegeben
            }

            // Prüfen, ob ein Trainer vorhanden ist
            if (calendarDay.CalendarEntry[calendarEntryPosition].Trainer != null)
            {
                pdfTable.AddCell(CreateTabeCell(calendarDay.CalendarEntry[calendarEntryPosition].Trainer.Abbreviation, FONT_NORMAL, COLOR_BLANK, 1, 1));  // Trainer
            }
            else
            {
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                  // Kein Trainer angegeben
            }


            if (nextDayIsSeminar(calendarDay, nextCalendarDay, calendarEntryPosition))
            {
                pdfTable.AddCell(CreateTabeCell(calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Title, FONT_NORMAL, COLOR_SEMINAR, 4, 1));       // Seminar
            }
            else
            {
                if (showComments && !String.IsNullOrEmpty(calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Comment))
                {
                    pdfTable.AddCell(
                        CreateTabeCell(
                            calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Title + "\n\n" + 
                            "Bemerkung: " + calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Comment,
                            FONT_NORMAL,
                            COLOR_SEMINAR,
                            4, 
                            1));                                                                                    // Seminar + Kommentar
                }
                else
                {
                    pdfTable.AddCell(
                        CreateTabeCell(
                            calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Title, 
                            FONT_NORMAL, 
                            COLOR_SEMINAR, 
                            4, 
                            1));                                                                                           // Seminar
                }
            }
        }

        /// <summary>
        /// Fügt einen Eintrag für einen Schultag zur angegebenen PdfTabelle hinzu
        /// </summary>
        /// <param name="pdfTable">Instanz der PdfTable in der die Zeile erzeugt werden soll</param>
        /// <param name="calendarDay">Aktueller Kalendartag</param>
        /// <param name="nextCalendarDay">Nächster Kalendartag</param>
        /// <param name="calendarEntryPosition">Position im CalendarEntry</param>
        private void CreateSchoolRow(PdfPTable pdfTable, MCalendarDay calendarDay, MCalendarDay nextCalendarDay, int calendarEntryPosition)
        {
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle Technik
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle RaumNr.   
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle Trainer

            if (nextDayIsSchool(calendarDay, nextCalendarDay, calendarEntryPosition))
            {
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_SCHOOL, 4, 1));                                 // Schule
            }
            else
            {
                if (showComments && !String.IsNullOrEmpty(calendarDay.CalendarEntry[calendarEntryPosition].School.Comment))
                {
                    pdfTable.AddCell(CreateTabeCell(
                        "Bemerkung: " + calendarDay.CalendarEntry[calendarEntryPosition].School.Comment, 
                        FONT_NORMAL, 
                        COLOR_SCHOOL,
                        4, 
                        1));                                                                                           // Schule + Kommentar
                }
                else
                {
                    pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_SCHOOL, 4, 1));                             // Schule
                }
            }
        }

        /// <summary>
        /// Fügt einen Eintrag für einen Praxistag zur angegebenen PdfTabelle hinzu
        /// </summary>
        /// <param name="pdfTable">Instanz der PdfTable in der die Zeile erzeugt werden soll</param>
        /// <param name="calendarDay">Aktueller Kalendartag</param>
        /// <param name="nextCalendarDay">Nächster Kalendartag</param>
        /// <param name="calendarEntryPosition">Position im CalendarEntry</param>
        private void CreatePraticeRow(PdfPTable pdfTable, MCalendarDay calendarDay, MCalendarDay nextCalendarDay, int calendarEntryPosition)
        {
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle Technik
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle RaumNr.   
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle Trainer

            if (nextDayIsPratice(calendarDay, nextCalendarDay, calendarEntryPosition))
            {

                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_PRATICE, 4, 1));                                // Praxis
            }
            else
            {
                if (showComments && !String.IsNullOrEmpty(calendarDay.CalendarEntry[calendarEntryPosition].Practice.Comment))
                {
                    pdfTable.AddCell(
                        CreateTabeCell(
                            "Bemerkung " + calendarDay.CalendarEntry[calendarEntryPosition].Practice.Comment,
                            FONT_NORMAL,
                            COLOR_PRATICE, 4, 1));                                                                     // Praxis + Kommentar
                }
                else
                {
                    pdfTable.AddCell(CreateTabeCell(
                        "",
                        FONT_NORMAL, 
                        COLOR_PRATICE, 
                        4, 
                        1));                                                                                           // Praxis
                }

            }
        }

        /// <summary>
        /// Fügt einen Eintrag für einen Seminar- und Praxistag zur angegebenen PdfTabelle hinzu
        /// </summary>
        /// <param name="pdfTable">Instanz der PdfTable in der die Zeile erzeugt werden soll</param>
        /// <param name="calendarDay">Aktueller Kalendartag</param>
        /// <param name="nextCalendarDay">Nächster Kalendartag</param>
        /// <param name="calendarEntryPosition">Position im CalendarEntry</param>
        private void CreateSeminarAndPraticeRow(PdfPTable pdfTable, MCalendarDay calendarDay, MCalendarDay nextCalendarDay, int calendarEntryPosition)
        {
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle Technik
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle RaumNr.   
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle Trainer

            if (nextDayIsPratice(calendarDay, nextCalendarDay, calendarEntryPosition))
            {

                pdfTable.AddCell(CreateTabeCell(calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Title, FONT_NORMAL, COLOR_SEMINAR, 2, 1));       // Seminar                                // Seminar
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_PRATICE, 2, 1));                                // Praxis
            }
            else
            {
                #region Kommentar für Seminar
                if (showComments && !String.IsNullOrEmpty(calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Comment))
                {
                    
                    pdfTable.AddCell(
                        CreateTabeCell(
                            calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Title + "\n\n" + 
                            "Bemerkung: " + calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Comment,
                            FONT_NORMAL,
                            COLOR_SEMINAR,
                            2,
                            1));                                                                                    // Seminar + Kommentar
                }
                else
                {
                    pdfTable.AddCell(CreateTabeCell(
                        calendarDay.CalendarEntry[calendarEntryPosition].Seminar.Title,
                        FONT_NORMAL, 
                        COLOR_SEMINAR,
                        2, 
                        1));                                                                                           // Seminar
                }
                #endregion

                #region Kommentar für Praxis
                if (showComments && !String.IsNullOrEmpty(calendarDay.CalendarEntry[calendarEntryPosition].Practice.Comment))
                {
                    pdfTable.AddCell(
                        CreateTabeCell(
                            "Bemerkung: " + calendarDay.CalendarEntry[calendarEntryPosition].Practice.Comment,
                            FONT_NORMAL,
                            COLOR_PRATICE,
                            2,
                            1));                                                                                    // Praxis + Kommentar
                }
                else
                {
                    pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_PRATICE, 2, 1));                            // Praxis
                }
                #endregion
            }
        }

        /// <summary>
        /// Fügt einen Eintrag für einen leeren Tag zur angegebenen PpdfTabelle hinzu
        /// </summary>
        /// <param name="pdfTable">PdfTabelle in der der Eintrag angezeigt werden soll</param>
        private void CreateBlankRow(PdfPTable pdfTable)
        {
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle Technik
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle RaumNr.   
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1, 1));                                      // Leere Zelle Trainer
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 4, 1));                                      // Leere Zellen Seminar / Schule / Praxis
        }

        /// <summary>
        /// Erzeugt eine Tabellenreihe zwei Zellen
        /// </summary>
        /// <param name="leftValue">Wert für die linke Zelle</param>
        /// <param name="rightValue">Wert für die rechte Zelle</param>
        /// <param name="font">Schriftformatierung für die beiden Zellen</param>
        /// <param name="spaceBefore">Abstand nach oben</param>
        private void CreateFooterTableRow(string leftValue, string rightValue, Font font, int spaceBefore)
        {
            PdfPTable pdfTable = new PdfPTable(2);
            pdfTable.WidthPercentage = 40;
            pdfTable.TotalWidth = 3f;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.SpacingBefore = spaceBefore;

            float[] widths = new float[] { 1f, 2f };
            pdfTable.SetWidths(widths);

            pdfTable.AddCell(CreateTabeCell(leftValue, font, COLOR_BLANK, 1, 1));
            pdfTable.AddCell(CreateTabeCell(rightValue, font, COLOR_BLANK, 1, 1));

            doc.Add(pdfTable);
        }

        /// <summary>
        /// Erzeugt eine Tabellenreihe für Wochenenden mit der aktuellen Kalenderwoche
        /// </summary>
        /// <param name="calenderWeek">Angabe der Kalendarwoche</param>
        private void CreateBodyTableRowWeekend(string calenderWeek)
        {
            int calendarWeekTmp = Convert.ToInt16(calenderWeek) + 1;

            PdfPCell pdfCell = new PdfPCell();
            PdfPTable pdfTable = new PdfPTable(calculateNumberOfCells());
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            pdfCell.Phrase = new Phrase("KW " + calendarWeekTmp.ToString(), FONT_SMALL_BOLD) { };
            pdfCell.Colspan = 3;
            pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable.AddCell(pdfCell);

            pdfCell.BackgroundColor = new BaseColor(204, 255, 255);
            pdfCell.Phrase = new Phrase("");
            pdfCell.Colspan = 29;
            pdfTable.AddCell(pdfCell);

            doc.Add(pdfTable);
        }

        /// <summary>
        /// Entscheidet anhand der Anzahl der Jahrgänge die größe des DIN-Formats
        /// </summary>
        /// <returns>Größe des DIN-Formats</returns>
        private Rectangle getFormatByNumberOfApprenticeships()
        {
            Rectangle format = null;

            switch (numberOfApprenticeships)
            {
                case 1:
                    format = iTextSharp.text.PageSize.A4.Rotate();
                    break;
                case 2:
                    format = iTextSharp.text.PageSize.A3.Rotate();
                    break;
                default:
                    break;
            }

            return format;
        }

        /// <summary>
        /// Berechnet die Anzahl der Zellen die zur Darstellung benötigt werden
        /// </summary>
        /// <returns>Anzahl der Zellen</returns>
        private int calculateNumberOfCells()
        {
            return 4 + (14 * numberOfApprenticeships);
        }

        /// <summary>
        /// Prüft, ob der aktuelle Tag ein Seminartag ist und ob auf diesen ein weiterer Seminartag mit dem selben
        /// Seminar folgt
        /// </summary>
        /// <param name="currentDay">Aktueller Tag</param>
        /// <param name="nextDay">Nächster Tag</param>
        /// <param name="calendarEntryPosition">Position in CalendarEntry</param>
        /// <returns>Gibt true zurück wenn kein Wechsel zwischen dem aktuellen und dem nächsten Tag erfolgt</returns>
        private bool nextDayIsSeminar(MCalendarDay currentDay, MCalendarDay nextDay, int calendarEntryPosition)
        {
            if (currentDay != null &&
                nextDay != null &&
                dayHasCalendarEntry(currentDay) &&
                dayHasCalendarEntry(nextDay) &&
                currentDay.CalendarEntry[calendarEntryPosition].Seminar != null)
            {
                if (currentDay.CalendarEntry[calendarEntryPosition].Seminar != nextDay.CalendarEntry[calendarEntryPosition].Seminar)
                {
                    return false;
                }
            }
            else if (nextDay != null && !dayHasCalendarEntry(nextDay))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Prüft, ob der aktuelle Tag ein Schultag ist und ob auf diesen ein weiterer Schultag mit dem selben
        /// Schul Objekt folgt
        /// </summary>
        /// <param name="currentDay">Aktueller Tag</param>
        /// <param name="nextDay">Nächster Tag</param>
        /// <param name="calendarEntryPosition">Position im CalendarEntry</param>
        /// <returns>Gibt true zurück wenn kein Wechsel zwischen dem aktuellen und dem nächsten Tag erfolgt</returns>
        private bool nextDayIsSchool(MCalendarDay currentDay, MCalendarDay nextDay, int calendarEntryPosition)
        {
            if (currentDay != null &&
                nextDay != null &&
                dayHasCalendarEntry(currentDay) &&
                dayHasCalendarEntry(nextDay) &&
                currentDay.CalendarEntry[calendarEntryPosition].School != null)
            {
                if (currentDay.CalendarEntry[calendarEntryPosition].School != nextDay.CalendarEntry[calendarEntryPosition].School)
                {
                    return false;
                }
            }
            else if (nextDay != null && !dayHasCalendarEntry(nextDay))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Prüft, ob der aktuelle Tag ein Praxistag ist und ob auf diesen ein weiterer Praxistag mit dem selben
        /// Praxis Objekt folgt
        /// </summary>
        /// <param name="currentDay">Aktueller Tag</param>
        /// <param name="nextDay">Nächster Tag</param>
        /// <param name="calendarEntryPosition">Position im CalendarEntry</param>
        /// <returns>Gibt true zurück wenn kein Wechsel zwischen dem aktuellen und dem nächsten Tag erfolgt</returns>
        private bool nextDayIsPratice(MCalendarDay currentDay, MCalendarDay nextDay, int calendarEntryPosition)
        {
            if (currentDay != null && 
                nextDay != null && 
                dayHasCalendarEntry(currentDay) && 
                dayHasCalendarEntry(nextDay) &&
                currentDay.CalendarEntry[calendarEntryPosition].Practice != null)
            {
                if (currentDay.CalendarEntry[calendarEntryPosition].Practice != nextDay.CalendarEntry[calendarEntryPosition].Practice)
                {
                    return false;
                }
            }
            else if (nextDay != null && !dayHasCalendarEntry(nextDay))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Prüft, ob der angegebene Tag eine Instanz von MCalendarEntry hat oder nicht
        /// </summary>
        /// <param name="currentDay">Zu prüfender Tag</param>
        /// <returns>Gibt true zurück, wenn der angegebene Tag eine Instanz von MCalendarEntry hat</returns>
        private bool dayHasCalendarEntry(MCalendarDay currentDay)
        {
            if (currentDay.CalendarEntry.Count != 0) {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Prüft, ob der angegebene Tag um einen Wochenendtag handelt
        /// </summary>
        /// <param name="currentDay">Zu prüfender Tag</param>
        /// <returns>Gibt ture zurück, wenn der angegebene ein Wochenendtag ist</returns>
        private bool dayIsWeekend(MCalendarDay currentDay)
        {
            if (currentDay.CalendarDate.DayOfWeek.ToString().Equals("Saturday")
                || currentDay.CalendarDate.DayOfWeek.ToString().Equals("Sunday"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gibt die Listenposition einer bestimmten Instanz von MSpeciality
        /// </summary>
        /// <param name="identifierOfYear">identifierOfYear Bezeichnung</param>
        /// <returns>Gesuchte Speciality Position</returns>
        private int GetSpecialityListPosition(string identifierOfYear)
        {
            MCalendar calendar = MCalendar.GetInstance();

            for (int i = 0; i < calendar.Speciality.Count; i++)
            {
                if (calendar.Speciality[i].IdentifierOfYear.Equals(identifierOfYear))
                {
                    return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// Gibt die Listenposition einer bestimmten Instanz von MSpeciality
        /// </summary>
        /// <param name="identifierOfYear">identifierOfYear der MSpeciality Instanz</param>
        /// <param name="specialityName">specialityName der MSpeciality Instanz</param>
        /// <returns>Gesuchte Speciality Position</returns>
        private int GetSpecialityListPosition(string identifierOfYear, string specialityName)
        {
            MCalendar calendar = MCalendar.GetInstance();

            for (int i = 0; i < calendar.Speciality.Count; i++)
            {
                if (calendar.Speciality[i].IdentifierOfYear.Equals(identifierOfYear) &&
                    calendar.Speciality[i].SpecialityName.Equals(specialityName))
                {
                    return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// Gibt die Anzahl der Elemente mit dem angegebenen identifierOfYear zurück
        /// </summary>
        /// <param name="identifierOfYear">identifierOfYear Bezeichnung</param>
        /// <returns>Anzahl der identifierOfYear mit der entsprechenden Bezeichnung</returns>
        private int CountSpecialityByIdentifierOfYear(string identifierOfYear)
        {
            MCalendar calendar = MCalendar.GetInstance();
            int specialityCounter = 0;

            foreach (MSpeciality speciality in calendar.Speciality)
            {
                if (speciality.IdentifierOfYear.Equals(identifierOfYear))
                {
                    specialityCounter++;
                }
            }

            return specialityCounter;
        }

        /// <summary>
        /// Gibt den SpecialityName einer MSpeciality Instanz mit der angegebenen Bezeichnung von
        /// identifierOfYear zurück
        /// </summary>
        /// <param name="identifierOfYear">identifierOfYear Bezeichnung</param>
        /// <returns>SpecialityName</returns>
        private string GetSpecialityNameByIdentifierOfYear(string identifierOfYear)
        {
            MCalendar calendar = MCalendar.GetInstance();
            foreach (MSpeciality speciality in calendar.Speciality)
            {
                if (speciality.IdentifierOfYear.Equals(identifierOfYear))
                {
                    return speciality.SpecialityName;
                }
            }

            return "";
        }
    }
}
