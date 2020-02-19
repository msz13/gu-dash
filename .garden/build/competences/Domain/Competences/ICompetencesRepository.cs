using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Competences
{
    public interface ICompetenceRepository
    {
        CompetenceId NextIdentity();
        void Add(Competence theCompetence);

        Task<Competence> CompetenceOfId(CompetenceId id);
    }
}
