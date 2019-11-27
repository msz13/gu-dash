using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using GuDash.Competences.Service.Domain.Progress;
using NodaTime;
using NodaTime.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using FluentAssertions;
using GuDash.Common.Domain.Model;
using GuDash.Competences.Service.Domain.Progress.Events;

namespace CompetencesUnitTests
{
    public class ProgressTest
    {
        [Fact]
        public void SnapshotConstruction()
        {
            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = SystemClock.Instance.InZone(tz).GetCurrentZonedDateTime();

            var habitProgressStarted = new HabitProgressStarted(1, new ProgressId(), new LearnerId(), new HabitId(), new NumericTarget(3), startActive);

            var progess = new Progress(habitProgressStarted.ProgressId);

            progess.ApplyEvent(habitProgressStarted);

            var snapshot = progess.GetSnapshot() as ProgressSnapshot;

            snapshot.Id.Should().Be(habitProgressStarted.ProgressId);
            snapshot.Version.Should().Be(habitProgressStarted.Version);
            snapshot.StartActive.ToString().Should().Be(startActive.ToString());
            snapshot.LearnerId.Should().Be(habitProgressStarted.LearnerId);
            snapshot.HabitId.Should().Be(habitProgressStarted.HabitId);
            snapshot.Target.Should().Be(habitProgressStarted.Target);
            snapshot.DaysWhenTargetReached.Should().Be(0);

            
            

        }
        
        

    }

    public class WhenProgressIncrease
    {

        [Fact]
        public void GivenNumericTarget_ThanRaiseTargetAchieved()
        {
            //Given Progress started
            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = SystemClock.Instance.InZone(tz).GetCurrentZonedDateTime();
            var habitProgressStarted = new HabitProgressStarted(0, new ProgressId(), new LearnerId(), new HabitId(), new NumericTarget(2), startActive);
            var progress = new Progress(habitProgressStarted.ProgressId);
            progress.ApplyEvent(habitProgressStarted);

            var progressDate = new LocalDate(2019, 11, 25);
            var progressValue = 2;

            progress.Increase(progressDate, 2);

            var dayTargetAchieved = progress.GetUncommittedEvents().Cast<object>().ToList()[0] as HabitProgressDayTargetAchieved;

            dayTargetAchieved.Date.Should().Be(progressDate);
            dayTargetAchieved.DayProgressValue.Should().Be(progressValue);
            dayTargetAchieved.DaysWhenTargetReached.Should().Be(1);
                          

            }

        [Fact]
        public void GivenNumericTarget_ThanRaiseDayProgressUpdated()
        {
            //Given Progress started
            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = SystemClock.Instance.InZone(tz).GetCurrentZonedDateTime();
            var habitProgressStarted = new HabitProgressStarted(0, new ProgressId(), new LearnerId(), new HabitId(), new NumericTarget(2), startActive);
           
            var progress = new Progress(habitProgressStarted.ProgressId);
            progress.ApplyEvent(habitProgressStarted);

            var progressDate = new LocalDate(2019, 11, 25);
            var progressValue = 1;

            progress.Increase(progressDate, progressValue);

            var dayProgressUpdated = progress.GetUncommittedEvents().Cast<object>().ToList()[0] as HabitDayProgressUpdated;

            dayProgressUpdated.Date.Should().Be(progressDate);
            dayProgressUpdated.DayProgressValue.Should().Be(progressValue);
            


        }

    }
}
