using System;
using System.Collections.Generic;
using System.Text;

namespace Quotes.Server
{
    /// <summary>
    /// Сервис получения котировок.
    /// </summary>
    internal class QuotesService : IQuotesService
    {
        private readonly Random _rnd = new Random();

        public QuotesService(double minPrice, double maxPrice)
        {
            if (minPrice >= maxPrice)
                throw new ArgumentException($"{nameof(minPrice)} должен быть меньше {nameof(maxPrice)}");

            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        /// <summary>
        /// Минимальное значение цены.
        /// </summary>
        public double MinPrice { get; }

        /// <summary>
        /// Максимальное значение цены.
        /// </summary>
        public double MaxPrice { get; }


        /// <summary>
        /// Получение цены котировки.
        /// </summary>
        /// <returns></returns>
        public double GetPrice()
        {
            double rndDouble = _rnd.NextDouble();
            return rndDouble * MinPrice + (1 - rndDouble) * MaxPrice;
        }
    }
}
