using System;
using System.Collections.Generic;
using System.Text;

namespace Quotes.Client.Math
{
    /// <summary>
    /// Расширения для работы с числовыми данными.
    /// </summary>
    public static class ArithmeticsExtensions
    {
        /// <summary>
        /// Опредеялет является ли число нечетным.
        /// </summary>
        public static bool IsOdd(this int num)
        {
            return (num & 1) == 1;
        }
    }
}
