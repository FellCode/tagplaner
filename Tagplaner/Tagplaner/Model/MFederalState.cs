using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MFederalState
    {   
        private int id;
        private string name;
        private string abbreviation;

        #region getter_setter
        public int Id 
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public string Abbreviation
        {
            get { return abbreviation; }
            set { this.abbreviation = value; }
        }
        #endregion

        public MFederalState(int id, string name, string abbreviation)
        {
            this.id = id;
            this.name = name;
            this.abbreviation = abbreviation;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
