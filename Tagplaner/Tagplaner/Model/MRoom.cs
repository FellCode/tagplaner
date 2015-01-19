using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Klasse für das Model für den Raum
    /// </summary>
    [Serializable()]
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
        /// <summary>
        /// Konstruktor mit Raumnummer
        /// </summary>
        /// <param name="number">Raumnummer</param>
        public MRoom( string number)
        {
         
            this.number = number;
        }

        /// <summary>
        /// Konstruktor mit Id und Raumnummer
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="number">Raumnummer</param>
        public MRoom(int id, string number)
        {
            this.id = id;
            this.number = number;
        }

        /// <summary>
        /// Konstruktor mit Id, Raumnummer und StandortID
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="number">Raumnummer</param>
        /// <param name="place_id">Id des Standorts des Tagplanes</param>
        public MRoom(int id, string number, int place_id)
        {
            this.id = id;
            this.number = number;
            this.place_id = place_id;
        }

        /// <summary>
        /// Konstruktor mit  Raumnummer und StandortID
        /// </summary>
        /// <param name="number">Raumnummer</param>
        /// <param name="place_id">Id des Standorts des Tagplanes</param>
        public MRoom( string number, int place_id)
        {
            this.number = number;
            this.place_id = place_id;
        }
        #endregion

        /// <summary>
        /// Überschreibt die ToString Methode zur Darstellung der Raumnummer in den ComboBoxen auf
        /// der grafischen Oberfläche
        /// </summary>
        /// <returns>Raumnummer</returns>
        public override string ToString()
        {
            return number;
        }
    }
}
