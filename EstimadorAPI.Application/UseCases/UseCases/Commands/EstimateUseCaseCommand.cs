using EstimadorAPI.Application.DTOs.Results;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Commands;

public record EstimateUseCaseCommand(int Id) : IRequest<EstimationResultDto>;

