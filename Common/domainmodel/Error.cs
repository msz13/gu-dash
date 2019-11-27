using GuDash.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
