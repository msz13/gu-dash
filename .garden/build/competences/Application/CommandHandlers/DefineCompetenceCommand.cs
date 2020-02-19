using CompetencesService.Application.CommandHandlers;
using GuDash.CompetencesService.Proto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.CompetencesService.Application.CommandHandlers
{
    public class DefineCompetenceCommand: IRequest<CommandResult>
    {
        public DefineCompetenceCommand(string learnerId, string name, string description, bool isRequired)
        {
            LearnerId = learnerId;
            Name = name;
            Description = description;
            IsRequired = isRequired;
        }
                
        public string LearnerId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }
        public bool IsRequired { get; private set; }
                

    }
}
