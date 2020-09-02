using System;
using System.Collections.Generic;
using System.Text;

namespace Quotes.Client.DTO
{
    [Serializable]
    public class QuoteDTO
    {
        public double Price { get; set; }

        public int Index { get; set; }

        public DateTime DateTime { get; set; }

    }
}
