namespace GudAsh.Competences.CompetencesUnitTests {

    using System;
    using Xunit;
    using System.Collections.Generic;
    using GuDash.Common.Domain.Model;
    using GuDash.Competences.Service.Application;
    using GuDash.CompetencesService.Domain.Competences;
    using GuDash.CompetencesService.Domain.Learner;
    using FluentAssertions;
    using GuDash.CompetencesService.Domain.Habit;
    using GuDash.CompetencesService.Domain.Competences.Events;
    using System.Linq;

    public class CompetenceTestFactory
    {
        public static Competence Create()
        {
            var snapshot = new CompetenceSnapshot(
                new CompetenceId(Guid.NewGuid()),
                new LearnerId(Guid.NewGuid()),
                0,
                "Sprawno�� fizyczna",
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

            string name = "M�stwo";
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

            var events = competence.GetUncommittedEvents();
            Assert.Single(events.ToList());

            var competenceAdded = events.Cast<object>().ToList().FirstOrDefault() as CompetenceDefined;
            Assert.Equal(id.Id, competenceAdded.CompetenceId.Id);
            Assert.Equal(name, competenceAdded.Name);
            Assert.Equal(learner.Id, competenceAdded.LearnerId.Id);
            Assert.Equal(description, competenceAdded.Description);

        }
        
        public void createFromEvents_return_Snapshot()
        {
            var competenceAdded = new CompetenceDefined(
                new LearnerId(Guid.NewGuid()),
                new CompetenceId(Guid.NewGuid()),
                "Sprawno�� fizyczna",
                "Bardzo wa�na",
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
                0,
                competenceAdded.Name,
                competenceAdded.Description,
                false,
                false,
                new List<CompetenceHabit> { }
                );

            


            var competence = new Competence();

            competence.LoadEvents(eventStream);


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
            var snapshot = new CompetenceSnapshot(new CompetenceId(Guid.NewGuid()), new LearnerId(Guid.NewGuid()), 0, "Sprawno�� fizyczna", "", false, false, new List<CompetenceHabit> { });

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
            Assert.IsType<CompetenceMarkedAsRequired>(competence.GetUncommittedEvents().Cast<object>().ToList().FirstOrDefault());

        }

        void markedAsNotReqired_returnIsRequiredFalse()
        {
            var competence = CompetenceTestFactory.Create();

            competence.MarkAsRequired();
            competence.MarkAsNotRequired();

            Assert.False(competence.IsRequired);
            Assert.IsType<CompetenceMarkedAsNotRequired>(competence.GetUncommittedEvents().As<List<IDomainEvent>>()[0]);

            competence.MarkAsNotRequired();
            Assert.Single(competence.GetUncommittedEvents().ToList());

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

            var name = "M�wi� dzie� dobry brudnej babie";

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

            var expectedEvent = competence.GetUncommittedEvents().Cast<object>().ToList().FirstOrDefault()  as CompetenceHabitAdded;
            expectedEvent.Should().BeOfType<CompetenceHabitAdded>();
            expectedEvent.Name.Should().Be("Probny");
            expectedEvent.HabitId.Should().Be(id);


        }


        [Fact]
        public void CreateHabit_WithSameName_NotificationHasErrors()
        {
            var competence = CompetenceTestFactory.Create();
            var name = "�wiczyc";
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
