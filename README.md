# 🚀 Estimador API - Clean Architecture .NET 10

API REST completa para la estimación automática de requisitos funcionales de casos de uso, construida con **Clean Architecture**, **.NET 10**, **Entity Framework Core** y soporte para **SQL Server** y **SQLite**.

## 📋 Características

### Arquitectura
- ✅ **Clean Architecture** con capas bien definidas (Domain, Application, Infrastructure, Presentation)
- ✅ **Patrón CQRS** con MediatR
- ✅ **Repository Pattern** con Unit of Work
- ✅ **Dependency Injection** configurado automáticamente
- ✅ **Domain-Driven Design** con entidades ricas

### Funcionalidades
- ✅ Gestión completa de Proyectos
- ✅ Gestión de Casos de Uso
- ✅ Estimación automática de requisitos
- ✅ Cálculo de complejidad y riesgo
- ✅ Gestión de actores y pasos
- ✅ Seguimiento de dependencias entre requisitos
- ✅ Reportes de estimación

### Base de Datos
- ✅ Soporte para **SQL Server**
- ✅ Soporte para **SQLite**
- ✅ Entity Framework Core 10
- ✅ Migraciones automáticas
- ✅ Auditoría (CreatedAt, UpdatedAt)

### API
- ✅ Documentación automática con Swagger/OpenAPI
- ✅ Validación con FluentValidation
- ✅ Manejo de errores centralizado
- ✅ CORS habilitado
- ✅ Endpoints RESTful

## 🏗️ Estructura del Proyecto

```
EstimadorAPI/
├── EstimadorAPI.API/                       # Capa de Presentación (API)
│   ├── Controllers/
│   │   ├── ProjectsController.cs
│   │   └── UseCasesController.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── EstimadorAPI.csproj
│
├── EstimadorAPI.Application/               # Capa de Aplicación
│   ├── DTOs/
│   ├── UseCases/
│   │   ├── Projects/
│   │   └── UseCases/
│   ├── Validators/
│   ├── Mapping/
│   └── DependencyInjection.cs
│
├── EstimadorAPI.Domain/                    # Capa de Dominio
│   ├── Entities/
│   │   ├── Project.cs
│   │   ├── UseCase.cs
│   │   ├── Actor.cs
│   │   ├── UseCaseStep.cs
│   │   ├── FunctionalRequirement.cs
│   │   ├── RequirementDependency.cs
│   │   └── EstimationResult.cs
│   ├── Enums/
│   ├── Interfaces/
│   │   ├── Repositories/
│   │   └── Services/
│   └── Common/
│
└── EstimadorAPI.Infrastructure/            # Capa de Infraestructura
    ├── Persistence/
    │   ├── EstimadorDbContext.cs
    │   └── Repositories/
    ├── Services/
    └── DependencyInjection.cs
```

## 🔧 Requisitos

- **.NET 10 SDK**
- **Visual Studio 2022** o **Visual Studio Code**
- **SQL Server** (opcional, si usas la base de datos de SQL Server)
- **Git**

## 🚀 Instalación y Configuración

### 1. Clonar o descargar el proyecto

```bash
git clone <url-del-proyecto>
cd EstimadorAPI
```

### 2. Restaurar dependencias

```bash
dotnet restore
```

### 3. Configurar la Base de Datos

**Opción A: Usando SQLite (más simple)**

Por defecto, la aplicación usa SQLite. Solo necesitas ejecutar la aplicación y se creará automáticamente el archivo `Estimador.db`.

**Opción B: Usando SQL Server**

1. Modifica `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=TU_SERVIDOR;Database=EstimadorDb;Trusted_Connection=true;TrustServerCertificate=true;",
    "SQLite": null
  }
}
```

2. Crea las migraciones (si es necesario):

```bash
cd EstimadorAPI.Infrastructure
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Ejecutar la aplicación

```bash
dotnet run
```

La API estará disponible en:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:7000`
- **Swagger UI**: `http://localhost:5000/swagger`

## 📚 API Endpoints

### Proyectos

```
GET     /api/projects              # Obtener todos los proyectos
GET     /api/projects/{id}         # Obtener proyecto por ID
GET     /api/projects/{id}/detail  # Obtener proyecto con detalles
GET     /api/projects/{id}/estimates # Obtener estimaciones del proyecto
POST    /api/projects              # Crear nuevo proyecto
PUT     /api/projects/{id}         # Actualizar proyecto
DELETE  /api/projects/{id}         # Eliminar proyecto
```

### Casos de Uso

```
GET     /api/usecases/{id}              # Obtener caso de uso
GET     /api/usecases/{id}/detail       # Obtener con detalles
GET     /api/usecases/project/{projectId} # Obtener casos de proyecto
GET     /api/usecases/{id}/estimation   # Obtener estimación
POST    /api/usecases               # Crear nuevo caso de uso
PUT     /api/usecases/{id}          # Actualizar caso de uso
DELETE  /api/usecases/{id}          # Eliminar caso de uso
POST    /api/usecases/{id}/estimate # Generar estimación
```

## 📝 Ejemplo de Uso

### 1. Crear un Proyecto

```bash
POST /api/projects
Content-Type: application/json

{
  "code": "PROJ-001",
  "name": "Sistema de Gestión",
  "description": "Nuevo sistema de gestión empresarial",
  "clientName": "Empresa XYZ"
}
```

