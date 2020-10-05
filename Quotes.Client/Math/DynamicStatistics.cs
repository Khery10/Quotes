using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Client.Math
{
    /// <summary>
    /// Подсчет статистики в динамическом потоке данных.
    /// </summary>
    public class DynamicStatistics
    {
        /// <summary>
        /// Счетчик чисел.
        /// </summary>
        private long _count;

        public double _mean;

        public double _median;

        public double _standartDeviation;

        /// <summary>
        /// Среднее.
        /// </summary>
        public double Mean
            => _mean;

        /// <summary>
        /// Медиана.
        /// </summary>
        public double Median
            => _median;


        /// <summary>
        /// Стандартное отклонение.
        /// </summary>
        public double StandardDeviation
            => _standartDeviation;


        /// <summary>
        /// Обновление статистики
        /// </summary>
        public void UpdateStatistics(double newNum)
        {
            ++_count;

            //обновляем статистические данные
            UpdateMean(newNum);
            UpdateMedian(newNum);
            UpdateStandardDeviation(newNum);
        }

        /// <summary>
        /// Обновление среднего.
        /// </summary>
        private void UpdateMean(double newNum)
            => _mean += (newNum - _mean) / (double)(_count);

        /// <summary>
        /// Обновление медианы.
        /// </summary>
        private void UpdateMedian(double newNum)
        {
            double delta = _mean / _count;    // delta = average/count
            if (newNum < _median)
                _median -= delta;
            else
                _median += delta;

        }

        private double _sumOfSquaresOfDifferences;
        /// <summary>
        /// Обновление стандартного отклонения.
        /// </summary>
        private void UpdateStandardDeviation(double newNum)
        {
            _sumOfSquaresOfDifferences += (newNum - _mean) * (newNum - _mean);
            _standartDeviation = System.Math.Sqrt(_sumOfSquaresOfDifferences / _count);
        }


    }
}
