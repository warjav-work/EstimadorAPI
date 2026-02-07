using EstimadorAPI.Application.DTOs.ProjectEstimates;
using EstimadorAPI.Application.UseCases.Projects.Queries;
using EstimadorAPI.Domain.Interfaces.Repositories;
using MediatR;

namespace EstimadorAPI.Application.UseCases.Projects.Handlers;

public class GetProjectEstimatesQueryHandler : IRequestHandler<GetProjectEstimatesQuery, ProjectEstimatesDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProjectEstimatesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProjectEstimatesDto> Handle(GetProjectEstimatesQuery request, CancellationToken cancellationToken)
    {
        var project = await _unitOfWork.Projects.GetProjectWithUseCasesAsync(request.Id);
        if (project == null)
            throw new KeyNotFoundException($"Proyecto con ID {request.Id} no encontrado");

        var useCases = project.UseCases.Where(uc => !uc.IsDeleted).ToList();
        var estimations = await _unitOfWork.EstimationResults.GetProjectEstimationsAsync(request.Id);

        var estimationList = estimations.ToList();
        var averageDays = estimationList.Any() ? estimationList.Average(e => (double)e.EstimatedDays) : 0;
        var averageComplexity = estimationList.Any() ? estimationList.Average(e => e.GetComplexityPercentage()) : 0;
        var averageRisk = estimationList.Any() ? estimationList.Average(e => e.GetRiskScore()) : 0;

        return new ProjectEstimatesDto
        {
            ProjectId = project.Id,
            TotalUseCases = project.GetTotalUseCases(),
            TotalRequirements = project.GetTotalRequirements(),
            TotalStoryPoints = project.GetTotalStoryPoints(),
            AverageDaysPerUseCase = (decimal)averageDays,
            AverageComplexityPercentage = averageComplexity,
            AverageRiskScore = averageRisk
        };
    }
}
