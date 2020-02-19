using GuDash.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using FluentAssertions;
using AutoFixture;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Domain.Learner.Events;
using GuDash.CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Application.CommandHandlers;
using CSharpFunctionalExtensions;
using CompetencesService.Application.CommandHandlers;


namespace CompetencesUnitTests
{
    public class LearnerTest
    {
        Learner LearnerFactory()
        {
            var id = new LearnerId(Guid.NewGuid());
            var eventsStream = new List<IDomainEvent>
            {
                new LearnerCreated(id, "Europe/Warsaw")
        };
            var learner = new Learner();
            learner.LoadEvents(eventsStream);

            return learner;
        }

        [Fact]
        public void Create_From_Constructor_Return_Events()
        {
            var id = new LearnerId(Guid.NewGuid());
            var timezone = "Europe/Warsaw";
            var learner = new Learner(id, timezone);

            var events = learner.GetUncommittedEvents();

            events.Should().HaveCount(1).And.ContainEquivalentOf(new LearnerCreated(id, timezone));
                     

        }

        [Fact]
        public void Create_From_Events()
        {
            var learnerEvent = new List<IDomainEvent>
            {
                new LearnerCreated(new LearnerId(Guid.NewGuid()), "Erope/Warsaw")
        };


            var learner = new Learner();
            learner.LoadEvents(learnerEvent);
            learner.GetUncommittedEvents().Should().HaveCount(0);
            learner.Version.Should().Be(1);
        }
        /*
        [Fact]
        public void Create_From_Snapshot()
        {
            var snaphot = new LearnerSnapshot(new LearnerId(Guid.NewGuid()),  "UTC");

            var learner = Learner.LoadFromSnapshot(snaphot);

            Assert.Equal(snaphot.LearnerId, learner.GetSnapshot().LearnerId);
            Assert.Equal(snaphot.Email, learner.GetSnapshot().Email);
            Assert.Equal(snaphot.TimeZoneId, learner.GetSnapshot().TimeZoneId);
        } */

        [Fact]
        public void Create_Competence()
        {
            var learner = LearnerFactory();

            var id = new CompetenceId(Guid.NewGuid());
            var name = "Rozwaga";
            var description = "wieloraka";
            var isRequired = false;

            var result = learner.DefineCompetence(id, name, description, isRequired);

            result.IsSuccess.Should().BeTrue();

            var competence = result.Value;

            competence.GetUncommittedEvents().Should()
                .HaveCount(1);
                //.And
                //.StartWith(new CompetenceDefined(learner.LearnerId, id, name, description, isRequired));            
          


        }

        [Fact]
        public void CompetenceInitiated_ValidArguments_AdsLernerCompetence()
        {
            var learner = LearnerFactory();
            
            var competenceId = new CompetenceId(Guid.NewGuid());

            
            learner.CompetenceDefined(competenceId, "Uprzejmosc", false);

            //var snapshot = learner.GetSnapshot() as LearnerSnapshot;

            var uncommittedEvents = learner.GetUncommittedEvents();

            uncommittedEvents.Should().HaveCount(1).And.ContainEquivalentOf(new LearnerCompetenceDefined(learner.LearnerId, competenceId, "Uprzejmosc", false));


        }
        
        [Fact]
        public void When_Add_Competence_Same_Name_Throw()
        {
            var learnerId = new LearnerId(Guid.NewGuid());
            var competenceId = new CompetenceId(Guid.NewGuid());
            
            var learnerCompetenceDefined = new LearnerCompetenceDefined(learnerId, competenceId,"Uprzejmość", false);

            var eventsStream = new List<IDomainEvent>
            {
                new LearnerCreated(learnerId, "Europe/Warsaw"),
                learnerCompetenceDefined
            };

            var learner = new Learner();
            learner.LoadEvents(eventsStream);
            var result = learner.DefineCompetence(learnerCompetenceDefined.CompetenceId, learnerCompetenceDefined.Name, "", learnerCompetenceDefined.IsRequired);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("NON_UNIQUE_COMPETENCE_NAME", "Competence with name Uprzejmość already exists"));
                                             
        } 

        [Fact]
        public void InitiateCompetence_NullName_Throws()
        {
            var learner = LearnerFactory();

            Action action = ()=>learner.DefineCompetence(new CompetenceId(Guid.NewGuid()), "", "", false);

            action.Should().Throw<InvalidOperationException>();


        }

        [Fact]
        public void CompetenceInitiated_SameNameOrID_NotChange()
        {
            var learnerId = new LearnerId(Guid.NewGuid());
            var competenceId = new CompetenceId(Guid.NewGuid());

            var learnerCompetenceDefined = new LearnerCompetenceDefined(learnerId, competenceId, "Uprzejmość", false);

            var eventsStream = new List<IDomainEvent>
            {
                new LearnerCreated(learnerId, "Europe/Warsaw"),
                learnerCompetenceDefined
            };


            var learner = new Learner();
            learner.LoadEvents(eventsStream);

            

            learner.CompetenceDefined(learnerCompetenceDefined.CompetenceId, "Fizyczna", learnerCompetenceDefined.IsRequired);
            //Assert.Single(learner.GetSnapshot().LearnerCompetences);

            learner.CompetenceDefined(new CompetenceId(Guid.NewGuid()), learnerCompetenceDefined.Name, learnerCompetenceDefined.IsRequired);
            //Assert.Single(learner.GetSnapshot().LearnerCompetences);


            learner.CompetenceDefined(learnerCompetenceDefined.CompetenceId, learnerCompetenceDefined.Name, learnerCompetenceDefined.IsRequired);
            //Assert.Single(learner.GetSnapshot().LearnerCompetences);

        }
            
        }
    
}
