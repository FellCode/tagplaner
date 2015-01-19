using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Die Klasse bildet Orte ab, an denen Veranstaltungen stattfinden können 
    /// </summary>
    [Serializable()]
    public class MPlace
    {
        private int id;
        private string place;
        private string contact;
        private List<MRoom> rooms;
        private int federalstate_id;

        #region getter
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }
        public string Place
        {
            get { return place; }
        }
        public string Contact
        {
            get { return contact; }
        }
        public List<MRoom> Rooms
        {
            get { return rooms; }
        }
        public int Federalstate_id
        {
            get { return federalstate_id; }
        }
        #endregion

        
        #region constructor
        /// <summary>
        /// Erzeugt ein Objekt vom Typ MPlace
        /// </summary>
        /// <param name="place">Name des Ortes</param>
        /// <param name="contact">Kontaktinformationen zum Ort</param>
        /// <param name="rooms">Liste von Räumen</param>
        public MPlace(string place, string contact, List<MRoom> rooms)
        {
           
            this.place = place;
            this.contact = contact;
            this.rooms = rooms;
        }
        /// <summary>
        /// Erzeugt ein Objekt vom Typ MPlace
        /// </summary>
        /// <param name="place">Name des Ortes</param>
        /// <param name="contact">Kontaktinformationen zum Ort</param>
        /// <param name="rooms">Liste von Räumen</param>
        public MPlace(int id, string place, string contact)
        {
            this.id = id;
            this.place = place;
            this.contact = contact;
        }
        /// <summary>
        /// Erzeugt ein Objekt vom Typ MPlace
        /// </summary>
        /// <param name="id">ID für die Datenbank</param>
        /// <param name="place">Bezeichnung des Ortes</param>
        /// <param name="contact">Kontaktinformationen</param>
        /// <param name="federalstate_id">ID aus Datenbank für das Bundesland</param>
        public MPlace(int id, string place, string contact, int federalstate_id)
        {
            this.id = id;
            this.place = place;
            this.contact = contact;
            this.federalstate_id = federalstate_id;
        }
        #endregion

        /// <summary>
        /// Gibt die Bezeichnung des Ortes zurück
        /// </summary>
        /// <returns>Bezeichnung des Ortes</returns>
        public override string ToString()
        {
            return place;
        }
    }
}
