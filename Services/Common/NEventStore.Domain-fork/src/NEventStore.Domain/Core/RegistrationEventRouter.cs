namespace NEventStore.Domain.Core
{
    using System;
    using System.Collections.Generic;

    public class RegistrationEventRouter : IRouteEvents
    {
        private readonly IDictionary<Type, Action<object>> handlers = new Dictionary<Type, Action<object>>();

        private Domain.IAggregate regsitered;

        public virtual void Register<T>(Action<T> handler)
        {
            this.handlers[typeof(T)] = @event => handler((T)@event);
        }

        public virtual void Register(Domain.IAggregate aggregate)
        {
            if (aggregate == null)
            {
                throw new ArgumentNullException("aggregate");
            }

            this.regsitered = aggregate;
        }

        public virtual void Dispatch(object eventMessage)
        {
            Action<object> handler;

            if (!this.handlers.TryGetValue(eventMessage.GetType(), out handler))
            {
                this.regsitered.ThrowHandlerNotFound(eventMessage);
            }

            handler(eventMessage);
        }
    }
}