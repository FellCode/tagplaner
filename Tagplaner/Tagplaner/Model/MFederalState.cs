using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Diese Klasse bildet ein Bundesland ab.
    /// </summary>
    [Serializable()]
    public class MFederalState
    {   
        private int id;
        private string name;
        private string abbreviation;

        #region getter_setter
        public int Id 
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public string Abbreviation
        {
            get { return abbreviation; }
            set { this.abbreviation = value; }
        }
        #endregion

        /// <summary>
        /// Erzeugt ein Objekt vom Typ MFederalState
        /// </summary>
        /// <param name="id">ID für die Datenbank</param>
        /// <param name="name">Name des Bundeslandes</param>
        /// <param name="abbreviation">Abkürzung/Kürzel des Names des Bundeslandes</param>
        public MFederalState(int id, string name, string abbreviation)
        {
            this.id = id;
            this.name = name;
            this.abbreviation = abbreviation;
        }

        /// <summary>
        /// Gibt den Namen des Bundeslandes zurück
        /// </summary>
        /// <returns>Name des Bundelandes</returns>
        public override string ToString()
        {
            return name;
        }

    }
}
