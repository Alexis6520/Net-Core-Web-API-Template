using Application.Services.Commands.DonutCommands;
using Application.Services.Queries.DTOs;
using Application.Services.Queries.GenericQueries;
using Application.Services.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class DonutsController(IMediator mediator) : BaseCustomController(mediator)
    {
        /// <summary>
        /// Crea una dona
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Result<int>>> CreateAsync(DonutCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomResult(result);
        }

        /// <summary>
        /// Devuelve una dona por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Result<DonutDTO>>> GetAsync(int id)
        {
            var result = await _mediator.Send(new FindQuery<DonutDTO, int>(id));
            return CustomResult(result);
        }
    }
}
