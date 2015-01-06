using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Tagplaner
{
    class CSerialize : ISerialize
    {
        private static BinaryFormatter formatter;
        private static FileStream stream;

        public static void SerializeObject(Object obj)
        {
            stream = new FileStream(@"C:\MyObjects.dat", FileMode.Create);
            formatter.Serialize(stream,obj);
            stream.Close();
        }
        public static Object DeserializeObjetct(Object obj)
        {
            FileStream stream = new FileStream(@"C:\MyObject.dat", FileMode.Open);
            return formatter.Deserialize(stream);
        }
    }
}
