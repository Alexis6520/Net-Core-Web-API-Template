using Application.Services.Queries.DTOs;
using Application.Services.Queries.DTOs.DonutDTOs;
using Application.Services.Queries.GenericQueries;
using Application.Services.Wrappers;
using Dapper;
using MediatR;
using System.Data;
using System.Net;

namespace Application.Services.QueryHandlers
{
    public class DonutQueryHandler(IDbConnection connection) :
        IRequestHandler<FindQuery<DonutDTO, int>, Result<DonutDTO>>,
        IRequestHandler<PagedQuery<DonutDTO>, Result<PagedList<DonutDTO>>>
    {
        private readonly IDbConnection _connection = connection;

        public async Task<Result<DonutDTO>> Handle(FindQuery<DonutDTO, int> request, CancellationToken cancellationToken)
        {
            var sql = "SELECT Id,Name FROM Donuts WHERE Id=@Id";
            object param = new { request.Id };
            var command = new CommandDefinition(sql, param, cancellationToken: cancellationToken);
            var donut = await _connection.QueryFirstOrDefaultAsync<DonutDTO>(command);
            if (donut == null) return Result<DonutDTO>.Fail(HttpStatusCode.NotFound, "Dona no encontrada");
            return Result<DonutDTO>.Success(donut);
        }

        public async Task<Result<PagedList<DonutDTO>>> Handle(PagedQuery<DonutDTO> request, CancellationToken cancellationToken)
        {
            var sql = "SELECT COUNT(Id) FROM Donuts; " +
                "SELECT Id,Name FROM Donuts " +
                "ORDER BY Id " +
                "OFFSET (@Page-1)*@PageSize ROWS " +
                "FETCH NEXT @PageSize ROWS ONLY";

            object param = new
            {
                request.Page,
                request.PageSize,
            };

            var command = new CommandDefinition(sql, param, cancellationToken: cancellationToken);
            using var gridReader = await _connection.QueryMultipleAsync(command);

            var page = new PagedList<DonutDTO>
            {
                Count = gridReader.ReadFirst<int>(),
                Items = gridReader.Read<DonutDTO>().ToList()
            };

            page.Pages = (int)Math.Ceiling(page.Count / (decimal)request.PageSize);
            return Result<PagedList<DonutDTO>>.Success(page);
        }
    }
}
