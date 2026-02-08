using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Commands;

/// <summary>
/// Comando para cambiar el responsable de pasos (reasignar actor)
/// </summary>
public record ReassignActorCommand(int OldActorId, int NewActorId) : IRequest<int>;

