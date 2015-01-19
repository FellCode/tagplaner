using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Diese Klasse bildet eine Fachrichtung des Jahrgangs ab.
    /// </summary>
    [Serializable()]
    public class MSpeciality
    {
        private int id;
        private string apprenticeship;
        private string specialityName;

        #region getter

        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public string SpecialityName
        {
            get { return specialityName; }
        }

        public string Apprenticeship
        {
            get { return apprenticeship; }
        }
        #endregion

        #region constructor

        /// <summary>
        /// Konstruktor für die Klasse MSpeciality
        /// </summary>
        /// <param name="specialityName">Fachrichtung</param>
        /// <param name="apprenticeship">Jahrgang</param>
        public MSpeciality(string specialityName, string apprenticeship)
        {

            this.specialityName = specialityName;
            this.apprenticeship = apprenticeship;
        }

        /// <summary>
        /// Konstruktor für ein Objekt der Klasse MSpeciality aus der Datenbank. 
        /// </summary>
        /// <param name="id">ID des Objektes aus der Datenbank</param>
        /// <param name="specialityName"></param>
        /// <param name="apprenticeship"></param>
        public MSpeciality(int id, string specialityName, string apprenticeship)
        {
            this.id = id;
            this.specialityName = specialityName;
            this.apprenticeship = apprenticeship;
        }

        #endregion
    }
}
