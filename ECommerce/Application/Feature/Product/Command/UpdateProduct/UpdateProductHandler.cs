using ECommerce.Application.Common;
using ECommerce.Application.Dtos.ProductsDtos;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Products;
using MediatR;

namespace ECommerce.Application.Feature.Product.Command.UpdateProduct
{
    /// <summary>
    /// Maneja la actualización de un producto existente.
    /// Orquesta la obtención del agregado, valida el Value Object Money
    /// y delega la modificación al dominio para proteger invariantes.
    /// </summary>
    public class UpdateProductHandler: IRequestHandler<UpdateProductCommand, Result<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductDto>>Handle(UpdateProductCommand update, CancellationToken cancellation)
        {
            //Obtener el producto
            var product = await _productRepository.GetByIdAsync(update.Id, cancellation);
            if (product == null) return Result<ProductDto>.Failure("No se encontro productos con este id");

            //Validar y crear el Value Object Money
            //Se protege la consistencia del precio antes de modificar el agregado.
            var money = Money.Create(update.Price, update.Currency);
            if(money.IsFailure) return Result<ProductDto>.Failure(money.Error);

            //Delegar la actualización al agregado
            //dominio es responsable de validar reglas internas.
            product.Update(update.Name, money.Value);

            //Persistir los cambios dentro de una transacción
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellation);

            //Proyección a DTO para el modelo de respuesta
            return Result<ProductDto>.Success(new ProductDto(product.Id,product.Name,product.Description,
                product.Price.Amount, product.Price.Currency,product.CategoryId, product.IsActive));
        }
    }
}
