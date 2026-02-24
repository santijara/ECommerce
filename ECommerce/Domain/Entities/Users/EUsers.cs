using ECommerce.Application.Common;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities.Users
{

    /// <summary>
    /// Entidad de dominio "Usuarios" (Users)
    /// Representa los usuarios que se pueden crear en el sistema
    /// </summary>
    public class EUsers
    {
        public Guid Id { get; private set; }
        public string Name { get;private set; }
        public string LastName { get; private set; }
        public string NumberPhone { get; private set; }
        public Email Email { get; private set; }
        public string Adress { get; private set; }
        public string Document { get; private set; }

        //EF
        private EUsers() { }
       public EUsers(string name, string lastName, string numberPhone, Email email, string adress, string document)
        {
            Id = Guid.NewGuid();
            Name = name;
            LastName = lastName;
            NumberPhone = numberPhone;
            Email = email;
            Adress = adress;
            Document = document;
        }
    }



    /// <summary>
    /// Objeto de valor "Email"
    /// 🔹 Incluye validación de negocio en la creación.
    /// </summary>
    public sealed record Email
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        //Factory Method
        public static Result<Email>Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return Result<Email>.Failure("Email invalido");
            if (!value.Contains("@")) return Result<Email>.Failure("Email incorrecto");

            return Result<Email>.Success(new Email(value.ToLowerInvariant()));
        }

        // Se utiliza para relacionar el Email hacia la base de datos
        internal static Email FromDatabase(string value)
        => new Email(value);

        public override string ToString() => Value;
       
    }

}
