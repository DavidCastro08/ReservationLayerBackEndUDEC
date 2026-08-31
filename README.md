# ReservationArquitectureLayerUDEC

API REST para gestión de reservas (hoteles, usuarios y reservas).

## Descripción
Proyecto en .NET 5 que implementa una arquitectura por capas: controladores (API), servicios de lógica de negocio y capa de infraestructura con Entity Framework Core para acceso a base de datos.

El objetivo es gestionar reservas de hotel: crear, actualizar, inactivar y consultar reservas; así como listar usuarios y hoteles.

## Requisitos
- .NET 5 SDK
- SQL Server (o instancia compatible)
- Visual Studio 2019/2022 o VS Code
- Postman o curl (para pruebas)

## Estructura principal
- `Program.cs` / `Startup.cs` - configuración del host, middleware, Swagger y CORS.
- `DependencyInjection/DependencyInjection.cs` - registro de dependencias y DbContext.
- `Infrastructure/ReservationLinkTicContext.cs` - DbContext generado por EF Core.
- `Infrastructure/*.cs` - entidades mapeadas: `Hotele`, `Reserva`, `Usuario`.
- `Infrastructure/Reservations/ReservationsInfrastructure.cs` - repositorio con acceso a datos.
- `LogicServices/Reservations/Reservations.cs` - lógica de negocio y validaciones.
- `LogicServices/Contracts/IReservations/IReservations.cs` - contrato de servicios.
- `Controllers/ReservationController.cs` - endpoints REST.
- `Domain/Reservations/SaveReservationsDTO.cs` - DTO para guardar/actualizar reservas.
- `DTO/Reservation/ReturnReservationDTO.cs` y `DTO/Generics/*` - DTOs de respuesta.

## Base de datos (modelo resumido)
- `Hoteles` (Hotele): `HotelID (Guid)`, `Nombre`, `Direccion`, `Ciudad`, `Telefono`, `NumeroHabitaciones (int)`.
- `Usuarios` (Usuario): `UsuarioID (Guid)`, `Nombre`, `Correo` (único), `Telefono`, `FechaRegistro`, `Contraseña`.
- `Reservas` (Reserva): `ReservaID (Guid)`, `UsuarioID`, `HotelID`, `FechaReserva`, `FechaInicial`, `FechaFinal`, `NumeroHabitaciones`, `NumeroPersonas`, `Observaciones`, `Estado (bool)`.

## Endpoints (Controller: `Reservation`)
Rutas base: `/Reservation`

- POST `/Reservation/SaveReservation`
  - Crea una reserva si hay disponibilidad y el usuario no tiene reserva activa.
  - Request: `SaveReservationsDTO` (ver `Domain/Reservations/SaveReservationsDTO.cs`).
  - Response: `ResponseDTO` (`isValid`, `message`, `error`).

- PUT `/Reservation/UpdateReservation`
  - Actualiza la reserva activa del usuario si hay disponibilidad.
  - Request: `SaveReservationsDTO`.

- DELETE `/Reservation/InactivateReservation?user={guid}`
  - Inactiva la reserva del usuario (marca `Estado = false`).

- GET `/Reservation/GetReservation?initialDate={}&finalDate={}&hotel={guid}&customer={guid}`
  - Obtiene reservas filtradas por fecha, hotel y/o cliente.
  - Response: `List<ReturnReservationDTO>` (fechas formateadas `yyyy/MM/dd  HH:mm:ss`).

- GET `/Reservation/SearchUser?email={email}&password={password}`
  - Busca usuario por correo y contraseña; devuelve `ResponseDTO` con `message`=nombre si coincide.
  - Atención: la comparación es de texto plano (ver recomendaciones).

- GET `/Reservation/GetAllUsers`
  - Devuelve `List<ResultSelectors>` para selectores (valueExpr = Guid, displayExpr = nombre).

- GET `/Reservation/GetAllHoteles`
  - Devuelve `List<ResultSelectors>` para selectores.

## Configuración y ejecución local
1. Abrir la carpeta raíz del proyecto (contiene `.csproj`).
2. Restaurar dependencias: `dotnet restore`.
3. Compilar: `dotnet build`.
4. Ejecutar: `dotnet run`.
5. Swagger UI: `https://localhost:{puerto}/swagger`.

## Configuración de la conexión a la base de datos
Actualmente la conexión a SQL Server está hardcodeada en dos lugares:
- `DependencyInjection/DependencyInjection.cs` (RegisterProfile)
- `Infrastructure/ReservationLinkTicContext.cs` (OnConfiguring)

Recomendación: mover la cadena de conexión a `appsettings.json` y usar `Configuration.GetConnectionString("DefaultConnection")` al registrar el DbContext. Evitar valores hardcodeados o sensibles en el código fuente.

Ejemplo en `appsettings.json`:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=ReservationArquitectureLayerUDEC;Trusted_Connection=True;"
  }
}

## Observaciones y mejoras recomendadas
- Seguridad de contraseñas: actualmente `Usuario.Contraseña` se compara en texto plano. Implementar hashing (bcrypt o Identity) y no exponer contraseñas.
- No dejar connection strings hardcodeadas. Usar `User Secrets` o variables de entorno para entornos de desarrollo/producción.
- Añadir validaciones a DTOs (DataAnnotations o FluentValidation) para entradas de API.
- Mejorar manejo de errores y logging con `ILogger` y respuestas HTTP apropiadas.
- Añadir autenticación y autorización (JWT / Identity) para proteger endpoints.
- Añadir pruebas unitarias e integración para lógica e infraestructura.

## Notas de diseño
- Separación clara por capas: Controller -> Service (logic) -> Infrastructure (EF Core).
- Se usan expresiones lambda para proyecciones (mappers) en la capa de lógica.

## Siguiente paso
Puedo:
- Actualizar el proyecto para mover la connection string a `appsettings.json` y adaptar `DependencyInjection` y `DbContext`.
- Añadir validaciones básicas a DTOs.
- Añadir hashing de contraseñas y proteger `SearchUser`.

Si deseas que cree alguno de esos cambios ahora, indícamelo y lo implemento.
