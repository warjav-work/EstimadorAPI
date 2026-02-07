namespace EstimadorAPI.Application.DTOs.ProjectEstimates
{
    public class ProjectEstimatesDto
    {
        public int ProjectId { get; set; }
        public int TotalUseCases { get; set; }
        public int TotalRequirements { get; set; }
        public int TotalStoryPoints { get; set; }
        public decimal AverageDaysPerUseCase { get; set; }
        public double AverageComplexityPercentage { get; set; }
        public double AverageRiskScore { get; set; }
    }
}
