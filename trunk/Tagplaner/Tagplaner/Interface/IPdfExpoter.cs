﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public interface IPdfExporter
    {
        bool ExportPdf(string filename);
    }
}
