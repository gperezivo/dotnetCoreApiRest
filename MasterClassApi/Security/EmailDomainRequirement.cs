using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterClassApi.Security
{
    //https://docs.microsoft.com/en-gb/aspnet/core/security/authorization/policies?view=aspnetcore-2.2

    public class EmailDomainRequirement : IAuthorizationRequirement
    {
        public EmailDomainRequirement(string domain)
        {
            Domain = domain;
        }

        public string Domain { get; private set; }
        
    }
}
