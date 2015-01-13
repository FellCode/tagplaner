using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MSpeciality
    {
        [Serializable()]
        private int id;
        private string apprenticeship;
        private string specialityName;

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
        #endregion

        #region constructor
        public MSpeciality(string specialityName, string apprenticeship)
        {
            
            this.specialityName = specialityName;
            this.apprenticeship = apprenticeship;
        }
        public MSpeciality(int id, string specialityName, string apprenticeship)
        {
            this.id = id;
            this.specialityName = specialityName;
            this.apprenticeship = apprenticeship;
        }
        #endregion
    }
}
