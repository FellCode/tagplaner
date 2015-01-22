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
        private MCalendar calendar;
        private Dictionary<string, string> dayDictionary = new Dictionary<string, string>();
        private int numberOfApprenticeships;
        private bool showComments;

        String ort = "Koeln/Bonn";
        String jahrgang = "2014/1";

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
        /// <param name="calendar">Instanz vom MCalendar</param>
        /// <param name="numberOfApprenticeships">Anzahl der Ausbildungsjahrgänge</param>
        /// <param name="showComments">Information, ob Kommentare angezeigt werden soll</param>
        /// <param name="trainerList">Instanz von Liste mit MTrainer Instanzen</param>
        public CPdfExporter(MCalendar calendar, int numberOfApprenticeships, bool showComments, List<MTrainer> trainerList)
        {
            this.calendar = calendar;
            this.numberOfApprenticeships = numberOfApprenticeships;
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
            return true;
            /*
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
            */
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
        /// <returns></returns>
        private PdfPCell CreateTabeCell(string text, Font font, BaseColor backgroundColor, int colSpan)
        {
            PdfPCell pdfCell = new PdfPCell();
            pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.Phrase = new Phrase(text, font);
            pdfCell.BackgroundColor = backgroundColor;
            pdfCell.Colspan = colSpan;

            return pdfCell;
        }

        /// <summary>
        /// Erzeugt den oberen Teil des PDF-Dokuments mit der Legende
        /// </summary>
        private void CreatePdfHeader()
        {
            PdfPTable headerTable = new PdfPTable(calculateNumberOfCells());
            headerTable.WidthPercentage = 100;

            PdfPCell pdfHeaderCellLeft = new PdfPCell(new Phrase("Ausbildung zum Fachinformatiker am Standort " + ort + " Jahrgang " + jahrgang + ". Jahr\nLegende:\n", FONT_BOLD)) { Colspan = 14 };
            PdfPCell pdfHeaderCellRight = new PdfPCell(new Phrase("Ausbildung zum Fachinformatiker am Standort " + ort + " Jahrgang " + jahrgang + ". Jahr\nLegende:\n", FONT_BOLD)) { Colspan = 14 };

            headerTable.AddCell(new PdfPCell(new Phrase("")) { Colspan = 4 });

            headerTable.AddCell(pdfHeaderCellLeft);
            headerTable.AddCell(pdfHeaderCellRight);

            doc.Add(headerTable);
            doc.Add(CreateLegend());
            doc.Add(CreateTopRow());
        }

        /// <summary>
        /// Erzeugt den Hauptteil des Tagplans
        /// </summary>
        private void CreatePdfBody()
        {
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
        ///  Erzeugt die Legende für die verschiedenen Arten von Tagplan 
        /// </summary>
        /// <returns>Tabelle für die Legende</returns>
        private PdfPTable CreateLegend()
        {
            PdfPTable legendTable = new PdfPTable(32);
            legendTable.WidthPercentage = 100;
            legendTable.SpacingAfter = 20;

            PdfPCell pdfLegendSchoolCell = new PdfPCell();
            PdfPCell pdfLegendPracticalCell = new PdfPCell();
            PdfPCell pdfLegendAcademyCell = new PdfPCell();
            PdfPCell pdfLegendHolidayCell = new PdfPCell();

            pdfLegendAcademyCell.BackgroundColor = BaseColor.YELLOW;
            pdfLegendSchoolCell.BackgroundColor = BaseColor.BLUE;
            pdfLegendPracticalCell.BackgroundColor = BaseColor.CYAN;
            pdfLegendHolidayCell.BackgroundColor = BaseColor.GREEN;

            legendTable.AddCell(new PdfPCell(new Phrase("Legende", FONT_SMALL_BOLD))
            {
                Colspan = 4,
                Rowspan = 4,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            });
            legendTable.AddCell(pdfLegendSchoolCell);
            legendTable.AddCell(new PdfPCell(new Phrase("Phase in Berufsschule", FONT_NORMAL)) { Colspan = 13 });
            legendTable.AddCell(pdfLegendSchoolCell);
            legendTable.AddCell(new PdfPCell(new Phrase("Phase in Berufsschule", FONT_NORMAL)) { Colspan = 13 });

            legendTable.AddCell(pdfLegendPracticalCell);
            legendTable.AddCell(new PdfPCell(new Phrase("Praxisphase im Betrieb", FONT_NORMAL)) { Colspan = 13 });
            legendTable.AddCell(pdfLegendPracticalCell);
            legendTable.AddCell(new PdfPCell(new Phrase("Praxisphase im Betrieb", FONT_NORMAL)) { Colspan = 13 });

            legendTable.AddCell(pdfLegendAcademyCell);
            legendTable.AddCell(new PdfPCell(new Phrase("Phase CSC Akademie", FONT_NORMAL)) { Colspan = 13 });
            legendTable.AddCell(pdfLegendAcademyCell);
            legendTable.AddCell(new PdfPCell(new Phrase("Phase CSC Akademie", FONT_NORMAL)) { Colspan = 13 });

            legendTable.AddCell(pdfLegendHolidayCell);
            legendTable.AddCell(new PdfPCell(new Phrase("Ferien/Feiertage", FONT_NORMAL)) { Colspan = 13 });
            legendTable.AddCell(pdfLegendHolidayCell);
            legendTable.AddCell(new PdfPCell(new Phrase("Ferien/Feiertage", FONT_NORMAL)) { Colspan = 13 });

            legendTable.AddCell(new PdfPCell(new Phrase("")) { Colspan = calculateNumberOfCells() });

            return legendTable;
        }

        /// <summary>
        /// Erzeugt den Tabellenkopf für die Tagplan-Tabelle
        /// </summary>
        /// <returns>Tabelle mit überschriften für den Tagplan</returns>
        public PdfPTable CreateTopRow()
        {
            PdfPTable topRowTable = new PdfPTable(calculateNumberOfCells());
            topRowTable.WidthPercentage = 100;

            topRowTable.AddCell(new PdfPCell(new Phrase("Tag", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Datum", FONT_SMALL_BOLD)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });

            topRowTable.AddCell(new PdfPCell(new Phrase("Ferien", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });

            for (int i = 0; i < numberOfApprenticeships; i++)
            {
                topRowTable.AddCell(new PdfPCell(new Phrase("Technik", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                topRowTable.AddCell(new PdfPCell(new Phrase("Ort", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                topRowTable.AddCell(new PdfPCell(new Phrase("Trainer", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                topRowTable.AddCell(new PdfPCell(new Phrase("Inhalt\nAnwendungsentwickler", FONT_SMALL_BOLD)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

                topRowTable.AddCell(new PdfPCell(new Phrase("Technik", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                topRowTable.AddCell(new PdfPCell(new Phrase("Ort", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                topRowTable.AddCell(new PdfPCell(new Phrase("Trainer", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
                topRowTable.AddCell(new PdfPCell(new Phrase("Inhalt\nSystemintegratoren", FONT_SMALL_BOLD)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
            }

            return topRowTable;
        }

        /// <summary>
        /// Erstellt eine Tabellenreihe mit den eigentlichen Tagplan Informationen
        /// </summary>
        /// <param name="calendarDay">Nächster Kalendertag aus der Liste von MCalendar<</param>
        /// <param name="position">Aktuelle Position in CalendarList</param>
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
                    1));                                                                        // Wochentag
            pdfTable.AddCell(
                CreateTabeCell(
                    calendarDay.CalendarDate.Date.ToShortDateString(),
                    FONT_NORMAL,
                    COLOR_BLANK,
                    2));                                                                        // Datum                  

            // Prüfen ob aktueller Tag ein Ferientag ist
            #region Ferien
            if (!String.IsNullOrEmpty(calendarDay.VacationName))
            {
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_HOLIDAY, 1));            // Ferien
            }
            else
            {
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1));              // Leere Zelle
            }
            #endregion

            // Prüfen, ob der aktuelle Tag ein Feiertag ist
            if (!String.IsNullOrEmpty(calendarDay.HolidayName))
            {
                #region Feiertag
                for (int i = 0; i < numberOfApprenticeships; i++)
                {
                    pdfTable.AddCell(CreateTabeCell(calendarDay.HolidayName,
                        FONT_NORMAL, COLOR_HOLIDAY, calculateNumberOfCells() / numberOfApprenticeships));
                }
                #endregion
            }
            // Regulären Tagplan erzeugen
            else
            {
                // Year two - FIAE
                MCalendarEntry calendarEntry = calendarDay.CalendarEntry[0];

                // Durchlauf pro Anzahl der Ausbildungsjahrgänge
                for (int i = 0; i < numberOfApprenticeships; i++)
                {
                    // Spalte für Anzahl der Fachrichtungen (2) pro Anzahl der Ausbildungsjahrgänge erstellen
                    for (int j = 0; j < 2 * numberOfApprenticeships; j++)
                    {
                        // Prüfen, ob der aktuelle Tag ein Seminartag ist
                        if (calendarEntry.Seminar != null)
                        {
                            CreateSeminarRow(pdfTable, calendarDay, nextCalendarDay, calendarEntry);
                        }
                        // Prüfen, ob der aktuelle Tag ein Schultag ist
                        else if (calendarEntry.School != null)
                        {
                            CreateSchoolRow(pdfTable, calendarDay, nextCalendarDay, calendarEntry);
                        }
                        // Prüfen, ob der aktuelle Tag ein Praxistag ist
                        else if (calendarEntry.Practice != null)
                        {
                            CreatePraticeRow(pdfTable, calendarDay, nextCalendarDay, calendarEntry);
                        }
                    }
                }
            }

            doc.Add(pdfTable);
        }

        /// <summary>
        /// Fügt einen Eintrag für ein Seminartag zur angegebenen PdfTabelle hinzu
        /// </summary>
        /// <param name="pdfTable">PdfTabelle in der der Eintrag angezeigt werden soll</param>
        /// <param name="calendarDay">Aktueller Kalendertag</param>
        /// <param name="nextCalendarDay">Nächster Kalendertag</param>
        /// <param name="calendarEntry">Kalendereintrag mit Informationen über den Seminartag</param>
        private void CreateSeminarRow(PdfPTable pdfTable, MCalendarDay calendarDay, MCalendarDay nextCalendarDay, MCalendarEntry calendarEntry)
        {
            pdfTable.AddCell(CreateTabeCell(calendarEntry.Seminar.HasTechnology, FONT_NORMAL, COLOR_BLANK, 1));     // Technik
            pdfTable.AddCell(CreateTabeCell(calendarEntry.Room.Number, FONT_NORMAL, COLOR_BLANK, 1));               // RaumNr.    
            pdfTable.AddCell(CreateTabeCell(calendarEntry.Trainer.Abbreviation, FONT_NORMAL, COLOR_BLANK, 1));      // Trainer

            if (nextDayIsSeminar(calendarDay, nextCalendarDay))
            {
                pdfTable.AddCell(CreateTabeCell(calendarEntry.Seminar.Title, FONT_NORMAL, COLOR_SEMINAR, 4));       // Seminar
            }
            else
            {
                if (showComments)
                {
                    pdfTable.AddCell(
                        CreateTabeCell(calendarEntry.Seminar.Title + "\n" + calendarEntry.Seminar.Comment,
                            FONT_NORMAL,
                            COLOR_SEMINAR,
                            4));                                                                                    // Seminar + Kommentar
                }
                else
                {
                    pdfTable.AddCell(CreateTabeCell(calendarEntry.Seminar.Title, FONT_NORMAL, COLOR_SEMINAR, 4));   // Seminar
                }
            }
        }

        /// <summary>
        /// Fügt einen Eintrag für einen Schultag zur angegebenen PdfTabelle hinzu
        /// </summary>
        /// <param name="pdfTable">PdfTabelle in der der Eintrag angezeigt werden soll</param>
        /// <param name="calendarDay">Aktueller Kalendertag</param>
        /// <param name="nextCalendarDay">Nächster Kalendertag</param>
        /// <param name="calendarEntry">Kalendereintrag mit Informationen über den Schultag</param>
        private void CreateSchoolRow(PdfPTable pdfTable, MCalendarDay calendarDay, MCalendarDay nextCalendarDay, MCalendarEntry calendarEntry)
        {
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1));                                      // Leere Zelle Technik
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1));                                      // Leere Zelle RaumNr.   
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1));                                      // Leere Zelle Trainer

            if (nextDayIsSchool(calendarDay, nextCalendarDay))
            {
                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_SCHOOL, 4));                                 // Schule
            }
            else
            {
                if (showComments)
                {
                    pdfTable.AddCell(CreateTabeCell(calendarEntry.School.Comment, FONT_NORMAL, COLOR_SCHOOL, 4));   // Schule + Kommentar
                }
                else
                {
                    pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_SCHOOL, 4));                             // Schule
                }
            }
        }

        /// <summary>
        /// Fügt einen Eintrag für einen Praxistag zur angegebenen PdfTabelle hinzu
        /// </summary>
        /// <param name="pdfTable">PdfTabelle in der der Eintrag angezeigt werden soll</param>
        /// <param name="calendarDay">Aktueller Kalendertag</param>
        /// <param name="nextCalendarDay">Nächster Kalendertag</param>
        /// <param name="calendarEntry">Kalendereintrag mit Informationen über den Praxistag</param>
        private void CreatePraticeRow(PdfPTable pdfTable, MCalendarDay calendarDay, MCalendarDay nextCalendarDay, MCalendarEntry calendarEntry)
        {
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1));                                      // Leere Zelle Technik
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1));                                      // Leere Zelle RaumNr.   
            pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_BLANK, 1));                                      // Leere Zelle Trainer

            if (nextDayIsPratice(calendarDay, nextCalendarDay))
            {

                pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_PRATICE, 4));                                // Praxis
            }
            else
            {
                if (showComments)
                {
                    pdfTable.AddCell(
                        CreateTabeCell(calendarEntry.Practice.Comment,
                            FONT_NORMAL,
                            COLOR_PRATICE, 4));                                                                     // Praxis + Kommentar
                }
                else
                {
                    pdfTable.AddCell(CreateTabeCell("", FONT_NORMAL, COLOR_PRATICE, 4));                            // Praxis
                }

            }
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

            pdfTable.AddCell(CreateTabeCell(leftValue, font, COLOR_BLANK, 1));
            pdfTable.AddCell(CreateTabeCell(rightValue, font, COLOR_BLANK, 1));

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
        /// <returns>Gibt true zurück wenn kein Wechsel zwischen dem aktuellen und dem nächsten Tag erfolgt</returns>
        private bool nextDayIsSeminar(MCalendarDay currentDay, MCalendarDay nextDay) 
        {
            if (currentDay != null &&
                nextDay != null &&
                dayHasCalendarEntry(currentDay) &&
                dayHasCalendarEntry(nextDay) &&
                currentDay.CalendarEntry[0].Seminar != null)
            {
                if (currentDay.CalendarEntry[0].Seminar != nextDay.CalendarEntry[0].Seminar)
                {
                    return false;
                }
            }

            if (nextDay == null) {
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
        /// <returns>Gibt true zurück wenn kein Wechsel zwischen dem aktuellen und dem nächsten Tag erfolgt</returns>
        private bool nextDayIsSchool(MCalendarDay currentDay, MCalendarDay nextDay)
        {
            if (currentDay != null &&
                nextDay != null &&
                dayHasCalendarEntry(currentDay) &&
                dayHasCalendarEntry(nextDay) &&
                currentDay.CalendarEntry[0].School != null)
            {
                if (currentDay.CalendarEntry[0].School != nextDay.CalendarEntry[0].School)
                {
                    return false;
                }
            }

            if (nextDay == null)
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
        /// <returns>Gibt true zurück wenn kein Wechsel zwischen dem aktuellen und dem nächsten Tag erfolgt</returns>
        private bool nextDayIsPratice(MCalendarDay currentDay, MCalendarDay nextDay)
        {
            if (currentDay != null && 
                nextDay != null && 
                dayHasCalendarEntry(currentDay) && 
                dayHasCalendarEntry(nextDay) && 
                currentDay.CalendarEntry[0].Practice != null)
            {
                if (currentDay.CalendarEntry[0].Practice != nextDay.CalendarEntry[0].Practice)
                {
                    return false;
                }
            }

            if (nextDay == null)
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

        private void test()
        {
            foreach (MSpeciality speciality in MCalendar.GetInstance().Speciality)
            {
                foreach (string identifierOfYear in identifierOfYearList)
                {
                    if (speciality.IdentifierOfYear.Equals(identifierOfYear))
                    {
                        DebugUserControl.GetInstance().AddDebugMessage(speciality.IdentifierOfYear);
                    }
                }       
            }
        }
    }
}
