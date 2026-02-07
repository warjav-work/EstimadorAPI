using EstimadorAPI.Domain.Entities;

namespace EstimadorAPI.Domain.Interfaces.Repositories
{
    public interface IUseCaseRepository : IRepository<UseCase>
    {
        Task<UseCase?> GetUseCaseWithDetailsAsync(int id);
        Task<IEnumerable<UseCase>> GetProjectUseCasesAsync(int projectId);
        Task<UseCase?> GetUseCaseByCodeAsync(string code);
    }
}
