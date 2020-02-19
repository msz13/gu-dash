using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using Marten;
using Marten.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    public interface ICompetencesStore : IDisposable
    {
        ICompetenceRepository Competences { get; }
        ILearnerRepository Learner { get; }                
        Task CommitChanges();
    }
}
