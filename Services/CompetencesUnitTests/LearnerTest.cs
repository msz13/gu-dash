using CompetencesService.Application.CommandHandlers;
using CompetencesService.Domain.Competences;
using FluentAssertions;
using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

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




    }


    public class CreateCompetenceTest
    {
        [Fact]
        public void Create_Competence()
        {
            var learner = CreateLearner();
            var newName = "Rozwaga";
            var uniquNameSvc = new Mock<ICompetenceUniqueNameService>();
            uniquNameSvc.Setup(s => s.IsUniqueName(It.IsAny<LearnerId>(), It.Is<CompetenceName>(n => n.Value == newName))).Returns(Task.FromResult(true));

            var result = learner.DefineCompetence(new CompetenceId(), newName, "", false, uniquNameSvc.Object);

            result.IsSuccess.Should().BeTrue();

            var competence = result.Value;

            competence.GetUncommittedEvents().Should()
                .HaveCount(1);
            //.And
            //.StartWith(new CompetenceDefined(learner.LearnerId, id, name, description, isRequired));            



        }


        [Fact]
        public void When_Add_Competence_Same_Name_Throw()
        {
            var learner = CreateLearner();
            var newName = "Rozwaga";
            var uniquNameSvc = new Mock<ICompetenceUniqueNameService>();
            uniquNameSvc.Setup(s => s.IsUniqueName(It.IsAny<LearnerId>(), It.Is<CompetenceName>(n => n.Value == newName))).Returns(Task.FromResult(false));

            var result = learner.DefineCompetence(new CompetenceId(), newName, "", false, uniquNameSvc.Object);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(new Error("NON_UNIQUE_COMPETENCE_NAME", "Competence with name Rozwaga already exists"));

        }

        [Fact]
        public void InitiateCompetence_NullName_Throws()
        {
            var learner = CreateLearner();
            var newName = "";
            var uniquNameSvc = new Mock<ICompetenceUniqueNameService>();
            uniquNameSvc.Setup(s => s.IsUniqueName(It.IsAny<LearnerId>(), It.Is<CompetenceName>(n => n.Value == newName))).Returns(Task.FromResult(true));

            Action action = () => learner.DefineCompetence(new CompetenceId(Guid.NewGuid()), "", "", false, uniquNameSvc.Object);

            action.Should().Throw<InvalidOperationException>();

        }

        private Learner CreateLearner()
        {
            var learnerId = new LearnerId(Guid.NewGuid());
            var competenceId = new CompetenceId(Guid.NewGuid());
            var eventsStream = new List<IDomainEvent> { new LearnerCreated(learnerId, "Europe/Warsaw") };
            var learner = new Learner();
            learner.LoadEvents(eventsStream);

            return learner;
        }
    }


}
