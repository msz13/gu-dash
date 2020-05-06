using GuDash.Common.Domain.Model;
using System.Collections.Generic;

namespace GuDash.Common.domainmodel
{
    public class Error : ValueObject
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; private set; }

        public string Message { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
