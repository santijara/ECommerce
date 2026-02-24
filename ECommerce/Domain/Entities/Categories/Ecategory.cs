using ECommerce.Application.Common;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities.Categories
{
    public sealed class Ecategory
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Guid? ParentCategoryId { get; private set; }
        public bool IsActive { get; private set; }

        private Ecategory() { } 

        private Ecategory(Guid id, string name, string? description, Guid? parentCategoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            IsActive = true;
        }

        public static Result<Ecategory> Create(string name,string? description,Guid? parentCategoryId)
        {
            if (string.IsNullOrWhiteSpace(name))return Result<Ecategory>.Failure("Nombre requerido");

            var category = new Ecategory(
                Guid.NewGuid(),
                name,
                description,
                parentCategoryId
            );

            return Result<Ecategory>.Success(category);
        }

    }
}
