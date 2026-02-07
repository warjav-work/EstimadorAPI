using EstimadorAPI.Application.DTOs.ProjectEstimates;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Projects.Queries;

public record GetProjectEstimatesQuery(int Id) : IRequest<ProjectEstimatesDto>;
