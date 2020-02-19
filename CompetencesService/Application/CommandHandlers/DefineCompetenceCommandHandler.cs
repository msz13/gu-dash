
using CompetencesService.Application.CommandHandlers;
using CSharpFunctionalExtensions;
using GuDash.CompetencesService.Domain.Competences;
using GuDash.CompetencesService.Domain.Learner;
using GuDash.CompetencesService.Infrastructure.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Application.CommandHandlers
{
    public class DefineCompetenceCommandHandler : IRequestHandler<DefineCompetenceCommand, Result<CompetenceId, Error>>
    {
        ICompetencesStore dataStore;

        public DefineCompetenceCommandHandler(ICompetencesStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public async Task<Result<CompetenceId, Error>> Handle(DefineCompetenceCommand command, CancellationToken cancellationToken)
        {

            var learner = await dataStore.Learner.LearnerOfId(new LearnerId(command.LearnerId));
           
            if (learner == null)
            {
                learner = new Learner(
                new LearnerId(command.LearnerId),
                "Europe/Warsaw"
                );

                dataStore.Learner.Add(learner);
            } 

            var competenceId = this.dataStore.Competences.NextIdentity();

            var competenceResult = learner.DefineCompetence(
                competenceId,
                command.Name,
                command.Description,
                command.IsRequired
                );

            await competenceResult
                .Tap(competence =>
                {
                    var snapshot = competence.GetSnapshot();
                    learner.CompetenceDefined(snapshot.CompetenceId, snapshot.Name, snapshot.IsRequired);
                })
                .Tap(async competence => {
                {
                    using (dataStore)
                    {
                        this.dataStore.Competences.Add(competence);
                            this.dataStore.Learner.Update(learner);
                        await this.dataStore.CommitChanges();
                    }

                } });


            return competenceResult.IsSuccess ?
                 Result.Success<CompetenceId, Error>(competenceId)
                 : Result.Failure<CompetenceId, Error>(competenceResult.Error);

            

        }
    }
}
