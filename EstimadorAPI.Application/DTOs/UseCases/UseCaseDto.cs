using EstimadorAPI.Domain.Enums;

namespace EstimadorAPI.Application.DTOs.UseCases
{
    public class UseCaseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Preconditions { get; set; }
        public string? Postconditions { get; set; }
        public UseCaseStatus Status { get; set; }
        public int ActorCount { get; set; }
        public int StepCount { get; set; }
        public int RequirementCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
