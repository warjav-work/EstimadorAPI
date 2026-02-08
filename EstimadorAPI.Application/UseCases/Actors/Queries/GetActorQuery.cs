using EstimadorAPI.Application.DTOs.Actors;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Queries;

/// <summary>
/// Query para obtener un actor por ID
/// </summary>
public record GetActorQuery(int Id) : IRequest<ActorDto>;