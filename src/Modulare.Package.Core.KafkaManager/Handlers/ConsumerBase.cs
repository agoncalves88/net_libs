namespace Modulare.Package.Core.KafkaManager.Handlers
{
    public class ConsumerBase
    {
        private readonly string _topicName;
        private readonly string _groupId;


        public ConsumerBase(string topicName, string groupId)
        {
            _topicName = topicName;
            _groupId = groupId;
        }
        public string TopicName => _topicName;
        public string GroupId => _groupId;
    }
}