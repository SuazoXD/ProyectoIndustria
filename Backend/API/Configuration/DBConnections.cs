using Microsoft.Extensions.Configuration;

namespace API.Configuration
{
    public class DBConnections
    {
        public string ConnectionString { get; }

        public DBConnections()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Carga appsettings.json
                .AddEnvironmentVariables() // Carga variables de entorno (GitHub Secrets o .env)
                .Build();

            // Obtener valores de variables de entorno o usar valores por defecto
            string dbServer = configuration["DB_SERVER"] ?? "localhost";
            string dbPort = configuration["DB_PORT"] ?? "1433";
            string dbName = configuration["DB_NAME"] ?? "MyDatabase3";
            string dbUser = configuration["DB_USER"] ?? "sa";
            string dbPassword = configuration["DB_PASSWORD"] ?? "YourStrong!Passw0rd";

            // Construir la cadena de conexión
            ConnectionString = $"Server={dbServer},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";
        }
    }
}