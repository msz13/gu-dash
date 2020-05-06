using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;

namespace GuDash.CompetencesService.Domain.Habit
{
    interface IHabitIdentity
    {
        LearnerId LearnerId { get; }

        CompetenceId CompetenceId { get; }

        HabitId HabitId { get; }

    }
}
