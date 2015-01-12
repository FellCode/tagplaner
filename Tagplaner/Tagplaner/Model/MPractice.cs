using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MPractice
    {
        private int id;
        private string comment;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public string Comment
        {
            get { return comment; }
        }
        #endregion

        #region constructor
        public MPractice( string comment)
        {
            this.comment = comment;
        }
        public MPractice(int id, string comment)
        {
            this.id = id;
            this.comment = comment;
        }
        #endregion
    }
}
