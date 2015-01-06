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

    // Bei Fehlern -> http://www.microsoft.com/en-us/download/details.aspx?id=3508 
    class WorksheetGenerator : IWorksheetGenerator
    {
        static void dostuff()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL konnte nicht gestartet werden :(");
                return;
            }
            xlApp.Visible = true;

            Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            if (ws == null)
            {
                Console.WriteLine("Worksheet konnte nicht erstellt werden :'(");
            }


            Range aRange = ws.get_Range("C3", "C7");

            if (aRange == null)
            {
                Console.WriteLine("Zellen konnten nicht markiert werden");
            }


            aRange.Merge(Missing.Value);
            Object[] args = new Object[1];

            args[0] = 6;
            aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            aRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.RosyBrown);
            aRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            aRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PeachPuff);
            aRange.Merge(Missing.Value);

            aRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            aRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            // Change the cells in the C1 to C7 range of the worksheet to the number 8.
            aRange.Value2 = "C# SEMINAR";
        }
        public bool WriteFile(object[][] days)
        {
            throw new NotImplementedException();
        }

    }
}
