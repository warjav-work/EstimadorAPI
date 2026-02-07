using AutoMapper;
using EstimadorAPI.Application.DTOs.Results;
using EstimadorAPI.Application.UseCases.UseCases.Commands;
using EstimadorAPI.Domain.Interfaces.Repositories;
using EstimadorAPI.Domain.Interfaces.Services;
using MediatR;

namespace EstimadorAPI.Application.UseCases.UseCases.Handlers;

public class EstimateUseCaseCommandHandler : IRequestHandler<EstimateUseCaseCommand, EstimationResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEstimationService _estimationService;
    private readonly IMapper _mapper;

    public EstimateUseCaseCommandHandler(
        IUnitOfWork unitOfWork,
        IEstimationService estimationService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _estimationService = estimationService;
        _mapper = mapper;
    }

    public async Task<EstimationResultDto> Handle(EstimateUseCaseCommand request, CancellationToken cancellationToken)
    {
        var useCase = await _unitOfWork.UseCases.GetUseCaseWithDetailsAsync(request.Id);
        if (useCase == null)
            throw new KeyNotFoundException($"Caso de uso con ID {request.Id} no encontrado");

        var estimationResult = await _estimationService.EstimateUseCaseAsync(useCase);

        var existingEstimation = await _unitOfWork.EstimationResults.GetByUseCaseIdAsync(request.Id);
        if (existingEstimation != null)
        {
            _unitOfWork.EstimationResults.Delete(existingEstimation);
        }

        await _unitOfWork.EstimationResults.AddAsync(estimationResult);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<EstimationResultDto>(estimationResult);
    }
}
