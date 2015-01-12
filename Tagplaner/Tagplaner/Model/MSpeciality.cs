using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MSpeciality
    {
        private int id;
        private string specialityName;
        private string year;
        private string region;

        #region getter

        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public string SpecialityName
        {
            get { return specialityName; }
        }
        public string Year
        {
            get { return year; }
        }
        public string Region
        {
            get { return region; }
        }
        #endregion

        #region constructor
        public MSpeciality( string specialityName, string year, string region)
        {
            
            this.specialityName = specialityName;
            this.year = year;
            this.region = region;
        }
        public MSpeciality(int id, string specialityName, string year, string region)
        {
            this.id = id;
            this.specialityName = specialityName;
            this.year = year;
            this.region = region;
        }
        #endregion
    }
}
