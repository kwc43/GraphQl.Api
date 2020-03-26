using System.Security.Claims;
using GraphQL;
using GraphQL.Authorization;
using GraphQl.Core.Interfaces.Repositories;
using GraphQl.Core.Policies;
using GraphQl.Infrastructure.Data.Context;
using GraphQl.Infrastructure.Data.Repositories;
using GraphQl.Infrastructure.GraphQl.Mutations;
using GraphQl.Infrastructure.GraphQl.Queries;
using GraphQl.Infrastructure.GraphQl.Schema;
using GraphQl.Infrastructure.GraphQl.Types;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQl.Infrastructure.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddGraphQlAuthentication(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.AddSingleton(s =>
            {
                var authenticationSettings = new AuthorizationSettings();

                authenticationSettings.AddPolicy(Policies.Employer, _ => _.RequireClaim(ClaimTypes.Role, "employer"));
                authenticationSettings.AddPolicy(Policies.Applicant, _ => _.RequireClaim(ClaimTypes.Role, "applicant"));

                return authenticationSettings;
            });
        }

        public static void AddGraphQlDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<ISchema, JobStackSchema>();

            services.AddSingleton<JobStackQueries>();
            services.AddSingleton<JobType>();
            services.AddSingleton<JobStackMutations>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IJobRepository, JobRepository>();
        }

        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
