using System.Collections.Generic;

namespace CompetencesService.Application.CommandHandlers
{
    public interface ICommandResult
    {
        bool IsSucces { get; }

        List<Error> Errors { get; }
    }
}