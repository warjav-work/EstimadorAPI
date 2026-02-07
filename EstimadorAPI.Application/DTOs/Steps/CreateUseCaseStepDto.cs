namespace EstimadorAPI.Application.DTOs.Steps
{
    public class CreateUseCaseStepDto
    {
        public int StepNumber { get; set; }
        public string? Description { get; set; }
        public int UseCaseId { get; set; }
    }
}
