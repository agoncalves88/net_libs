namespace Package.Core.KafkaManager.Handlers
{
    public interface IConsumerHandler<T> where T : IMessageHandler
    {
        void ProcessMessage(T message);
    }
}