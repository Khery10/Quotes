using System;
using System.Collections.Generic;
using System.Text;

namespace Quotes.Server
{
    /// <summary>
    /// Сервис получения котировок.
    /// </summary>
    internal interface IQuotesService
    {
        /// <summary>
        /// Получение цены котировки.
        /// </summary>
        /// <returns></returns>
        public double GetPrice();
    }
}
