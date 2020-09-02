using Quotes.Client.DTO;
using Quotes.Lib.Serialization;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
           {
               QuotesClientSettings settings = XmlDataSerializer.GetInstanceFile<QuotesClientSettings>("QuotesClientSettings_sample.xml");

               UdpClient client = new UdpClient(settings.EndPoint.Port);
               client.Client.ReceiveBufferSize = 0;

               IPAddress remoteAddress = IPAddress.Parse(settings.EndPoint.IPAddress);
               client.JoinMulticastGroup(remoteAddress);

               BinaryDataSerializer binarySerializer = new BinaryDataSerializer();

               while (true)
               {
                   try
                   {
                       UdpReceiveResult result = await client.ReceiveAsync();

                       QuoteDTO quote = binarySerializer.Deserialize<QuoteDTO>(result.Buffer);

                       Console.WriteLine($"Price = {quote.Price}, Index = {quote.Index}");
                       
                   }
                   catch(Exception ex)
                   {
                       Console.WriteLine(ex);
                   }
                   finally
                   {
                       Thread.Sleep(1000);
                   }
           
               }
           });


            Console.ReadKey();
        }
    }
}
