using Application.Services.Wrappers;
using MediatR;

namespace Application.Services.Commands.DonutCommands
{
    /// <summary>
    /// Comando para crear una dona
    /// </summary>
    public class DonutCreateCommand : IRequest<Result<int>>
    {
        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal Price { get; set; }
    }
}
