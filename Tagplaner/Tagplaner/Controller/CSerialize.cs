using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Tagplaner
{
    /// <summary>
    /// Author: Isabella Pfeuster, Christopher Holler
    /// Date: 07.01.2015
    /// </summary>
    class CSerialize : ISerialize
    {
        private static BinaryFormatter formatter = new BinaryFormatter();
        private static FileStream stream;

        /// <summary>
        /// Methode zur Serialisierung eines Kalenderobjektes
        /// </summary>
        /// <param name="obj">Kalendarobjekt</param>
        public void SerializeObject(MCalendar obj, string name)
        {
            stream = new FileStream(@""+ name, FileMode.Create);
            formatter.Serialize(stream,obj);
            stream.Close();
        }
        /// <summary>
        /// Methode zum Deserialisierung eines Kalenderobjektes
        /// </summary>
        /// <returns>Gibt ein MCalendarobjekt zurück</returns>
        public MCalendar DeserializeObject(string name)
        {
            FileStream stream = new FileStream(@""+ name, FileMode.Open);
            return (MCalendar)formatter.Deserialize(stream);
        }
    }
}
