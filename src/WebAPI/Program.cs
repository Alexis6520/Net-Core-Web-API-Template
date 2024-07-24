using Application.Services;
using Infrastructure;
using Infrastructure.Persistence.EFCore;
using NLog;
using NLog.Web;
using WebAPI;
using WebAPI.Middlewares;

var logger = LogManager.Setup().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddApplicationServices()
        .AddInfrastructure(builder.Configuration)
        .AddEFCore(builder.Configuration)
        .AddSwagger()
        .AddJWTAuthentication(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseMiddleware<NLogRequestPostedBodyMiddleware>(new NLogRequestPostedBodyMiddlewareOptions());
    app.UseMiddleware<ExceptionMiddleware>();
    app.InitializeDatabase();
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Programa detenido por excepción");
    throw;
}
finally
{
    LogManager.Shutdown();
}

public partial class Program { } // Para pruebas de integración