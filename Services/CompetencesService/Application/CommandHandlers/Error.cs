using GuDash.Common.Domain.Model;
using System.Collections.Generic;

namespace CompetencesService.Application.CommandHandlers
{
    public sealed class Error : ValueObject
    {

        public string Code { get; }
        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }

}
