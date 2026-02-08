using EstimadorAPI.Application.UseCases.Actors.Commands;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para eliminar un actor
/// </summary>
public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteActorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(request.Id);
        if (actor == null)
            throw new KeyNotFoundException($"Actor con ID {request.Id} no encontrado");

        // Limpiar referencias en UseCaseSteps
        var steps = await _unitOfWork.UseCaseSteps.FindAsync(s =>
            s.ResponsibleActorId == request.Id && !s.IsDeleted);

        foreach (var step in steps)
        {
            step.ResponsibleActorId = null;
            _unitOfWork.UseCaseSteps.Update(step);
        }

        _unitOfWork.Actors.Delete(actor);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
