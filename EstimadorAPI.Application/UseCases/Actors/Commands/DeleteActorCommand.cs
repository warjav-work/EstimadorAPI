using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Commands;

/// <summary>
/// Comando para eliminar un actor
/// </summary>
public record DeleteActorCommand(int Id) : IRequest<bool>;

