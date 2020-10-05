using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Quotes.Server.DTO
{

    /// <summary>
    /// Класс для отправки котировки.
    /// </summary>
    [DataContract]
    public class QuoteDTO
    {

        /// <summary>
        /// Цена котировки.
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
