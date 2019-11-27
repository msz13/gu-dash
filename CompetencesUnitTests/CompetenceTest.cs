namespace GudAsh.Competences.CompetencesUnitTests {

    using System;
    using Xunit;
    using GuDash.Competences.API.Domain.Competences;
    using GuDash.Competences.Service.Domain.Learner;
    using System.Collections.Generic;
    using GuDash.Common.Domain.Model;
    using GuDash.Competences.API.Domain.Competences.Events;
    using GuDash.Competences.Service.Domain.Competences.Events;
    using FluentAssertions;
    using GuDash.Competences.Service.Domain.Habit;
    using GuDash.Competences.Service.Application;
    using GuDash.Competences.Service.Domain.Competences;

    public class CompetenceTestFactory
    {
        public static Competence Create()
        {
            var snapshot = new CompetenceSnapshot(
                new CompetenceId(Guid.NewGuid()),
                new LearnerId(Guid.NewGuid()),
                "Sprawnoœæ fizyczna",
                "",
                false,
                false,
                new List<CompetenceHabit> { });

            return Competence.LoadFromSnapshot(snapshot);
        }
    }
    public class CompetenceTest
    {
        
        [Fact]
        public void CreateFromConstructor_Return_Events()
        {

            string name = "Mêstwo";
            CompetenceId id = new CompetenceId(Guid.NewGuid());
            LearnerId learner = new LearnerId(Guid.NewGuid());
            string description = "Zwykle";

            var competence = new Competence(
                id,
                name,
                learner,
                description,
                false
                );

            Assert.NotNull(competence);

            var events = competence.GetChanges();
            Assert.Equal(1, events.Count);

            var competenceAdded = events[0] as CompetenceAdded;
            Assert.Equal(id.Id, competenceAdded.CompetenceId.Id);
            Assert.Equal(name, competenceAdded.Name);
            Assert.Equal(learner.Id, competenceAdded.LearnerId.Id);
            Assert.Equal(description, competenceAdded.Description);

        }
        [Fact]
        public void createFromEvents_return_Snapshot()
        {
            var competenceAdded = new CompetenceAdded(
                new LearnerId(Guid.NewGuid()),
                new CompetenceId(Guid.NewGuid()),
                "Sprawnoœæ fizyczna",
                "Bardzo wa¿na",
                false                               
            );

            var eventStream = new List<IDomainEvent>
            {
                competenceAdded
            };

            var expectedSnapshot = new CompetenceSnapshot
                (
                competenceAdded.CompetenceId,
                competenceAdded.LearnerId,
                competenceAdded.Name,
                competenceAdded.Description,
                false,
                false,
                new List<CompetenceHabit> { }
                );

            //var competence = Competence.LoadFromEvents(eventStream, 0);

            var competence = Competence.LoadFromEvents(eventStream, 0);


            Assert.Equal(expectedSnapshot.CompetenceId, competence.GetSnapshot().CompetenceId);
            Assert.Equal(expectedSnapshot.LearnerId, competence.GetSnapshot().LearnerId);
            Assert.Equal(expectedSnapshot.Description, competence.GetSnapshot().Description);
            Assert.Equal(expectedSnapshot.Name, competence.GetSnapshot().Name);
            expectedSnapshot.CompetenceHabits.Should().BeEmpty();
            Assert.Equal(0, competence.Version);

        }
        [Fact]
        void createFromSnaphot_return_Snapshot()
        {
            var snapshot = new CompetenceSnapshot(new CompetenceId(Guid.NewGuid()), new LearnerId(Guid.NewGuid()), "Sprawnoœæ fizyczna", "", false, false, new List<CompetenceHabit> { });

            var competence = Competence.LoadFromSnapshot(snapshot);

            Assert.Equal(snapshot.Name, competence.GetSnapshot().Name);
            Assert.Equal(snapshot.CompetenceId, competence.GetSnapshot().CompetenceId);
            Assert.Equal(snapshot.LearnerId, competence.GetSnapshot().LearnerId);
            Assert.Equal(snapshot.Description, competence.GetSnapshot().Description);
            Assert.Equal(snapshot.IsActive, competence.GetSnapshot().IsActive);
            Assert.Equal(snapshot.IsRequired, competence.GetSnapshot().IsRequired);
        }
        [Fact]
        void markedAsRequired_returnIsRequiredTrue()
        {
            var competence = CompetenceTestFactory.Create();

            competence.MarkAsRequired();

            Assert.True(competence.IsRequired);
            Assert.IsType<CompetenceMarkedAsRequired>(competence.GetChanges()[0]);

        }

        void markedAsNotReqired_returnIsRequiredFalse()
        {
            var competence = CompetenceTestFactory.Create();

            competence.MarkAsRequired();
            competence.MarkAsNotRequired();

            Assert.False(competence.IsRequired);
            Assert.IsType<CompetenceMarkedAsNotRequired>(competence.GetChanges()[0]);

            competence.MarkAsNotRequired();
            Assert.Equal(1, competence.GetChanges().Count);

        }
        [Fact]
        void Handle_Hold_Habit_Added()
        {


        }
    }

    public class CompetenceInitiateCreateHabitTest
    {
        [Fact]
        public void Return_Hold_Habid()
        {
            var competence = CompetenceTestFactory.Create();

            var id = Guid.NewGuid();

            var name = "Mówiæ dzieñ dobry brudnej babie";

            var description = "";

            var target = new TargetData(TargetData.TargetType.NUMERIC, 2);
            
            var progressDays = new List <DayOfWeek>{ DayOfWeek.Saturday, DayOfWeek.Sunday };

            var notification = new CompetenceNotification();

            var habit = competence.AddHoldedHabit(id, name, description, target, progressDays, notification);

            var snapshot = habit.GetSnapshot() as HabitSnapshot;


            snapshot.Id.Id.Should().Be(id.ToString());
            snapshot.CompetenceId.Should().Be(competence.GetSnapshot().CompetenceId);
            snapshot.LearnerId.Should().Be(competence.GetSnapshot().LearnerId);
            snapshot.Status.Should().Be(Habit.Status.HOLDED);
            snapshot.Name.Should().Be(name);
            snapshot.Description.Should().Be(description);
            snapshot.Target.Should().Be(target);
            snapshot.ProgressDays.Should().Equal(progressDays); 
            
        }

        [Fact]
        public void HabbitAdded_WithSameHabit_Rasie_Event_Once()
        {
            var competence = CompetenceTestFactory.Create();

            var id = new HabitId();

            competence.HabitAdded(id, "Probny");

            var expectedEvent = competence.GetChanges()[0] as CompetenceHabitAdded;
            expectedEvent.Should().BeOfType<CompetenceHabitAdded>();
            expectedEvent.Name.Should().Be("Probny");
            expectedEvent.HabitId.Should().Be(id);


        }


        [Fact]
        public void CreateHabit_WithSameName_NotificationHasErrors()
        {
            var competence = CompetenceTestFactory.Create();
            var name = "Æwiczyc";
            var notification =  new CompetenceNotification();

            var habit = competence.AddHoldedHabit(
                Guid.NewGuid(),
                name,
                "",
                new TargetData(TargetData.TargetType.CHECKBOX, 1),
                new List<DayOfWeek> { DayOfWeek.Monday }, 
                notification);

            competence.HabitAdded(habit.GetSnapshot().Id as HabitId, name);

            var habit2 = competence.AddHoldedHabit(
                Guid.NewGuid(),
                name,
                "",
                new TargetData(TargetData.TargetType.CHECKBOX, 1),
                new List<DayOfWeek> { DayOfWeek.Monday }, 
                notification);

            habit2.Should().BeNull();
            notification.HasErrors().Should().Be(true);
            notification.Errors[0].Code.Should().Be("NON_UNIQUE_HABIT_NAME");
            notification.Errors[0].Message.Should().Contain(name);
            


        }
    }

}
