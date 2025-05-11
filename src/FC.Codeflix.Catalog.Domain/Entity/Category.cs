using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Domain.SeedWork;
using FC.Codeflix.Catalog.Domain.Validation;
namespace FC.Codeflix.Catalog.Domain.Entity;

public class Category: AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }


    public Category(string name, string description,bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;

        Validate();
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }

    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }

    public void Update(string name,string? description = null)
    {
        Name = name;
        Description = description ?? Description;
        Validate();
    }

    private void Validate()
    {
        //if(String.IsNullOrEmpty(Name) || String.IsNullOrWhiteSpace(Name))
        //    throw new EntityValidationException($"{nameof(Name)} should not be empty or null");

        DomainValidation.NotNullOrEmpty(Name,nameof(Name));

        //if (Name.Length < 3)
        //    throw new EntityValidationException($"{nameof(Name)} cannot be less than 3 characters long");

        DomainValidation.MinLength(Name, 3, nameof(Name));

        //if (Name.Length > 255)
        //    throw new EntityValidationException($"{nameof(Name)} cannot have more than 255 characters long");
        DomainValidation.MaxLength(Name, 255, nameof(Name));

        //if (Description == null)
        //    throw new EntityValidationException($"{nameof(Description)} should not be null");

        DomainValidation.NotNull(Description, nameof(Description));

        //if (Description.Length > 10000)
        //    throw new EntityValidationException($"{nameof(Description)} cannot have more than 10000 characters long");

        DomainValidation.MaxLength(Description,10000,nameof(Description));
    }

}
