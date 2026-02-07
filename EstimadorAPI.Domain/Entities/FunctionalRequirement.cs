using EstimadorAPI.Domain.Common;
using EstimadorAPI.Domain.Enums;


namespace EstimadorAPI.Domain.Entities;

/// <summary>
/// Representa un requisito funcional estimado
/// </summary>
public class FunctionalRequirement : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public RequirementComplexity Complexity { get; set; }
    public RequirementStatus Status { get; set; } = RequirementStatus.Pending;
    public int StoryPoints { get; set; }
    public int UseCaseId { get; set; }
    public int? UseCaseStepId { get; set; }

    // Relaciones
    public UseCase? UseCase { get; set; }
    public UseCaseStep? UseCaseStep { get; set; }
    public ICollection<RequirementDependency> Dependencies { get; set; } = new List<RequirementDependency>();
    public ICollection<RequirementDependency> DependentOn { get; set; } = new List<RequirementDependency>();

    protected FunctionalRequirement() { }

    public FunctionalRequirement(
        string code,
        string title,
        string description,
        RequirementComplexity complexity,
        int useCaseId,
        int? useCaseStepId = null)
    {
        Code = code;
        Title = title;
        Description = description;
        Complexity = complexity;
        UseCaseId = useCaseId;
        UseCaseStepId = useCaseStepId;
        StoryPoints = CalculateStoryPoints(complexity);
    }

    public void UpdateRequirement(
        string title,
        string description,
        RequirementComplexity complexity,
        RequirementStatus status)
    {
        Title = title;
        Description = description;
        Complexity = complexity;
        Status = status;
        StoryPoints = CalculateStoryPoints(complexity);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(RequirementStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddDependency(int dependsOnRequirementId)
    {
        if (!Dependencies.Any(d => d.RequirementId == dependsOnRequirementId))
        {
            Dependencies.Add(new RequirementDependency
            {
                RequirementId = this.Id,
                DependsOnRequirementId = dependsOnRequirementId
            });
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveDependency(int dependsOnRequirementId)
    {
        var dependency = Dependencies.FirstOrDefault(d => d.RequirementId == dependsOnRequirementId);
        if (dependency != null)
        {
            Dependencies.Remove(dependency);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    private static int CalculateStoryPoints(RequirementComplexity complexity)
    {
        return complexity switch
        {
            RequirementComplexity.Simple => 3,
            RequirementComplexity.Moderate => 5,
            RequirementComplexity.Complex => 8,
            _ => 0
        };
    }
}