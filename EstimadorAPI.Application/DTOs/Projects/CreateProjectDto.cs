namespace EstimadorAPI.Application.DTOs.Projects
{
    public class CreateProjectDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ClientName { get; set; }
    }
}
