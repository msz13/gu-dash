using FluentAssertions;
using GuDash.CompetencesService.Domain.Competences;
using System;

namespace CompetencesUnitTests
{
    public class ShouldRiseEventsTests
    {
        public void Same_Type_Should_Pass()
        {
            var competence = new Competence(new CompetenceId(), "dfdg", new GuDash.CompetencesService.Domain.Learner.LearnerId(), "");

            Action action = () => competence.Should().RiseEvents;

            action.Should().NotThrow();
        }
    }
}
