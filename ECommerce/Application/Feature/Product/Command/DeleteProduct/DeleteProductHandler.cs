using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Feature.Product.Command.DeleteProduct
{
    /// <summary>
    /// Maneja la eliminación de un producto.
    /// Orquesta la obtención del agregado y su eliminación,
    /// asegurando persistencia transaccional mediante UnitOfWork.
    /// </summary>
    public class DeleteProductHandler: IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result>Handle(DeleteProductCommand command, CancellationToken cancellation)
        {
            //Obtener el producto desde el repositorio
            var product = await _productRepository.GetByIdAsync(command.Id, cancellation);
            if (product == null) return Result.Failure("No se encontro informacion");

            //Eliminación del agregado
            // Aquí se delega al repositorio la responsabilidad de remover la entidad.
            _productRepository.Remove(product);

            //Confirmar los cambios dentro de una transacción
            await _unitOfWork.SaveChangesAsync(cancellation);

            return Result.Success();
        }
    }
}
