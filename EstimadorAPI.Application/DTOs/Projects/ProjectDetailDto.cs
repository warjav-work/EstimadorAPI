using EstimadorAPI.Application.DTOs.UseCases;

namespace EstimadorAPI.Application.DTOs.Projects
{
    public class ProjectDetailDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ClientName { get; set; }
        public ICollection<UseCaseDto> UseCases { get; set; } = new List<UseCaseDto>();
        public DateTime CreatedAt { get; set; }
    }
}
