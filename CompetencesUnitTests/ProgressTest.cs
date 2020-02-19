using NodaTime;
using NodaTime.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using FluentAssertions;
using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Progress;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Progress.Events;

namespace CompetencesUnitTests
{
    public class ProgressTest
    {
        [Fact]
        public void SnapshotConstruction()
        {
            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = SystemClock.Instance.InZone(tz).GetCurrentZonedDateTime();

            var habitProgressStarted = new HabitProgressStarted(
                1,
                new ProgressId(),
                new LearnerId(),
                new HabitId(),
                new NumericTarget(3),
                startActive,
                new List<IsoDayOfWeek> { IsoDayOfWeek.Monday, IsoDayOfWeek.Tuesday });

            var progess = new Progress(habitProgressStarted.ProgressId);

            progess.LoadEvents(new List<IDomainEvent> { habitProgressStarted });

            var snapshot = progess.GetSnapshot() as ProgressSnapshot;

            snapshot.Id.Should().Be(habitProgressStarted.ProgressId);
            snapshot.Version.Should().Be(habitProgressStarted.Version);
            snapshot.StartActive.ToString().Should().Be(startActive.ToString());
            snapshot.LearnerId.Should().Be(habitProgressStarted.LearnerId);
            snapshot.HabitId.Should().Be(habitProgressStarted.HabitId);
            snapshot.Target.Should().Be(habitProgressStarted.Target);
            snapshot.ProgressDays.Should().BeEquivalentTo(new List<IsoDayOfWeek> { IsoDayOfWeek.Monday, IsoDayOfWeek.Tuesday });
            snapshot.DaysWhenTargetReached.Should().Be(0);
            snapshot.DayProgress.Should().BeNull();            
            

        }
        [Fact]
        public void WhenProgressEnd_RaiseHabitProgressEnded()
        {
            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = SystemClock.Instance.InZone(tz).GetCurrentZonedDateTime();

            var initialEvent = new HabitProgressStarted(0, new ProgressId(), new LearnerId(), new HabitId(), new NumericTarget(2), startActive, new List<IsoDayOfWeek> { IsoDayOfWeek.Monday, IsoDayOfWeek.Tuesday });

            var progress = new Progress(initialEvent.ProgressId);
            progress.LoadEvents(new List<IDomainEvent> { initialEvent } );

            var endActive = startActive.Plus(Duration.FromDays(3));
            progress.End(endActive);

            var endActiveEvent = progress.GetUncommittedEvents().Cast<object>().ToList().FirstOrDefault() as HabitProgressEnded;

            endActiveEvent.Should().BeOfType<HabitProgressEnded>();
            endActiveEvent.EndDate.ToString().Should().Be(endActive.ToString());


            

        }   
        

    }

    public class ProgressTestFactory
    {
        private const string V = "numeric";

        public static Progress Create(string targetType)
        {
            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = tz.AtStrictly(new LocalDateTime(2019, 11, 27, 0, 0));

            ITarget target;

            if (targetType == "numeric")
                target = new NumericTarget(2);
            else target = new CheckboxTarget();


            var habitProgressStarted = new HabitProgressStarted(0,
                                                                new ProgressId(),
                                                                new LearnerId(),
                                                                new HabitId(),
                                                                target,
                                                                startActive,
                                                                new List<IsoDayOfWeek> { IsoDayOfWeek.Monday, IsoDayOfWeek.Tuesday, IsoDayOfWeek.Wednesday, IsoDayOfWeek.Thursday, IsoDayOfWeek.Friday });
            var progress = new Progress(habitProgressStarted.ProgressId);
            progress.LoadEvents(new List<IDomainEvent> { habitProgressStarted });

            return progress;
        }
    }

    #region numericTatrget
    public class GivenNumericTargetWhenProgressIncrease
    {


        
        static LocalDate date = new LocalDate(2019, 12, 10);

        public GivenNumericTargetWhenProgressIncrease()
        {
            
        }

        Progress CreateHabitProgress()
        {
            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = tz.AtStrictly(new LocalDateTime(2019, 11, 27, 0, 0));
            var habitProgressStarted = new HabitProgressStarted(
                0,
                new ProgressId(),
                new LearnerId(),
                new HabitId(),
                new NumericTarget(2),
                startActive,
                new List<IsoDayOfWeek> { IsoDayOfWeek.Monday, IsoDayOfWeek.Tuesday, IsoDayOfWeek.Wednesday, IsoDayOfWeek.Thursday, IsoDayOfWeek.Friday });
            var progress = new Progress(habitProgressStarted.ProgressId);
            progress.LoadEvents(new List<IDomainEvent> { habitProgressStarted });

            return progress;
        }

        [Fact]
        public void GivenDateBeforeStartActive_Throws() 
        {
            var progress = CreateHabitProgress();

            var snapshot = progress.GetSnapshot() as ProgressSnapshot;
            var updateDate = snapshot.StartActive.Minus(Duration.FromDays(1)).Date;
            Action act = ()=> progress.Update(updateDate, 1);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>().Where(e=> e.Message.Contains("Day"));

        }

