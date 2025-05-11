
using Fc.Codeflix.Catalog.Application.Interfaces;
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Repository;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryUseCase : ICreateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryUseCase(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork        
    )
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryOutputDTO> Handle(
        CreateCategoryInputDTO input,
        CancellationToken cancellationToken
    )
    {
        var category = new DomainEntity.Category(
            input.Name,
            input.Description,
            input.IsActive
        );

        await _categoryRepository.Insert(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return CategoryOutputDTO.FromCategory(category);
    }
}
