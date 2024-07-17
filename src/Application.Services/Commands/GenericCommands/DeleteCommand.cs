using Application.Services.Wrappers;
using MediatR;

namespace Application.Services.Commands.GenericCommands
{
    /// <summary>
    /// Comando genérico para eliminar
    /// </summary>
    /// <typeparam name="T">Tipo de entidad a eliminar</typeparam>
    /// <typeparam name="I">Tipo de identificador</typeparam>
    public class DeleteCommand<T, I>(I id) : IRequest<Result>
    {
        public I Id { get; set; } = id;
    }
}
