using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Package.Core.DomainEventManager.Dispatcher;
using Package.Core.DomainEventManager.DispatcherEvents;

namespace Package.Core.DomainEventManager.ServiceColletion
{
    public static class EventoDispatcherServiceCollection
    {
        public static void AddDispatcher(this IServiceCollection services, IConfiguration configuration, IServiceProvider provider)
        {
            DomainEvent.Dispatcher = new EventDispatcher(provider);
        }
    }
}