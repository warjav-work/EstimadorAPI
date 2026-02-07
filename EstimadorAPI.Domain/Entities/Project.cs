using EstimadorAPI.Domain.Common;

namespace EstimadorAPI.Domain.Entities;

/// <summary>
/// Representa un proyecto que contiene casos de uso
/// </summary>
public class Project : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ClientName { get; set; }

    // Relaciones
    public ICollection<UseCase> UseCases { get; set; } = new List<UseCase>();

    protected Project() { }

    public Project(string code, string name, string description, string? clientName = null)
    {
        Code = code;
        Name = name;
        Description = description;
        ClientName = clientName;
    }

    public void UpdateProject(string name, string description, string? clientName)
    {
        Name = name;
        Description = description;
        ClientName = clientName;
        UpdatedAt = DateTime.UtcNow;
    }

    public int GetTotalUseCases()
    {
        return UseCases.Count(uc => !uc.IsDeleted);
    }

    public int GetTotalRequirements()
    {
        return UseCases
            .Where(uc => !uc.IsDeleted)
            .Sum(uc => uc.Requirements.Count);
    }

    public int GetTotalStoryPoints()
    {
        return UseCases
            .Where(uc => !uc.IsDeleted)
            .SelectMany(uc => uc.Requirements)
            .Sum(r => r.StoryPoints);
    }
}