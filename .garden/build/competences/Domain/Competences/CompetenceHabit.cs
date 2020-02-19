using GuDash.Common.Domain.Model;
using GuDash.CompetencesService.Domain.Habit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Competences
{
    public class CompetenceHabit 
    {
        public CompetenceHabit(HabitId id, string name)
        {
            Id = id;
            Name = name;
        }

        public HabitId Id { get; private set; }

        public string Name { get; private set; }

        
    }
}
