using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    interface IPdfExporter
    {
        // erster Parameter muss noch definiert werden
        bool exportPdf(Object[] objects, string filename);
    }
}
