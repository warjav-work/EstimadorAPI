using EstimadorAPI.Application.DTOs.UseCases;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Commands;

public record UpdateUseCaseCommand(int Id, UpdateUseCaseDto Dto) : IRequest<UseCaseDto>;

