using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Domain.Enums;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Queries;

/// <summary>
/// Query para obtener actores por tipo
/// </summary>
public record GetActorsByTypeQuery(int UseCaseId, ActorType Type) : IRequest<IEnumerable<ActorDto>>;
