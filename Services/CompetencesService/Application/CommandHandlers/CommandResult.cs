using System.Collections.Generic;

namespace CompetencesService.Application.CommandHandlers
{
    public class CommandResult : ICommandResult
    {
        public class Error
        {
            public Error(string code, string message)
            {
                Code = code;
                Message = message;
            }

            public string Code { get; private set; }

            public string Message { get; private set; }

        }
        public bool IsSucces { get { return !HasErrors(); } }


        public List<Error> Errors { get; private set; } = new List<Error> { };
        List<CommandHandlers.Error> ICommandResult.Errors { get; }

        public void AddError(Error error)
        {
            Errors.Add(error);
        }

        private bool HasErrors()
        {
            return Errors.Count != 0;

        }
    }
}
