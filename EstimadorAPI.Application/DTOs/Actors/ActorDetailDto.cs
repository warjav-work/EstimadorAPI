using EstimadorAPI.Application.DTOs.Steps;
using EstimadorAPI.Domain.Enums;

namespace EstimadorAPI.Application.DTOs.Actors
{
    /// <summary>
    /// DTO para representar un actor con detalles
    /// </summary>
    public class ActorDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ActorType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public int UseCaseId { get; set; }
        public ICollection<UseCaseStepDto> ResponsibleForSteps { get; set; } = new List<UseCaseStepDto>();
    }
}
