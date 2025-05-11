

using Fc.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using MediatR;

namespace Fc.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;

public class DeleteCategoryUseCase : IDeleteCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryUseCase(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCategoryInputDTO request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);

        await _categoryRepository.Delete(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Unit.Value;
    }
}
