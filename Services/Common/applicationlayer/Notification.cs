using System.Collections.Generic;

namespace GuDash.Common.applicationlayer
{
    public class Notification
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
        public List<Error> Errors { get; private set; } = new List<Error> { };

        public void AddError(Notification.Error error)
        {
            Errors.Add(error);
        }

        public bool HasErrors()
        {
            return Errors.Count != 0;

        }

    }
}
