using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Package.Core.KafkaManager.Handlers
{
    public abstract class Consumer<T> : ConsumerBase, IConsumerHandler<T> where T : IMessageHandler
    {
        protected Consumer(string topicName, string groupId) : base(topicName, groupId)
        {
        }

        public abstract void ProcessMessage(T message);

        public virtual async Task Listen(string kafkaConnectionString)
        {
            if(string.IsNullOrEmpty(kafkaConnectionString))
                throw new Exception("kafkaConnectionString is null in appsettings.json {kafka:ConnectionString}");
            
            var conf = new ConsumerConfig
            {
                GroupId = this.GroupId,
                BootstrapServers = kafkaConnectionString,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe(this.TopicName);
                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };
                try
                {
                    while (true)
                    {
                        var retMessage = c.Consume(cts.Token);
                        if (!string.IsNullOrEmpty(retMessage.Message.Value))
                            this.ProcessMessage(JsonSerializer.Deserialize<T>(retMessage.Message.Value));
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }
        }
    }
}