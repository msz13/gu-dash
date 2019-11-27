using GuDash.Competences.Service.Domain.Habit;
using GuDash.Competences.Service.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Domain.Progress
{
    interface IProgressIdentity
    {
        ProgressId ProgressId { get; }

        LearnerId LearnerId { get; }

        HabitId HabitId { get; }
    }
}
