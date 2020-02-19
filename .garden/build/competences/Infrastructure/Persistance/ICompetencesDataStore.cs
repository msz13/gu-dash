using GuDash.CompetencesService.Domain.Competences;
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
                
        Task CommitChanges();
    }
}
