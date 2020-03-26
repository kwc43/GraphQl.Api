using System.Net;
using GraphiQl;
using GraphQL;
using GraphQl.Core.Interfaces;
using GraphQl.Core.Interfaces.Repositories;
using GraphQl.Core.Interfaces.Specifications;
using GraphQl.Core.Specifications;
using GraphQl.Infrastructure.Data.Context;
using GraphQl.Infrastructure.Data.Repositories;
using GraphQl.Infrastructure.Extensions;
using GraphQl.Infrastructure.GraphQl.Mutations;
using GraphQl.Infrastructure.GraphQl.Queries;
using GraphQl.Infrastructure.GraphQl.Schema;
using GraphQl.Infrastructure.GraphQl.Types;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQl.Api
{
    public class Startup
    {
        private const string ConnectionStringName = "Deafult";

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:8787";
                options.Audience = "resourceapi";
                options.RequireHttpsMetadata = false;
            });

            services.AddContext(ConnectionStringName);

            services.AddRepositories();

            services.AddGraphQlDependencies();
            services.AddGraphQlAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
            });

            app.UseRouting();
            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();

            app.UseGraphiql("/graphiql", options => { options.GraphQlEndpoint = "/graphql"; });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
