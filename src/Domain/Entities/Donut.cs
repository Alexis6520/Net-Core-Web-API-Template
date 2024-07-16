namespace Domain.Entities
{
    /// <summary>
    /// Representa una dona
    /// </summary>
    public class Donut
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public int Id { get; set; }

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
