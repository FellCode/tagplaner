using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MRoom
    {
        private int id;
        private string number;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public String Number
        {
            get { return number; }
        }
        #endregion

        #region constructor

        public MRoom( string number)
        {
         
            this.number = number;
        }

        public MRoom(int id, string number)
        {
            this.id = id;
            this.number = number;
        }
        #endregion

        public override string ToString()
        {
            return number;
        }
    }
}
