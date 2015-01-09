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
    class CPdfExporter : IPdfExporter
    {
        private Document doc;
        private List<MTrainer> trainerList;
        String ort = "Koeln/Bonn";
        String jahrgang = "2014/1";
        private string[] dayArray = { "Mo", "Di", "Mi", "Do", "Fr" };

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

        readonly Font FONT_UNIQUE = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);

        public bool exportPdf(string filename)
        {
            float margin = Utilities.MillimetersToPoints(Convert.ToSingle(20));
            doc = new Document(
                iTextSharp.text.PageSize.A3,
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

            legendTable.AddCell(new PdfPCell(new Phrase("")) { Colspan = 4, Rowspan = 4 });
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

        public PdfPTable CreateTopRow()
        {
            PdfPTable topRowTable = new PdfPTable(32);
            topRowTable.WidthPercentage = 100;

            topRowTable.AddCell(new PdfPCell(new Phrase("Tag", FONT_SMALL_BOLD)) { Colspan = 1 });
            topRowTable.AddCell(new PdfPCell(new Phrase("Datum", FONT_SMALL_BOLD)) { Colspan = 2 });

            topRowTable.AddCell(new PdfPCell(new Phrase("")) { Colspan = 1 });

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
        /// 
        /// </summary>
        private void CreatePdfBody()
        {
            int dayCount = 0;
            for (int i = 0; i < 302; i++)
            {
                if (i % 6 == 0)
                {
                    CreateBodyTableRowWeekend(((i + 1) / 6) + 1);

                }
                else
                {
                    CreateBodyTableRow(dayCount);
                    dayCount++;
                }

                if (dayCount == 5) { dayCount = 0; }
            }

        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        private void CreateTableRow()
        {
        }

        private PdfPCell CreateCellSchool()
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.BLUE;
            pdfcell.Phrase = new Phrase(
                "",
                new Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD)
            );
            pdfcell.Colspan = 5;

            return pdfcell;
        }

        private PdfPCell CreateCellSeminar(string seminarName, int colspan = 4)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            pdfcell.Phrase = new Phrase(
                seminarName,
                new Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD)
            );

            return pdfcell;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftValue"></param>
        /// <param name="rightValue"></param>
        /// <param name="font"></param>
        /// <param name="spaceBefore"></param>
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
        /// create an single table cell
        /// </summary>
        /// <param name="value"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private PdfPCell CreateFooterTableCell(string value, Font font)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.Phrase = new Phrase(value, font);

            return pdfcell;
        }

        /// <summary>
        /// create a table row with seventeen cells
        /// </summary>
        private void CreateBodyTableRow(int dayNumber)
        {
            PdfPTable pdfTable = new PdfPTable(32);
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            // Meta informations
            pdfTable.AddCell(this.CreateBodyTableCell(dayArray[dayNumber], 1));
            pdfTable.AddCell(this.CreateBodyTableCell("27.01.14", 2));
            pdfTable.AddCell(this.CreateBodyTableCellHoliday());

            // Year two
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCell("210"));
            pdfTable.AddCell(this.CreateBodyTableCellSeminar("C# .net", 2));
            pdfTable.AddCell(this.CreateBodyTableCellPratice("", 2));
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCellPratice());

            // Year one
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCellSchool());
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCell(""));
            pdfTable.AddCell(this.CreateBodyTableCellSchool());

            // pdfTable.AddCell(this.CreateBodyTableCellHoliday("Alexander-ferien", 14));

            doc.Add(pdfTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PdfPCell CreateBodyTableCellSchool()
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.BLUE;
            pdfcell.Phrase = new Phrase("");
            pdfcell.Colspan = 4;

            return pdfcell;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seminarName"></param>
        /// <returns></returns>
        private PdfPCell CreateBodyTableCellSeminar(string seminarName, int colspan = 4)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.CYAN;
            pdfcell.Phrase = new Phrase(seminarName, FONT_NORMAL);
            pdfcell.Colspan = colspan;

            return pdfcell;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PdfPCell CreateBodyTableCellPratice(string comment = "", int colspan = 4)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.YELLOW;
            pdfcell.Phrase = new Phrase(comment);
            pdfcell.Colspan = colspan;

            return pdfcell;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PdfPCell CreateBodyTableCellHoliday(string holidayName = "", int colspan = 1)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.GREEN;
            pdfcell.Phrase = new Phrase(holidayName, FONT_NORMAL);
            pdfcell.Colspan = colspan;
            pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;

            return pdfcell;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="colspan"></param>
        /// <returns></returns>
        private PdfPCell CreateBodyTableCellUniqueDay(string name, int colspan = 1)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.BackgroundColor = BaseColor.RED;
            pdfcell.Phrase = new Phrase(name, FONT_UNIQUE);
            pdfcell.Colspan = colspan;
            pdfcell.HorizontalAlignment = Element.ALIGN_CENTER;

            return pdfcell;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="colspan"></param>
        /// <returns></returns>
        private PdfPCell CreateBodyTableCell(string value = "", int colspan = 1)
        {
            PdfPCell pdfcell = new PdfPCell();
            pdfcell.Phrase = new Phrase(value, FONT_NORMAL);
            pdfcell.Colspan = colspan;

            return pdfcell;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calenderWeek"></param>
        private void CreateBodyTableRowWeekend(int calenderWeek)
        {
            PdfPCell pdfCell = new PdfPCell();
            PdfPTable pdfTable = new PdfPTable(32);
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            pdfCell.Phrase = new Phrase("KW " + calenderWeek, FONT_SMALL_BOLD) { };
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
