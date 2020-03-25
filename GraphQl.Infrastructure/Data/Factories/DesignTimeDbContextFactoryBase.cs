using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GraphQl.Infrastructure.Data.Factories
{
    public abstract class DesignTimeDbContextFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext> where TDbContext :DbContext
    {
        private readonly string _environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        private readonly string _appDirectory = Directory.GetCurrentDirectory();
        private const string ConnectionStringName = "Default";

        protected string ConnectionString;

        protected abstract TDbContext CreateNewInstance(DbContextOptions<TDbContext> options);

        public TDbContext CreateDbContext(string[] args)
        {
            return Create(_environmentName, _appDirectory, ConnectionStringName);
        }

        private TDbContext Create(string environmentName, string appDirectory, string connectionStringName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(appDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            ConnectionString = connectionStringName;

            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new InvalidOperationException(
                    $"Could not find a connection string named: {connectionStringName}");
            }

            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();

            optionsBuilder.UseSqlServer(ConnectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}