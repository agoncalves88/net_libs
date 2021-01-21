using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Modulare.Package.Core.DomainEventManager.Dispatcher;
using Modulare.Package.Core.DomainEventManager.DispatcherEvents;

namespace Modulare.Package.Core.DomainEventManager.ServiceColletion
{
    public static class EventoDispatcherServiceCollection
    {
        public static void AddDispatcher(this IServiceCollection services, IConfiguration configuration, IServiceProvider provider)
        {
            DomainEvent.Dispatcher = new EventDispatcher(provider);
        }
    }
}