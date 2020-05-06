using CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Proto;
using Marten.Events.Projections;

namespace CompetencesService.Application.ReadModel
{
    public class CompetenceViewProjection : ViewProjection<CompetenceDTO, string>
    {
        public CompetenceViewProjection()
        {
            ProjectEvent<CompetenceDefined>(e => e.CompetenceId.Id, Persist);
            ProjectEvent<CompetenceRenamed>(e => e.CompetenceId.Id, Persist);
        }

        private void Persist(CompetenceDTO competence, CompetenceDefined evt)
        {
            competence.Id = evt.CompetenceId.Id;
            competence.Name = evt.Name.Value;
            competence.LearnerId = evt.LearnerId.Id;
            competence.Description = evt.Description;
            competence.IsRequired = evt.IsRequired;
            competence.NumberOfActiveHabits = 0;
            competence.NumberOfHoldedHabits = 0;
            competence.NumberOfDoneHabits = 0;
        }

        private void Persist(CompetenceDTO competence, CompetenceRenamed evt)
        {
            competence.Name = evt.NewName.Value;
        }

    }
}
