
using CompetencesService.Application.CommandHandlers;
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
    public class DefineCompetenceCommandHandler : IRequestHandler<DefineCompetenceCommand, CommandResult>
    {
        ICompetencesStore dataStore;

        public DefineCompetenceCommandHandler(ICompetencesStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public async Task<CommandResult> Handle(DefineCompetenceCommand command, CancellationToken cancellationToken)
        {
            var learner = new Learner(
                new LearnerId(command.LearnerId),
                "",
                ""
                );

            var competenceId = this.dataStore.Competences.NextIdentity();

            var competence = learner.DefineCompetence(
                competenceId,
                command.Name,
                command.Description,
                command.IsRequired
                );

            using (dataStore)
            {
                this.dataStore.Competences.Add(competence);
                await this.dataStore.CommitChanges();
            }
            

            return new CommandResult
            {
                IsSucces = true,
                CompetenceId = competenceId.Id
            };
        }
    }
}
