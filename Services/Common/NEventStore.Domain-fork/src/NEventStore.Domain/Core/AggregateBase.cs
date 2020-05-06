namespace NEventStore.Domain.Core
{
    using GuDash.Common.Domain.Model;
    using NEventStore.Domain;
    using System;
    using System.Collections.Generic;


    public abstract class AggregateBase : EntityWithCompositeId, IAggregate
    {
        public IIdentity Id { get; protected set; }
        //private readonly ICollection<object> uncommittedEvents = new LinkedList<object>();
        private readonly List<object> uncommittedEvents = new List<object>();

        private IRouteEvents registeredRoutes;

        protected AggregateBase()
            : this(null)
        {
            this.registeredRoutes = new ConventionEventRouter(true, this);


        }

        protected AggregateBase(IRouteEvents handler)
        {
            if (handler == null)
            {
                return;
            }

            this.RegisteredRoutes = handler;
            this.RegisteredRoutes.Register(this);
        }

        protected IRouteEvents RegisteredRoutes
        {
            get
            {
                return this.registeredRoutes; //?? (this.registeredRoutes = new ConventionEventRouter(true, this));
            }
            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("AggregateBase must have an event router to function");
                }

                this.registeredRoutes = value;
            }
        }

        //public IIdentity Id { get; protected set; }

        public int Version { get; protected set; }

        // protected void ApplyEvent(object @event) //TODO zrobiæ protected i zrobiæ metodê load events
        // {
        //     this.RegisteredRoutes.Dispatch(@event);
        //     this.Version++;
        // }

        public void Apply(object @event)
        {
            this.RegisteredRoutes.Dispatch(@event);
            this.Version++;
        }

        public IEnumerable<object> GetUncommittedEvents()
        {
            return this.uncommittedEvents;
        }

        public void ClearUncommittedEvents()
        {
            this.uncommittedEvents.Clear();
        }
        /*
        IMemento Domain.IAggregate.GetSnapshot()
        {
            IMemento snapshot = this.GetSnapshot();
            snapshot.Id = this.Id;
            snapshot.Version = this.Version;
            return snapshot;
        }*/

        /*
    public virtual bool Equals(IAggregate other)
    {
        return null != other && other.Id == this.Id;
    } */

        protected void Register<T>(Action<T> route)
        {
            this.RegisteredRoutes.Register(route);
        }

        protected void RaiseEvent(object @event)
        {
            //this.ApplyEvent(@event);
            this.Apply(@event);
            this.uncommittedEvents.Add(@event);
        }

        public virtual IMemento GetSnapshot()
        {
            return null;
        }

        public void LoadEvents(List<IDomainEvent> events)
        {

            foreach (var evt in events)
            {
                Apply(evt);
            }



        }



        //public abstract void LoadSnapshot<TMemento>(TMemento snapshot) where TMemento : IMemento;
        /*
                public override int GetHashCode()
                {
                    return this.Id.GetHashCode();
                }

                public override bool Equals(object obj)
                {
                    return this.Equals(obj as Domain.IAggregate);
                }
                */
    }
}