using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Habit
{
    interface IHabitIdentity
    {
       LearnerId LearnerId { get;  }

        CompetenceId CompetenceId { get;  }

        HabitId HabitId { get; }

    }
}
