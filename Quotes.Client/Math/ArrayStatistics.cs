using System;
using System.Collections.Generic;
using System.Text;

namespace Quotes.Client.Math
{
    /// <summary>
    /// Подсчет статистики по данным.
    /// </summary>
    public static class ArrayStatistics
    {
        /// <summary>
        /// Минимальное число ряда.
        /// </summary>
        public static double Minimum(double[] data)
        {
            if (data.Length == 0)
                return double.NaN;

            double min = double.PositiveInfinity;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < min || double.IsNaN(data[i]))
                    min = data[i];
            }

            return min;
        }


        /// <summary>
        /// Максимальное число ряда.
        /// </summary>
        public static double Maximum(double[] data)
        {
            if (data.Length == 0)
                return double.NaN;

            double max = double.NegativeInfinity;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > max || double.IsNaN(data[i]))
                    max = data[i];
            }

            return max;
        }

        /// <summary>
        /// Мода неотсортированного ряда.
        /// </summary>
        public static double Mode(double[] data)
        {
            if (data.Length == 0)
                return double.NaN;
            
            if (data.Length == 1)
                return data[0];
            
            double num = (double)(data.Length - 1) * 0.5 + 1.0;
            int num2 = (int)num;
            double num3 = SelectInplace(data, num2 - 1);
            double num4 = SelectInplace(data, num2);
            return num3 + (num - (double)num2) * (num4 - num3);
        }

        /// <summary>
        /// Медиана неотсортированного ряда.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double Median(double[] data)
        {
            int num = data.Length / 2;
            if (!data.Length.IsOdd())
            {
                return (SelectInplace(data, num - 1) + SelectInplace(data, num)) / 2.0;
            }

            return SelectInplace(data, num);
        }


        /// <summary>
        /// Стандартное отклонение.
        /// </summary>
        public static double StandardDeviation(double[] data)
            => System.Math.Sqrt(Variance(data));

        /// <summary>
        /// Среднее ариметическое.
        /// </summary>
        public static double Mean(double[] data)
        {
            if (data.Length == 0)
                return double.NaN;

            double sum = 0.0;
            ulong counter = 0uL;
            for (int i = 0; i < data.Length; i++)
            {
                sum += (data[i] - sum) / (double)(++counter);
            }

            return sum;
        }

        /// <summary>
        /// Дисперсия.
        /// </summary>
        public static double Variance(double[] samples)
        {
            if (samples.Length <= 1)
            {
                return double.NaN;
            }

            double num = 0.0;
            double num2 = samples[0];
            for (int i = 1; i < samples.Length; i++)
            {
                num2 += samples[i];
                double num3 = (double)(i + 1) * samples[i] - num2;
                num += num3 * num3 / (((double)i + 1.0) * (double)i);
            }

            return num / (double)(samples.Length - 1);
        }

        /// <summary>
        /// Получение элемента в ранжированном ряду.
        /// </summary>
        private static double SelectInplace(double[] data, int rank)
        {
            int num = 0;
            int num2 = data.Length - 1;
            while (num2 > num + 1)
            {
                int num3 = num + num2 >> 1;
                double num4 = data[num3];
                data[num3] = data[num + 1];
                data[num + 1] = num4;
                if (data[num] > data[num2])
                {
                    double num5 = data[num];
                    data[num] = data[num2];
                    data[num2] = num5;
                }

                if (data[num + 1] > data[num2])
                {
                    double num6 = data[num + 1];
                    data[num + 1] = data[num2];
                    data[num2] = num6;
                }

                if (data[num] > data[num + 1])
                {
                    double num7 = data[num];
                    data[num] = data[num + 1];
                    data[num + 1] = num7;
                }

                int num8 = num + 1;
                int num9 = num2;
                double num10 = data[num8];
                while (true)
                {
                    num8++;
                    if (!(data[num8] < num10))
                    {
                        do
                        {
                            num9--;
                        }
                        while (data[num9] > num10);
                        if (num9 < num8)
                        {
                            break;
                        }

                        double num11 = data[num8];
                        data[num8] = data[num9];
                        data[num9] = num11;
                    }
                }

                data[num + 1] = data[num9];
                data[num9] = num10;
                if (num9 >= rank)
                {
                    num2 = num9 - 1;
                }

                if (num9 <= rank)
                {
                    num = num8;
                }
            }

            if (num2 == num + 1 && data[num2] < data[num])
            {
                double num12 = data[num];
                data[num] = data[num2];
                data[num2] = num12;
            }

            return data[rank];
        }
    }
}
