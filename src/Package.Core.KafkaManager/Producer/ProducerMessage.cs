using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Package.Core.KafkaManager.Producer
{
    public class ProducerMessage : IProducerMessage
    {
        private readonly string _plaintext;

        public ProducerMessage(IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration["Kafka:ConnectionString"]))
                throw new Exception("Kafka:ConnectionString is not implemented in appsettings.json");
            _plaintext = configuration["Kafka:ConnectionString"];
        }

        public async Task<string> Send<T>(T message, string topicName)
        {
            var config = new ProducerConfig {BootstrapServers = _plaintext};

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var sendResult = producer.ProduceAsync(topicName,
                        new Message<Null, string> {Value = JsonSerializer.Serialize(message)}).GetAwaiter().GetResult();

                    Console.WriteLine($"Mensagem '{sendResult.Value}' de '{sendResult.TopicPartitionOffset}'");
                    return sendResult.Value;
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
            return string.Empty;
        }
    }
}