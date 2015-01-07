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
        List<MHoliday> GetHoliday(String region);
    }
}
