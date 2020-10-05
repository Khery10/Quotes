using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Quotes.Lib.Serialization
{
    /// <summary>
    /// Вспомогательный класс сериализации/десериализации xml.
    /// </summary>
    public class XmlDataSerializer
    {
        /// <summary>
        /// Получение объекта из xml содержимого.
        /// </summary>
        public static T GetInstance<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException(nameof(xml));

            object instance = null;
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml)))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                instance = xmlSerializer.Deserialize(xmlReader);
            }

            return GetValidatedInstance<T>(instance);
        }

        /// <summary>
        /// Получение объекта из файла xml.
        /// </summary>
        public static T GetInstanceFile<T>(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            object result = null;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                result = serializer.Deserialize(stream);
            }

            //валидируем полученный объект
            return GetValidatedInstance<T>(result);
        }

        public static T GetValidatedInstance<T>(object instance)
        {
            Validator.ValidateObject(instance, new ValidationContext(instance), true);
            return (T)instance;
        }
    }
}
