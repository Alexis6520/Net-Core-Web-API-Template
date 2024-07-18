using Application.Services.Wrappers;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Services.Commands.DonutCommands
{
    /// <summary>
    /// Comando para actualizar una dona
    /// </summary>
    public class DonutUpdateCommand : IRequest<Result>
    {
        /// <summary>
        /// Id de registro a actualizar
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Nuevo nombre
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nuevo precio
        /// </summary>
        public decimal Price { get; set; }
    }
}
