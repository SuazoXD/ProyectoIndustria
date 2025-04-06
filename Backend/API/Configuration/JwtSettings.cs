using Microsoft.Extensions.Configuration;

namespace API.Configuration
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }

        public JwtSettings(IConfiguration configuration)
        {
            Key = configuration["Jwt:Key"] ?? "default-key"; // Valor por defecto si no está en appsettings o entorno
            Issuer = configuration["Jwt:Issuer"] ?? "TuIssuer";
            Audience = configuration["Jwt:Audience"] ?? "TuAudience";
            ExpiresInMinutes = int.Parse(configuration["Jwt:ExpiresInMinutes"] ?? "60");
        }
    }
}