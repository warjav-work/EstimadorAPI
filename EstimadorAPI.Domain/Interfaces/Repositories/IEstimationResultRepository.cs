using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Domain.Interfaces.Repositories
{
    public interface IEstimationResultRepository : IRepository<EstimationResult>
    {
        Task<EstimationResult?> GetByUseCaseIdAsync(int useCaseId);
        Task<IEnumerable<EstimationResult>> GetProjectEstimationsAsync(int projectId);
    }
}
