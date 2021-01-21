using System.Threading.Tasks;

namespace Modulare.Package.Core.KafkaManager.Producer
{
    public interface IProducerMessage
    {
        Task<string> Send<T>(T message, string topicName);
    }
}