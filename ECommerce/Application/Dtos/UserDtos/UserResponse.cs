namespace ECommerce.Application.Dtos.UserDtos
{
    /// <summary>
    /// DTO que representa la respuesta al crear un usuario en la aplicación.
    /// Contiene los datos necesarios del usuario creado.
    /// </summary>
    public class UserResponse
    {
       public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NumberPhone { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Document { get; set; }
    }
}
