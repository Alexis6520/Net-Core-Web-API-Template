using Application.Services.Commands.DonutCommands;
using Application.Services.Commands;
using Application.Services.Queries.DTOs;
using Application.Services.Queries.DTOs.DonutDTOs;
using Application.Services.Queries;
using Application.Services.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
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

        /// <summary>
        /// Devuelve todas las donas de forma paginada
        /// </summary>
        /// <param name="page">Número de página</param>
        /// <param name="pageSize">Tamaño de página</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Result<PagedList<DonutDTO>>>> GetAllAsync(int page = 1, int pageSize = 5)
        {
            var result = await _mediator.Send(new PagedQuery<DonutDTO>(page, pageSize));
            return CustomResult(result);
        }

        /// <summary>
        /// Actualiza una dona
        /// </summary>
        /// <param name="id">Id de dona</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(int id, DonutUpdateCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return CustomResult(result);
        }

        /// <summary>
        /// Elimina una dona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _mediator.Send(new DeleteCommand<Donut, int>(id));
            return CustomResult(result);
        }
    }
}
