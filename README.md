# APIecommerce

API REST de ecommerce con .NET 10, SQL Server y rutas versionadas (`/api/v1/...`).

## Ejecutar

Requisitos: .NET 10, Docker y `dotnet-ef`.

```bash
export MSSQL_SA_PASSWORD='tu-clave-segura'
docker compose up -d
dotnet user-secrets set "ConnectionStrings:ConexionSql" "<cadena-de-conexion>" --project APIecommerce
dotnet user-secrets set "ApiSettings:SecretKey" "<clave-jwt-larga>" --project APIecommerce
dotnet ef database update --project APIecommerce
dotnet run --project APIecommerce --launch-profile http
```

Disponible en `http://localhost:5261`. OpenAPI en `/openapi/v1.json` durante desarrollo.

## Arquitectura

- **Controllers** (`Categories`, `Products`, `Users`): reciben solicitudes HTTP, validan datos de entrada y devuelven códigos de estado y DTOs. Las lecturas públicas están marcadas explícitamente; las operaciones administrativas requieren el rol `Admin`.
- **Models y DTOs**: los modelos representan las entidades guardadas en la base de datos (`Category`, `Product`, `User`). Los DTOs definen los datos aceptados o devueltos por la API, sin exponer directamente las entidades en cada endpoint.
- **Mapping**: Mapster convierte entre modelos y DTOs; `MapsterConfig` define el nombre de la categoría en las respuestas de productos.
- **Repository**: cada repositorio implementa una interfaz de `Repository/IRepository`. Encapsula consultas y operaciones CRUD para categorías y productos, además de registro, autenticación y generación de tokens para usuarios.
- **Data y Migrations**: `ApplicationDBContext` define los conjuntos de categorías, productos y usuarios. EF Core usa SQL Server como proveedor y las migraciones describen los cambios del esquema.
- **Flujo de una solicitud**: el controlador valida la entrada, llama a la interfaz del repositorio y transforma el resultado con Mapster; el repositorio consulta o actualiza la base de datos mediante `ApplicationDBContext`.
- **Servicios transversales**: `Program.cs` registra dependencias, autenticación JWT, versionado de API, CORS, caché de respuestas y OpenAPI. BCrypt se utiliza para proteger contraseñas.

Dependencias principales: Entity Framework Core y SQL Server, Asp.Versioning, Mapster, BCrypt.Net-Next y JWT Bearer.

Categorías tiene v1 y v2; productos y usuarios usan rutas versionadas. Lecturas de categorías/productos y registro/login son públicos; las demás operaciones requieren rol `Admin`.
