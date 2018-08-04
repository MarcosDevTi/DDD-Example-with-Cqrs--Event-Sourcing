using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DDDExample.Domain.Core
{
    public interface IUser
    {
        string Name { get; }
        Guid UserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
