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

        public void SerializeObject(Object obj)
        {
            stream = new FileStream(@"C:\Tagplan.tp", FileMode.Create);
            formatter.Serialize(stream,obj);
            stream.Close();
        }
        public Object DeserializeObject(Object obj)
        {
            FileStream stream = new FileStream(@"C:\Tagplan.tp", FileMode.Open);
            return formatter.Deserialize(stream);
        }
    }
}
