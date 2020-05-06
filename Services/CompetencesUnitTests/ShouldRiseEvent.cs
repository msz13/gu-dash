using FluentAssertions.Primitives;
using NEventStore.Domain;

namespace CompetencesUnitTests
{
    public static class RiseEventExtension
    {
        public static RiseEventAssertation Should(this IAggregate instance)
        {

            return new RiseEventAssertation(instance);
        }
    }

    public class RiseEventAssertation : ReferenceTypeAssertions<IAggregate, RiseEventAssertation>
    {

        public RiseEventAssertation(IAggregate instance)
        {
            Subject = instance;
        }

        protected override string Identifier { get; }
    }
}