**Respuesta:**
```json
{
  "id": 1,
  "code": "PROJ-001",
  "name": "Sistema de Gestión",
  "description": "Nuevo sistema de gestión empresarial",
  "clientName": "Empresa XYZ",
  "totalUseCases": 0,
  "totalRequirements": 0,
  "totalStoryPoints": 0,
  "createdAt": "2024-02-04T12:00:00Z"
}
```

### 2. Crear un Caso de Uso

```bash
POST /api/usecases
Content-Type: application/json

{
  "code": "UC-001",
  "title": "Autenticación de Usuario",
  "description": "Permite que usuarios se autentiquen en el sistema",
  "preconditions": "Usuario registrado",
  "postconditions": "Sesión activa",
  "projectId": 1
}
```

### 3. Obtener Estimación

```bash
POST /api/usecases/1/estimate
```

**Respuesta:**
```json
{
  "id": 1,
  "totalRequirements": 6,
  "totalStoryPoints": 25,
  "simpleRequirements": 2,
  "moderateRequirements": 3,
  "complexRequirements": 1,
  "estimatedDays": 4,
  "complexityPercentage": 16.67,
  "riskScore": 4.5,
  "estimatedCompletionDate": "2024-02-08T12:00:00Z"
}
```

## 🏛️ Patrones y Prácticas

### CQRS (Command Query Responsibility Segregation)
- **Commands**: Modifican el estado (Create, Update, Delete, Estimate)
- **Queries**: Solo leen datos (Get, GetAll, GetDetail)
- Implementado con **MediatR**

### Repository Pattern
- `IRepository<T>`: Interfaz genérica
- `Repository<T>`: Implementación base
- Repositorios específicos: `IProjectRepository`, `IUseCaseRepository`, etc.

### Unit of Work
- `IUnitOfWork`: Coordina múltiples repositorios
- Transacciones automáticas
- Cambios coherentes entre entidades

### Validación
- **FluentValidation**: Validación declarativa
- Validadores por DTO
- Integración automática con ASP.NET Core

### Mapeo de Objetos
- **AutoMapper**: Mapeo entre DTOs y Entidades
- Perfiles de mapeo personalizados
- Transformaciones complejas

## 🔐 Seguridad

- [x] Validación de entrada
- [x] Manejo de errores centralizado
- [x] CORS configurado
- [x] Protección contra inyección de dependencias

**Mejoras futuras:**
- [ ] Autenticación JWT
- [ ] Autorización basada en roles
- [ ] Rate limiting
- [ ] Encriptación de datos sensibles

## 📊 Modelos de Datos

### Project
```csharp
{
  Id: int,
  Code: string,
  Name: string,
  Description: string,
  ClientName: string?,
  UseCases: ICollection<UseCase>,
  CreatedAt: DateTime,
  UpdatedAt: DateTime?
}
```

### UseCase
```csharp
{
  Id: int,
  Code: string,
  Title: string,
  Description: string,
  Preconditions: string?,
  Postconditions: string?,
  Status: UseCaseStatus,
  ProjectId: int,
  Actors: ICollection<Actor>,
  Steps: ICollection<UseCaseStep>,
  Requirements: ICollection<FunctionalRequirement>,
  EstimationResult: EstimationResult?
}
```

### FunctionalRequirement
```csharp
{
  Id: int,
  Code: string,
  Title: string,
  Description: string,
  Complexity: RequirementComplexity, // Simple (3), Moderate (5), Complex (8)
  Status: RequirementStatus,
  StoryPoints: int,
  UseCaseId: int,
  Dependencies: ICollection<RequirementDependency>
}
```

## 🧪 Testing (Próximas versiones)

Se incluirán:
- Unit Tests con xUnit
- Integration Tests
- Mock repositories
- Fixtures de datos

## 📈 Performance

- Lazy loading deshabilitado (explicit eager loading)
- Índices en claves foráneas
- Soft delete para auditoría
- Caché de consultas frecuentes (futuro)

## 🐛 Troubleshooting

### Error: "Database not found"
```bash
# Si usas SQL Server, crea la base de datos manualmente
# O usa SQLite (default)
```

### Error: "Connection timeout"
Verifica la cadena de conexión en `appsettings.json`

### Error de migraciones
```bash
cd EstimadorAPI.Infrastructure
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 📝 Convenciones

### Nomenclatura
- **Entidades**: `PascalCase` (Project, UseCase, FunctionalRequirement)
- **DTOs**: `PascalCase` con sufijo `Dto` (ProjectDto, CreateProjectDto)
- **Comandos**: `PascalCase` con sufijo `Command` (CreateProjectCommand)
- **Queries**: `PascalCase` con sufijo `Query` (GetProjectQuery)

### Archivos
- Controllers: `{Entity}Controller.cs`
- Handlers: `{Entity}Handlers.cs`
- Repositorios: `{Entity}Repository.cs`
- Validadores: `{Dto}Validator.cs`

## 🚀 Próximas Mejoras

- [ ] Autenticación y autorización
- [ ] Rate limiting
- [ ] Caching distribuido
- [ ] GraphQL endpoint
- [ ] Message Queue (RabbitMQ)
- [ ] Health checks
- [ ] Logging centralizado (Serilog)
- [ ] Monitoring y telemetría

## 📄 Licencia

MIT

## 👨‍💻 Autor

Estimador Team warjav-work

## 📧 Contacto

Para soporte o sugerencias, contacta a warjav.work@gmail.com