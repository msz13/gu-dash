using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Proto;
using Marten;
using Marten.Linq;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    public class MartenDataStore : ICompetencesStore
    {
        private readonly IDocumentSession session;

        public MartenDataStore(IDocumentStore store)
        {
            session = store.OpenSession();

        }

        public MartenDataStore()
        {
        }

        public ICompetenceRepository Competences
        {
            get
            {
                return new CompetenceRepository(session);
            }
        }

        

        public async Task CommitChanges()
        {
            await session.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                session.Dispose();
            }

        }
    }
}
