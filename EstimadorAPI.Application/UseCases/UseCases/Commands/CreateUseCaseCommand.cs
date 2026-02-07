using EstimadorAPI.Application.DTOs.UseCases;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Commands;

public record CreateUseCaseCommand(CreateUseCaseDto Dto) : IRequest<UseCaseDto>;

