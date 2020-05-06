using GuDash.CompetencesService.Domain.Habit;

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
