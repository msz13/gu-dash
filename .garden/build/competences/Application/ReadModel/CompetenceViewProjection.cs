using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.CompetencesService.Domain.Competences.Events;
using GuDash.CompetencesService.Proto;
using Marten.Events.Projections;

namespace CompetencesService.Application.ReadModel
{
    public class CompetenceViewProjection: ViewProjection<CompetenceDTO, string>
    {
        public CompetenceViewProjection()
        {
            ProjectEvent<CompetenceDefined>(e=>e.CompetenceId.Id, Persist);
        }

        private void Persist(CompetenceDTO competence, CompetenceDefined evt)
        {
            competence.Name = evt.Name;
            competence.Description = evt.Description;
            competence.IsRequired = evt.IsRequired;
            competence.NumberOfActiveHabits = 0;
            competence.NumberOfHoldedHabits = 0;
            competence.NumberOfDoneHabits = 0;
        }
    }
}
