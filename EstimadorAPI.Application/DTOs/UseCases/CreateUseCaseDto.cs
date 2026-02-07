namespace EstimadorAPI.Application.DTOs.UseCases
{
    public class CreateUseCaseDto
    {
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Preconditions { get; set; }
        public string? Postconditions { get; set; }
        public int ProjectId { get; set; }
    }
}
