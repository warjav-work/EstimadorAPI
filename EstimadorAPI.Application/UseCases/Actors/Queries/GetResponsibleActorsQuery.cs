using EstimadorAPI.Application.DTOs.Actors;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Queries;

/// <summary>
/// Query para obtener actores que son responsables de pasos
/// </summary>
public record GetResponsibleActorsQuery(int UseCaseId) : IRequest<IEnumerable<ActorDto>>;
