using System.Net;
using System.Text.Json.Serialization;

namespace Application.Services.Wrappers
{
    /// <summary>
    /// Resultado de una operación
    /// </summary>
    public class Result
    {
        public Result() { }

        public Result(bool succeeded, HttpStatusCode statusCode, IEnumerable<string> errors = null)
        {
            Succeeded = succeeded;
            StatusCode = statusCode;
            Errors = errors;
        }

        /// <summary>
        /// Indica si la operación fue exitosa
        /// </summary>
        [JsonIgnore]
        public bool Succeeded { get; set; }

        /// <summary>
        /// Estatus Http a devolver
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Mensajes de error a devolver
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Devuelve un resultado exitoso
        /// </summary>
        /// <param name="statusCode">Estatus Http</param>
        /// <returns>Resultado exitoso</returns>
        public static Result Success(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result
            {
                Succeeded = true,
                StatusCode = statusCode,
            };
        }

        /// <summary>
        /// Devuelve un resultado fallido
        /// </summary>
        /// <param name="statusCode">Estatus Http</param>
        /// <param name="errors">Mensajes de error</param>
        /// <returns>Resultado fallido</returns>
        public static Result Fail(HttpStatusCode statusCode, params string[] errors)
        {
            return new Result
            {
                Succeeded = false,
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }

    /// <summary>
    /// Resultado de una operación que devuelve un valor
    /// </summary>
    public class Result<T> : Result
    {
        public Result() { }

        public Result(bool succeeded, HttpStatusCode statusCode, IEnumerable<string> errors = null) : base(succeeded, statusCode, errors)
        {
        }

        /// <summary>
        /// Valor devuelto por la operación
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Value { get; set; }

        /// <summary>
        /// Devuelve un resultado exitoso con valor
        /// </summary>
        /// <param name="value">Valor a devolver</param>
        /// <param name="statusCode">Estatus Http</param>
        /// <returns>Resultado exitoso</returns>
        public static Result<T> Success(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result<T>
            {
                Succeeded = true,
                StatusCode = statusCode,
                Value = value
            };
        }

        /// <summary>
        /// Devuelve un resultado fallido
        /// </summary>
        /// <param name="statusCode">Estatus Http</param>
        /// <param name="errors">Mensajes de error</param>
        /// <returns>Resultado fallido</returns>
        public static new Result<T> Fail(HttpStatusCode statusCode, params string[] errors)
        {
            return new Result<T>
            {
                Succeeded = false,
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }
}
