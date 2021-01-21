using System.Threading.Tasks;

namespace Modulare.Package.Core.DomainEventManager.DispatcherEvents
{
    public interface IDomainHandler<T> where T : IDomainEvent
    {
        Task Handle(T @event);
    }
}