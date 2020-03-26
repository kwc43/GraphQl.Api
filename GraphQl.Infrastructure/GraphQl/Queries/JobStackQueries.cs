using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Core.Entities.Jobs;
using GraphQl.Core.Interfaces;
using GraphQl.Core.Interfaces.Repositories;
using GraphQl.Core.Specifications;
using GraphQl.Infrastructure.GraphQl.Types;
using GraphQL.Types;

namespace GraphQl.Infrastructure.GraphQl.Queries
{
    public class JobStackQueries : ObjectGraphType
    {
        private const string JobQueryIdParameterName = nameof(Job.Id);

        public JobStackQueries(IJobRepository jobRepository)
        {
            FieldAsync<JobType>("job",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> {Name = JobQueryIdParameterName }),
                resolve: async context =>
                {
                    return await jobRepository.GetBySpecificationAsync(new JobSpecification(job =>
                        job.Id == context.GetArgument<int>(JobQueryIdParameterName, default)));
                });
        }
    }
}
