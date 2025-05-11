using Fc.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Repository;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesUseCase : IListCategories
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategoriesUseCase(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ListCategoriesOutputDTO> Handle(ListCategoriesInputDTO input, CancellationToken cancellationToken)
    {
        var searchOutput = await _categoryRepository.Search(
            new(
                page: input.Page,
                perPage: input.PerPage,
                search: input.Search,
                orderBy: input.Sort,
                order: input.Dir
            ),
            cancellationToken
        );

        var response = new ListCategoriesOutputDTO(
            page: searchOutput.CurrentPage,
            perPage: searchOutput.PerPage,
            total: searchOutput.Total,
            items: searchOutput.Items.Select(x => CategoryOutputDTO.FromCategory(x)).ToList()
        );


        return response;
    }
}
