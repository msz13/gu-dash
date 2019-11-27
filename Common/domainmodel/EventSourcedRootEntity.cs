using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace GuDash.Common.Domain.Model
{
    public abstract class EventSourcedRootEntity : EntityWithCompositeId
    {
        public EventSourcedRootEntity()
        {
            this.mutatingEvents = new List<IDomainEvent>();
            this.type = this.GetType();
        }

        public int Version { get; set; } = -1;

        private Type type;

      

        protected EventSourcedRootEntity(IEnumerable<IDomainEvent> eventStream, int streamVersion)
            : this()
        {
            foreach (var e in eventStream)
                When(e);
                this.unmutatedVersion = streamVersion;
        }
                

        readonly List<IDomainEvent> mutatingEvents;
        readonly int unmutatedVersion;

        protected int MutatedVersion
        {
            get { return this.unmutatedVersion + 1; }
        }

        protected int UnmutatedVersion
        {
            get { return this.unmutatedVersion; }
            
        }

        public IList<IDomainEvent> GetChanges()
        {
            return this.mutatingEvents.ToArray();
        }

        protected void When(IDomainEvent ev)
        {
            var methodName = "On"+ev.GetType().Name;
                                 
            var method = this.type.GetMethod(methodName);

            IDomainEvent[] param = {ev };
            method.Invoke(this, param);
            Version = ev.Version;
            
        }    
        
      

        protected void Apply(IDomainEvent e)
        {
            e.Version = ++this.Version;
            this.mutatingEvents.Add(e);

            When(e);
        }
    }
}
