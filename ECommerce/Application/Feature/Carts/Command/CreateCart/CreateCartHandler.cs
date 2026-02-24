using ECommerce.Application.Common;
using ECommerce.Application.Dtos.CartDtos;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Carts;
using MediatR;

namespace ECommerce.Application.Feature.Carts.Command.CreateCart
{
    /// <summary>
    /// Caso de uso encargado de crear un nuevo carrito para un usuario
    /// y agregar un producto seleccionado.
    /// </summary>
    public class CreateCartHandler: IRequestHandler<CreateCartCommand, Result<CreateCartResponse>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IProductRepository _productRepository;

        public CreateCartHandler(IUnitOfWork unitOfWork, ICartRepository cartRepository, IUserService userService, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _userService = userService;
            _productRepository = productRepository;
        }

        public async Task<Result<CreateCartResponse>>Handle(CreateCartCommand create, CancellationToken cancellationToken)
        {
            //Validar existencia del usuario.
            // Se evita crear carritos asociados a usuarios inexistentes.
            var user = await _userService.GetByIdUser(create.UserId);
            if (user == null) return Result<CreateCartResponse>.Failure("No se encontro usuario");

            //Validar existencia del producto.
            //Esto evita crear carritos sin productos disponibles
            var product = await _productRepository.GetByIdAsync(create.ProductId, cancellationToken);
            if (product == null) return Result<CreateCartResponse>.Failure("No se encontro producto");

            //Crear el carrito mediante el patron de diseno de Factory Method          
            var cart = Cart.create(user.Id);

            //Agregar el producto al carrito.
            //El dominio valida cantidad y precio internamente.
            cart.AddItem(product.Id, create.Quantity, product.Price);

            //Persistir cambios
            await _cartRepository.AddAsync(cart);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //Retornar respuesta estructurada del caso de uso.
            return Result<CreateCartResponse>.Success(new CreateCartResponse() {Id = cart.Id, ProductId = product.Id, UserId = user.Id, Quantity = create.Quantity});
           
        }
    }
}
