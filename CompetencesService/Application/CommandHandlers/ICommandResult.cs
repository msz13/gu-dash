using System.Collections.Generic;
using static CompetencesService.Application.CommandHandlers.CommandResult;

namespace CompetencesService.Application.CommandHandlers
{
    public interface ICommandResult
    {
        bool IsSucces { get;  }

        List<Error> Errors { get; }
    }
}