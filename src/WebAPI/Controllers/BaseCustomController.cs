using Application.Services.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Clase base para controladores
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseCustomController(IMediator mediator) : ControllerBase
    {
        protected readonly IMediator _mediator = mediator;

        /// <summary>
        /// Devuelve una respuesta personalizada para los envoltorios Result
        /// </summary>
        /// <param name="result">Resultado a devolver</param>
        /// <returns></returns>
        protected ActionResult CustomResult(Result result)
        {
            var statusCode = (int)result.StatusCode;

            if (!result.Succeeded)
            {
                return StatusCode(statusCode, result);
            }

            return StatusCode(statusCode);
        }

        /// <summary>
        /// Devuelve una respuesta personalizada para los envoltorios Result con Valor
        /// </summary>
        /// <typeparam name="T">Tipo de valor</typeparam>
        /// <param name="result">Resultado a devolver</param>
        /// <returns></returns>
        protected ActionResult<Result<T>> CustomResult<T>(Result<T> result)
        {
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
