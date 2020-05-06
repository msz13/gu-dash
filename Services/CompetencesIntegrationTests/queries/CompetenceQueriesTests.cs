using CompetencesService.Application.QueryHandlers;
using CompetencesService.Domain.Competences.Events;
using FluentAssertions;
using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Domain.Learner;
using Marten;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CompetencesIntegrationTests.queries
{
    public class CompetenceQueriesTest : IClassFixture<MartenStoreTestFactory>, IDisposable
    {


        IDocumentStore store;


        public CompetenceQueriesTest(MartenStoreTestFactory competenceStore)
        {


            this.store = competenceStore.Store;
        }

        [Fact]
        public async void QueryOneCompetence_Succes()
        {
            //given            
            var competenceId = new CompetenceId();
            var learnerId = new LearnerId();
            var name = "Komunikacja";
            var description = "w firmie";
            var isReaquired = false;

            using (var session = store.OpenSession())
            {
                session.Events.StartStream(competenceId.Id, new IDomainEvent[] { new CompetenceDefined(learnerId, competenceId, new CompetenceName(name), description, isReaquired) });
                session.SaveChanges();
            }

            var query = new CompetenceQuery(competenceId.Id, learnerId.Id);

            var queryHandler = new CompetenceQueryHandler(store);

            //when
            var competence = await queryHandler.Handle(query, new CancellationToken());

            //than
            competence.Id.Should().Be(query.CompetenceId);
            competence.LearnerId.Should().Be(query.LearnerId);
            competence.Name.Should().Be(name);
            competence.Description.Should().Be(description);
            competence.IsActive.Should().BeFalse();
            competence.IsRequired.Should().BeFalse();
            competence.NumberOfActiveHabits.Should().Be(0);
            competence.NumberOfDoneHabits.Should().Be(0);
            competence.NumberOfHoldedHabits.Should().Be(0);

        }

        [Fact]
        public async void CompetenceRenamed_QueryCompetenceDTOWithNewName()
        {
            //Given
            var competenceId = new CompetenceId();
            var learnerId = new LearnerId();
            var newName = new CompetenceName("Sprawność");

            var initialEvents = new IDomainEvent[]
            {
                new CompetenceDefined(learnerId, competenceId, new CompetenceName("Forma"), "", false),
                new CompetenceRenamed(learnerId, competenceId, newName)
            };

            using (var session = store.OpenSession())
            {
                session.Events.StartStream(competenceId.Id, initialEvents);
                session.SaveChanges();
            }

            var query = new CompetenceQuery(competenceId.Id, learnerId.Id);

            var queryHandler = new CompetenceQueryHandler(store);

            //when
            var competence = await queryHandler.Handle(query, new CancellationToken());

            //than
            competence.Name.Should().Be(newName.Value);




        }

        [Fact]
        public async void QueryReturnNull_Throws_NotFoundException()
        {
            //given            
            var competenceId = new CompetenceId();
            var learnerId = new LearnerId();

            using (var session = store.OpenSession())
            {
                session.Events.StartStream(competenceId.Id, new IDomainEvent[] { new CompetenceDefined(learnerId, competenceId, new CompetenceName("Komunikacja"), "", false) });
                session.SaveChanges();
            }

            var query = new CompetenceQuery(new CompetenceId().Id, learnerId.Id);

            var queryHandler = new CompetenceQueryHandler(store);

            //when
            Func<Task> action = async () => await queryHandler.Handle(query, new CancellationToken());

            //than
            action.Should().ThrowExactly<KeyNotFoundException>().WithMessage($"Competence with id: {query.CompetenceId} of user account Id: {query.LearnerId} not found");

        }

        public void Dispose()
        {
            store.Advanced.Clean.CompletelyRemoveAll();
        }
    }
}
