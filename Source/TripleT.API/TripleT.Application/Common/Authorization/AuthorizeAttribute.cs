using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TripleT.Application.Common.Authorization.Models;
using TripleT.Domain.Entities;

namespace TripleT.Application.Common.Authorization
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Role { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            AuthenticatedUser user = (AuthenticatedUser)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            } 
            else if (!string.IsNullOrEmpty(Role) && !ValidateRole(user.Role))
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }

        private bool ValidateRole(string role)
        {
            if (Role.Equals(role, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
