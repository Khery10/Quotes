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
using System.Threading.Tasks;

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

        private Timer _timer;
        private UdpClient _client;

        /// <summary>
        /// Заруск прослушивания котировок в отдельном потоке.
        /// </summary>
        public void Run()
        {
            _client = new UdpClient(_settings.EndPoint.Port);
            _client.Client.ReceiveBufferSize = 0;

            IPAddress remoteAddress = IPAddress.Parse(_settings.EndPoint.IPAddress);
            _client.JoinMulticastGroup(remoteAddress);

            _timer = new Timer(GetQuotes);
            _timer.Change(0, _settings.Delay);
        }

        public void Stop()
        {
            _client.Dispose();
            _timer.Dispose();
        }

        /// <summary>
        /// Получение котировок.
        /// </summary>
        /// <param name="state"></param>
        private void GetQuotes(object state)
        {
            try
            {
                IPEndPoint endPoint = null;
                byte[] result = _client.Receive(ref endPoint);
                QuoteDTO quote = JsonDataSerializer.Deserialize<QuoteDTO>(result);

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
        }        
    }
}
