using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using NodaTime;
using NodaTime.Extensions;
using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Progress;

namespace CompetencesUnitTests
{
    public class HabitTest
    {
        [Fact]
        public void Activate_Return_Active_Habit()
        {
            var habit = new Habit(new HabitId(),new LearnerId(), new CompetenceId(), "Ćwiczyć", "", 
                new TargetData(TargetData.TargetType.CHECKBOX, 1), new List<DayOfWeek> { DayOfWeek.Monday});

            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = SystemClock.Instance.InZone(tz).GetCurrentZonedDateTime();

            habit.Activate();

            var snapshot = habit.GetSnapshot() as HabitSnapshot;

            //TODO check if event raised var raisedEvent = habit.

            snapshot.Status.Should().Be(Habit.Status.ACTIVE);            


        }

        [Fact]
        public void StartHabitProgress_returnProgress()
        {

            var habit = new Habit(new HabitId(), new LearnerId(), new CompetenceId(), "Ćwiczyć", "",
               new TargetData(TargetData.TargetType.CHECKBOX, 1), new List<DayOfWeek> { DayOfWeek.Monday });

            DateTimeZone tz = DateTimeZoneProviders.Tzdb["Europe/Warsaw"];
            var startActive = SystemClock.Instance.InZone(tz).GetCurrentZonedDateTime();
            var id = new ProgressId();
                      

        }

    }
}
