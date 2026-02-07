using EstimadorAPI.Application.DTOs.Projects;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Projects.Commands;

public record UpdateProjectCommand(int Id, UpdateProjectDto Dto) : IRequest<ProjectDto>;

