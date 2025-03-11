# ProyectoIndustria
Proyecto de Industria 

Configuracion de la base de datos: 
- Ejecuta el comando completo en una sola línea en PowerShell
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 --name sqlserver-container -d mcr.microsoft.com/mssql/server:2022-latest

Conexion con la base de datos 
- conectarte a SQL Server en Docker usando DBeaver
    Host: localhost
    Port: 1433
    Database: (Déjalo vacío por ahora, luego puedes seleccionar una base específica)
    Authentication: SQL Server Authentication
    User: sa
    Password: YourStrong!Passw0rd

- Ejecutar la siguiente consulta 
    CREATE DATABASE MyDatabase;

Migraciones en base de datos dentro de la carpeta 
    dotnet ef migrations add InitialCreate --project Infrastructure --startup-project API
    dotnet ef database update --project Infrastructure --startup-project API

