using EstimadorAPI.Application.DTOs.Actors;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Queries;

/// <summary>
/// Query para obtener todos los actores con sus pasos relacionados
/// </summary>
public record GetActorWithStepsQuery(int ActorId) : IRequest<ActorDetailDto>;

