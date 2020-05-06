using FluentAssertions;
using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Infrastructure.Persistance;
using Marten;
using System;
using System.Collections.Generic;
using Xunit;



namespace CompetencesIntegrationTests.repositories
{

    public class CompetenceRepoTest : IClassFixture<MartenStoreTestFactory>, IDisposable
    {

        IDocumentStore store;
        MartenDataStore dbContext;


        public CompetenceRepoTest(MartenStoreTestFactory competencesStore)
        {
            this.store = competencesStore.Store;
            this.dbContext = new MartenDataStore(this.store, new TestMediator());
        }


        [Fact]
        public async void AddCompetence()
        {
            //Given
            var competenceRepo = dbContext.Competences;

            var competence = new Competence(
                competenceRepo.NextIdentity(),
                new CompetenceName("Uprzejmość"),
                new LearnerId(),
                "",
                false
                );


            //When
            using (dbContext)
            {
                competenceRepo.Add(competence);
                await dbContext.CommitChanges();
            }


            //Then
            var resultCompetence = await competenceRepo.CompetenceOfId(competence.Id as CompetenceId);

            resultCompetence.Id.Should().Be(competence.Id);
            resultCompetence.GetSnapshot().Name.Should().Be(competence.GetSnapshot().Name);


        }

        [Fact]
        public void Update_ClearsEvents()
        {

            var competenceRepo = dbContext.Competences;

            var competence = new Competence(
                competenceRepo.NextIdentity(),
                new CompetenceName("Uprzejmość"),
                new LearnerId(),
                "",
                false
                );

            //when
            competenceRepo.Update(competence);

            //than
            competence.GetUncommittedEvents().Should().BeEmpty();


        }


        [Fact]
        public async void UpdateCompetence()
        {
            //Given
            var competenceRepo = dbContext.Competences;

            var competence = new Competence(
                competenceRepo.NextIdentity(),
                new CompetenceName("Uprzejmość"),
                new LearnerId(),
                "",
                false
                );

            competenceRepo.Add(competence);
            competence.ClearUncommittedEvents();

            //When
            competence.MarkAsRequired();
            competenceRepo.Update(competence);
            using (dbContext)
            {
                await dbContext.CommitChanges();
            };

            //Then
            var updatedCompetence = await competenceRepo.CompetenceOfId(competence.CompetenceId);

            updatedCompetence.IsRequired.Should().BeTrue();

        }


        public void Dispose()
        {
            this.store.Advanced.Clean.CompletelyRemoveAll();
            this.dbContext.Dispose();
        }

    }

    public class EventDispatcherTest : IClassFixture<MartenStoreTestFactory>, IDisposable
    {

        IDocumentStore store;

        public EventDispatcherTest(MartenStoreTestFactory testStore)
        {
            this.store = testStore.Store;
        }

        [Fact]
        public async void DispatchEvents()
        {
            //given
            var mediator = new TestMediator();
            var dbContext = new MartenDataStore(store, mediator);
            var competenceId = new CompetenceId();
            var learnerId = new LearnerId();
            var inictialEvents = new List<IDomainEvent>
            {
                new CompetenceDefined(learnerId, competenceId, new CompetenceName("Uprzejmość"), "", false),
                new CompetenceMarkedAsRequired(competenceId, learnerId)
            };

            var competence = new Competence();
            competence.LoadEvents(inictialEvents);

            //when
            using (dbContext)
            {
                dbContext.Competences.Add(competence);
                await dbContext.CommitChanges();
            }

            //than
            mediator.PublishedEvents.Should().Equal(inictialEvents);


        }

        public void Dispose()
        {
            store.Advanced.Clean.CompletelyRemoveAll();
        }
    }
}
