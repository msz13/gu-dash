
using CompetencesService.Application.CommandHandlers;
using CompetencesService.Domain.Competences;
using CSharpFunctionalExtensions;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Infrastructure.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Application.CommandHandlers
{
    public class RenameCompetenceCommandHandler : IRequestHandler<DefineCompetenceCommand, Result<CompetenceId, Error>>
    {
        ICompetencesStore dataStore;
        private readonly ICompetenceUniqueNameService competenceUniqueNameService;

        public RenameCompetenceCommandHandler(ICompetencesStore dataStore, ICompetenceUniqueNameService competenceUniqueNameService)
        {
            this.dataStore = dataStore;
            this.competenceUniqueNameService = competenceUniqueNameService;
        }

        public async Task<Result<CompetenceId, Error>> Handle(DefineCompetenceCommand command, CancellationToken cancellationToken)
        {

            var learner = await dataStore.Learner.LearnerOfId(new LearnerId(command.LearnerId))
                .ToResult<Learner>("Learner not found")
                .OnFailureCompensate(() =>
                {
                    return CreateLearner(command.LearnerId);
                })
                .Finally(r => r.Value);

            var competenceId = this.dataStore.Competences.NextIdentity();

            var competenceResult = learner.DefineCompetence(competenceId,
                                                            command.Name,
                                                            command.Description,
                                                            command.IsRequired,
                                                            competenceUniqueNameService
                                                            );
            if (competenceResult.IsFailure)
                return Result.Failure<CompetenceId, Error>(competenceResult.Error);

            this.dataStore.Competences.Add(competenceResult.Value);
            using (dataStore)
            {
                await dataStore.CommitChanges();
            }

            return Result.Success<CompetenceId, Error>(competenceResult.Value.CompetenceId);


        }

        private Result<Learner> CreateLearner(string learnerId)
        {
            var learner = new Learner(new LearnerId(learnerId), "Europe/Warsaw");
            dataStore.Learner.Add(learner);
            return Result.Success<Learner>(learner);
        }
    }

}
