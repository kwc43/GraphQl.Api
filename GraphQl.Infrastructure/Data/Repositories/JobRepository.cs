using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Core.Entities.Jobs;
using GraphQl.Core.Interfaces;
using GraphQl.Core.Interfaces.Repositories;
using GraphQl.Infrastructure.Data.Context;

namespace GraphQl.Infrastructure.Data.Repositories
{
    public sealed class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
