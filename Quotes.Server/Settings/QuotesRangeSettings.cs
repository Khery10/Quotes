using System;
using System.ComponentModel.DataAnnotations;


namespace Quotes.Server.Settings
{
    /// <summary>
    /// Настройка дипозона котировок.
    /// </summary>
    public sealed class QuotesRangeSettings
    {
        /// <summary>
        /// Минимальное значение котировок.
        /// </summary>
        [Range(1, Int32.MaxValue)]
        public int MinValue { get; set; }

        /// <summary>
        /// Максимальное значение котировок.
        /// </summary>
        [Required]
        [Range(1, Int32.MaxValue)]
        public int MaxValue { get; set; }
    }
}
