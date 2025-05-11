
using Fc.Codeflix.Catalog.Application.Interfaces;
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Repository;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategoryUseCase : IUpdateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryUseCase(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryOutputDTO> Handle(UpdateCategoryInputDTO request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);

        category.Update(request.Name,request.Description);

        if (category.IsActive != request.IsActive)
        {
            if (request.IsActive) category.Activate();
            else category.Deactivate();
        };

        await _categoryRepository.Update(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return CategoryOutputDTO.FromCategory(category);
    }
}
