using EstimadorAPI.Application.DTOs.UseCases;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Queries;

public record GetProjectUseCasesQuery(int ProjectId) : IRequest<IEnumerable<UseCaseDto>>;

