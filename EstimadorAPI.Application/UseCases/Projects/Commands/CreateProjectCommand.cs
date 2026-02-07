using EstimadorAPI.Application.DTOs.Projects;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Projects.Commands;

public record CreateProjectCommand(CreateProjectDto Dto) : IRequest<ProjectDto>;
