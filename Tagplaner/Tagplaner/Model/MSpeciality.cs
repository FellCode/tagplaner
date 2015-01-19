using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    public class MSpeciality
    {
        private int id;
        private string apprenticeship;
        private string specialityName;
        private string identifierOfYear;

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

        public string IdentifierOfYear
        {
            get { return identifierOfYear; }
        }

        public string Apprenticeship
        {
            get { return apprenticeship; }
        }
        #endregion

        #region constructor
        public MSpeciality(string specialityName, string identifierOfYear, string apprenticeship)
        {
            this.identifierOfYear = identifierOfYear;
            this.specialityName = specialityName;
            this.apprenticeship = apprenticeship;
        }
        public MSpeciality(int id, string specialityName, string identifierOfYear, string apprenticeship)
        {
            this.id = id;
            this.specialityName = specialityName;
            this.apprenticeship = apprenticeship;
        }

        #endregion
    }
}
