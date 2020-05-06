using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using Marten;
using MediatR;
using System;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Infrastructure.Persistance
{
    public class MartenDataStore : ICompetencesStore
    {
        private readonly IDocumentSession session;

        private readonly IMediator mediator;

        public MartenDataStore(IDocumentStore store, IMediator mediator)
        {
            this.session = store.OpenSession();
            this.mediator = mediator;


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

        public ILearnerRepository Learner
        {
            get
            {
                return new LearnerRepository(session);
            }
        }

        public async Task CommitChanges()
        {
            //await DispatchEvents();
            await session.SaveChangesAsync();
        }

        private async Task DispatchEvents()
        {
            var pendingStreams = session.PendingChanges.Streams();
            foreach (var stream in pendingStreams)
            {
                foreach (var evt in stream.Events)
                {
                    await this.mediator.Publish(evt.Data);
                };
            }

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
