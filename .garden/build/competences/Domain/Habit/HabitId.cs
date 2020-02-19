using System;
using GuDash.Common.Domain.Model;

namespace GuDash.CompetencesService.Domain.Habit
{
    public class HabitId : Identity
    {
        public HabitId() : base() { }

        public HabitId(Guid id) : base(id) { }

    }
}