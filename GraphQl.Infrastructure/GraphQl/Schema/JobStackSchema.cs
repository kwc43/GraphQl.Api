using System;
using System.Collections.Generic;
using System.Text;
using GraphQL;
using GraphQl.Infrastructure.GraphQl.Mutations;
using GraphQl.Infrastructure.GraphQl.Queries;

namespace GraphQl.Infrastructure.GraphQl.Schema
{
    public class JobStackSchema : GraphQL.Types.Schema
    {
        public JobStackSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            Query = dependencyResolver.Resolve<JobStackQueries>();
            Mutation = dependencyResolver.Resolve<JobStackMutations>();
        }
    }
}
