using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Domain.Enums;

namespace EstimadorAPI.Application.DTOs.Steps
{
    public class UseCaseStepDto
    {
        public int StepNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public FlowType FlowType { get; set; }
        public int UseCaseId { get; set; }
        public int? ResponsibleActorId { get; set; }
        public ActorDto? ResponsibleActor { get; set; }
    }
}
