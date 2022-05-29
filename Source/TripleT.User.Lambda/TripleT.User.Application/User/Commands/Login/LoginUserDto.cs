using System;
using TripleT.User.Domain.Primitives;

namespace TripleT.User.Application.User.Commands.Login
{
    public class LoginUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string Cellphone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRoles Role { get; set; }
        public string Token { get; set; }
        public int TokenExpiresInSeconds { get; set; }
    }
}
