using System.Globalization;

namespace Security.Domain.Exceptions
{
    public class SecurityBaseException : Exception
    {
        public SecurityBaseException() : base() { }

        public SecurityBaseException(string message) : base(message) { }

        public SecurityBaseException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
