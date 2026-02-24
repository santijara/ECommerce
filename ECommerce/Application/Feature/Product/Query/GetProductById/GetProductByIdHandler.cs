using ECommerce.Application.Common;
using ECommerce.Application.Dtos.ProductsDtos;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Feature.Product.Query.GetProductById
{
    public sealed class GetProductByIdHandler: IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        /// <summary>
        /// Maneja la consulta para obtener un producto específico por su identificador.
        /// 
        /// Forma parte del modelo de lectura (CQRS).
        /// Se encarga de recuperar el agregado desde el repositorio
        /// y proyectarlo hacia un DTO sin exponer la entidad de dominio.
        /// </summary>
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request,CancellationToken cancellationToken)
        {
            //Obtener producto
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

            //Validación
            if (product is null) return Result<ProductDto>.Failure("No se encontro informacion de productos");

            //Proyección a DTO
            return Result<ProductDto>.Success(new ProductDto(
     product.Id,
     product.Name,
     product.Description,
     product.Price.Amount,
     product.Price.Currency,
     product.CategoryId,
     product.IsActive
 ));

        }
    }

}
