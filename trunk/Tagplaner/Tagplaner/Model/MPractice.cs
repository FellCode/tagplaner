using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MPractice
    {
        private string comment;

        #region getter
        public string Comment
        {
            get { return comment; }
        }
        #endregion

        #region constructor
        public MPractice(string comment)
        {
            this.comment = comment;
        }
        #endregion
    }
}
