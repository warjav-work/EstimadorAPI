using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Commands;

public record DeleteUseCaseCommand(int Id) : IRequest<bool>;
