using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Domain.Enums;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Commands;

/// <summary>
/// Comando para cambiar el tipo de un actor
/// </summary>
public record ChangeActorTypeCommand(int Id, ActorType NewType) : IRequest<ActorDto>;

