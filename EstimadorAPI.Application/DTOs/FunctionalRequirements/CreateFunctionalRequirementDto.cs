namespace EstimadorAPI.Application.DTOs.FunctionalRequirements
{
    public class CreateFunctionalRequirementDto
    {
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int UseCaseId { get; set; }
    }
}
