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
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
