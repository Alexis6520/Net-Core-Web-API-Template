namespace Domain.Settings
{
    /// <summary>
    /// Modela la configuración para la autenticación de JWT (Json Web Token)
    /// </summary>
    public class JWTSettings
    {
        /// <summary>
        /// Clave secreta de encriptado
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Nombre del emisor del Token
        /// </summary>
        public string ValidIssuer { get; set; }

        /// <summary>
        /// Nombre de las aplicaciones válidas para usar los Token
        /// </summary>
        public List<string> ValidAudiences { get; set; }
    }
}
