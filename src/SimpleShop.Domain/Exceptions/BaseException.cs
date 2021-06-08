using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public string Id { get; private set; } = BuildId();

        private static string BuildId()
        {
            return DateTime.UtcNow.ToString("yyyyMMdd-HHmmss") + "-" + Guid.NewGuid().ToString();
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
