using EstimadorAPI.Application.DTOs.Actors;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Queries;

/// <summary>
/// Query para obtener todos los actores de un caso de uso
/// </summary>
public record GetUseCaseActorsQuery(int UseCaseId) : IRequest<IEnumerable<ActorDto>>;
