using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DDDExample.Domain.Core;
using Microsoft.AspNetCore.Http;

namespace DDDExample.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid UserId()
        {
            return IsAuthenticated() ? 
                new Guid(GetClaimsIdentity().FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value) :
                Guid.Empty;
        }


        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
