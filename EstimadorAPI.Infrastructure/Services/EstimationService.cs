using EstimadorAPI.Domain.Entities;
using EstimadorAPI.Domain.Enums;
using EstimadorAPI.Domain.Interfaces.Repositories;
using EstimadorAPI.Domain.Interfaces.Services;

namespace EstimadorAPI.Infrastructure.Services;

public class EstimationService : IEstimationService
{
    private readonly IUnitOfWork _unitOfWork;
    private const double PointsPerDay = 0.5; // Rendimiento típico del equipo

    public EstimationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<EstimationResult> EstimateUseCaseAsync(UseCase useCase)
    {
        var requirements = useCase.Requirements.Where(r => !r.IsDeleted).ToList();

        var simpleReqs = requirements.Count(r => r.Complexity == RequirementComplexity.Simple);
        var moderateReqs = requirements.Count(r => r.Complexity == RequirementComplexity.Moderate);
        var complexReqs = requirements.Count(r => r.Complexity == RequirementComplexity.Complex);

        var totalPoints = requirements.Sum(r => r.StoryPoints);

        var estimationResult = new EstimationResult(useCase.Id);
        var estimatedDays = await CalculateEstimatedDaysAsync(totalPoints);

        estimationResult.UpdateEstimation(
            requirements.Count,
            totalPoints,
            simpleReqs,
            moderateReqs,
            complexReqs,
            estimatedDays
        );

        return estimationResult;
    }

    public async Task<decimal> CalculateEstimatedDaysAsync(int totalStoryPoints, int teamSize = 1)
    {
        var dailyCapacity = teamSize * 16; // 2 sprints de 8 puntos por día
        var days = (decimal)totalStoryPoints / dailyCapacity;
        return Math.Ceiling(days);
    }

    public async Task<double> CalculateRiskScoreAsync(UseCase useCase)
    {
        var requirements = useCase.Requirements.Where(r => !r.IsDeleted).ToList();
        var complexReqs = requirements.Count(r => r.Complexity == RequirementComplexity.Complex);
        var totalPoints = requirements.Sum(r => r.StoryPoints);

        var complexityRisk = complexReqs * 2;
        var pointsRisk = totalPoints / 10.0;

        return complexityRisk + pointsRisk;
    }

    public async Task<Dictionary<string, int>> GetComplexityBreakdownAsync(UseCase useCase)
    {
        var requirements = useCase.Requirements.Where(r => !r.IsDeleted).ToList();

        return new Dictionary<string, int>
        {
            { "Simple", requirements.Count(r => r.Complexity == RequirementComplexity.Simple) },
            { "Moderate", requirements.Count(r => r.Complexity == RequirementComplexity.Moderate) },
            { "Complex", requirements.Count(r => r.Complexity == RequirementComplexity.Complex) }
        };
    }
}