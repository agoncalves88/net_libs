using System.Threading.Tasks;

namespace Package.Core.DomainEventManager.DispatcherEvents
{
    public static class DomainEvent
    {
        public static IEventDispatcher Dispatcher { get; set; }
        public static void Raise<T>(T @event) where T : IDomainEvent
        {
            Dispatcher.Dispatch(@event);
        }
    }
}