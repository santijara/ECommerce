using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Products;
using MediatR;

namespace ECommerce.Application.Feature.Product.Command.CreateProduct
{
    /// <summary>
    /// Maneja la creación de un producto.
    /// Orquesta la validación de Value Objects (Money),
    /// delega la creación al dominio y persiste el agregado.
    /// </summary>
    public sealed class CreateProductHandler
     : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(
            IProductRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateProductCommand request,CancellationToken cancellationToken)
        {
            //Creación del Value Object Money
            //Se valida que el monto y la moneda sean correctos antes de crear el agregado Product.
            var moneyResult = Money.Create(request.Amount, request.Currency);

            if (moneyResult.IsFailure)
                return Result<Guid>.Failure(moneyResult.Error);

            //Creación del agregado Product mediante Factory Method
            //El dominio es responsable de proteger sus invariantes.
            var product = Eproducts.Create(       
                request.Name,
                request.Description,
                moneyResult.Value
                );

            //Persistencia del agregado dentro de una transacción
            await _repository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Retorno del identificador generado
            return Result<Guid>.Success(product.Id);
        }

    }

}
