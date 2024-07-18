using Application.Services.Wrappers;
using MediatR;

namespace Application.Services.Commands.DonutCommands
{
    /// <summary>
    /// Comando para crear una dona
    /// </summary>
    public class DonutCreateCommand : IRequest<Result<int>>
    {
        public DonutCreateCommand() { }

        public DonutCreateCommand(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

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
