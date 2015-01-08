using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    public class MSchool
    {
        private string comment;

        #region getter
        public string Comment
        {
            get { return comment; }
        }
        #endregion

        #region constructor
        public MSchool(string comment)
        {
            this.comment = comment;
        }
        #endregion
    }
}
