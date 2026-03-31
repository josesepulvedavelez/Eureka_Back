# Contexto del Proyecto - EurekaBack

## Estado Actual
- **Tecnología**: .NET 7
- **Tipo**: WebAPI (único proyecto)
- **Nombre del proyecto**: EurekaBack
- **Estructura actual**: 
  - Controllers (ArticulosController, ClientesController, FacturasController)
  - Models (Articulo, Cliente, Factura, FacturaDetalle)
  - DTOs (FacturaDto, FacturaDetalleDto)
  - Data (EurekaContext - DbContext)
  - Program.cs con configuración directa

## Endpoints Actuales

### ArticulosController (`api/Articulos`)
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `AddArticulo` | Crear nuevo artículo |
| GET | `GetArticulos` | Obtener todos los artículos |
| GET | `GetArticulo/{articuloId}` | Obtener artículo por ID |
| PUT | `UpdateArticulo/{articuloId}` | Actualizar artículo |

### ClientesController (`api/Clientes`)
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `AddCliente` | Crear nuevo cliente |
| GET | `GetClientes` | Obtener todos los clientes |
| GET | `GetCliente/{clienteId}` | Obtener cliente por ID |
| PUT | `UpdateCliente/{clienteId}` | Actualizar cliente |

### FacturasController (`api/Facturas`)
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `GetFacturas` | Obtener todas las facturas con datos del cliente |
| GET | `GetFacturas/{fechaInicial}/{fechaFinal}` | Obtener facturas por rango de fechas |
| GET | `GetFactura/{facturaId}` | Obtener factura específica con datos del cliente |
| GET | `GetFacturaDetalle/{facturaId}` | Obtener detalles de una factura |
| POST | `AddFactura` | Crear nueva factura con sus detalles (transacción) |

## Modelos de Datos (Entidades)

### Articulo
- Codigo (string)
- Descripcion (string)
- Costo (decimal)
- Porcentaje (double)
- PrecioSugerido (decimal)
- Cantidad (int)
- Estado (bool)
- ArticuloId (int - PK)

### Cliente
- Cc_Nit (string)
- Nombre_RazonSocial (string)
- Direccion (string)
- Telefono (string)
- Estado (bool)
- ClienteId (int - PK)

### Factura
- No (string)
- Fecha (DateTime)
- Total (decimal)
- ClienteId (int - FK)
- FacturaId (int - PK)
- lstFacturaDetalle (List<FacturaDetalle>)

### FacturaDetalle
- Precio (decimal)
- Cantidad (int)
- SubTotal (decimal)
- ArticuloId (int - FK)
- FacturaId (int - FK)
- FacturaDetalleId (int - PK)

## DTOs Actuales
- **FacturaDto**: Incluye datos del cliente + datos de factura + lista de detalles
- **FacturaDetalleDto**: Incluye datos del artículo + datos del detalle

## Reglas de Negocio Identificadas
1. **Al crear factura**:
   - Se debe crear en una transacción
   - Se actualiza el stock del artículo (restar cantidad vendida)
   - Se calcula el total sumando los subtotales de los detalles
   - Se relaciona con un cliente existente

2. **Al actualizar artículo**: Se actualizan todos los campos excepto el ID
3. **Al actualizar cliente**: Se actualizan todos los campos excepto el ID
4. **Estado**: Clientes y artículos tienen campo Estado (booleano) para activo/inactivo

## Objetivo - Clean Architecture
Transformar EurekaBack manteniendo los controladores exactamente como están actualmente.

### Capas a crear:

1. **EurekaBack.Domain** (Class Library)
   - Entities: Articulo, Cliente, Factura, FacturaDetalle
   - Enums: (si aplica)
   - Interfaces: IArticuloRepository, IClienteRepository, IFacturaRepository
   - Exceptions: DomainExceptions personalizadas

2. **EurekaBack.Application** (Class Library)
   - DTOs: FacturaDto, FacturaDetalleDto (mover los existentes)
   - Features/
     - Articulos/
       - Commands: CreateArticuloCommand, UpdateArticuloCommand
       - Queries: GetArticulosQuery, GetArticuloByIdQuery
       - Handlers: (cada command/query con su handler)
     - Clientes/
       - Commands: CreateClienteCommand, UpdateClienteCommand
       - Queries: GetClientesQuery, GetClienteByIdQuery
       - Handlers: (cada command/query con su handler)
     - Facturas/
       - Commands: CreateFacturaCommand
       - Queries: GetFacturasQuery, GetFacturasByDateRangeQuery, GetFacturaByIdQuery, GetFacturaDetalleQuery
       - Handlers: (cada command/query con su handler)
   - Interfaces: IApplicationDbContext (abstracción)
   - Mappers: Mappers manuales (sin AutoMapper)
   - Validators: FluentValidation para cada Command

3. **EurekaBack.Infrastructure** (Class Library)
   - Data/
     - EurekaContext.cs (mover el existente)
     - Configurations/EntityConfigurations (Fluent API)
   - Repositories/
     - ArticuloRepository
     - ClienteRepository
     - FacturaRepository
   - UnitOfWork/
     - IUnitOfWork
     - UnitOfWork (implementación)
   - DependencyInjection.cs (configuración de servicios)

4. **EurekaBack.WebAPI** (proyecto renombrado)
   - Controllers/ (exactamente como están)
   - Middleware/
     - GlobalExceptionHandlerMiddleware.cs
   - Program.cs (configurar DI con las nuevas capas)

## Restricciones Técnicas
- **NO usar AutoMapper**: Mapeo manual entre entidades y DTOs
- **EF Core sin migraciones**: La base de datos ya existe. Usar DbContext con las tablas existentes. Las configuraciones de Fluent API deben coincidir con la estructura actual de la BD
- **Controladores intactos**: Mantener exactamente la misma estructura, rutas y nombres. Solo cambiar inyección de dependencias: de `EurekaContext` a `IMediator`
- **CQRS con MediatR**: Implementar Commands y Queries
- **Manejo de excepciones**: Middleware global centralizado que reemplace los try-catch dispersos
- **Transacciones**: Mantener la lógica transaccional de Facturas en el Application Layer usando UnitOfWork

## Tecnologías a Implementar
- **MediatR**: Para implementar CQRS
- **FluentValidation**: Para validaciones en capa de aplicación
- **Entity Framework Core**: Solo para consulta/actualización (sin migraciones)
- **Swagger/OpenAPI**: Mantener documentación de API

## Estructura de Carpetas Esperada
