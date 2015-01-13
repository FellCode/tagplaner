using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    interface IHoliday    
    {
        List<MHoliday> GetHoliday(String pfad1 ,String pfad2, DateTime startDate, DateTime endDate);
    }
}
