using Application.Services;
using Infrastructure.Persistence.EFCore;
using NLog;
using NLog.Web;
using WebAPI;

var logger = LogManager.Setup().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddApplicationServices()
        .AddEFCore(builder.Configuration);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.UseMiddleware<NLogRequestPostedBodyMiddleware>(new NLogRequestPostedBodyMiddlewareOptions());
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
