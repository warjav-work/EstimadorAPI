using EstimadorAPI.Domain.Common;
using EstimadorAPI.Domain.Enums;

namespace EstimadorAPI.Domain.Entities;

/// <summary>
/// Representa un actor participante en un caso de uso
/// </summary>
public class Actor : Entity
{
    public string Name { get; set; } = string.Empty;
    public ActorType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int UseCaseId { get; set; }

    // Relaciones
    public UseCase? UseCase { get; set; }

    protected Actor() { }

    public Actor(string name, ActorType type, string description, int useCaseId)
    {
        Name = name;
        Type = type;
        Description = description;
        UseCaseId = useCaseId;
    }

    public void UpdateActor(string name, ActorType type, string description)
    {
        Name = name;
        Type = type;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}
