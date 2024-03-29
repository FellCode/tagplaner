﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Klasse mit dem Model zu einem Schultag
    /// </summary>
    [Serializable()]
    public class MSchool
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
            set { this.Comment = value; }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Konstruktor mit Bermerkung
        /// </summary>
        /// <param name="comment">Bemerkung zu einem Schultag</param>
        public MSchool( string comment)
        {
            
            this.comment = comment;
        }
        /// <summary>
        /// Konstruktor mit Id und Bermerkung
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="comment">Bemerkung zu einem Schultag</param>
        public MSchool(int id, string comment)
        {
            this.id = id;
            this.comment = comment;
        }
        #endregion
    }
}
