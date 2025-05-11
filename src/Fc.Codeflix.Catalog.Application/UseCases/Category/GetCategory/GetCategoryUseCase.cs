
using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Repository;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategoryUseCase : IGetCategory
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryUseCase(
        ICategoryRepository categoryRepository
    )
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryOutputDTO> Handle(GetCategoryInputDTO input, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(input.Id, cancellationToken);

        return CategoryOutputDTO.FromCategory(category);
    }

}
