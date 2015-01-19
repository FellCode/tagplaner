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
        private MCalendar calendar;
        Dictionary<string, string> dayDictionary = new Dictionary<string, string>();
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

        /// <summary>
        /// Erstellt eine neue Instanz von CPdfExporter
        /// </summary>
        /// <param name="calendar">Instanz vom MCalendar</param>
        /// <param name="trainerList">Instanz von List mit MTrainer Instanzen</param>
        public CPdfExporter(MCalendar calendar, List<MTrainer> trainerList)
        {
            this.calendar = calendar;
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
                iTextSharp.text.PageSize.A2,
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

            this.CreatePdfHeader();
            this.CreatePdfBody();
            this.CreatePdfFooter();

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
        /// Erzeugt den oberen Teil des PDF-Dokuments mit der Legende
        /// </summary>
        private void CreatePdfHeader()
        {
            PdfPTable headerTable = new PdfPTable(32);
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

            legendTable.AddCell(new PdfPCell(new Phrase("")) { Colspan = 32 });

            return legendTable;
        }

        /// <summary>
        /// Erzeugt den Tabellenkopf für die Tagplan-Tabelle
        /// </summary>
        /// <returns>Tabelle mit überschriften für den Tagplan</returns>
        public PdfPTable CreateTopRow()
        {
            PdfPTable topRowTable = new PdfPTable(32);
            topRowTable.WidthPercentage = 100;

            topRowTable.AddCell(new PdfPCell(new Phrase("Tag", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Datum", FONT_SMALL_BOLD)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });

            topRowTable.AddCell(new PdfPCell(new Phrase("Ferien", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });

            topRowTable.AddCell(new PdfPCell(new Phrase("Technik", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Ort", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Trainer", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Inhalt\nAnwendungsentwickler", FONT_SMALL_BOLD)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

            topRowTable.AddCell(new PdfPCell(new Phrase("Technik", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Ort", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Trainer", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Inhalt\nSystemintegratoren", FONT_SMALL_BOLD)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

            topRowTable.AddCell(new PdfPCell(new Phrase("Technik", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Ort", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Trainer", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Inhalt\nAnwendungsentwickler", FONT_SMALL_BOLD)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

            topRowTable.AddCell(new PdfPCell(new Phrase("Technik", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Ort", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Trainer", FONT_SMALL_BOLD)) { Colspan = 1, HorizontalAlignment = Element.ALIGN_CENTER });
            topRowTable.AddCell(new PdfPCell(new Phrase("Inhalt\nSystemintegratoren", FONT_SMALL_BOLD)) { Colspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });

            return topRowTable;
        }

        /// <summary>
        /// Erzeugt den Hauptteil des Tagplans
        /// </summary>
        private void CreatePdfBody()
        {
            for (int i = 0; i < calendar.CalendarList.Count; i++)
            {
                MCalendarDay calendarDay = calendar.CalendarList.ElementAt(i);

                switch (dayDictionary[calendarDay.CalendarDate.DayOfWeek.ToString()])
                {
                    case "Sa":
                        break;
                    case "So":
                        CreateBodyTableRowWeekend(calendarDay.CalendarWeek);
                        break;
                    default:
                        CreateBodyTableRow(calendarDay);
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

            foreach (MTrainer trainer in this.trainerList)
            {
                CreateFooterTableRow(trainer.Abbreviation, trainer.Surname + " " + trainer.Name, FONT_NORMAL, 0);
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

            pdfTable.AddCell(this.CreateFooterTableCell(leftValue, font));
            pdfTable.AddCell(this.CreateFooterTableCell(rightValue, font));

            doc.Add(pdfTable);
        }

        /// <summary>
        /// Erstellt eine neue Zelle für FooterTableRow
        /// </summary>
        /// <param name="value">Text für die erzeugte Zelle</param>
        /// <param name="font">Schriftformatierung</param>
        /// <returns></returns>
        private PdfPCell CreateFooterTableCell(string value, Font font)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.Phrase = new Phrase(value, font);

            return pdfcell;
        }

        /// <summary>
        /// Erstellt eine Tabellenreihe mit den eigentlichen Tagplan Informationen
        /// </summary>
        /// <param name="calendarDay">CalendatTag aus der Liste von MCalendar</param>
        private void CreateBodyTableRow(MCalendarDay calendarDay)
        {
            PdfPTable pdfTable = new PdfPTable(32);
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            // Meta informations
            pdfTable.AddCell(this.CreateBodyTableCell(dayDictionary[calendarDay.CalendarDate.DayOfWeek.ToString()], 1));
            pdfTable.AddCell(this.CreateBodyTableCell(calendarDay.CalendarDate.Date.ToShortDateString(), 2));
            
            
            // Ferien
            if (!String.IsNullOrEmpty(calendarDay.VacationName))
            {
                pdfTable.AddCell(this.CreateBodyTableCellHoliday("", 1));
            }
            else
            {
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
            }

            // Feiertag
            if (!String.IsNullOrEmpty(calendarDay.HolidayName))
            {
                pdfTable.AddCell(this.CreateBodyTableCellHoliday(calendarDay.HolidayName, 14));
                pdfTable.AddCell(this.CreateBodyTableCellHoliday(calendarDay.HolidayName, 14));
            }
            else
            {
                // Year two - FIAE
                MCalendarEntry calendarEntry = calendarDay.CalendarEntry.ElementAt(0);
               
                if (calendarEntry.Seminar != null)
                {
                    pdfTable.AddCell(this.CreateBodyTableCell(calendarEntry.Seminar.HasTechnology, 1));
                    pdfTable.AddCell(this.CreateBodyTableCell(calendarEntry.Room.Number, 1));
                    pdfTable.AddCell(this.CreateBodyTableCell(calendarEntry.Trainer.Abbreviation, 1));
                    pdfTable.AddCell(this.CreateBodyTableCellSeminar(calendarEntry.Seminar.Title, 4));
                }
                else if (calendarEntry.School != null)
                {
                    pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                    pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                    pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                    pdfTable.AddCell(this.CreateBodyTableCellSchool());
                }
                else if (calendarEntry.Practice != null)
                {
                    pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                    pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                    pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                    pdfTable.AddCell(this.CreateBodyTableCellPratice("", 4));
                }

                // Year two  - FISI
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCellPratice("", 4));

                // Year one  - FIAE
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCellSchool());

                // Year one  - FISI
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCell("", 1));
                pdfTable.AddCell(this.CreateBodyTableCellSchool());
            }



            // pdfTable.AddCell(this.CreateBodyTableCellHoliday("Alexander-ferien", 14));

            doc.Add(pdfTable);
        }

        /// <summary>
        /// Erstellt eine Zelle für einen Schultag
        /// </summary>
        /// <returns>Zelle mit Formatierung für Schultage</returns>
        private PdfPCell CreateBodyTableCellSchool()
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.BLUE;
            pdfcell.Phrase = new Phrase("");
            pdfcell.Colspan = 4;
            pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            return pdfcell;
        }

        /// <summary>
        /// Erstellt eine Zelle für einen Seminartag
        /// </summary>
        /// <param name="seminarName">Name des Seminars</param>
        /// <param name="colspan">Anzahl der Zellen die zusammengefasst werden sollen</param>
        /// <returns>Zelle mit Formatierung für Seminartage</returns>
        private PdfPCell CreateBodyTableCellSeminar(string seminarName, int colspan)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.CYAN;
            pdfcell.Phrase = new Phrase(seminarName, FONT_NORMAL);
            pdfcell.Colspan = colspan;
            pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            return pdfcell;
        }

        /// <summary>
        /// Erstellt eine Zelle für einen Praxistag
        /// </summary>
        /// <param name="comment">Kommentar für Praxistage</param>
        /// <param name="colspan">Anzahl der Zellen die zusammengefasst werden sollen</param>
        /// <returns>Zelle mit Fomatierung für Praxistage</returns>
        private PdfPCell CreateBodyTableCellPratice(string comment, int colspan)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            pdfcell.Phrase = new Phrase(comment);
            pdfcell.Colspan = colspan;
            pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            return pdfcell;
        }

        /// <summary>
        /// Erstellt eine Zelle für einen Ferien- / Feiertag
        /// </summary>
        /// <param name="holidayName">Name des Ferien- / Feiertags</param>
        /// <param name="colspan">Anzahl der Zellen die zusammengefasst werden sollen</param>
        /// <returns>Zelle mit Formatierung für Ferien- und Feiertage</returns>
        private PdfPCell CreateBodyTableCellHoliday(string holidayName, int colspan)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.GREEN;
            pdfcell.Phrase = new Phrase(holidayName, FONT_NORMAL);
            pdfcell.Colspan = colspan;
            pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            return pdfcell;
        }

        
        /// <summary>
        /// Erstellt eine Zelle für einen besonderen Tag wie zum Beispiel 
        /// IHK-Prüfungen
        /// </summary>
        /// <param name="name">Name für den besonderen Tag</param>
        /// <param name="colspan">Anzahl der Zellen die zusammengefasst werden sollen</param>
        /// <returns>Zelle mit Fomatierung für Praxistage</returns>
        private PdfPCell CreateBodyTableCellUniqueDay(string name, int colspan)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.RED;
            pdfcell.Phrase = new Phrase(name, FONT_UNIQUE);
            pdfcell.Colspan = colspan;
            pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            return pdfcell;
        }

        /// <summary>
        /// Erstellt eine Zelle
        /// </summary>
        /// <param name="value">Text der in der Zelle stehen soll</param>
        /// <param name="colspan">Anzahl der Zellen die zusammengefasst werden sollen</param>
        /// <returns>Unformatierte Zelle</returns>
        private PdfPCell CreateBodyTableCell(string value, int colspan)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.Phrase = new Phrase(value, FONT_NORMAL);
            pdfcell.Colspan = colspan;
            pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfcell.VerticalAlignment = Element.ALIGN_MIDDLE;

            return pdfcell;
        }

        /// <summary>
        /// Erzeugt eine Tabellenreihe für Wochenenden mit der aktuellen Kalenderwoche
        /// </summary>
        /// <param name="calenderWeek">Angabe der Kalendarwoche</param>
        private void CreateBodyTableRowWeekend(string calenderWeek)
        {
            int calendarWeekTmp = Convert.ToInt16(calenderWeek) + 1;

            PdfPCell pdfCell = new PdfPCell();
            PdfPTable pdfTable = new PdfPTable(32);
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
    }
}
