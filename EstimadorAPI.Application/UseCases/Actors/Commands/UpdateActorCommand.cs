using EstimadorAPI.Application.DTOs.Actors;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Commands;

/// <summary>
/// Comando para actualizar un actor existente
/// </summary>
public record UpdateActorCommand(int Id, UpdateActorDto Dto) : IRequest<ActorDto>;
