using EstimadorAPI.Domain.Common;

namespace EstimadorAPI.Domain.Entities;

/// <summary>
/// Representa una dependencia entre requisitos
/// </summary>
public class RequirementDependency : Entity
{
    public int RequirementId { get; set; }
    public int DependsOnRequirementId { get; set; }

    // Relaciones
    public FunctionalRequirement? Requirement { get; set; }
    public FunctionalRequirement? DependsOnRequirement { get; set; }

    public RequirementDependency() { }

    public RequirementDependency(int requirementId, int dependsOnRequirementId)
    {
        RequirementId = requirementId;
        DependsOnRequirementId = dependsOnRequirementId;
    }
}