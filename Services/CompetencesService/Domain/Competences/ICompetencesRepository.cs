﻿using System.Threading.Tasks;

namespace GuDash.CompetencesService.Domain.Competences
{
    public interface ICompetenceRepository
    {
        CompetenceId NextIdentity();
        void Add(Competence theCompetence);

        void Update(Competence theCompetence);

        Task<Competence> CompetenceOfId(CompetenceId id);
    }
}
