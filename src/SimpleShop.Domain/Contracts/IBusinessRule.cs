using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleShop.Domain.Contracts
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}
