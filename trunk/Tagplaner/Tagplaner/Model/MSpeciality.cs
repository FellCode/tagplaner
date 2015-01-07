using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MSpeciality
    {
        private String specialityName {get ; set;}
        private String year { get; set; }
        private String region { get; set; }

        public MSpeciality(String specialityName, String year, String region)
        {
            this.specialityName = specialityName;
            this.year = year;
            this.region = region;
        }
    }
}
