﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{

    //Author: Stefan, Arnold
    public interface IWorksheetGenerator
    {

        //days muss Kalendertag sowie Seminar/Urlaub/Schultag und Bemerkung/Raum usw. enthalten
        bool WriteFile(MCalendar calendar);


    }
}
