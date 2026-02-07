using EstimadorAPI.Domain.Enums;

namespace EstimadorAPI.Application.DTOs.Actors
{
    public class CreateActorDto
    {
        public string Name { get; set; } = string.Empty;
        public ActorType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public int UseCaseId { get; set; }
    }
}
