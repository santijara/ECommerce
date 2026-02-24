# ECommerce API – Clean Architecture (.NET 8)

API REST desarrollada con .NET 8 para la gestión y venta de productos en línea, aplicando:
- Clean Architecture
- Principios SOLID
- Domain-Driven Design (DDD)
- Result Pattern
- Repository Pattern
- UnitOfWork
- FluentValidation
- Middleware global de excepciones


## Características
- Gestión de usuarios
- Administración de inventario y productos
- Creación y gestión de carritos de compra
- Generación de órdenes
- Procesamiento de pagos
- Generación automática de factura electrónica

## Flujo Principal del Sistema
- El usuario agrega productos al carrito.
- Se genera una orden de compra.
- Se procesa el pago.
- Al confirmarse el pago:
- Se genera automáticamente la factura electrónica.
- La factura es enviada al correo electrónico del cliente.


# Tecnologías Utilizadas
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- FluentValidation
- Clean Architecture
- Domain-Driven Design (DDD)
- Result Pattern
- Middleware personalizado para manejo de excepciones


# Arquitectura

El proyecto sigue la estructura de Clean Architecture, separando responsabilidades en capas independientes:

ECommerce/
 ├── Domain
 ├── Application
 ├── Infrastructure
 

## Domain
- Entidades
- Value Objects
- Excepciones de dominio
- Reglas de negocio
- Interfaces (ej. IUnitOfWork)

## Application
- Commands / Queries
- Handlers
- DTOs
- Validaciones (FluentValidation)
- Interfaces de repositorios

## Infrastructure
- DbContext
- Repositorios
- UnitOfWork
- Integraciones externas

## Api
- Controllers
- Configuración de DI
- Middleware
- Configuración HTTP


# Manejo Global de Excepciones
Se implementa un middleware personalizado para capturar y transformar excepciones en respuestas HTTP consistentes.

Mapeo de excepciones:

Excepción	          Código HTTP
NotFoundException	    404
ValidationExceptions	400
ConflictExceptions	    409
DomainExceptions	    422
Exception	            500


# Flujo de una Petición
1 Cliente envía request
2 Controller recibe comando
3 Se ejecuta validación (FluentValidation)
4 Handler procesa la lógica
5 Se guardan cambios con UnitOfWork
6 Middleware maneja posibles excepciones
7 Se retorna ApiResponse<T>

# Testing
Se implementan pruebas unitarias en:
-Application Layer (Handlers)
- Domain
- Infrastructure

## Se usa:
- xUnit
- FluentAssertions
- Moq
- UseInMemoryDataBase


# Principios Aplicados
- Separación de responsabilidades
- Inversión de dependencias
- Encapsulamiento de dominio
- Manejo centralizado de errores
- Código testeable
- Arquitectura mantenible y escalable


# Posibles Mejoras Futuras
-Implementar CQRS completo con MediatR
- Implementar Domain Events
- Agregar autenticación JWT
- Dockerización
- Logging estructurado con Serilog
- Pruebas de integración


# Como Ejecutar el Proyecto
bash
- git clone https://github.com/santijara/ECommerce.git
- cd ECommerce
- dotnet restore
- dotnet run --project ECommerce/ECommerce.csproj

# Ejecutar pruebas
bash
dotnet test

# Autor
Santiago Jaramillo Torres
Backend Developer – .NET