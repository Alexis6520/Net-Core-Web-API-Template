using Application.Services.Commands.DonutCommands;
using Application.Services.Wrappers;
using Domain.Entities;
using Domain.Services;
using MediatR;
using System.Net;

namespace Application.Services.CommandHandlers.DonutHandlers
{
    public class DonutCreateHandler(IUnitOfWork unitOfWork) : IRequestHandler<DonutCreateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<int>> Handle(DonutCreateCommand request, CancellationToken cancellationToken)
        {
            var donut = new Donut
            {
                Name = request.Name,
                Price = request.Price,
            };

            await _unitOfWork.Donuts.AddAsync(donut, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(donut.Id, HttpStatusCode.Created);
        }
    }
}