        [Fact]
        public void GivenProgressEnded_Throws()
        {
            var progress = CreateHabitProgress();
            var snapshot = progress.GetSnapshot() as ProgressSnapshot;
            var endActive = snapshot.StartActive.Plus(Duration.FromDays(1));
            var updateDate = endActive.Date;
            
            progress.End(endActive);

            Action act = ()=> progress.Update(updateDate, 2);

            act.Should().ThrowExactly<InvalidOperationException>();


        }

        [Fact]
        public void GiventNegativeValue_Throws()
        {
            var progress = ProgressTestFactory.Create("numeric");

            Action act = () => progress.Update(new LocalDate(2019, 12, 9), -1);

            act.Should().ThrowExactly<InvalidOperationException>();
        }

        [Fact]
        public void GivenDayNotInProgressDays_Throws()
        {
            var progress = ProgressTestFactory.Create("numeric");

            Action act = () => progress.Update(new LocalDate(2019, 12, 8), 1);

            act.Should().ThrowExactly<InvalidOperationException>();

        }

        [Theory]
        [MemberData(nameof(Data))]
        public void WhenUpdate_ThanRaise(IDomainEvent initialEvant, int dayProgressValue, LocalDate date, IDomainEvent expectedEvent)
        {
            var progress = ProgressTestFactory.Create("numeric");

            if(initialEvant!=null)
                progress.LoadEvents(new List<IDomainEvent> { initialEvant});

            progress.Update(date, dayProgressValue);

            var outcomeEvent = progress.GetUncommittedEvents().Cast<object>().ToList().FirstOrDefault();

            
            var expectedType = expectedEvent.GetType();

            outcomeEvent.Should().BeOfType(expectedType);

            var dayProgressProp = expectedType.GetProperty("DayProgressValue");
            var progressVal = dayProgressProp.GetValue(outcomeEvent,null);
            var expVal = dayProgressProp.GetValue(expectedEvent, null);

            progressVal.Should().Be(expVal);


            if (expectedType == typeof(HabitProgressDayTargetAchieved))
            {
                var daysOnTargetProp = expectedType.GetProperty("DaysWhenTargetReached");
                var daysOnTarget = daysOnTargetProp.GetValue(outcomeEvent, null);
                var expDaysOnTarget = daysOnTargetProp.GetValue(expectedEvent, null);

                daysOnTarget.Should().Be(expDaysOnTarget);
            }
                 
            progress.ClearUncommittedEvents();         
                         
                         

            }

       

        public static IEnumerable<object[]> Data ()
        {

            yield return new object[]
          {
              null,  
              1,
                date,
                new HabitDayProgressUpdated(new ProgressId(), new LearnerId(), new HabitId(), date, 1),

          };
            yield return new object[]
            {
                new HabitDayProgressUpdated(new ProgressId(), new LearnerId(), new HabitId(), date, 1),
                2,
                date,
                new HabitProgressDayTargetAchieved(new ProgressId(), new LearnerId(), new HabitId(), date, 2, 1)             

            };                  

            
            yield return new object[]
            {
                new HabitProgressDayTargetAchieved(new ProgressId(), new LearnerId(), new HabitId(), date, 2, 1),
                3,
                date,
                new HabitDayProgressUpdated(new ProgressId(),  new LearnerId(), new HabitId(), date, 3)
            };

            yield return new object[]
           {
                new HabitProgressDayTargetAchieved(new ProgressId(), new LearnerId(), new HabitId(), date, 2, 1),
                1,
                date,
                new HabitProgressDayTargetAchievementCancelled(new ProgressId(),  new LearnerId(), new HabitId(), date, 1, 0)
           };

            yield return new object[]
          {
                new HabitProgressDayTargetAchieved(new ProgressId(), new LearnerId(), new HabitId(), date, 2, 1),
                3,
                date.PlusDays(1),
                new HabitProgressDayTargetAchieved(new ProgressId(),  new LearnerId(), new HabitId(), date.PlusDays(1), 3, 2)
          };
        }

    }

    #endregion
    public class GivenCheckboxTarget_WhenTargetUpdate
    {
       
        [Fact]
        public void GivenValueOne_RaiseTargetAchieved()
        {
            var progress = ProgressTestFactory.Create("checkbox");

            progress.Update(new LocalDate(2019, 12, 8), 1);

            var resultEvent = progress.GetUncommittedEvents().Cast<HabitProgressDayTargetAchieved>().ToList().FirstOrDefault();

            resultEvent.Should().BeOfType<HabitProgressDayTargetAchieved>();

            resultEvent.DayProgressValue.Should().Be(1);

            resultEvent.DaysWhenTargetReached.Should().Be(1);           


        }

        [Fact]
        public void GivenValueTwo_Throws()
        {
            var progress = ProgressTestFactory.Create("checkbox");

            Action act = () => progress.Update(new LocalDate(2019, 12, 8), 2);

            act.Should().ThrowExactly<InvalidOperationException>();
        }
    }
}
