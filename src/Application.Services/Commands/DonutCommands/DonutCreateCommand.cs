using Application.Services.Wrappers;
using MediatR;

namespace Application.Services.Commands.DonutCommands
{
    /// <summary>
    /// Comando para crear una dona
    /// </summary>
    public class DonutCreateCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
