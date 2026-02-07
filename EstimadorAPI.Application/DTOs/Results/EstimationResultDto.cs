namespace EstimadorAPI.Application.DTOs.Results
{
    public class EstimationResultDto
    {
        public int UseCaseId { get; set; }
        public int TotalRequirements { get; set; }
        public int TotalStoryPoints { get; set; }
        public int SimpleRequirements { get; set; }
        public int ModerateRequirements { get; set; }
        public int ComplexRequirements { get; set; }
        public decimal EstimatedDays { get; set; }
        public DateTime EstimatedCompletionDate { get; set; }
        public double ComplexityPercentage { get; internal set; }
        public double RiskScore { get; internal set; }
    }
}
