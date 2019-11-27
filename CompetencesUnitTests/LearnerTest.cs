using GuDash.Common.Domain.Model;
using GuDash.Competences.API.Domain.Competences;
using GuDash.Competences.Service.Domain.Learner;
using GuDash.Competences.Service.Domain.Competences.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using GuDash.Competences.Service.Domain.Learner.Events;

namespace CompetencesUnitTests
{
    public class LearnerTest
    {
        Learner LearnerFactory()
        {
            var id = new LearnerId(Guid.NewGuid());
            var eventsStream = new List<LearnerCreated>
            {
                new LearnerCreated(id, "a@a.pl", "Europe/Warsaw")
        };
            return Learner.LoadFromEvents(eventsStream, 0);
        }
        [Fact]
        public void Create_From_Constructor_Return_Events()
        {
            var id = new LearnerId(Guid.NewGuid());
            var email = "a@a.pl";
            var timezone = "Europe/Warsaw";
            var learner = new Learner(id, email, timezone);

            var events = learner.GetChanges();

            var learnerCreated = events[0] as LearnerCreated;


            Assert.Equal(1, events.Count);
            Assert.Equal(id.Id, learnerCreated.LearnerId);
            Assert.Equal(email, learnerCreated.Email);
            Assert.Equal(timezone, learnerCreated.TimeZoneId);


        }
        [Fact]
        public void Create_From_Events()
        {
            var learnerEvent = new List<IDomainEvent>
            {
                new LearnerCreated(new LearnerId(Guid.NewGuid()), "a@a.pl", "Erope/Warsaw")
        };


            var learner = Learner.LoadFromEvents(learnerEvent, 0);
            Assert.Equal(0, learner.GetChanges().Count);
            Assert.Equal(0, learner.Version);
        }

        [Fact]
        public void Create_From_Snapshot()
        {
            var snaphot = new LearnerSnapshot(new LearnerId(Guid.NewGuid()), "Umiarkowanie", "UTC");

            var learner = Learner.LoadFromSnapshot(snaphot);

            Assert.Equal(snaphot.LearnerId, learner.GetSnapshot().LearnerId);
            Assert.Equal(snaphot.Email, learner.GetSnapshot().Email);
            Assert.Equal(snaphot.TimeZoneId, learner.GetSnapshot().TimeZoneId);
        }

        [Fact]
        public void Create_Competence()
        {
            var learner = LearnerFactory();

            var id = new CompetenceId(Guid.NewGuid());
            var name = "Rozwaga";
            var description = "wieloraka";
            var isRequired = false;

            var competence = learner.InitiateCompetence(id, name, description, isRequired);

            var competenceSnapshot = competence.GetSnapshot();


            Assert.Equal(id, competenceSnapshot.CompetenceId);
            Assert.Equal(name, competenceSnapshot.Name);
            Assert.Equal(description, competenceSnapshot.Description);
            Assert.Equal(isRequired, competenceSnapshot.IsRequired);


        }

        [Fact]
        public void CompetenceInitiated_ValidArguments_AdsLLernerCompetence()
        {
            var learner = LearnerFactory();

            var competenceId = new CompetenceId(Guid.NewGuid());

            var competenceCreated = new CompetenceAdded(
                learner.GetSnapshot().LearnerId,
                competenceId,
                "Roztropność",
                "",
                false
                );

            learner.CompetenceInitiated(competenceCreated.CompetenceId, competenceCreated.Name, competenceCreated.IsRequired);

            var snapshot = learner.GetSnapshot();

            var learnerCompetenceInitiated = learner.GetChanges()[0] as LearnerCompetenceInitiated;

            Assert.Single(snapshot.LearnerCompetences);
                      
            Assert.Equal(competenceId, learnerCompetenceInitiated.CompetenceId);

            learner.CompetenceInitiated(new CompetenceId(Guid.NewGuid()), "Uprzejmość", learnerCompetenceInitiated.IsRequired);
            Assert.Equal(2, learner.GetSnapshot().LearnerCompetences.Count);


        }

        [Fact]
        public void When_Add_Competence_Same_Name_Throw()
        {
            var learnerId = new LearnerId(Guid.NewGuid());
            var competenceId = new CompetenceId(Guid.NewGuid());
            
            var learnerCompetenceInitiated = new LearnerCompetenceInitiated(learnerId, competenceId,"Uprzejmość", false);

            var eventsStream = new List<IDomainEvent>
            {
                new LearnerCreated(learnerId, "a@a.pl", "Europe/Warsaw"),
                learnerCompetenceInitiated
            };

            var learner = Learner.LoadFromEvents(eventsStream, 0);
            Action initCompetence = ()=> learner.InitiateCompetence(learnerCompetenceInitiated.CompetenceId, learnerCompetenceInitiated.Name, "", learnerCompetenceInitiated.IsRequired);
            Assert.Throws<LearnerErrors>(initCompetence);           
                                             
        }   

        [Fact]
        public void InitiateCompetence_NullName_Throws()
        {
            var learner = LearnerFactory();

            learner.InitiateCompetence(new CompetenceId(Guid.NewGuid()), "They should be handled differently. For example client app request errors should be logged because for example client side validation should be improved", "", false);
        }

        [Fact]
        public void CompetenceInitiated_SameNameOrID_NotChange()
        {
            var learnerId = new LearnerId(Guid.NewGuid());
            var competenceId = new CompetenceId(Guid.NewGuid());

            var learnerCompetenceInitiated = new LearnerCompetenceInitiated(learnerId, competenceId, "Uprzejmość", false);

            var eventsStream = new List<IDomainEvent>
            {
                new LearnerCreated(learnerId, "a@a.pl", "Europe/Warsaw"),
                learnerCompetenceInitiated
            };

            
            var learner = Learner.LoadFromEvents(eventsStream, 0);

            Assert.Single(learner.GetSnapshot().LearnerCompetences);

            learner.CompetenceInitiated(learnerCompetenceInitiated.CompetenceId, "Fizyczna", learnerCompetenceInitiated.IsRequired);
            Assert.Single(learner.GetSnapshot().LearnerCompetences);

            learner.CompetenceInitiated(new CompetenceId(Guid.NewGuid()), learnerCompetenceInitiated.Name, learnerCompetenceInitiated.IsRequired);
            Assert.Single(learner.GetSnapshot().LearnerCompetences);


            learner.CompetenceInitiated(learnerCompetenceInitiated.CompetenceId, learnerCompetenceInitiated.Name, learnerCompetenceInitiated.IsRequired);
            Assert.Single(learner.GetSnapshot().LearnerCompetences);

        }
            
        }
    
}
