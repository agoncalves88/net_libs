using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Package.Core.KafkaManager.Handlers
{
    public class ConsumerDispatcher 
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private List<Task> _Tasks { get; set; }
        
        public ConsumerDispatcher(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public void ConfigureConsumer<TContent>() where TContent : class, IMessageHandler
        {
            this._Tasks = this._Tasks ?? new List<Task>();
            var result = this._serviceProvider.GetServices(typeof(Consumer<TContent>));

            foreach (dynamic service in result)
            {
                _Tasks.Add(Task.Run(() => ((Consumer<TContent>) service).Listen(_configuration["Kafka:ConnectionString"])));
            }
        }

        public void RaiseConsumers()
            => Task.WaitAll(_Tasks.ToArray());
    }
}