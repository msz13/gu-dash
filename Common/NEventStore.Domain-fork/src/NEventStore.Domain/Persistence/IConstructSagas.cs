namespace NEventStore.Domain.Persistence
{
    using System;

    public interface IConstructSagas
	{
		ISaga Build(Type type, string id);
	}
}