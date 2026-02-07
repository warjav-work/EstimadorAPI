using AutoMapper;
using EstimadorAPI.Application.DTOs.Results;
using EstimadorAPI.Application.UseCases.UseCases.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Handlers;

public class GetUseCaseEstimationQueryHandler : IRequestHandler<GetUseCaseEstimationQuery, EstimationResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUseCaseEstimationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EstimationResultDto> Handle(GetUseCaseEstimationQuery request, CancellationToken cancellationToken)
    {
        var estimation = await _unitOfWork.EstimationResults.GetByUseCaseIdAsync(request.UseCaseId);
        if (estimation == null)
            throw new KeyNotFoundException($"Estimación para el caso de uso {request.UseCaseId} no encontrada");

        return _mapper.Map<EstimationResultDto>(estimation);
    }
}
