using EstimadorAPI.Application.DTOs.Actors;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Queries;


/// <summary>
/// Query para obtener estadisticas de actores de un UseCase
/// </summary>
public record GetActorStatisticsQuery(int useCaseId) : IRequest<ActorStatisticsDto>;
