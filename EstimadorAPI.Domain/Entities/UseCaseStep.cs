using EstimadorAPI.Domain.Common;
using EstimadorAPI.Domain.Enums;

namespace EstimadorAPI.Domain.Entities;

/// <summary>
/// Representa un paso en un flujo de caso de uso
/// </summary>
public class UseCaseStep : Entity
{
    public int StepNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public FlowType FlowType { get; set; }
    public int UseCaseId { get; set; }
    public int? ResponsibleActorId { get; set; }

    // Relaciones
    public UseCase? UseCase { get; set; }
    public Actor? ResponsibleActor { get; set; }
    public ICollection<FunctionalRequirement> Requirements { get; set; } = new List<FunctionalRequirement>();

    protected UseCaseStep() { }

    public UseCaseStep(int stepNumber, string description, FlowType flowType, int useCaseId, int? responsibleActorId = null)
    {
        StepNumber = stepNumber;
        Description = description;
        FlowType = flowType;
        UseCaseId = useCaseId;
        ResponsibleActorId = responsibleActorId;
    }

    public void UpdateStep(int stepNumber, string description, FlowType flowType, int? responsibleActorId = null)
    {
        StepNumber = stepNumber;
        Description = description;
        FlowType = flowType;
        ResponsibleActorId = responsibleActorId;
        UpdatedAt = DateTime.UtcNow;
    }
}