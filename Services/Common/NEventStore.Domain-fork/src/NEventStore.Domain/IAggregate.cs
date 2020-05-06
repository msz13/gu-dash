namespace NEventStore.Domain
{
    using GuDash.Common.Domain.Model;
    using System.Collections.Generic;

    public interface IAggregate
    {
        IIdentity Id { get; }
        int Version { get; }


        IEnumerable<object> GetUncommittedEvents();
        void ClearUncommittedEvents();

        IMemento GetSnapshot();

        //void LoadSnapshot(IMemento snapshot);

        void LoadEvents(List<IDomainEvent> events);

        /* public static TAggregate LoadFromEvents<TAggregate>(IList events)
             where TAggregate : IAggregate, new()
         {
             var aggregate = new TAggregate();

             aggregate.LoadEvents((List<IDomainEvent>)events);
             return aggregate;

         } */
    }
}