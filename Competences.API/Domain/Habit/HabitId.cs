using System;
using GuDash.Common.Domain.Model;

namespace GuDash.Competences.Service.Domain.Habit
{
    public class HabitId : Identity
    {
        public HabitId() : base() { }

        public HabitId(Guid id) : base(id) { }

    }
}