using ECommerce.Application.Common;
using ECommerce.Application.Dtos.ProductsDtos;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Feature.Product.Query.GetAllProduct
{
    /// <summary>
    /// Maneja la consulta para obtener todos los productos.
    /// Forma parte del modelo de lectura (CQRS).
    /// Se encarga de proyectar entidades de dominio hacia DTOs,
    /// </summary>
    public class GetAllProductHandler: IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductDto>>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IEnumerable<ProductDto>>>Handle(GetAllProductsQuery query, CancellationToken cancellation)
        {
            //Obtener datos desde el repositorio
            var response = await _productRepository.GetAllAsync(cancellation);

            //Validación 
            if (response == null) return Result<IEnumerable<ProductDto>>.Failure("No se encontro informacion");

            //Proyección a DTO (evita exponer entidad de dominio)
            return Result<IEnumerable<ProductDto>>.Success(response.Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Price.Amount, x.Price.Currency, x.CategoryId, x.IsActive)));
        }
    }
}
