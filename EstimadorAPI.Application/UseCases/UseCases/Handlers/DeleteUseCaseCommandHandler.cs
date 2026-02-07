using EstimadorAPI.Application.UseCases.UseCases.Commands;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Handlers;

public class DeleteUseCaseCommandHandler : IRequestHandler<DeleteUseCaseCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUseCaseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUseCaseCommand request, CancellationToken cancellationToken)
    {
        var useCase = await _unitOfWork.UseCases.GetByIdAsync(request.Id);
        if (useCase == null)
            throw new KeyNotFoundException($"Caso de uso con ID {request.Id} no encontrado");

        _unitOfWork.UseCases.Delete(useCase);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
