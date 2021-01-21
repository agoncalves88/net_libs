namespace Package.Core.DomainEventManager.DispatcherEvents
{
   public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent;
    }
}