using GuDash.CompetencesService.Domain.Habit;
using GuDash.CompetencesService.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Progress
{
    interface IProgressIdentity
    {
        ProgressId ProgressId { get; }

        LearnerId LearnerId { get; }

        HabitId HabitId { get; }
    }
}
