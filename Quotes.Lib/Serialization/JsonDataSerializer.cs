using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Quotes.Lib.Serialization
{
    /// <summary>
    /// Вспомогательный класс сериализации/десериализации двоичных данных.
    /// </summary>
    public class JsonDataSerializer
    {
        public static byte[] Serialize(object graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(graph.GetType());
            
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, graph);
                return stream.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                throw new ArgumentNullException(nameof(buffer));

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}
