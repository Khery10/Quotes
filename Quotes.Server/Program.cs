using Autofac;
using Quotes.Lib.Serialization;
using Quotes.Server.Settings;
using System;
using System.Collections.Generic;

namespace Quotes.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            QuotesServerSettings serverSettings = XmlDataSerializer
                .GetInstanceFile<QuotesServerSettings>(@"QuotesServerSettings.xml");

            //настраиваем DI и запускаем работу сервера
            CompositionRoot(serverSettings).Resolve<QuotesServerApplication>().Run();
        }

        private static IContainer CompositionRoot(QuotesServerSettings settings)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<QuotesServerApplication>().WithParameter("endPoint", settings.EndPoint);
            builder.RegisterType<QuotesService>()
                .As<IQuotesService>()
                .WithParameters(new List<NamedParameter>()
                {
                    new NamedParameter("minPrice", settings.QuotesRange.MinValue),
                    new NamedParameter("maxPrice", settings.QuotesRange.MaxValue)
                });

            return builder.Build();
        }
    }
}
