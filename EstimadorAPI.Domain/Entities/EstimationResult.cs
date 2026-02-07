using EstimadorAPI.Domain.Common;

namespace EstimadorAPI.Domain.Entities;

/// <summary>
/// Representa el resultado de una estimación de caso de uso
/// </summary>
public class EstimationResult : Entity
{
    public int UseCaseId { get; set; }
    public int TotalRequirements { get; set; }
    public int TotalStoryPoints { get; set; }
    public int SimpleRequirements { get; set; }
    public int ModerateRequirements { get; set; }
    public int ComplexRequirements { get; set; }
    public decimal EstimatedDays { get; set; }
    public DateTime EstimatedCompletionDate { get; set; }

    // Relaciones
    public UseCase? UseCase { get; set; }

    protected EstimationResult() { }

    public EstimationResult(int useCaseId)
    {
        UseCaseId = useCaseId;
    }

    public void UpdateEstimation(
        int totalRequirements,
        int totalStoryPoints,
        int simpleReqs,
        int moderateReqs,
        int complexReqs,
        decimal estimatedDays)
    {
        TotalRequirements = totalRequirements;
        TotalStoryPoints = totalStoryPoints;
        SimpleRequirements = simpleReqs;
        ModerateRequirements = moderateReqs;
        ComplexRequirements = complexReqs;
        EstimatedDays = estimatedDays;
        EstimatedCompletionDate = DateTime.UtcNow.AddDays((double)estimatedDays);
        UpdatedAt = DateTime.UtcNow;
    }

    public double GetComplexityPercentage()
    {
        if (TotalRequirements == 0)
            return 0;
        return (ComplexRequirements / (double)TotalRequirements) * 100;
    }

    public double GetRiskScore()
    {
        var complexityRisk = ComplexRequirements * 2;
        var pointsRisk = TotalStoryPoints / 10.0;
        return complexityRisk + pointsRisk;
    }
}