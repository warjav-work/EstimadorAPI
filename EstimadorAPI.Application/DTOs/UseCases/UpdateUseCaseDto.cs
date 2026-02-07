namespace EstimadorAPI.Application.DTOs.UseCases
{
    public class UpdateUseCaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Preconditions { get; set; }
        public string? Postconditions { get; set; }
    }
}
