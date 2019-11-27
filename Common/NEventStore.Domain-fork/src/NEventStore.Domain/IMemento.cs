namespace NEventStore.Domain
{
    using GuDash.Common.Domain.Model;
    using System;

    public interface IMemento
	{
		IIdentity Id { get; set; }

		int Version { get; set; }
	}
}