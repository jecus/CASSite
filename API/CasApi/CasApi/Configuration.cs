using Entity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CasApi
{
    public partial class Startup
    {
        public virtual void RegisterVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(c =>
            {
                c.UseApiBehavior = false;
                c.RegisterMiddleware = true;
                c.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddVersionedApiExplorer(c =>
            {
                c.SubstituteApiVersionInUrl = true;
                // https://github.com/Microsoft/aspnet-api-versioning/wiki/Version-Format
                c.GroupNameFormat = "'v'VVV";
            });
        }
        
        public virtual void RegisterHealthChecks(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddSqlServer(Configuration.GetConnectionString("CORE_CONNECTION_STRING"), name: "CoreContext", failureStatus: HealthStatus.Unhealthy);
        }
        
        public virtual void RegisterDatabases(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("CORE_CONNECTION_STRING"), optionsBuilder =>
                {
                    optionsBuilder.CommandTimeout(180);
                });
                builder.EnableSensitiveDataLogging(false);
                builder.EnableDetailedErrors();
            });
        }
    }
}