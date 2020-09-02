
using Quotes.Lib.Validation;
using System.Xml.Serialization;

namespace Quotes.Server.Settings
{
    /// <summary>
    /// Настройки сервера, отправляющего котировки.
    /// </summary>
    [XmlRoot(ElementName = "Settings")]
    public sealed class QuotesServerSettings
    {
        [ValidateObject(ErrorMessage = "Не задана настройка адреса")]
        public EndPointSettings EndPoint { get; set; }

        [ValidateObject(ErrorMessage = "Не задана настройка диапозона котировок")]
        public QuotesRangeSettings QuotesRange { get; set; }
       
    }
}
