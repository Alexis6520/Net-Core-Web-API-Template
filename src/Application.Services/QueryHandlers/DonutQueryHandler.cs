using Application.Services.Queries.DTOs;
using Application.Services.Queries.GenericQueries;
using Application.Services.Wrappers;
using Dapper;
using MediatR;
using System.Data;
using System.Net;

namespace Application.Services.QueryHandlers
{
    public class DonutQueryHandler(IDbConnection connection) : IRequestHandler<FindQuery<DonutDTO, int>, Result<DonutDTO>>
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
    }
}
