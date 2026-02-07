using EstimadorAPI.Application.DTOs.Actors;
using EstimadorAPI.Application.DTOs.FunctionalRequirements;
using EstimadorAPI.Application.DTOs.Results;
using EstimadorAPI.Application.DTOs.Steps;
using EstimadorAPI.Domain.Enums;

namespace EstimadorAPI.Application.DTOs.UseCases
{
    public class UseCaseDetailDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Preconditions { get; set; }
        public string? Postconditions { get; set; }
        public UseCaseStatus Status { get; set; }
        public ICollection<ActorDto> Actors { get; set; } = new List<ActorDto>();
        public ICollection<UseCaseStepDto> Steps { get; set; } = new List<UseCaseStepDto>();
        public ICollection<FunctionalRequirementDto> Requirements { get; set; } = new List<FunctionalRequirementDto>();
        public EstimationResultDto? EstimationResult { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
