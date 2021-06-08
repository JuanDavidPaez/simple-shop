using SimpleShop.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Domain.Exceptions
{
    public class BusinessRuleValidationException : BaseException
    {

        public BusinessRuleValidationException(string message) : base(message)
        {
        }
    }
}
