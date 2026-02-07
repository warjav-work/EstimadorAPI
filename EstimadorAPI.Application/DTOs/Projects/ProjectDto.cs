namespace EstimadorAPI.Application.DTOs.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ClientName { get; set; }
        public int TotalUseCases { get; set; }
        public int TotalRequirements { get; set; }
        public int TotalStoryPoints { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
