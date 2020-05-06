using CSharpFunctionalExtensions;
using MediatR;

namespace CompetencesService.Application.CommandHandlers
{
    public class RenameCompetenceCommand : IRequest<Result<int, Error>>
    {
        public string UserId { get; private set; }
        public string CompetenceId { get; private set; }
        public string NewName { get; private set; }

        public RenameCompetenceCommand(string userId, string competenceId, string newName)
        {
            this.UserId = userId;
            this.CompetenceId = competenceId;
            this.NewName = newName;
        }
    }
}
