using MediatR;

namespace EstimadorAPI.Application.UseCases.Projects.Commands;

public record DeleteProjectCommand(int Id) : IRequest<bool>;
