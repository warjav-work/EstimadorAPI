using EstimadorAPI.Application.DTOs.UseCases;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Queries;

public record GetUseCaseQuery(int Id) : IRequest<UseCaseDto>;
