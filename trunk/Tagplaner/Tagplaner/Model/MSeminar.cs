using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Klasse mit dem Model für die Attribute eines Seminars
    /// </summary>
    [Serializable()]
    public class MSeminar
    {
        private int id;
        private string title;
        private string subtitle;
        private string abbreviation;
        private string hasTechnology;
        private string comment;

        /// <summary>
        /// Überschreibt die ToString Methode zur Darstellung des Titels eines Seminars in 
        /// den ComboBoxen
        /// </summary>
        /// <returns>Titel des Seminars</returns>
        public override String ToString()
        {
            return title;
        }

        #region getter

        public int Id
        {
            get { return id;  }
            set { this.id = value;  }
        }

        public string Title
        {
            get { return title; }
            set { this.title = value; }
        }
        public string Subtitle
        {
            get { return subtitle; }
            set { this.Subtitle = value; }
        }
        public string Abbreviation
        {
            get { return abbreviation; }
            set { this.abbreviation = value; }
        }
        public string HasTechnology
        {
            get { return hasTechnology; }
            set { this.hasTechnology = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { this.comment = value; }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Konstruktor mit Titel, Untertitel, Abkürzung und Technik und Kommentar
        /// </summary>
        /// <param name="title">Titel des Seminars</param>
        /// <param name="subtitle">Untertitel des Seminars</param>
        /// <param name="abbreviation">Abkürzung des Seminars</param>
        /// <param name="hasTechnology">Anmerkungen zur benötigten Technik</param>
        /// <param name="comment">Kommentare</param>
        public MSeminar( string title, string subtitle, string abbreviation, string hasTechnology, string comment)
        {
            this.title = title;
            this.subtitle = subtitle;
            this.abbreviation = abbreviation;
            this.hasTechnology = hasTechnology;
            this.comment = comment;
        }
        /// <summary>
        /// Konstruktor mit Titel, Untertitel, Abkürzung und Technik
        /// </summary>
        /// <param name="title">Titel des Seminars</param>
        /// <param name="subtitle">Untertitel des Seminars</param>
        /// <param name="abbreviation">Abkürzung des Seminars</param>
        /// <param name="hasTechnology">Anmerkungen zur benötigten Technik</param>
        public MSeminar(string title, string subtitle, string abbreviation, string hasTechnology)
        {
            this.title = title;
            this.subtitle = subtitle;
            this.abbreviation = abbreviation;
            this.hasTechnology = hasTechnology;
          
        }
        /// <summary>
        /// Konstruktor mit Id, Titel, Untertitel, Kürzel, Technik  und Bermerkung
        /// </summary>
        /// <param name="id">Datenbank ID</param>
        /// <param name="title">Titel des Seminars</param>
        /// <param name="subtitle">Untertitel des Seminars</param>
        /// <param name="abbreviation">Abkürzung des Seminars</param>
        /// <param name="hasTechnology">Anmerkungen zur benötigten Technik</param>
        /// <param name="comment">Kommentare</param>
        public MSeminar(int id, string title, string subtitle, string abbreviation, string hasTechnology, string comment)
        {
            this.id = id;
            this.title = title;
            this.subtitle = subtitle;
            this.abbreviation = abbreviation;
            this.hasTechnology = hasTechnology;
            this.comment = comment;
        }

        /// <summary>
        /// Konstruktor nur mit dem Comment fals nur die Tagart Seminar ausgewählt wurde
        /// </summary>
        /// <param name="comment"></param>
        public MSeminar(string comment)
        {
            this.comment = comment;
        }

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        public MSeminar()
        {

        }
        #endregion
    }
}
