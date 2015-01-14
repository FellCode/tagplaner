using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    [Serializable()]
    public class MTrainer
    {
        private int id;
        private string name;
        private string surname;
        private string abbreviation;
        private bool isInternal;
        private bool isCotrainer;

        #region getter

        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return name; }
        }
        public string Surname
        {
            get { return surname; }
        }
        public string Abbreviation
        {
            get { return abbreviation; }
        }
        public bool IsInternal
        {
            get { return isInternal; }
        }
        public bool IsCotrainer
        {
            get { return isCotrainer; }
        }
        #endregion

        #region constructor
        public MTrainer( string name, string surname, string abbreviation, bool isInternal, bool isCotrainer)
        {
            this.name = name;
            this.surname = surname;
            this.abbreviation = abbreviation;
            this.isInternal = isInternal;
            this.isCotrainer = isCotrainer;
        }

        public MTrainer(int id, string name, string surname, string abbreviation, bool isInternal)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.abbreviation = abbreviation;
            this.isInternal = isInternal;
            
        }

        public MTrainer(string name, string surname, string abbreviation, bool isInternal)
        {
         
            this.name = name;
            this.surname = surname;
            this.abbreviation = abbreviation;
            this.isInternal = isInternal;

        }
        #endregion

        public override string ToString()
        {
            return name + " " + surname;
        }

    }
}
