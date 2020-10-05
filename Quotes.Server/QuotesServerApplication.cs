
using Quotes.Lib.Serialization;
using Quotes.Server.DTO;
using Quotes.Server.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace Quotes.Server
{
    /// <summary>
    /// Приложение-сервер отправки котировок.
    /// </summary>
    internal sealed class QuotesServerApplication
    {
        /// <summary>
        /// Настройка работы сервера.
        /// </summary>
        private readonly EndPointSettings _endPointSettings;

        /// <summary>
        /// Сервис получения котировок.
        /// </summary>
        private readonly IQuotesService _quotesService;

        public QuotesServerApplication(EndPointSettings endPoint, IQuotesService quotesService)
        {
            _endPointSettings = endPoint
                ?? throw new ArgumentNullException(nameof(endPoint));

            _quotesService = quotesService
                ?? throw new ArgumentNullException(nameof(quotesService));
        }

        /// <summary>
        /// Запуск приложения-сервера передачи котировок.
        /// </summary>
        internal void Run()
        {
            UdpClient sender = new UdpClient();

            IPAddress remoteAddress = IPAddress.Parse(_endPointSettings.IPAddress);
            IPEndPoint endPoint = new IPEndPoint(remoteAddress, _endPointSettings.Port);

            int counter = 0;
            while (true)
            {
                //получаем цену котировок
                double price = _quotesService.GetPrice();

                //отправляемая информацию о котировке
                QuoteDTO package = new QuoteDTO()
                {
                    Price = price,
                    Index = counter++
                };

                byte[] buffer = JsonDataSerializer.Serialize(package);
                sender.Send(buffer, buffer.Length, endPoint);
            }

        }
    }
}
