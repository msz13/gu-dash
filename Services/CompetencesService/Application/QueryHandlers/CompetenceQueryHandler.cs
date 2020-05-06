using CSharpFunctionalExtensions;
using GuDash.CompetencesService.Proto;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencesService.Application.QueryHandlers
{
    public class CompetenceQueryHandler : IRequestHandler<CompetenceQuery, CompetenceDTO>
    {
        IQuerySession session;

        public CompetenceQueryHandler(IDocumentStore store)
        {
            this.session = store.QuerySession();
        }

        public async Task<CompetenceDTO> Handle(CompetenceQuery query, CancellationToken canceletionToken)
        {
            CompetenceDTO competence;

            using (session)
            {
                try
                {
                    competence = await session.Query<CompetenceDTO>().Where(x => x.Id == query.CompetenceId && x.LearnerId == query.LearnerId).SingleAsync();
                }
                catch (InvalidOperationException err)
                {
                    Console.WriteLine(err.Message);
                    throw new KeyNotFoundException($"Competence with id: {query.CompetenceId} of user account Id: {query.LearnerId} not found");
                }

            }


            return competence;
        }
    }
}
