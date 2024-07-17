using Application.Services.Commands.DonutCommands;
using Application.Services.Wrappers;
using Domain.Services;
using MediatR;
using System.Net;

namespace Application.Services.CommandHandlers.DonutHandlers
{
    public class DonutUpdateHandler(IUnitOfWork unitOfWork) : IRequestHandler<DonutUpdateCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(DonutUpdateCommand request, CancellationToken cancellationToken)
        {
            var donut = await _unitOfWork.Donuts
                .FindAsync(cancellationToken, request.Id);

            if (donut == null)
            {
                return Result.Fail(HttpStatusCode.NotFound, "Dona no encontrada");
            }

            donut.Name = request.Name;
            donut.Price = request.Price;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(HttpStatusCode.NoContent);
        }
    }
}
