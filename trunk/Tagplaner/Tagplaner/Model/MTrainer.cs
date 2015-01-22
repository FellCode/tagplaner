using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Die Klasse stellt das Model dar, dass die Attribute des Trainers enthält.
    /// </summary>
    [Serializable()]
    public class MTrainer
    {
        private int id;
        private string name;
        private string surname;
        private string abbreviation;
        private bool isInternal;
        private bool isCotrainer;

        #region getter

        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return name; }
        }
        public string Surname
        {
            get { return surname; }
        }
        public string Abbreviation
        {
            get { return abbreviation; }
        }
        public bool IsInternal
        {
            get { return isInternal; }
        }
        public bool IsCotrainer
        {
            get { return isCotrainer; }
        }
        #endregion

        #region constructor
        /// <summary>
        /// Konstruktor mit Nachname, Vorname, Kürzel und istIntern und istCotrainer
        /// </summary>
        /// <param name="name">Nachname des Trainers</param>
        /// <param name="surname">Vorname des Trainers</param>
        /// <param name="abbreviation">Kürzel des Trainers</param>
        /// <param name="isInternal">Flag ob Intern oder Extern</param>
        /// <param name="isCotrainer">Falg ob Cotrainer oder nicht</param>
        public MTrainer( string name, string surname, string abbreviation, bool isInternal, bool isCotrainer)
        {
            this.name = name;
            this.surname = surname;
            this.abbreviation = abbreviation;
            this.isInternal = isInternal;
            this.isCotrainer = isCotrainer;
        }
        /// <summary>
        /// Konstruktor mit Id, Nachname, Vorname, Kürzel und istIntern
        /// <param name="id">Datenbank ID</param>
        /// <param name="name">Nachname des Trainers</param>
        /// <param name="surname">Vorname des Trainers</param>
        /// <param name="abbreviation">Kürzel des Trainers</param>
        /// <param name="isInternal">Flag ob intern oder extern</param>
        /// </summary>
        public MTrainer(int id, string name, string surname, string abbreviation, bool isInternal)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.abbreviation = abbreviation;
            this.isInternal = isInternal;
            
        }
        /// <summary>
        /// Konstruktor mit Nachname, Vorname, Kürzel und istIntern
        /// </summary>
        /// <param name="name">Nachname des Trainers</param>
        /// <param name="surname">Vorname des Trainers</param>
        /// <param name="abbreviation">Kürzel des Trainers</param>
        /// <param name="isInternal">Flag ob intern oder extern</param>
        public MTrainer(string name, string surname, string abbreviation, bool isInternal)
        {
         
            this.name = name;
            this.surname = surname;
            this.abbreviation = abbreviation;
            this.isInternal = isInternal;

        }
        #endregion

        /// <summary>
        /// Hier wird die ToString Methode überschrieben um für die ComboBox den Namen des Trainers
        /// anzuzeigen.
        /// </summary>
        /// <returns>Vorname und Nachname des Trainers zur Ansicht in der Combobox</returns>
        public override string ToString()
        {
            return name + " " + surname;
        }

    }
}
