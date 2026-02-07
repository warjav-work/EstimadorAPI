using EstimadorAPI.Domain.Common;
using EstimadorAPI.Domain.Enums;


namespace EstimadorAPI.Domain.Entities;

/// <summary>
/// Representa un caso de uso completo
/// </summary>
public class UseCase : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Preconditions { get; set; }
    public string? Postconditions { get; set; }
    public UseCaseStatus Status { get; set; } = UseCaseStatus.Draft;
    public int ProjectId { get; set; }

    // Relaciones
    public Project? Project { get; set; }
    public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    public ICollection<UseCaseStep> Steps { get; set; } = new List<UseCaseStep>();
    public ICollection<FunctionalRequirement> Requirements { get; set; } = new List<FunctionalRequirement>();
    public EstimationResult? EstimationResult { get; set; }

    protected UseCase() { }

    public UseCase(string code, string title, string description, int projectId)
    {
        Code = code;
        Title = title;
        Description = description;
        ProjectId = projectId;
    }

    public void UpdateUseCase(string title, string description, string? preconditions, string? postconditions)
    {
        Title = title;
        Description = description;
        Preconditions = preconditions;
        Postconditions = postconditions;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(UseCaseStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public int GetTotalRequiredSteps()
    {
        return Steps.Count;
    }

    public int GetActorCount()
    {
        return Actors.Count;
    }

    public int GetRequirementCount()
    {
        return Requirements.Count;
    }
}