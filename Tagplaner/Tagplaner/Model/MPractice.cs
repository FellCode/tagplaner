using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{   
    /// <summary>
    /// Klasse mit dem Model zu einem Praxistag
    /// </summary>
    [Serializable()]
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
            set { this.comment = value; }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Konstruktor mit Bemerkung
        /// </summary>
        /// <param name="comment">Bemerkung zu einem Praxistag</param>
        public MPractice( string comment)
        {
            this.comment = comment;
        }
        /// <summary>
        /// Konstruktor mit Id und Bemerkung
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="comment">Bemerkung zu einem Praxistag</param>
        public MPractice(int id, string comment)
        {
            this.id = id;
            this.comment = comment;
        }
        #endregion
    }
}
