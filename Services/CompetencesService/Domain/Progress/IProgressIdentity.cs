using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;

namespace GuDash.CompetencesService.Domain.Progress
{
    interface IProgressIdentity
    {
        ProgressId ProgressId { get; }

        LearnerId LearnerId { get; }

        HabitId HabitId { get; }
    }
}
