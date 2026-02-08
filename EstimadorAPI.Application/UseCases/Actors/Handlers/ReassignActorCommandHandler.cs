using EstimadorAPI.Application.UseCases.Actors.Commands;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Handlers;

/// <summary>
/// Handler para reasignar pasos de un actor a otro
/// </summary>
public class ReassignActorCommandHandler : IRequestHandler<ReassignActorCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReassignActorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(ReassignActorCommand request, CancellationToken cancellationToken)
    {
        var oldActor = await _unitOfWork.Actors.GetByIdAsync(request.OldActorId);
        if (oldActor == null)
            throw new KeyNotFoundException($"Actor antiguo {request.OldActorId} no encontrado");

        var newActor = await _unitOfWork.Actors.GetByIdAsync(request.NewActorId);
        if (newActor == null)
            throw new KeyNotFoundException($"Nuevo actor {request.NewActorId} no encontrado");

        if (oldActor.UseCaseId != newActor.UseCaseId)
            throw new InvalidOperationException("Los actores deben estar en el mismo UseCase");

        var steps = await _unitOfWork.UseCaseSteps.FindAsync(s =>
            s.ResponsibleActorId == request.OldActorId && !s.IsDeleted);

        int count = 0;
        foreach (var step in steps)
        {
            step.ResponsibleActorId = request.NewActorId;
            _unitOfWork.UseCaseSteps.Update(step);
            count++;
        }

        await _unitOfWork.SaveChangesAsync();
        return count;
    }
}
