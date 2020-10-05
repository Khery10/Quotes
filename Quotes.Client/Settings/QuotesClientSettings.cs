using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Quotes.Client
{

    /// <summary>
    /// Настройка работы клиента.
    /// </summary>
    [XmlRoot(ElementName = "Settings")]
    public class QuotesClientSettings
    {
        /// <summary>
        /// Задержка приема пакетов.
        /// </summary>
        [Range(0, Int32.MaxValue)]
        public int Delay { get; set; }

        /// <summary>
        /// Настройка мультикастовой рассылки.
        /// </summary>
        [Required]
        public EndPointSettings EndPoint { get; set; }
    }

    public class EndPointSettings
    {
        [Required(AllowEmptyStrings = true)]
        public string IPAddress { get; set; }

        [Range(0, UInt16.MaxValue)]
        public int Port { get; set; }
    }

}
