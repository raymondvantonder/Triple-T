using System;
using System.Collections.Generic;
using System.Text;

namespace TripleT.Application.Common.Authorization.Models
{
    public class AuthenticatedUser
    {
        public AuthenticatedUser(long userId, string role)
        {
            UserId = userId;
            Role = role;
        }

        public long UserId { get; private set; }
        public string Role { get; private set; }
    }
}
