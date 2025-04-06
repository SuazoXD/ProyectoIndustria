

# 🌟 ProyectoIndustria - GrooveArchive

¡Bienvenido al GrooveArchive Este proyecto, desarrollado por **Estudiantes de Clase de Indutria**, está construido con **ASP.NET Core** y **Entity Framework Core** para ofrecer una solución robusta y eficiente. 🚀

---

## 📋 Descripción

**ProyectoIndustria** es un sistema backend diseñado para gestionar datos y lógica de negocio de manera escalable. Este README te guiará para configurarlo y ejecutarlo en tu entorno local o con Docker.

---

## 🛠️ Requisitos previos

Asegúrate de tener instalados:
- [.NET SDK](https://dotnet.microsoft.com/download).
- [Docker](https://www.docker.com/get-started) (opcional, para contenedores).
- [Git](https://git-scm.com/) para clonar el repositorio.
- Un cliente SQL como [DBeaver](https://dbeaver.io/) o SSMS.

---

## 🚀 Configuración inicial

### 1. Clonar el repositorio
```bash
git clone https://github.com/SuazoXD/ProyectoIndustria.git
cd ProyectoIndustria
```

---

## 🗄️ Configuración de la base de datos

### 1. Ejecutar SQL Server en Docker
Ejecuta este comando en una sola línea en **PowerShell**:
```powershell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" -p 1433:1433 --name sqlserver-container -d mcr.microsoft.com/mssql/server:2022-latest
```

### 2. Conectarte a la base de datos
Usa un cliente como **DBeaver** con esta configuración:
- **Host**: `localhost`
- **Port**: `1433`
- **Database**: (déjalo vacío por ahora)
- **Authentication**: SQL Server Authentication
- **User**: `sa`
- **Password**: `YourStrong!Passw0rd`

### 3. Crear la base de datos
Ejecuta esta consulta SQL:
```sql
CREATE DATABASE MyDatabase;
```

---

## 🌐 Migraciones de la base de datos

Dentro del directorio del proyecto, ejecuta:

### 1. Generar migraciones
```bash
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project API
```

### 2. Actualizar la base de datos
```bash
dotnet ef database update --project Infrastructure --startup-project API
```

---

## 📌 Notas importantes

- **Contraseña segura**: Asegúrate de que `YourStrong!Passw0rd` cumpla con los requisitos de SQL Server (mínimo 8 caracteres, con mayúsculas, minúsculas, números y símbolos).
- **Puerto**: Si el puerto `1433` está ocupado, cámbialo en el comando Docker (ej: `-p 1434:1433`).
- **Logs**: Para verificar el contenedor, usa:
  ```bash
  docker logs sqlserver-container
  ```
---

## 🐳 Uso de Docker

Si prefieres ejecutar la aplicación en un contenedor Docker, sigue estos pasos:

### 1. Descargar la imagen
```bash
docker pull ghcr.io/suazoxd/proyectoindustria/api:dev
```

### 2. Crear y ejecutar el contenedor
```bash
docker run -d --name proyectoindustria-api --env-file .env -p 8080:8080 ghcr.io/suazoxd/proyectoindustria/api:dev
```
-Nota: Asegúrate de estar en el directorio con el archivo .env configurado.

-Puedes cambiar el puerto externo (ej: -p 5000:8080).

### 3. Ver los logs
```bash
docker logs proyectoindustria-api
```

## 👤 Autores

Desarrollado por **GrooveArchive**. Visita el proyecto en [GitHub](https://github.com/SuazoXD/ProyectoIndustria). ¡Gracias por tu interés! 🙌

---

