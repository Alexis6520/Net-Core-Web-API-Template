using Application.Services.Commands;
using Application.Services.Wrappers;
using Domain.Entities;
using Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Services.CommandHandlers.DonutHandlers
{
    public class DonutDeleteHandler(IUnitOfWork unitOfWork, ILogger<DonutDeleteHandler> logger) : IRequestHandler<DeleteCommand<Donut, int>, Result>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<DonutDeleteHandler> _logger = logger;

        public async Task<Result> Handle(DeleteCommand<Donut, int> request, CancellationToken cancellationToken)
        {
            var donut = await _unitOfWork.Donuts
                .FindAsync(cancellationToken, request.Id);

            if (donut == null)
            {
                return Result.Fail(HttpStatusCode.NotFound, "Dona no encontrada");
            }

            _unitOfWork.Donuts.Remove(donut);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Dona {Id} eliminada", donut.Id);
            return Result.Success(HttpStatusCode.NoContent);
        }
    }
}
