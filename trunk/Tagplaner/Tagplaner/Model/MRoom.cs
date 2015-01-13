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
        private int place_id;

        #region getter
        public int Id
        {
            get { return id; }
        }
        public String Number
        {
            get { return number; }
        }
        public int Place_id
        {
            get { return place_id; }
        }
        #endregion

        #region constructor

        public MRoom( string number)
        {
         
            this.number = number;
        }

        public MRoom(int id, string number, int place_id)
        {
            this.id = id;
            this.number = number;
            this.place_id = place_id;
        }
        #endregion

        public override string ToString()
        {
            return number;
        }
    }
}
