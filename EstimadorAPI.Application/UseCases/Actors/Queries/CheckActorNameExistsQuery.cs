using MediatR;

namespace EstimadorAPI.Application.UseCases.Actors.Queries;

public record CheckActorNameExistsQuery(int UseCaseId, string Name, int? ExcludeActorId = null) : IRequest<bool>;


