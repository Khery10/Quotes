
using Quotes.Lib.Serialization;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //получаем на стройку работы клиента
            QuotesClientSettings settings =
                XmlDataSerializer.GetInstanceFile<QuotesClientSettings>("QuotesClientSettings_sample.xml");

            //запускаем получение данных и подсчет статистики
            QuotesClientApplication app = new QuotesClientApplication(settings);

            app.OnSocketError += error =>
            Console.WriteLine($@"UDP-клиент многоадресной рассылки вернул ошибку с кодом {error}");
            app.Run();

            Console.WriteLine("Нажмите Enter для получения статистики...");

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {                   
                    string message = $@"
Среднее - {app.Statistics.Mean}
Медиана - {app.Statistics.Median}
Стандартное отклонение - {app.Statistics.StandardDeviation}
{new string('-', 20)}
";
                    Console.WriteLine(message);
                }
                else
                    break;
            }

            Console.ReadLine();
        }
    }
}
