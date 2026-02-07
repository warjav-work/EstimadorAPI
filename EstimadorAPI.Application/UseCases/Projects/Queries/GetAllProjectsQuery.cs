using EstimadorAPI.Application.DTOs.Projects;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Projects.Queries;

public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>;
