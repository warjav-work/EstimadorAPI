using EstimadorAPI.Application.DTOs.Actors;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Commands;

/// <summary>
/// Comando para crear un nuevo actor
/// </summary>
public record CreateActorCommand(CreateActorDto Dto) : IRequest<ActorDto>;

