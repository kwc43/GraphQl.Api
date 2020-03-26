using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Authorization;
using GraphQl.Core.Entities.Jobs;
using GraphQl.Core.Policies;
using GraphQl.Core.Values;
using GraphQL.Types;

namespace GraphQl.Infrastructure.GraphQl.Types
{
    public class JobType : ObjectGraphType<Job>
    {
        public JobType()
        {
            Field(x => x.Id);
            Field(x => x.JobTitle);
            Field(x => x.AnnualSalary).AuthorizeWith(Policies.Employer);
            Field(x => x.JobLocation);
            Field(x => x.JobDescription);
            Field(x => x.JobRequirements);
            Field<EnumerationGraphType<JobStatus>>("jobStatus");
            Field<StringGraphType>("modified", resolve: context => context.Source.Modified ?? context.Source.Created);
            Field<IntGraphType>("applicantCount", resolve: context => context.Source.JobApplications.Count);
        }
    }
}
