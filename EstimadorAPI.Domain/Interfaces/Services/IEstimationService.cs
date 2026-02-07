using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Domain.Interfaces.Services
{
    /// <summary>
    /// Servicio de dominio para estimación de requisitos
    /// </summary>
    public interface IEstimationService
    {
        Task<EstimationResult> EstimateUseCaseAsync(UseCase useCase);
        Task<decimal> CalculateEstimatedDaysAsync(int totalStoryPoints, int teamSize = 1);
        Task<double> CalculateRiskScoreAsync(UseCase useCase);
        Task<Dictionary<string, int>> GetComplexityBreakdownAsync(UseCase useCase);
    }
}
