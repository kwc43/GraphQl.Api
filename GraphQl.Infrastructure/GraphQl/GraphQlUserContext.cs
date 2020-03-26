using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using GraphQL.Authorization;

namespace GraphQl.Infrastructure.GraphQl
{
    public class GraphQlUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
    { 
            public ClaimsPrincipal User { get; set; }
    }
}
