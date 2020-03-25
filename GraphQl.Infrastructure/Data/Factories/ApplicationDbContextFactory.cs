using System;
using System.Collections.Generic;
using System.Text;
using GraphQl.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GraphQl.Infrastructure.Data.Factories
{
    public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}
