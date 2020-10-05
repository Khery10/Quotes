using Quotes.Client.DTO;
using Quotes.Client.Math;
using Quotes.Lib.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Quotes.Client
{
    /// <summary>
    /// Приложение-клиент для приема котировок.
    /// </summary>
    public sealed class QuotesClientApplication
    {
        /// <summary>
        /// Настройки работы клиента.
        /// </summary>
        private readonly QuotesClientSettings _settings;

        public QuotesClientApplication(QuotesClientSettings settings)
        {
            _settings = settings
                ?? throw new ArgumentNullException(nameof(settings));

            Statistics = new DynamicStatistics();
        }

        /// <summary>
        /// Статистика по полуенным данным.
        /// </summary>
        public DynamicStatistics Statistics { get; }


        /// <summary>
        /// Событие ошибки подключения к серверу.
        /// </summary>
        public event Action<SocketError> OnSocketError;



        /// <summary>
        /// Заруск прослушивания котировок в отдельном потоке.
        /// </summary>
        public void Run()
        {
            //обработчик сравбатывающий периодически
            TimerCallback callback = async (state) =>
            {
                UdpClient client = new UdpClient(_settings.EndPoint.Port);
                client.Client.ReceiveBufferSize = 0;

                IPAddress remoteAddress = IPAddress.Parse(_settings.EndPoint.IPAddress);
                client.JoinMulticastGroup(remoteAddress);

                while (true)
                {
                    try
                    {
                        UdpReceiveResult result = await client.ReceiveAsync();
                        QuoteDTO quote = JsonDataSerializer.Deserialize<QuoteDTO>(result.Buffer);

                        //обновление статистики
                        Statistics.UpdateStatistics(quote.Price);
                    }
                    catch (SocketException ex)
                    {
                        OnSocketError(ex.SocketErrorCode);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"В процессе получения котировок произошла неожиданная ошибка: {ex}");
                    }
                };
            };

            Timer timer = new Timer(callback);
            timer.Change(0, _settings.Delay);
        }      
    }
}
