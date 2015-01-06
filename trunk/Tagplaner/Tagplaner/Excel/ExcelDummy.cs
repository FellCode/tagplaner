using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    class ExcelDummy
    {
        WorksheetGenerator wsg = new WorksheetGenerator();

        public void DoStuff()
        {
            string[,] days = new string[2,5];
            days[0, 0] = "MO";
            days[0, 1] = "01.01.2020";
            days[0, 2] = "KW 01";
            days[0, 3] = "BS";
            days[0, 4] = "";
            days[1, 0] = "DI";
            days[1, 1] = "02.01.2020";
            days[1, 2] = "KW 01";
            days[1, 3] = "SE";
            days[1, 4] = "C# Seminar";


            wsg.WriteFile(days);
        }
    }
}
