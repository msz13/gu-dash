using CompetencesService.Application.ReadModel;
using Marten;
using Marten.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
