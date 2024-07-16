using Domain.Services;
using Infrastructure.Persistence.EFCore.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Infrastructure.Persistence.EFCore
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("Main"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbConnection>(x => new SqlConnection(configuration.GetConnectionString("Main")));
            return services;
        }
    }
}
