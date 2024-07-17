using System.Net;
using System.Text.Json;
using Application.Services.Wrappers;

namespace WebAPI.Middlewares
{
    /// <summary>
    /// Middleware para manejar excepciones
    /// </summary>
    /// <param name="requestDelegate"></param>
    /// <param name="logger"></param>
    public class ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _requestDelegate = requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context); // Ejecuta el siguiente método en el pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar soliciutd");
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                var responseModel = Result.Fail(HttpStatusCode.InternalServerError, "Hubo un problema al procesar su solicitud");
                var json = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(json);
            }
        }
    }
}
