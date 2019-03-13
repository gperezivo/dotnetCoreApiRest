using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterClassApi.Security
{
    public class EmailDomainAuthHandler 
        : AuthorizationHandler<EmailDomainRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            EmailDomainRequirement requirement
            )
        {
            if (context.User.HasClaim(
                    c =>
                        c.Type == ClaimTypes.Email
                        && c.Value.EndsWith(requirement.Domain)
                )
            )
            {
                context.Succeed(requirement);
            }
            else
                context.Fail();
            return Task.CompletedTask;
        }
    }
}
