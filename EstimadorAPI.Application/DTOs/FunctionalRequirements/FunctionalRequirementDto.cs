using EstimadorAPI.Domain.Enums;

namespace EstimadorAPI.Application.DTOs.FunctionalRequirements
{
    public class FunctionalRequirementDto
    {
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public RequirementComplexity Complexity { get; set; }
        public RequirementStatus Status { get; set; } = RequirementStatus.Pending;
        public int StoryPoints { get; set; }
        public int UseCaseId { get; set; }
        public int? UseCaseStepId { get; set; }
        public int? DependentOnIds { get; set; }
    }
}
