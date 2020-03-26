using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using GraphQl.Core.Entities.Jobs;

namespace GraphQl.Core.Specifications
{
    public sealed class JobSpecification : SpecificationBase<Job>
    {
        public JobSpecification(Expression<Func<Job, bool>> criteria) : base(criteria)
        {
            AddInclude(j => j.Employer);
            AddInclude(j => j.JobApplications);
        }
    }
}
