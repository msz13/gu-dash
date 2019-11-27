namespace NEventStore.Domain
{
    using GuDash.Common.Domain.Model;
    using System;
    using System.Collections;

    public interface IAggregate
	{
        IIdentity Id { get; }
		int Version { get; }

		void ApplyEvent(object @event);
		ICollection GetUncommittedEvents();
		void ClearUncommittedEvents();

		IMemento GetSnapshot();

        //void LoadSnapshot(IMemento snapshot);
	}
}