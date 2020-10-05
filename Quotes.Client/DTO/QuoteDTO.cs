using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Quotes.Client.DTO
{

    /// <summary>
    /// Класс транспортировки данных о котировке.
    /// </summary>
    [DataContract]
    public class QuoteDTO
    {

        /// <summary>
        /// Цена котровки.
        /// </summary>
        [DataMember]
        public double Price { get; set; }


        /// <summary>
        /// Индекс пакета.
        /// </summary>
        [DataMember]
        public int Index { get; set; }

    }
}
