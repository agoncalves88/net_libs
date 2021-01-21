using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Package.Core.DomainEventManager.DispatcherEvents;

namespace Package.Core.DomainEventManager.Dispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider serviceCollection;

        public EventDispatcher(IServiceProvider serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            List<Task> tasks = new List<Task>();

            foreach (var handler in serviceCollection.GetServices<IDomainHandler<TEvent>>())
            {
                tasks.Add(Task.Run(() => handler.Handle(eventToDispatch)));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}