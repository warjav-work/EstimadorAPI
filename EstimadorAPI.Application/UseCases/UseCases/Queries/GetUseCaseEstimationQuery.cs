using EstimadorAPI.Application.DTOs.Results;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Queries;

public record GetUseCaseEstimationQuery(int UseCaseId) : IRequest<EstimationResultDto>;

