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
        /// Methode zur Serialisierung eines MCalendar-Objektes
        /// </summary>
        /// <param name="obj"></param>
        public void SerializeObject(MCalendar obj)
        {
            stream = new FileStream(@"C:\Tagplan\Tagplan.tp", FileMode.Create);
            formatter.Serialize(stream,obj);
            stream.Close();
        }
        public MCalendar DeserializeObject()
        {
            FileStream stream = new FileStream(@"C:\Tagplan\Tagplan.tp", FileMode.Open);
            return (MCalendar)formatter.Deserialize(stream);
        }
    }
}
