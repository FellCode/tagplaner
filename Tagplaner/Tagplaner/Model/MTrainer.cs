using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    class MTrainer
    {
        private string name;
        private string surname;
        private string abbreviation;
        private bool isInternal;
        private bool isCotrainer;

        #region getter
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
        public MTrainer(string name, string surname, string abbreviation, bool isInternal, bool isCotrainer)
        {
            this.name = name;
            this.surname = surname;
            this.abbreviation = abbreviation;
            this.isInternal = isInternal;
            this.isCotrainer = isCotrainer;
        }
        #endregion

    }
}
