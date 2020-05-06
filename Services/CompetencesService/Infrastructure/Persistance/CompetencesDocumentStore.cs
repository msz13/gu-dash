using CompetencesService.Application.ReadModel;
using GuDash.CompetencesService.Proto;
using Marten;
using Marten.Events;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    public class CompetencesDocumentStore : StoreOptions
    {
        public static IDocumentStore CreateStore(string connectionStr)
        {
            return new DocumentStore(new CompetencesDocumentStore(connectionStr));
        }

        public CompetencesDocumentStore(string connectionStr)
        {
            Connection(connectionStr);
            UseDefaultSerialization(nonPublicMembersStorage: NonPublicMembersStorage.NonPublicSetters);
            Events.StreamIdentity = StreamIdentity.AsString;
            Events.InlineProjections.Add(new CompetenceViewProjection());
            Schema.For<CompetenceDTO>().Identity(x => x.Id);
        }
    }
}
