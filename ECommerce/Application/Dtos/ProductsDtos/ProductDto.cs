namespace ECommerce.Application.Dtos.ProductsDtos
{
    /// <summary>
    /// DTO que representa la información necesaria para agregar un producto (finalizar compra) en el sistema.
    /// Se utiliza para recibir los datos desde el cliente o la API.
    /// </summary>

    public sealed record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Amount,
    string Currency,
    Guid CategoryId,
    bool IsActive
);


}
