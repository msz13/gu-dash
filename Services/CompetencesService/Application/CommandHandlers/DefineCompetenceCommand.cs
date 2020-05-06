using CSharpFunctionalExtensions;
using GuDash.CompetencesService.Domain.Competences;
using MediatR;
using Error = CompetencesService.Application.CommandHandlers.Error;

namespace GuDash.CompetencesService.Application.CommandHandlers
{
    public class DefineCompetenceCommand : IRequest<Result<CompetenceId, Error>>
    {
        private string v;

        public DefineCompetenceCommand(string learnerId, string name, string? description, bool? isRequired)
        {
            LearnerId = learnerId;
            Name = name;
            Description = description;
            IsRequired = isRequired;
        }

       

        public string LearnerId { get; private set; }

        public string Name { get; private set; }

        public string? Description { get; private set; }
        public bool? IsRequired { get; private set; }


    }
}
