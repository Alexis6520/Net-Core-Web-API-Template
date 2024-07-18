namespace Application.Services.Queries.DTOs
{
    /// <summary>
    /// Clase genérica para devolver resultados paginados
    /// </summary>
    /// <typeparam name="T">Tipo a devolver en los resultados</typeparam>
    public class PagedList<T>
    {
        /// <summary>
        /// Cantidad global de resultados
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Total de páginas
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// Resultados de la página solicitada
        /// </summary>
        public List<T> Items { get; set; }
    }
}
