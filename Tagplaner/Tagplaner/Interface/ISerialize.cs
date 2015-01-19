using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagplaner
{
    /// <summary>
    /// Interface für die Klasse CSerialize.
    /// </summary>
    public interface ISerialize
    {
        /// <summary>
        /// Methode zur Serialisierung eines Kalenderobjektes
        /// </summary>
        /// <param name="obj">Kalendarobjekt</param>
        /// <param name="name">Der Name unterdem das Objekt gespeichert wird</param>
        void SerializeObject(Object obj, string name);

        /// <summary>
        /// Methode zum Deserialisierung eines Kalenderobjektes
        /// </summary>
        /// <param name="name">Name des zu öffnenden Objektes</param>
        /// <returns>Gibt ein MCalendarobjekt zurück</returns>
        Object DeserializeObject(string name);
    }
}
