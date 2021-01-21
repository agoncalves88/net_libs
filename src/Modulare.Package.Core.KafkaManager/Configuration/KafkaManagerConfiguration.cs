using System;
using System.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulare.Package.Core.KafkaManager.Handlers;
using Modulare.Package.Core.KafkaManager.Producer;
using Microsoft.AspNetCore.Hosting;

namespace Modulare.Package.Core.KafkaManager.Configuration
{
    public static class KafkaManagerConfiguration
    {
        private static ConsumerDispatcher handler;
        private static IConfiguration _configuration;

        public static void AddKafkaManager(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton<IProducerMessage, ProducerMessage>();
            _configuration = configuration;
        }

        public static void RegisterMessage<TContent>(this IServiceCollection service, IServiceProvider provider)
            where TContent : class, IMessageHandler
        {
            handler = handler ?? new ConsumerDispatcher(provider, _configuration);
            handler.ConfigureConsumer<TContent>();
        }

        public static void RaiseConsumers(this IServiceProvider provider)
            => handler.RaiseConsumers();
    }
}