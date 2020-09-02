using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Quotes.Lib.Serialization
{
    /// <summary>
    /// Вспомогательный класс сериализации/десериализации двоичных данных.
    /// </summary>
    public class BinaryDataSerializer
    {
        private BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public byte[] Serialize(object graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph));

            using (MemoryStream stream = new MemoryStream())
            {
                _binaryFormatter.Serialize(stream, graph);
                return stream.ToArray();
            }
        }

        public T Deserialize<T>(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                throw new ArgumentNullException(nameof(buffer));

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                return (T)_binaryFormatter.Deserialize(stream);
            }
        }
    }
}
