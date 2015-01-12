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
        private int number;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public int Number
        {
            get { return number; }
        }
        #endregion

        #region constructor

        public MRoom( int number)
        {
         
            this.number = number;
        }

        public MRoom(int id, int number)
        {
            this.id = id;
            this.number = number;
        }
        #endregion
    }
}
