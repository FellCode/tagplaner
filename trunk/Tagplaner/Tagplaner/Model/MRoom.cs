using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MRoom
    {
        private int number;

        #region getter
        public int Number
        {
            get { return number; }
        }
        #endregion

        #region constructor
        public MRoom(int number)
        {
            this.number = number;
        }
        #endregion
    }
}
