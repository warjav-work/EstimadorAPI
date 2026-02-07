using EstimadorAPI.Application.DTOs.Projects;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Projects.Queries;

public record GetProjectDetailQuery(int Id) : IRequest<ProjectDetailDto>;
