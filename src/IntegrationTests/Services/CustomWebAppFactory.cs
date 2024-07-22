using Infrastructure.Persistence.EFCore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace IntegrationTests.Services
{
    /// <summary>
    /// Fabricador de aplicación para pruebas de integración
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomWebAppFactory<T> : WebApplicationFactory<T> where T : class
    {
        private const string ConnectionString = "Server=BATICOMPUTADORA;Database=Kripy_Kreme_Test;Trusted_Connection=True; TrustServerCertificate=True;";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(SetPersistence);
            builder.UseEnvironment("Development");
        }

        private static void SetPersistence(IServiceCollection services)
        {
            var serviceDesc = services.SingleOrDefault(sd => sd.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (serviceDesc != null) services.Remove(serviceDesc);

            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseSqlServer(ConnectionString);
            });

            serviceDesc = services.SingleOrDefault(sd => sd.ServiceType == typeof(IDbConnection));
            if (serviceDesc != null) services.Remove(serviceDesc);
            services.AddScoped<IDbConnection>(sp => new SqlConnection(ConnectionString));
        }
    }
}
