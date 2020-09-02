
using System.ComponentModel.DataAnnotations;


namespace Quotes.Server.Settings
{
    /// <summary>
    /// Настройка адреса отправки пакетов.
    /// </summary>
    public sealed class EndPointSettings
    {
        /// <summary>
        /// IP адрес для отправки пакетов.
        /// </summary>           
        [Required(AllowEmptyStrings = false, ErrorMessage = "Не заполнен IPAddress")]
        public string IPAddress { get; set; }

        /// <summary>
        /// Порт для отправки пакетов.
        /// </summary>   
        [Range(0, ushort.MaxValue, ErrorMessage = "Задан неверный порт")]
        public int Port { get; set; }
    }
}
