using CompetencesService.Domain.Competences;
using CSharpFunctionalExtensions;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Infrastructure.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencesService.Application.CommandHandlers
{
    public class RenameCompetenceCommandHandler : IRequestHandler<RenameCompetenceCommand, Result<int, Error>>
    {
        private readonly ICompetencesStore store;
        private readonly ICompetenceUniqueNameService competenceUniqueNameService;

        public RenameCompetenceCommandHandler(ICompetencesStore store, ICompetenceUniqueNameService competenceUniqueNameService)
        {
            this.store = store;
            this.competenceUniqueNameService = competenceUniqueNameService;
        }


        public async Task<Result<int, Error>> Handle(RenameCompetenceCommand command, CancellationToken cancellationToken)
        {
            var competenceRepo = store.Competences;

            var competence = await competenceRepo.CompetenceOfId(new CompetenceId(command.CompetenceId));

            var result = competence.Rename(command.NewName, competenceUniqueNameService);

            if (result.IsSuccess)
            {
                competenceRepo.Update(competence);
                using (store)
                {
                    await store.CommitChanges();
                };
            }

            return result;
        }
    }
}
